using System;
using System.Reflection;
using Attributes;

namespace Tools {
    public abstract class Reflection {
        public static void ValidateClassFields(object obj) {
            foreach (FieldInfo f in obj.GetType().GetFields(BindingFlags.NonPublic | BindingFlags.Instance))
            {
                if (Attribute.IsDefined(f, typeof(IgnoreAttribute))) return;
                if (f.GetValue(obj) == null) throw new ArgumentNullException("field: " + f.Name + " is null");
            }
        }
    }
}