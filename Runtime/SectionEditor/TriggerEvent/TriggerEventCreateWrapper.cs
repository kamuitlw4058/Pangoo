#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Sirenix.OdinInspector;


namespace Pangoo.Editor{

    public class TriggerEventCreateWrapper  : CreateWrapper<TriggerEventTable.TriggerEventRow,TriggerEventTableOverview>
    {
        public override string Name{
            get{
                if(m_Row == null){
                    return string.Empty;
                }
                return m_Row.Name;
            }
            set{
                m_Row.Name = value;
            }
        }

        [ShowInInspector]
        public TriggerTypeEnum triggerType{
            get{
                return m_Wrapper.TriggerType;
            }set{
                m_Wrapper.TriggerType= value;
            }
        }

        TriggerEventWrapper m_Wrapper;
        public TriggerEventCreateWrapper(){
            m_Wrapper = new TriggerEventWrapper(m_Row);
        }

        protected override bool Check(){
            if(!base.Check()){
                return false;
            }

            if(m_Wrapper.TriggerType == TriggerTypeEnum.Unknown){
                EditorUtility.DisplayDialog("错误", "Trigger Type 不能是unknown", "确定");
                return false;
            }
 
            return true;

        }
    }
}
#endif