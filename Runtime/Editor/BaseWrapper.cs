#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;

namespace Pangoo.Editor{

    public class BaseWrapper 
    {
        protected  OdinEditorWindow m_AddWindow;
        protected  OdinEditorWindow m_CreateWindow;

        public delegate void RemoveRowHandler(int id);
        public RemoveRowHandler RemoveRow;


        public delegate void RemoveRowRefHandler(int id);
        public RemoveRowRefHandler RemoveRowRef;


        public virtual string Title{
            get{
                return Name;
            }
        }

        [FoldoutGroup("$Title",expanded:false)]
        [ShowInInspector]
        public int Id{
            get{
                if(m_Row == null){
                    return 0;
                }
                return m_Row.Id;
            }
        }


        [FoldoutGroup("$Title")]
        [ShowInInspector]
        public virtual string Name{
            get{

                return $"{m_Row.Id}";
            }
        }

        protected ExcelRowBase m_Row;
        public BaseWrapper(ExcelRowBase row){
            m_Row = row;
        }
        
        
        protected virtual void Remove(){
            if(RemoveRow != null){
                RemoveRow(Id);
            }

        }
        protected virtual void RemoveRef(){
            if(RemoveRowRef != null){
                RemoveRowRef(Id);
            }
        }

    }
}
#endif