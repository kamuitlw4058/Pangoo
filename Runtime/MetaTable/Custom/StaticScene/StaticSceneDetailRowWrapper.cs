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




        public IEnumerable AssetPathIdValueDropdown()
        {
            return GameSupportEditorUtility.GetAssetPathIds(assetTypes: new List<string> { "Scene" });
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
            set
            {

            }
        }





        [ShowInInspector]
        [LabelText("加载场景Uuid")]
        [ListDrawerSettings(Expanded = true)]
        [ValueDropdown("StaticSceneUuidValueDropdown", IsUniqueList = true)]

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

