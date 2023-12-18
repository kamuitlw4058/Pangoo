#if UNITY_EDITOR

using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using LitJson;
using UnityEngine;
using Sirenix.OdinInspector;
using System.Xml.Serialization;
using Pangoo.Common;
using MetaTable;

namespace Pangoo.MetaTable
{
    [Serializable]
    public partial class CharacterDetailRowWrapper : MetaTableDetailRowWrapper<CharacterOverview, UnityCharacterRow>
    {
        [LabelText("资源id")]
        [ValueDropdown("AssetPathIdValueDropdown")]
        [ShowInInspector]
        // [InlineButton("ShowCreateAssetPath", SdfIconType.Plus, Label = "")]
        public int AssetPathId
        {
            get
            {
                return UnityRow.Row?.AssetPathId ?? 0;
            }
            set
            {

                UnityRow.Row.AssetPathId = value;
                Save();
            }

        }

        [LabelText("资源Uuid")]
        [ValueDropdown("AssetPathUuidValueDropdown")]
        [ShowInInspector]
        // [InlineButton("ShowCreateAssetPath", SdfIconType.Plus, Label = "")]
        public string AssetPathUuid
        {
            get
            {
                return UnityRow.Row?.AssetPathUuid;
            }
            set
            {

                UnityRow.Row.AssetPathUuid = value;
                Save();
            }

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

        [Button("更新AssetPathUuid通过Id")]
        public void UpdateAssetPathUuidByAssetPathId()
        {
            var row = GameSupportEditorUtility.GetAssetPathById(AssetPathId);
            if (row != null)
            {
                AssetPathUuid = row.Uuid;
            }
        }



        public IEnumerable AssetPathIdValueDropdown()
        {
            return GameSupportEditorUtility.GetAssetPathIds(assetTypes: new List<string> { "Character" });
        }

        public IEnumerable AssetPathUuidValueDropdown()
        {
            return GameSupportEditorUtility.GetAssetPathUuids(assetTypes: new List<string> { "Character" });
        }


        [ShowInInspector]
        public bool IsPlayer
        {
            get
            {
                return UnityRow.Row?.IsPlayer ?? false;
            }
            set
            {

                UnityRow.Row.IsPlayer = value;
                Save();
            }
        }



        [ShowInInspector]
        [DelayedProperty]
        public float LinearSpeed
        {
            get
            {
                return UnityRow.Row?.MoveSpeed ?? 1;

            }
            set
            {
                if (UnityRow.Row != null && Overview != null)
                {
                    UnityRow.Row.MoveSpeed = value;
                    Save();
                }

            }
        }


        [ShowInInspector]
        [DelayedProperty]
        public Vector3 CameraOffset
        {
            get
            {
                return UnityRow.Row?.CameraOffset ?? Vector3.zero;

            }
            set
            {
                if (UnityRow.Row != null && Overview != null)
                {
                    UnityRow.Row.CameraOffset = value;
                    Save();
                }

            }
        }


        [ShowInInspector]
        [DelayedProperty]
        public float XMaxPitch
        {
            get
            {
                return UnityRow.Row?.XMaxPitch ?? 90;

            }
            set
            {
                if (UnityRow.Row != null && Overview != null)
                {
                    UnityRow.Row.XMaxPitch = value;
                    Save();
                }

            }
        }

        [ShowInInspector]
        [DelayedProperty]
        public float YMaxPitch
        {
            get
            {
                return UnityRow.Row?.YMaxPitch ?? 360;

            }
            set
            {
                if (UnityRow.Row != null && Overview != null)
                {
                    UnityRow.Row.YMaxPitch = value;
                    Save();
                }

            }
        }


        [ShowInInspector]
        [DelayedProperty]
        [LabelText("只处理相机")]
        public bool CameraOnly
        {
            get
            {
                return UnityRow.Row?.CameraOnly ?? false;

            }
            set
            {
                if (UnityRow.Row != null && Overview != null)
                {
                    UnityRow.Row.CameraOnly = value;
                    Save();
                }

            }
        }

    }
}
#endif

