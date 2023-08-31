#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using System;
using UnityEditor;

namespace Pangoo.Editor{

    public partial class TriggerEventWrapper  
    {

        [Button("添加指令")]
        [FoldoutGroup("$Title")]
        [PropertyOrder(11)]
        public void AddInstructionId(){
            var addWrapper = new AddWrapper<InstructionTableOverview>(m_DetailRow.GetInstructionList());
            addWrapper.ConfirmAdd = AddInstructionId;
            m_AddWindow = OdinEditorWindow.InspectObject(addWrapper);
        }

        public void AddInstructionId(int id){
            var overview = GameSupportEditorUtility.GetExcelTableOverviewByRowId<TriggerEventTableOverview>(m_DetailRow.Id);
            m_DetailRow.AddInstructionId(id);
            UpdateInstructions();
            EditorUtility.SetDirty(overview);
            AssetDatabase.SaveAssets();
            if(m_AddWindow != null){
                m_AddWindow.Close();
                m_AddWindow = null;
            }
        }
        

        [Button("创建新的指令")]
        [FoldoutGroup("$Title")]
        [PropertyOrder(12)]
        public void CreateInstruction(){
            var createWrapper = new InstructionCreateWrapper();
            createWrapper.ConfirmCreate = CreateTrigger;
            m_CreateWindow = OdinEditorWindow.InspectObject(createWrapper);
        }

        public void CreateTrigger(InstructionTable.InstructionRow row){
            Debug.Log($"创建对应数据。row:{row.Id}");
            var packageDir =  GameSupportEditorUtility.GetPakcageDirByOverviewRowId<TriggerEventTableOverview>(m_Row.Id);
            var overview = AssetDatabaseUtility.FindAssetFirst<InstructionTableOverview>(packageDir);
            overview.Data.Rows.Add(row);
            AddInstructionId(row.Id);
            EditorUtility.SetDirty(overview);
            m_CreateWindow.Close();
            AssetDatabase.SaveAssets();
            Debug.Log($"创建对应数据。row:{row.Id} 完成");
        }


        [Serializable]
        public class TriggerInstrucationWrapper:InstructionWrapper{
            public TriggerInstrucationWrapper(InstructionTable.InstructionRow row) : base(row)
            {
            }

            [Button("删除引用")]
            [TableTitleGroup("操作")]
            [TableColumnWidth(80,resizable:false)]
            protected override void RemoveRef(){
                base.RemoveRef();
            }
        }

    }
}
#endif