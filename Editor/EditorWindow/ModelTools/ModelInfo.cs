using System.IO;
using Pangoo;
using UnityEngine;
using UnityEditor;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using System;


namespace Pangoo.Editor
{
    public class ModelInfo:ScriptableObject
    {
        public string Name;
        public string Version;

        public ModelFileInfo[] Files;

        public class ModelFileInfo{
            public string Path;
            public string FileType;

        }
    }
}