
using System;
using System.ComponentModel;
using System.IO;
using System.Xml.Serialization;

namespace Pangoo.Common
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
            var type = AssemblyUtility.GetType(className);
            if (type == null)
            {
                return null;
            }

            return CreateInstance(type);
        }

        public static Object CreateInstance(Type type)
        {
            if (type == null)
            {
                return null;
            }

            return Activator.CreateInstance(type);

        }
    }
}