using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace Facturanet.DTOs
{
    internal static class CloneUtil 
    {
        public static T SerializeClone<T>(T original)
        {
            if (!typeof(T).IsSerializable)
            {
                throw new ArgumentException("The type must be serializable.", "original");
            }

            // Don't serialize a null object, simply return the default for that object
            if (Object.ReferenceEquals(original, null))
            {
                return default(T);
            }

            IFormatter formatter = new BinaryFormatter();
            Stream stream = new MemoryStream();
            using (stream)
            {
                formatter.Serialize(stream, original);
                stream.Seek(0, SeekOrigin.Begin);
                return (T)formatter.Deserialize(stream);
            }
        }

        public static void ReflectionCopy<T>(T original, T copy)
            where T : new()
        {
            //puedo hacer un cache de los campos de los tipos
            FieldInfo[] fis = original.GetType().GetFields(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public /*| System.Reflection.BindingFlags.NonPublic*/ | BindingFlags.GetProperty | BindingFlags.SetProperty);
            foreach (FieldInfo fi in fis)
            {
                fi.SetValue(copy, fi.GetValue(original));
            }
        }

        public static T ReflectionClone<T>(T original)
            where T : new()
        {
            T clone = new T();
            ReflectionCopy(original, clone);
            return clone;
        }


        public delegate TResult Func<T1, TResult>(T1 arg1);
        static Dictionary<Type, Delegate> _cachedIL = new Dictionary<Type, Delegate>();
        public static T ILClone<T>(T original)
        {
            Delegate myExec = null;
            if (!_cachedIL.TryGetValue(typeof(T), out myExec))
            {
                // Create ILGenerator
                DynamicMethod dymMethod = new DynamicMethod("DoClone", typeof(T), new Type[] { typeof(T) }, true);
                ConstructorInfo cInfo = original.GetType().GetConstructor(new Type[] { });

                ILGenerator generator = dymMethod.GetILGenerator();

                LocalBuilder lbf = generator.DeclareLocal(typeof(T));
                //lbf.SetLocalSymInfo("_temp");

                generator.Emit(OpCodes.Newobj, cInfo);
                generator.Emit(OpCodes.Stloc_0);
                foreach (FieldInfo field in original.GetType().GetFields(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic))
                {
                    // Load the new object on the eval stack... (currently 1 item on eval stack)
                    generator.Emit(OpCodes.Ldloc_0);
                    // Load initial object (parameter)          (currently 2 items on eval stack)
                    generator.Emit(OpCodes.Ldarg_0);
                    // Replace value by field value             (still currently 2 items on eval stack)
                    generator.Emit(OpCodes.Ldfld, field);
                    // Store the value of the top on the eval stack into the object underneath that value on the value stack.
                    //  (0 items on eval stack)
                    generator.Emit(OpCodes.Stfld, field);
                }

                // Load new constructed obj on eval stack -> 1 item on stack
                generator.Emit(OpCodes.Ldloc_0);
                // Return constructed object.   --> 0 items on stack
                generator.Emit(OpCodes.Ret);

                myExec = dymMethod.CreateDelegate(typeof(Func<T, T>));
                _cachedIL.Add(typeof(T), myExec);
            }
            return ((Func<T, T>)myExec)(original);
        }
    }
}
