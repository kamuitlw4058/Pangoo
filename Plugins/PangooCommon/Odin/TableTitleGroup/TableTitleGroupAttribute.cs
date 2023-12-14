using System;
using System.Diagnostics;
using Sirenix.OdinInspector;

namespace Pangoo.Common
{
    [AttributeUsage(AttributeTargets.All, AllowMultiple = true, Inherited = true)]
    [Conditional("UNITY_EDITOR")]
    public class TableTitleGroupAttribute : PropertyGroupAttribute
    {
        public TableTitleGroupAttribute(string path)
            : base(path)
        {
        }

    }
}
