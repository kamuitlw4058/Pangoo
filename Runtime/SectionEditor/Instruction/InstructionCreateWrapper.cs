#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Sirenix.OdinInspector;
using Pangoo.Core.VisualScripting;


namespace Pangoo.Editor{

    public class InstructionCreateWrapper  : CreateWrapper<InstructionTable.InstructionRow,InstructionTableOverview>
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
        [ValueDropdown("GetInstructionType")]
        [OnValueChanged("OnInstructionTypeChange")]
        public string InstructionType{
            get{
                return m_Row.InstructionType;
            }
            set{
                m_Row.InstructionType = value;
            }
        }

        [ShowInInspector]
        public Instruction InstructionInstance{
            get{
                return m_Wrapper.InstructionInstance;
            }
            set{
                m_Wrapper.InstructionInstance = value;
            }
        }


        public IEnumerable GetInstructionType(){
            return GameSupportEditorUtility.GetInstructionType(m_Row.InstructionType);
        }


        public void OnInstructionTypeChange(){
            m_Wrapper.OnInstructionTypeChange();
        }




        InstructionWrapper m_Wrapper;
        public InstructionCreateWrapper(){
            m_Wrapper = new InstructionWrapper(m_Row);
        }

        protected override bool Check(){
            if(!base.Check()){
                return false;
            }

            return true;

        }

        public override void Create(){
            if(!Check()){
                Debug.Log($"Check 失败!");
                return;
            }
            m_Wrapper.SaveParams(false);

            if(ConfirmCreate != null){
                Debug.Log($"ConfirmCreate!");
                ConfirmCreate(m_Row);
            }
            
        }
    }
}
#endif