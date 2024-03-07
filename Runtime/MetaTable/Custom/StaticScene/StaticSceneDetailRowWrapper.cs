#if UNITY_EDITOR

using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector.Editor;

using System.Linq;

using LitJson;
using UnityEngine;
using Sirenix.OdinInspector;
using System.Xml.Serialization;
using Pangoo.Common;
using MetaTable;
using Pangoo.Core.Characters;
using Pangoo.Core.VisualScripting;

namespace Pangoo.MetaTable
{
    [Serializable]
    public partial class StaticSceneDetailRowWrapper : MetaTableDetailRowWrapper<StaticSceneOverview, UnityStaticSceneRow>
    {

        [LabelText("资源Uuid")]
        [ValueDropdown("AssetPathUuidValueDropdown")]
        [PropertyOrder(0)]
        [ShowInInspector]
        [InlineButton("AddAssetPath", SdfIconType.Plus, Label = "")]
        public string AssetPathUuid
        {
            get
            {
                return UnityRow.Row.AssetPathUuid;
            }
            set
            {
                UnityRow.Row.AssetPathUuid = value;
                Save();
            }

        }

        [ShowInInspector]
        [PropertyOrder(1)]
        [LabelText("位置")]
        public Vector3 Position
        {
            get
            {
                return UnityRow.Row?.Position ?? Vector3.zero;
            }
        }

        [ShowInInspector]
        [PropertyOrder(2)]
        [LabelText("旋转")]
        public Vector3 Rotation
        {
            get
            {
                return UnityRow.Row?.Rotation ?? Vector3.zero;
            }
        }


        void AddAssetPath()
        {
            var assetOverview = AssetDatabaseUtility.FindAssetFirst<AssetPathOverview>(Overview.Config.StreamResScriptableObjectDir);
            Debug.Log($"assetOverview:{assetOverview} Overview.RowDirPath:{Overview.RowDirPath}");
            if (assetOverview != null)
            {
                var assetNewObject = AssetPathNewRowWrapper.Create(assetOverview, ConstExcelTable.StaticSceneAssetTypeName, Name, afterCreateAsset: OnAfterCreateAsset);
                assetNewObject.MenuWindow = MenuWindow;
                var window = OdinEditorWindow.InspectObject(assetNewObject);
                assetNewObject.OpenWindow = window;
            }
        }


        public void OnAfterCreateAsset(string uuid)
        {
            AssetPathUuid = uuid;
        }




        public IEnumerable AssetPathUuidValueDropdown()
        {
            return GameSupportEditorUtility.GetAssetPathUuids(assetTypes: new List<string> { "Scene" });
        }

        GameObject m_Prefab;


        [ShowInInspector]
        [LabelText("资源预制体")]
        [ReadOnly]
        public GameObject Prefab
        {
            get
            {
                if (m_Prefab == null)
                {
                    m_Prefab = GameSupportEditorUtility.GetPrefabByAssetPathUuid(UnityRow.Row.AssetPathUuid);
                }
                return m_Prefab;
            }
        }

        [ShowInInspector]
        [LabelText("模型控制列表")]
        [FoldoutGroup("场景显示", expanded: true)]

        [PropertyOrder(6)]
        [ValueDropdown("OnModelListDropdown")]
        public string[] ModelList
        {
            get
            {
                return UnityRow.Row.ModelList.ToSplitArr<string>();
            }
            set
            {
                UnityRow.Row.ModelList = value.ToListString();
                Save();
            }
        }

        public IEnumerable OnModelListDropdown()
        {
            return GameSupportEditorUtility.RefPrefabStringDropdown(Prefab, false); ;
        }


        [ShowInInspector]
        [LabelText("显示类型")]
        [FoldoutGroup("场景显示")]
        [PropertyOrder(6)]
        public SceneShowType ShowType
        {
            get
            {
                return UnityRow.Row.ShowType.ToEnum<SceneShowType>();
            }
            set
            {
                UnityRow.Row.ShowType = value.ToString();
                Save();
            }
        }


        [ShowInInspector]
        [LabelText("默认隐藏")]
        [FoldoutGroup("场景显示")]
        [PropertyOrder(6)]
        [ShowIf("@this.ShowType == SceneShowType.Auto || this.ShowType == SceneShowType.ManualAlways")]

        public bool DefaultHide
        {
            get
            {
                return UnityRow.Row.HideDefault;
            }
            set
            {
                UnityRow.Row.HideDefault = value;
                Save();
            }
        }



        [ShowInInspector]
        [LabelText("玩家没有进入场景时也显示")]
        [PropertyOrder(6)]
        [ShowIf("@this.ShowType != SceneShowType.Always && this.ShowType != SceneShowType.ManualAlways")]
        [FoldoutGroup("场景显示")]

        public bool ShowOnNoPlayEnter
        {
            get
            {
                return UnityRow.Row.ShowOnNoPlayerEnter;
            }
            set
            {
                UnityRow.Row.ShowOnNoPlayerEnter = value;
                Save();
            }
        }


        [ShowInInspector]
        [LabelText("加载场景Uuid")]
        [FoldoutGroup("场景显示")]
        [ListDrawerSettings(Expanded = true)]
        [ValueDropdown("StaticSceneUuidValueDropdown", IsUniqueList = true)]
        [PropertyOrder(6)]
        [ShowIf("@this.ShowType != SceneShowType.Always && this.ShowType != SceneShowType.ManualAlways")]
        public string[] LoadSceneUuids
        {
            get
            {
                return UnityRow.Row.LoadSceneUuids?.ToSplitArr<string>() ?? new string[0];
            }
            set
            {
                UnityRow.Row.LoadSceneUuids = value.ToList().ToListString();
                Save();
            }
        }



        public IEnumerable StaticSceneIdValueDropdown()
        {
            return GameSupportEditorUtility.GetStaticSceneIds(excludeIds: new List<int> { UnityRow.Row.Id });
        }

        public IEnumerable StaticSceneUuidValueDropdown()
        {
            return GameSupportEditorUtility.GetStaticSceneUuids(excludeUuid: new List<string> { UnityRow.Uuid });
        }

        [ShowInInspector]
        [LabelText("使用场景脚步声")]
        [FoldoutGroup("脚步声", expanded: true)]

        [PropertyOrder(7)]

        public bool UseSceneFootstep
        {
            get
            {
                return UnityRow.Row.UseSceneFootstep;
            }
            set
            {
                UnityRow.Row.UseSceneFootstep = value;
                Save();
            }
        }


        FootstepEntry? m_Footstep;

        [ShowInInspector]
        [PropertyOrder(8)]
        [FoldoutGroup("脚步声")]


        public string FootstepStr
        {
            get
            {
                return UnityRow.Row.Footsetp;
            }
        }


        [ShowInInspector]
        [HideLabel]
        [HideReferenceObjectPicker]
        [OnValueChanged("OnFootstepChanged", includeChildren: true)]
        [PropertyOrder(9)]
        [FoldoutGroup("脚步声")]


        public FootstepEntry? Footstep
        {
            get
            {
                if (m_Footstep == null)
                {
                    try
                    {
                        m_Footstep = JsonMapper.ToObject<FootstepEntry>(UnityRow.Row.Footsetp);

                    }
                    catch
                    {

                    }
                    if (m_Footstep == null)
                    {
                        m_Footstep = new FootstepEntry();
                        UnityRow.Row.Footsetp = JsonMapper.ToJson(m_Footstep);
                        Save();
                    }

                }
                return m_Footstep;
            }
            set
            {

                m_Footstep = value;
            }
        }

        void OnFootstepChanged()
        {
            UnityRow.Row.Footsetp = JsonMapper.ToJson(m_Footstep);
            Save();
        }



        public IEnumerable SoundUuidDropdown()
        {
            return SoundOverview.GetUuidDropdown();
        }






    }
}
#endif

