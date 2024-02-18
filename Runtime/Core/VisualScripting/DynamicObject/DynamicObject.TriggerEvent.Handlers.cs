using System;
using System.Collections.Generic;

using UnityEngine;
using Pangoo.Core.Common;
using Pangoo.Core.Characters;
using Sirenix.OdinInspector;
using UnityEngine.Playables;
using UnityEngine.EventSystems;



namespace Pangoo.Core.VisualScripting
{

    public partial class DynamicObject
    {
        public bool AllTriggerEnabled { get; set; } = true;

        [ShowInInspector]
        public Dictionary<TriggerTypeEnum, List<TriggerEvent>> TriggerDict = new Dictionary<TriggerTypeEnum, List<TriggerEvent>>();

        public void TriggerClear(TriggerTypeEnum triggerType)
        {
            if (TriggerDict.ContainsKey(triggerType))
            {
                TriggerDict.Remove(triggerType);
            }
        }

        public void TriggerRegister(TriggerEvent trigger)
        {
            if (TriggerDict.ContainsKey(trigger.TriggerType))
            {
                TriggerDict[trigger.TriggerType].Add(trigger);
            }
            else
            {
                TriggerDict.Add(trigger.TriggerType, new List<TriggerEvent>() { trigger });
            }
        }



        public bool TriggerInovke(TriggerTypeEnum triggerType, string uuid = null)
        {
            bool ret = false;
            if (!AllTriggerEnabled)
            {
                return ret;
            }
            if (TriggerDict.TryGetValue(triggerType, out List<TriggerEvent> triggers))
            {
                triggers.ForEach((o) =>
                {
                    if (o.Enabled)
                    {
                        bool flag = false;
                        if (uuid.IsNullOrWhiteSpace() || uuid.Equals(Row.Uuid))
                        {
                            flag = true;
                        }

                        if (o.Filter != null)
                        {
                            if (!o.Filter.Check())
                            {
                                flag = false;
                            }
                        }

                        if (flag)
                        {
                            ret = true;
                            Log($"Invoke:{triggerType}");
                            o.OnInvoke(CurrentArgs);
                        }
                    }
                });
            }
            return ret;
        }


        public void TriggerStop(TriggerTypeEnum triggerType)
        {
            if (TriggerDict.TryGetValue(triggerType, out List<TriggerEvent> triggers))
            {
                triggers.ForEach((o) =>
                {
                    o.IsStoped = true;
                });
            }
        }

        public void TriggerSetEnabled(string uuid, bool val)
        {
            foreach (var triggers in TriggerDict.Values)
            {
                foreach (var trigger in triggers)
                {
                    if (trigger.Row.Uuid.Equals(uuid))
                    {
                        trigger.SetEnabled(val);
                    }
                }
            }
        }

        public void TriggerEnabled(string uuid, bool val)
        {
            foreach (var triggers in TriggerDict.Values)
            {
                foreach (var trigger in triggers)
                {
                    if (trigger.Row.Uuid.Equals(uuid))
                    {
                        trigger.Enabled = val;
                    }
                }
            }
        }

        public void TriggerSetTargetIndex(string uuid, int index)
        {
            foreach (var triggers in TriggerDict.Values)
            {
                foreach (var trigger in triggers)
                {
                    if (trigger.Row.Uuid.Equals(uuid))
                    {
                        trigger.SetTargetIndex(index);
                    }
                }
            }
        }

        public void TriggerUpdate()
        {
            foreach (var triggers in TriggerDict.Values)
            {
                foreach (var trigger in triggers)
                {
                    trigger.OnUpdate();
                }
            }
        }

        public bool TriggerIsRunning(TriggerTypeEnum triggerType)
        {
            bool ret = false;
            if (TriggerDict.TryGetValue(triggerType, out List<TriggerEvent> triggers))
            {

                triggers.ForEach((o) =>
                {
                    if (o.IsRunning)
                    {
                        ret = true;
                    }
                });
            }
            return ret;
        }



    }
}