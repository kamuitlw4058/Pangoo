#if UNITY_EDITOR
using System.Collections.Generic;
using System.Linq;
using System.Collections;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UIElements;

namespace Pangoo.Editor
{
    public class CharacterDetailWrapper : ExcelTableRowDetailWrapper<CharacterTableOverview, CharacterTable.CharacterRow>
    {

        [LabelText("资源ID")]
        [ValueDropdown("AssetPathIdValueDropdown")]
        [ShowInInspector]
        [InlineButton("ShowCreateAssetPath", SdfIconType.Plus, Label = "")]
        public int AssetPathId
        {
            get
            {
                return Row?.AssetPathId ?? 0;
            }
            set
            {
                if (Row != null && Overview != null)
                {
                    Row.AssetPathId = value;
                    Save();
                }
            }

        }

        void ShowCreateAssetPath()
        {
            var assetOverview = GameSupportEditorUtility.GetExcelTableOverviewByConfig<AssetPathTableOverview>(Overview.Config);
            var assetNewObject = AssetPathNewWrapper.Create(assetOverview, Id, ConstExcelTable.CharacterAssetTypeName, Name, afterCreateAsset: OnAfterCreateAsset);
            var window = OdinEditorWindow.InspectObject(assetNewObject);
            assetNewObject.Window = window;
        }

        public void OnAfterCreateAsset(int id)
        {
            AssetPathId = id;
        }


        public IEnumerable AssetPathIdValueDropdown()
        {
            return GameSupportEditorUtility.GetAssetPathIds(assetTypes: new List<string> { "Character" });
        }


        [ShowInInspector]
        public bool IsPlayer
        {
            get
            {
                return Row?.IsPlayer ?? false;
            }
            set
            {
                if (Row != null && Overview != null)
                {
                    Row.IsPlayer = value;
                    Save();
                }
            }
        }



        [ShowInInspector]
        [DelayedProperty]
        public float LinearSpeed
        {
            get
            {
                return Row?.MoveSpeed ?? 1;

            }
            set
            {
                if (Row != null && Overview != null)
                {
                    Row.MoveSpeed = value;
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
                return Row?.CameraOffset ?? Vector3.zero;

            }
            set
            {
                if (Row != null && Overview != null)
                {
                    Row.CameraOffset = value;
                    Save();
                }

            }
        }


        [ShowInInspector]
        [DelayedProperty]
        public float MaxPitch
        {
            get
            {
                return Row?.MaxPitch ?? 90;

            }
            set
            {
                if (Row != null && Overview != null)
                {
                    Row.MaxPitch = value;
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
                return Row?.CameraOnly ?? false;

            }
            set
            {
                if (Row != null && Overview != null)
                {
                    Row.CameraOnly = value;
                    Save();
                }

            }
        }



    }
}
#endif