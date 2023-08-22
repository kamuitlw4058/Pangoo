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

        public  virtual List<ExcelRowBase> BaseRows {
            get{
                return null;
            }
        }

        private Dictionary<string,List<int>> PackagePathDict = new Dictionary<string, List<int>>();

        public List<int> Ids{
            get{
                List<int> ret = new List<int>();
                var tmpRows = BaseRows;
                for(int i =0;i < tmpRows.Count;i ++){
                    ret.Add(tmpRows[i].Id);
                }
                return ret;
            }
        }


        public void AddPackagePath( string path,List<int> ids ){
            PackagePathDict.Add(path,ids);
        }

        public string GetPackagePathById(int id){
            foreach(var kv in PackagePathDict){
                if(kv.Value.Contains(id)){
                    return kv.Key;
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
