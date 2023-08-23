using Pangoo;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities;
using Sirenix.Utilities.Editor;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;
using System;
using System.IO;
using System.Text.RegularExpressions;


namespace Pangoo.Editor
{
    public class VolumeEditor : OdinEditorWindow
    {
        [ReadOnly]
        [ShowInInspector]
        public static VolumeComponent m_VolumeComponent;

        [ReadOnly]
        [ShowInInspector]
        public static GameMainConfig m_GameMainConfig;

        [ShowInInspector]
        public static string PackageDir{
            get{
                if(m_GameMainConfig == null){
                    return string.Empty;
                }

                return m_GameMainConfig.PackageDir;
            }
        }

        [ShowInInspector]
        public static string VolumeDir {
            get{
                return Path.Join(PackageDir,"StreamRes/Volume").Replace("\\","/");
            }
        }


        [ShowInInspector]
        public static string PrefabDir {
            get{
                return Path.Join(PackageDir,"StreamRes/Prefab/Volume").Replace("\\","/");
            }
        }



        private static OdinEditorWindow m_CreateWindow;


        [MenuItem("Pangoo/Volume编辑", false, 6)]
        public static void ShowWindow()
        {
            var window = GetWindow<VolumeEditor>();
            window.position = GUIHelper.GetEditorWindowRect().AlignCenter(800, 700);
            window.titleContent = new GUIContent("Volume编辑");
            // window.MenuWidth = 180;

            window.Show();
        }


        protected override void OnEnable() {
            m_VolumeComponent = Transform.FindFirstObjectByType<VolumeComponent>();
            m_GameMainConfig = AssetDatabaseUtility.FindAssetFirst<GameMainConfig>();
        }

        [Button("CreateVolume")]
        public void CreateVolume(){

            m_CreateWindow =  OdinEditorWindow.InspectObject(new VolumeCreateWindow());

        }


        private static void ConfirmCreate( string name)
        {
            // var volumePath = $"{PackageDir}";
            DirectoryUtility.ExistsOrCreate(PrefabDir);
            DirectoryUtility.ExistsOrCreate(VolumeDir);

            // var volumeProfile = ScriptableObject.CreateInstance<VolumeProfile>();
            // AssetDatabase.CreateAsset(volumeProfile,"Assets/GameMain/StreamRes/Volume/Volume.asset");

            // var go = new GameObject("Volume");
            // if(volumeComponent != null){
            //     go.transform.parent = volumeComponent.transform;
            // }
            
            // go.transform.localPosition = Vector3.zero;
            // var volume = go.AddComponent<Volume>();
            // volume.isGlobal = true;
            // volume.weight = 1;
            // volume.sharedProfile = volumeProfile;


            // 设置GameObject作为预制体
            // string prefabPath = "Assets/Prefabs/MyPrefab.prefab";
            // PrefabUtility.SaveAsPrefabAsset(obj, prefabPath);

            m_CreateWindow.Close();
        }


        public class VolumeCreateWindow{
            
            public string Name = "";

            public VolumeCreateWindow(){
 
            }

             [Button("新建", ButtonSizes.Large)]
            public void Create(){

                if ( Name.IsNullOrWhitespace())
                {
                    EditorUtility.DisplayDialog("错误", "Name必须填写", "确定");
                    // GUIUtility.ExitGUI();
                    return;
                }

                if(StringUtility.ContainsChinese(Name)){
                    EditorUtility.DisplayDialog("错误", "Name不能包含中文", "确定");
                    // GUIUtility.ExitGUI();
                    return;
                }

                if(StringUtility.IsOnlyDigit(Name)){
                    EditorUtility.DisplayDialog("错误", "Name不能全是数字", "确定");
                    return;
                }

                if(char.IsDigit(Name[0])){
                    EditorUtility.DisplayDialog("错误", "Name开头不能是数字", "确定");
                    return;
                }

                ConfirmCreate( Name);

            }
        }


    }
}