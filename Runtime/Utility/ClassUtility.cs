
using System;
using System.ComponentModel;
using System.IO;
using System.Xml.Serialization;
using GameFramework;

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
    }
}