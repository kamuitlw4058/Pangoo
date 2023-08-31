
#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Sirenix.OdinInspector;
using Pangoo;
using System;

namespace Pangoo.Editor{

        [Serializable]
        public partial class DynamicObjectWrapper:BaseNamedWrapper{

           [ShowInInspector]
           [FoldoutGroup("$Title")]
            public override string Name{
                get{
                    return $"{m_DetailRow.Id}-{m_DetailRow.Name}-{m_DetailRow.NameCn}";
                }
            }
            DynamicObjectTable.DynamicObjectRow m_DetailRow;

            [ShowInInspector]
            [PropertyOrder(10)]
            [FoldoutGroup("$Title")]
            [LabelText("触发器")]
            // [HideLabel]
            [ListDrawerSettings(HideAddButton =true,HideRemoveButton =true,DraggableItems =false, Expanded = true )]
            [TableList]
            // [GUIColor("#FF0000")]
            public List<DynamicObjecTriggerWrapper> m_Triggers = new List<DynamicObjecTriggerWrapper>();

            GameObject m_GameObject;



            public DynamicObjectWrapper(DynamicObjectTable.DynamicObjectRow row,GameObject go = null): base(row){
                m_DetailRow = row;
                m_GameObject = go;
                UpdateTriggers();
            }
            
            public void UpdateTriggers(){
                m_Triggers.Clear();
                foreach(var trigger_id in m_DetailRow.TriggerEventIds.ToListInt()){
                    var trigger = GameSupportEditorUtility.GetExcelTableRowWithOverviewById<TriggerEventTableOverview,TriggerEventTable.TriggerEventRow>(trigger_id);
                    var triggerWarrper = new DynamicObjecTriggerWrapper(trigger,m_GameObject);
                    triggerWarrper.RemoveRow = RemoveTrigger;
                    triggerWarrper.RemoveRowRef = RemoveTrigger;
                    m_Triggers.Add(triggerWarrper);
                }
                
            }
            public void RemoveTrigger(int id){
                m_DetailRow.RemoveTriggerEventId(id);
                var overview = GameSupportEditorUtility.GetExcelTableOverviewByRowId<DynamicObjectTableOverview>(Id);
                EditorUtility.SetDirty(overview);
                AssetDatabase.SaveAssets();
                UpdateTriggers();
            }

            [Button("完全删除")]
            [FoldoutGroup("$Title")]
            [PropertyOrder(11)]
            public void RemoveCompletely(){
                Debug.Log($"删除动态物体:{Name}");
                var dynamicObjectOverview = GameSupportEditorUtility.GetExcelTableOverviewByRowId<DynamicObjectTableOverview>(Id);
                dynamicObjectOverview.FullRemoveRowById(Id);

                Debug.Log($"完成删除:{Name}");
                
            }

            public void OnUpdate(){
                foreach(var trigger in m_Triggers){
                    trigger.OnUpdate();
                }
            }

       
    }
}
#endif