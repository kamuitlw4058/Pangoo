#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using UnityEditor;

namespace Pangoo.Editor{

    public class CreateWrapper<TRow,TOverview> where TRow: ExcelRowBase,new() where TOverview :ExcelTableOverview
    {
        public delegate void ConfirmCreateHandler(TRow row);

        public ConfirmCreateHandler ConfirmCreate;

        [ShowInInspector]
        public int Id{
            get{
                if(m_Row == null){
                    return 0;
                }
                return m_Row.Id;
            }set{
                m_Row.Id = value;
            }
        }


        [ShowInInspector]
        public virtual string Name{
            get{
                if(m_Row == null){
                    return string.Empty;
                }

                return $"{m_Row.Id}";
            }
            set{
            }

        }

        protected TRow m_Row;

        public CreateWrapper(){
            m_Row = new TRow();


        }

        protected virtual bool Check(){
            if(m_Row == null){
                EditorUtility.DisplayDialog("错误", "创建数据失败", "确定");
                return false;
            }

            if ( Id == 0 )
            {
                EditorUtility.DisplayDialog("错误", "Id 必须填写", "确定");
                return false;
            }

            if(GameSupportEditorUtility.ExistsExcelTableOverviewId<TOverview>(m_Row.Id)){
                EditorUtility.DisplayDialog("错误", "已经存在对应的Id", "确定");
                return false;
            }


            return true;
        }


        [Button("新建", ButtonSizes.Large)]
        public virtual void Create(){
            if(!Check()){
                Debug.Log($"Check 失败!");
                return;
            }

            if(ConfirmCreate != null){
                Debug.Log($"ConfirmCreate!");
                ConfirmCreate(m_Row);
            }
            
        }
    }
}
#endif