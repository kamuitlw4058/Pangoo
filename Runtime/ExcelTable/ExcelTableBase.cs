using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Pangoo
{
    public abstract class ExcelTableBase
    {
        public virtual void CustomInit()
        {

        }
        
        public virtual string[] GetHeadNames()
        {
            return null;
        }

        public virtual string[] GetDescNames()
        {
            return null;
        }

        public virtual string[] GetTypeNames()
        {
            return null;
        }


        public virtual void Merge(ExcelTableBase table)
        {

        }
        public virtual void GetFieldInfos()
        {
            
        }
        
        public virtual string GetFieldDesc(string field)
        {
            return field;
        }

        public virtual List<string[]> GetTableHeadList()
        {
            List<string[]> tableHeadList = new List<string[]>();
            tableHeadList.Add(GetHeadNames());
            tableHeadList.Add(GetTypeNames());
            tableHeadList.Add(GetDescNames());
            
            return tableHeadList;
        }
        public virtual List<string[]> GetTableRowDataList()
        {
            return null;
        }
    }

}
