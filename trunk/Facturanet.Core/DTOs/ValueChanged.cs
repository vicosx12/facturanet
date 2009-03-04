using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel;
using System.Reflection;


namespace Facturanet.DTOs
{
    public struct ValueChanged
    {
        public readonly object OriginalValue;
        public readonly object NewValue;

        private static object GetDefaultTypeValue(Type targetType)
        {
            return targetType.IsValueType ? Activator.CreateInstance(targetType) : null;
        }

        public ValueChanged(object originalValue, object newValue)
        {
            OriginalValue = originalValue ?? GetDefaultTypeValue(newValue.GetType());
            NewValue = newValue ?? GetDefaultTypeValue(originalValue.GetType());
        }

        private static string Quoted(object value)
        {
            if (value == null)
                return "null";
            else
                return string.Format("'{0}'", value);
        }

        public override string ToString()
        {
            return string.Format("{0}=>{1}", Quoted(OriginalValue), Quoted(NewValue));
        }
    }

    public class ValueChangedCollection : Dictionary<string, ValueChanged>
    {
        public void Add(string key, object originalValue, object newValue)
        {
            if (originalValue != null || newValue != null)
                base.Add(key, new ValueChanged(originalValue, newValue));
        }

        public override string ToString()
        {
            string txt = "";
            foreach (var item in this)
                txt += string.Format("{0}: {1}; ", item.Key, item.Value);
            return txt;
        }
    }
}
