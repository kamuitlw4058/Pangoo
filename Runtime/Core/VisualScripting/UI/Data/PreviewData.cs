using System;
using UnityEngine;
using Pangoo.Core.Common;
using LitJson;
using Pangoo.Core.Services;
using Pangoo.MetaTable;
using Pangoo.Common;

namespace Pangoo.Core.VisualScripting
{

    [Serializable]

    public class PreviewData
    {
        public Args args;
        public DynamicObject DynamicObject;

        public UIService UIService;

        public IDynamicObjectPreviewRow PreviewRow;

        UIPreviewParams m_Params;
        public UIPreviewParams Params
        {
            get
            {
                if (m_Params == null)
                {
                    m_Params = new UIPreviewParams();
                    m_Params.Load(PreviewRow.Params);
                }
                return m_Params;
            }
        }

        public string Name
        {
            get
            {
                return DynamicObject.Row.PreviewName;
            }
        }


        public string Desc
        {
            get
            {
                return DynamicObject.Row.PreviewDesc;
            }
        }

        public Vector3 PreviewRotation
        {
            get
            {
                return DynamicObject.Row.PreviewRotation;
            }
        }

        public Vector3 PreviewRotationUp
        {
            get
            {
                return DynamicObject.Row.PreviewRotationUp;
            }
        }

        public Vector3 PreviewDirection
        {
            get
            {
                return DynamicObject.CachedTransfrom.TransformDirection(PreviewRotation);
            }
        }

        public Vector3 PreviewDirectionUp
        {
            get
            {
                return DynamicObject.CachedTransfrom.TransformDirection(PreviewRotationUp);
            }
        }



        public Vector3 CurrentPosition
        {
            get
            {
                return DynamicObject.CachedTransfrom.position;
            }
            set
            {
                DynamicObject.CachedTransfrom.position = value;
            }
        }

        public Vector3 CurrentRotation
        {
            get
            {
                return DynamicObject.CachedTransfrom.localRotation.eulerAngles;
            }
            set
            {
                DynamicObject.CachedTransfrom.localRotation = Quaternion.Euler(value);
            }
        }

        public void LookAt(Transform transform, Vector3 worldUp)
        {
            DynamicObject.CachedTransfrom.LookAt(transform, worldUp);

        }



        public void Rotate(Vector3 axis, float angle, Space space)
        {
            DynamicObject.CachedTransfrom.Rotate(axis, angle, space);
        }

        public Vector3 CurrentScale
        {
            get
            {
                return DynamicObject.CachedTransfrom.localScale;
            }
            set
            {
                DynamicObject.CachedTransfrom.localScale = value;
            }

        }

        public KeyCode[] ExitKeyCodes
        {
            get
            {
                return PreviewRow.ExitKeyCodes.ToSplitArr<KeyCode>();
            }
        }

        public KeyCode[] InteractKeyCodes
        {
            get
            {
                return PreviewRow.PreviewInteractKeyCodes.ToSplitArr<KeyCode>();
            }
        }

        public Vector3 OldPosition { get; set; }

        public Vector3 OldRotation { get; set; }


        public Vector3 OldScale { get; set; }

    }
}