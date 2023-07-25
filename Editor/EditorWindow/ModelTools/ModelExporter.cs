using System.IO;
using Pangoo;
using UnityEngine;
using UnityEditor;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using System;

namespace Pangoo.Editor
{
    public class ModelExporter:ScriptableObject
    {
       
     
         private static OdinEditorWindow m_CreateWindow;

        [Button("导出本地模型", ButtonSizes.Large)]
        public void ShowCreateWindow()
        {
            m_CreateWindow = OdinEditorWindow.InspectObject(new ModelImportLocal());
        }



    }

}
