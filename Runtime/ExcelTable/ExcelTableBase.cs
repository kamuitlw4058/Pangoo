using System;
using System.Collections;
using System.Collections.Generic;
using LitJson;

#if UNITY_EDITOR
using System.Reflection;
using System.Linq;
using OfficeOpenXml;
using UnityEditor;
#endif

using Sirenix.OdinInspector;
using UnityEngine;
using System.IO;
using Object = UnityEngine.Object;

namespace Pangoo
{
    [Serializable]
   public abstract partial class ExcelTableBase
    {
        public int Priority = 0;

        public  virtual IReadOnlyList<ExcelRowBase> BaseRows {
            get{
                return null;
            }
        }

        public virtual IReadOnlyList<ExcelNamedRowBase> NamedBaseRows{
            get{
                return null;
            }
        }



        public T GetRowById<T>(int id) where T:ExcelRowBase
        {
            foreach(var row in BaseRows){
                if(row.Id == id){
                    return (T)row;
                }
            }
            return null;
        }


        public virtual void CustomInit()
        {

        }

        public virtual void Init(){
            CustomInit();
        }


        public virtual void Merge(ExcelTableBase table)
        {

        }
    }

}
