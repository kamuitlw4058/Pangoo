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
using Pangoo.Core.VisualScripting;

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

        [ShowInInspector]
        [DelayedProperty]
        [LabelText("斜坡限制")]
        public float SlopeLimit
        {
            get
            {
                return UnityRow.Row?.SlopeLimit ?? 45f;

            }
            set
            {
                if (UnityRow.Row != null && Overview != null)
                {
                    UnityRow.Row.SlopeLimit = value;
                    Save();
                }

            }
        }

        [ShowInInspector]
        [DelayedProperty]
        [LabelText("步高限制")]
        public float StepOffset
        {
            get
            {
                return UnityRow.Row?.StepOffset ?? 0.3f;

            }
            set
            {
                if (UnityRow.Row != null && Overview != null)
                {
                    UnityRow.Row.StepOffset = value;
                    Save();
                }

            }
        }
        
        [ShowInInspector]
        [DelayedProperty]
        [LabelText("皮肤宽度")]
        public float SkinWidth
        {
            get
            {
                return UnityRow.Row?.SkinWidth ?? 0.08f;

            }
            set
            {
                if (UnityRow.Row != null && Overview != null)
                {
                    UnityRow.Row.SkinWidth = value;
                    Save();
                }

            }
        }

        [ShowInInspector]
        [DelayedProperty]
        [LabelText("最小移动距离")]
        public float MinMoveDistance
        {
            get
            {
                return UnityRow.Row?.MinMoveDistance ?? 0.001f;

            }
            set
            {
                if (UnityRow.Row != null && Overview != null)
                {
                    UnityRow.Row.MinMoveDistance = value;
                    Save();
                }

            }
        }

        [ShowInInspector]
        [DelayedProperty]
        [LabelText("中心")]
        public Vector3 Center
        {
            get
            {
                return UnityRow.Row?.Center ?? new Vector3();

            }
            set
            {
                if (UnityRow.Row != null && Overview != null)
                {
                    UnityRow.Row.Center = value;
                    Save();
                }

            }
        }
        
        [ShowInInspector]
        [DelayedProperty]
        [LabelText("半径")]
        public float Radius
        {
            get
            {
                return UnityRow.Row?.Radius ?? 0.5f;

            }
            set
            {
                if (UnityRow.Row != null && Overview != null)
                {
                    UnityRow.Row.Radius = value;
                    Save();
                }

            }
        }
        
        [ShowInInspector]
        [DelayedProperty]
        [LabelText("高度")]
        public float Height
        {
            get
            {
                return UnityRow.Row?.Height ?? 2f;

            }
            set
            {
                if (UnityRow.Row != null && Overview != null)
                {
                    UnityRow.Row.Height = value;
                    Save();
                }

            }
        }
    }
}
#endif

