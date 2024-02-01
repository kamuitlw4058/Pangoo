using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using Pangoo.Core.VisualScripting;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Math;

namespace Pangoo.Core.Common
{
    public class PangooColliderReceiver : MonoBehaviour
    {

        public DynamicObject dynamicObject;
        public SubObjectTriggerEventType subObjectTriggerEventType;

        public bool TriggerEnter;

        private void OnTriggerEnter(Collider other)
        {

            if (other.tag.Equals("Player"))
            {
                TriggerEnter = true;
                Debug.Log($"EntityDynamicObject Extra OnTriggerEnter");

                switch (subObjectTriggerEventType)
                {
                    case SubObjectTriggerEventType.Both:
                    case SubObjectTriggerEventType.BaseTrigger:
                        dynamicObject?.TriggerEnter3d(other);
                        break;
                }

                switch (subObjectTriggerEventType)
                {
                    case SubObjectTriggerEventType.Both:
                    case SubObjectTriggerEventType.ExtraTrigger:
                        dynamicObject?.ExtraTriggerEnter3d(other);
                        break;
                }

            }
        }



        private void OnTriggerExit(Collider other)
        {

            if (other.tag.Equals("Player"))
            {
                TriggerEnter = false;
                Debug.Log($"EntityDynamicObject Extra OnTriggerExit");
                switch (subObjectTriggerEventType)
                {
                    case SubObjectTriggerEventType.Both:
                    case SubObjectTriggerEventType.BaseTrigger:
                        dynamicObject?.TriggerExit3d(other);
                        break;
                }

                switch (subObjectTriggerEventType)
                {
                    case SubObjectTriggerEventType.Both:
                    case SubObjectTriggerEventType.ExtraTrigger:
                        dynamicObject?.ExtraTriggerExit3d(other);
                        break;
                }

            }
        }

        private void OnDisable()
        {
            if (TriggerEnter)
            {
                Debug.Log($"EntityDynamicObject Extra OnTriggerExit");
                switch (subObjectTriggerEventType)
                {
                    case SubObjectTriggerEventType.Both:
                    case SubObjectTriggerEventType.BaseTrigger:
                        dynamicObject?.TriggerExit3d(null);
                        break;
                }

                switch (subObjectTriggerEventType)
                {
                    case SubObjectTriggerEventType.Both:
                    case SubObjectTriggerEventType.ExtraTrigger:
                        dynamicObject?.ExtraTriggerExit3d(null);
                        break;
                }
                TriggerEnter = false;
            }
        }


    }
}