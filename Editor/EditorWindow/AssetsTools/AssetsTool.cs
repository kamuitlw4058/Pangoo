
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Pangoo;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;
using UnityEditor.SceneManagement;

namespace Pangoo.Editor
{
    public class AssetsTool
    {
        const string BasePath = "Assets";

        [FolderPath(ParentFolder = BasePath)]
        public string Path;



        [Button("查找")]
        void Find()
        {
            var FindPath = $"{BasePath}/{Path}";
            Debug.Log($"Path:{FindPath}");
            var assets = AssetDatabase.FindAssets(string.Empty, new string[] { FindPath });
            Debug.Log(assets.Length);

        }


    }

}
