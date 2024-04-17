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
        [LabelText("移动速度")]
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
        [LabelText("跑步速度")]
        public float RunSpeed
        {
            get
            {
                return UnityRow.Row?.RunSpeed ?? 2;
            }
            set
            {
                if (UnityRow.Row != null && Overview != null)
                {
                    UnityRow.Row.RunSpeed = value;
                    Save();
                }
            }
        }


        [ShowInInspector]
        [DelayedProperty]
        [LabelText("相机高度")]
        public float CameraHeight
        {
            get
            {
                return UnityRow.Row.CameraHeight;
            }
            set
            {
                UnityRow.Row.CameraHeight = value;
                Save();
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
        [LabelText("角色斜坡限制")]
        public float SlopeLimit
        {
            get
            {
                return UnityRow.Row?.CharacterSlopeLimit ?? 45f;

            }
            set
            {
                if (UnityRow.Row != null && Overview != null)
                {
                    UnityRow.Row.CharacterSlopeLimit = value;
                    Save();
                }

            }
        }

        [ShowInInspector]
        [DelayedProperty]
        [LabelText("角色步高限制")]
        public float StepOffset
        {
            get
            {
                return UnityRow.Row?.CharacterStepOffset ?? 0.3f;

            }
            set
            {
                if (UnityRow.Row != null && Overview != null)
                {
                    UnityRow.Row.CharacterStepOffset = value;
                    Save();
                }

            }
        }

        [ShowInInspector]
        [DelayedProperty]
        [LabelText("角色皮肤宽度")]
        public float SkinWidth
        {
            get
            {
                return UnityRow.Row?.CharacterSkinWidth ?? 0.08f;

            }
            set
            {
                if (UnityRow.Row != null && Overview != null)
                {
                    UnityRow.Row.CharacterSkinWidth = value;
                    Save();
                }

            }
        }

        [ShowInInspector]
        [DelayedProperty]
        [LabelText("角色最小移动距离")]
        public float MinMoveDistance
        {
            get
            {
                return UnityRow.Row?.CharacterMinMoveDistance ?? 0.001f;

            }
            set
            {
                if (UnityRow.Row != null && Overview != null)
                {
                    UnityRow.Row.CharacterMinMoveDistance = value;
                    Save();
                }

            }
        }

        [ShowInInspector]
        [DelayedProperty]
        [LabelText("碰撞中心")]
        public Vector3 Center
        {
            get
            {
                return UnityRow.Row?.ColliderCenter ?? new Vector3();

            }
            set
            {
                UnityRow.Row.ColliderCenter = value;
                Save();
            }
        }

        [ShowInInspector]
        [DelayedProperty]
        [LabelText("碰撞半径")]
        public float Radius
        {
            get
            {
                return UnityRow.Row?.ColliderRadius ?? 0.5f;

            }
            set
            {
                if (UnityRow.Row != null && Overview != null)
                {
                    UnityRow.Row.ColliderRadius = value;
                    Save();
                }

            }
        }

        [ShowInInspector]
        [DelayedProperty]
        [LabelText("碰撞高度")]
        public float ColliderHeight
        {
            get
            {
                return UnityRow.Row?.ColliderHeight ?? 2f;

            }
            set
            {
                if (UnityRow.Row != null && Overview != null)
                {
                    UnityRow.Row.ColliderHeight = value;
                    Save();
                }

            }
        }

        [ShowInInspector]
        [LabelText("默认配置")]
        [ValueDropdown("@CharacterConfigOverview.GetUuidDropdown()")]
        public string CharacterConfigUuid
        {
            get
            {
                return UnityRow.Row.CharacterConfigUuid;
            }
            set
            {
                UnityRow.Row.CharacterConfigUuid = value;
                Save();
            }
        }
    }
}
#endif

