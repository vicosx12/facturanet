using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Facturanet.Util
{
    public static class ObjectExtensions
    {
        public static bool ImplementsInterface(this Type type, Type theinterface)
        {
            return (type.GetInterfaces().Contains(theinterface));
        }

        public static bool ImplementsInterface<InterfaceType>(this Type type)
        {
            return ImplementsInterface(type, typeof(InterfaceType));
        }

        public static void DoIfItIs<T>(this object item, Action<T> action)
            where T : class
        {
            var casted = item as T;
            if (casted != null)
                action(casted);
        }
    }
}
