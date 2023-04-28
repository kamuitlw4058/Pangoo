using Sirenix.OdinInspector;
using UnityEngine;
using LitJson;
#if UNITY_EDITOR
using System.IO;
using UnityEditor;
#endif

namespace Pangoo
{
    public abstract class GameConfigBase : ScriptableObject
    {

#if UNITY_EDITOR
        [Button("保存配置", 30)]
        public void SaveConfig()
        {
            EditorUtility.SetDirty(this);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            GUIUtility.ExitGUI();
        }
#endif
    }
}

