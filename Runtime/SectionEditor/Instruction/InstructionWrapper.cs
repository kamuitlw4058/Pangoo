#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using GameFramework;
using System;
using Sirenix.OdinInspector;
using Pangoo.Core.VisualScripting;
using Pangoo.Core.Common;
using System.Reflection;

namespace Pangoo.Editor{

    [Serializable]
    public class InstructionWrapper :BaseNamedWrapper
    {
        InstructionTable.InstructionRow  m_DetailRow;
        public InstructionWrapper(InstructionTable.InstructionRow row) : base(row)
        {
            m_DetailRow = row;
            OnInstructionTypeChange();
        }

        public override string Title{
            get{
                if(m_DetailRow == null){
                    return string.Empty;
                }
                if(InstructionInstance != null){
                     var title = InstructionInstance.GetType().GetCustomAttribute(typeof(Pangoo.Core.Common.TitleAttribute));
                     if(title != null){
                        return title.ToString();
                     }
                }
                return $"{m_DetailRow.InstructionType}".Humanize();
            }
        }

        // [SerializeField]
        [FoldoutGroup("$Title")]
        [ShowInInspector]
        [PropertyOrder(10)]
        [InfoBox("This info box is only shown while in editor mode.", InfoMessageType.Warning, "@this.InstructionInstance == null")]

        public Instruction InstructionInstance;

        [ShowInInspector]
        [FoldoutGroup("$Title")]
        [ValueDropdown("GetInstructionType")]
        [OnValueChanged("OnInstructionTypeChange")]
        public string InstructionType{
            get{
                return m_DetailRow.InstructionType;
            }
            set{
                m_DetailRow.InstructionType = value;
            }
        }

        public void OnInstructionTypeChange(){
            if(m_DetailRow.InstructionType == null){
                InstructionInstance = null;
                return;
            }

            var type = Utility.Assembly.GetType(m_DetailRow.InstructionType);
            if(type == null){
                InstructionInstance = null;
                return;  
            }


            if(InstructionInstance == null || type != InstructionInstance.GetType()){
                InstructionInstance = Activator.CreateInstance(type ) as Instruction;
                InstructionInstance.LoadParams(m_DetailRow.Params);
                return;
            }
             
        }

        public IEnumerable GetInstructionType()
        {
            return GameSupportEditorUtility.GetInstructionType(m_DetailRow.InstructionType);
        }
        
        [Button("保存参数")]
        [TableColumnWidth(80,resizable:false)]
        public void SaveParams(bool updateOverride = true){
            if(m_DetailRow != null && InstructionInstance != null){
                m_DetailRow.Params = InstructionInstance.ParamsString();

                if(updateOverride){
                    var overview = GameSupportEditorUtility.GetExcelTableOverviewByRowId<InstructionTableOverview>(m_DetailRow.Id);
                    EditorUtility.SetDirty(overview);
                    AssetDatabase.SaveAssets();
                }
            }
        }



        
    }
}
#endif