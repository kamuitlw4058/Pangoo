
#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using Pangoo;
using System;
using Pangoo.Core.Common;
using Pangoo.Core.VisualScripting;

namespace Pangoo.Editor
{

    public partial class DynamicObjectWrapper
    {



        [Button("添加触发器")]
        [FoldoutGroup("$Title")]
        [PropertyOrder(11)]
        public void AddTrigger()
        {
            var addWrapper = new AddWrapper<TriggerEventTableOverview>(m_DetailRow.GetTriggerEventIdList());
            addWrapper.ConfirmAdd = AddTrigger;
            m_AddWindow = OdinEditorWindow.InspectObject(addWrapper);
        }

        public void AddTrigger(int id)
        {
            var overview = GameSupportEditorUtility.GetExcelTableOverviewByRowId<DynamicObjectTableOverview>(m_DetailRow.Id);
            m_DetailRow.AddTriggerEventId(id);
            UpdateTriggers();
            EditorUtility.SetDirty(overview);
            AssetDatabase.SaveAssets();
            if (m_AddWindow != null)
            {
                m_AddWindow.Close();
                m_AddWindow = null;
            }
        }




        [Button("创建新的触发器")]
        [FoldoutGroup("$Title")]
        [PropertyOrder(12)]
        public void CreateTrigger()
        {
            var createWrapper = new TriggerEventCreateWrapper();
            createWrapper.ConfirmCreate = CreateTrigger;
            m_CreateWindow = OdinEditorWindow.InspectObject(createWrapper);
        }

        public void CreateTrigger(TriggerEventTable.TriggerEventRow row)
        {
            Debug.Log($"创建对应数据。row:{row.Id}");
            var packageDir = GameSupportEditorUtility.GetPakcageDirByOverviewRowId<DynamicObjectTableOverview>(m_Row.Id);
            var overview = AssetDatabaseUtility.FindAssetFirst<TriggerEventTableOverview>(packageDir);
            overview.Data.Rows.Add(row);
            AddTrigger(row.Id);
            EditorUtility.SetDirty(overview);
            m_CreateWindow.Close();
            AssetDatabase.SaveAssets();
            Debug.Log($"创建对应数据。row:{row.Id} 完成");

        }


        [Serializable]
        public class DynamicObjecTriggerWrapper : TriggerEventWrapper
        {
            public delegate void TriggerStartHandler();
            public TriggerStartHandler StartTrigger;

            InstructionList RunningInstructionList = null;

            GameObject m_GameObject;


            public DynamicObjecTriggerWrapper(TriggerEventTable.TriggerEventRow row, GameObject go = null) : base(row)
            {
                m_GameObject = go;
            }

            [Button("删除引用")]
            [TableTitleGroup("操作")]
            [TableColumnWidth(80, resizable: false)]
            protected override void RemoveRef()
            {
                base.RemoveRef();
            }



            [Button("触发")]
            [TableTitleGroup("操作")]
            protected void Run()
            {
                List<Instruction> instructions = new();

                foreach (var instruction in m_Instructions)
                {
                    instructions.Add(instruction.InstructionInstance);
                }
                Debug.Log($"Start Run Instruction:{instructions.Count}");
                RunningInstructionList = new InstructionList(instructions.ToArray());
                RunningInstructionList.Start(new Args(m_TriggerRow, m_GameObject));
            }

            public void OnUpdate()
            {

                if (RunningInstructionList != null)
                {
                    RunningInstructionList.OnUpdate();
                    if (RunningInstructionList.IsFinished)
                    {
                        RunningInstructionList = null;
                    }
                }
                else
                {
                }
            }
        }



    }
}
#endif