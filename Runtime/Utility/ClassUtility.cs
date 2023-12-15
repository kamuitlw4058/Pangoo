
using System;
using System.Collections;
using System.ComponentModel;
using System.IO;
using System.Xml.Serialization;
using Sirenix.OdinInspector;
using GameFramework;
using System.Reflection;


namespace Pangoo
{
    public static class ClassUtility
    {
        public static T CreateInstance<T>(string className) where T : class
        {
            return CreateInstance(className) as T;
        }

        public static Object CreateInstance(string className)
        {
            if (className.IsNullOrWhiteSpace())
            {
                return null;
            }
            var triggerType = Utility.Assembly.GetType(className);
            if (triggerType == null)
            {
                return null;
            }

            return Activator.CreateInstance(triggerType);
        }


        public static IEnumerable GetTypeByCategoryAttr<T>(string excludeType = null)
        {
            var types = Utility.Assembly.GetTypes(typeof(T));
            Type currentType = null;
            if (excludeType != null)
            {
                currentType = Utility.Assembly.GetType(excludeType);
            }

            ValueDropdownList<string> ret = new();
            for (int i = 0; i < types.Length; i++)
            {
                var type = types[i];
                if (type == currentType)
                {
                    continue;
                }
                var attr = type.GetCustomAttribute(typeof(Core.Common.CategoryAttribute));
                if (attr == null)
                {
                    ret.Add(types[i].ToString(), types[i].ToString());
                }
                else
                {
                    ret.Add(attr.ToString(), types[i].ToString());
                }

            }
            return ret;
        }
    }
}