using System;
using System.Collections.Generic;
using UnityEngine;
using Pangoo.Core.Common;
using LitJson;
using Pangoo.Core.Services;
using Pangoo.MetaTable;
using Pangoo.Common;

namespace Pangoo.Core.VisualScripting
{

    [Serializable]

    public class DialogueData
    {
        public Args args;
        public DynamicObject DynamicObject;

        public UIService UIService;

        public IDialogueRow DialogueRow;


        public bool WaitClosed;


        public bool DontControllPlayer;

        public bool ShowCursor;


        public Action FinishAction;



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



        public Vector3 OldPosition { get; set; }

        public Vector3 OldRotation { get; set; }


        public Vector3 OldScale { get; set; }

        public List<DialogueUpdateData> dialogueUpdateDatas = new List<DialogueUpdateData>();


    }
}