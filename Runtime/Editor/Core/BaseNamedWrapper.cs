#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace Pangoo.Editor{

    public class BaseNamedWrapper :BaseWrapper
    {
        [FoldoutGroup("$Title")]
        [ShowInInspector]
        public override string Name{
            get{
                if(m_NamedRow == null){
                    return string.Empty;
                }

                return $"{m_NamedRow.Id}-{m_NamedRow.Name}";
            }
        }
        protected ExcelNamedRowBase m_NamedRow; 
        public BaseNamedWrapper(ExcelNamedRowBase row):base(row){
            m_NamedRow = row;
        }

    }
}
#endif