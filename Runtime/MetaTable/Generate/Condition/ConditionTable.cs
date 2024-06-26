// 本文件使用工具自动生成，请勿进行手动修改！

using System;
using System.Collections;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using LitJson;
using UnityEngine;
using Sirenix.OdinInspector;
using System.Xml.Serialization;
using Pangoo.Common;
using MetaTable;

namespace Pangoo.MetaTable
{
    [Serializable]
    public partial class ConditionTable : MetaTableBase,IConditionTable
    {


        public Dictionary<string, IConditionRow> RowDict = new Dictionary<string, IConditionRow>();

        public override IReadOnlyList<IMetaTableRow> BaseRows => RowDict.Values.ToList();

        public override void AddRows(IReadOnlyList<IMetaTableRow> rows)
        {
             for (int i = 0; i < rows.Count; i++)
            {
               var o = rows[i];
               if (o.Uuid == null)
                {
                   Debug.LogError("AddRows Uuid Is Null");
                   return;
                }
               if (RowDict.ContainsKey(o.Uuid))
                {
                   Debug.LogError($"{GetType().Name} Uuid:{o.Uuid} Dup! Please Check");
                   return;
                }
                   RowDict.Add(o.Uuid, o as IConditionRow);
            }
        }

        public override void MergeRows(IReadOnlyList<IMetaTableRow> rows)
        {
            for (int i = 0; i < rows.Count; i++)
            {
                if (!RowDict.ContainsKey(rows[i].Uuid))
                {
                    RowDict.Add(rows[i].Uuid, rows[i] as IConditionRow);
                }
            }
        }

        public IConditionRow GetRowByUuid(string uuid)
        {
            if (RowDict.TryGetValue(uuid, out IConditionRow row))
            {
                return row;
            }
            return null;
        }

        public IConditionRow GetRowById(int id)
        {
            var Values = RowDict.Values;
            foreach (var val in Values)
            {
                if (val.Id == id)
                {
                    return val;
                }
            }
            return null;
        }

        public override IMetaTableRow GetMetaTableRowByUuid(string uuid)
        {
            return GetRowByUuid(uuid);
        }

        public override IMetaTableRow GetMetaTableRowById(int id)
        {
            return GetRowById(id);
        }

        public override string TableName => "Condition";
    }
}

