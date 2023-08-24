
#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;

using Pangoo;

namespace Pangoo.Editor{
    public partial class DynamicObjectEditor : MonoBehaviour
    {
        private  OdinEditorWindow m_AddDynamicObjectWindow;

        [Button("添加")]
        public void AddDynamicObject(){
             m_AddDynamicObjectWindow = OdinEditorWindow.InspectObject(new DynamicObjectAddWindow(this,DynamicObjectIds));
        }

        public void ConfirmAdd(int id){
            GameSectionTableOverview gameSectionTableOverview = GameSupportEditorUtility.GetExcelTableOverviewByRowId<GameSectionTableOverview>(Section);
            var gameSectionRow = gameSectionTableOverview.Data.GetRowById(Section);

            if(gameSectionRow == null){
                Debug.Log($"gameSectionRow:{gameSectionRow}");
                return;
            }

            gameSectionRow.AddDynamicObjectId(id);
            EditorUtility.SetDirty(gameSectionTableOverview);
            OnSectionChange();
            AssetDatabase.SaveAssets();
            m_AddDynamicObjectWindow.Close();

        }


        public class DynamicObjectAddWindow{
            

            [ValueDropdown("GetDynamicObject", ExpandAllMenuItems = true)]
            public int DynamicObjectId;

            public IEnumerable GetDynamicObject(){
                return GameSupportEditorUtility.GetExcelTableOverviewNamedIds<DynamicObjectTableOverview>(ids:m_DynamicObjectIds);;
            }

            DynamicObjectEditor m_Editor;

            List<int> m_DynamicObjectIds;

            public DynamicObjectAddWindow(DynamicObjectEditor editor,List<int> ids){
                m_Editor = editor;
                m_DynamicObjectIds = ids;
            }


            [Button("添加", ButtonSizes.Large)]
            public void Create(){

                if(DynamicObjectId == 0 )
                {
                    EditorUtility.DisplayDialog("错误", "Id必须填写.不能为0", "确定");
                    // GUIUtility.ExitGUI();
                    return;
                }


                m_Editor.ConfirmAdd(DynamicObjectId);
            }
        }
    }
}
#endif