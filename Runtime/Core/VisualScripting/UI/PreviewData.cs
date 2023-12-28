using System;
using UnityEngine;
using Pangoo.Core.Common;
using LitJson;
using Pangoo.Core.Services;
using Pangoo.MetaTable;

namespace Pangoo.Core.VisualScripting
{

    [Serializable]

    public class PreviewData
    {
        public Args args;
        public DynamicObject DynamicObject;

        public UIService UIService;

        public IDynamicObjectPreviewRow PreviewRow;

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

        public Vector3 OldPosition { get; set; }

        public Vector3 OldRotation { get; set; }


        public Vector3 OldScale { get; set; }

    }
}