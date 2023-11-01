using System;
using System.Collections.Generic;
using Pangoo.Core.Common;
using UnityEngine;

namespace Pangoo.Core.VisualScripting
{
    [Serializable]
    public class ConditionList : TPolymorphicList<Condition>
    {
        [SerializeReference]
        private Condition[] m_Conditions = Array.Empty<Condition>();

        // PROPERTIES: ----------------------------------------------------------------------------

        public override int Length => this.m_Conditions.Length;

        // EVENTS: --------------------------------------------------------------------------------

        public event Action EventStartCheck;
        public event Action EventEndCheck;

        // CONSTRUCTOR: ---------------------------------------------------------------------------

        public ConditionList()
        { }

        public ConditionList(params Condition[] conditions) : this()
        {
            this.m_Conditions = conditions;
        }

        // PUBLIC METHODS: ------------------------------------------------------------------------

        public bool Check(Args args)
        {
            this.EventStartCheck?.Invoke();

            foreach (Condition condition in this.m_Conditions)
            {
                if (condition == null) continue;

                if (!condition.Check(args))
                {
                    this.EventEndCheck?.Invoke();
                    return false;
                }
            }

            this.EventEndCheck?.Invoke();
            return true;
        }

        public int GetState(Args args)
        {
            int ret = 0;
            this.EventStartCheck?.Invoke();

            if (this.m_Conditions.Length > 0)
            {
                ret = this.m_Conditions[0].GetState(args);
            }

            this.EventEndCheck?.Invoke();
            return ret;
        }

        public Condition Get(int index)
        {
            index = Mathf.Clamp(index, 0, this.Length - 1);
            return this.m_Conditions[index];
        }

        public static ConditionList BuildConditionList(List<int> ids, ConditionTable table = null, TriggerEvent trigger = null)
        {
            List<Condition> vals = new();

            foreach (var rowId in ids)
            {
                ConditionTable.ConditionRow row = ConditionRowExtension.GetById(rowId, table);
                if (row == null || row.ConditionType == null)
                {
                    continue;
                }

                var instance = ClassUtility.CreateInstance<Condition>(row.ConditionType);
                instance.Load(row.Params);
                // instance.Trigger = trigger;

                vals.Add(instance);
            }

            return new ConditionList(vals.ToArray());
        }
    }
}
