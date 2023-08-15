using System;
using System.Collections;
using System.Collections.Generic;
using Pangoo;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityGameFramework.Runtime;

public class RuntimeDataComponent : GameFrameworkComponent
{
    [InlineEditor()]
    public List<ExcelTableOverview> tableOverviews=new List<ExcelTableOverview>();
    [InlineEditor()]
    public RuntimeDataTableOverview runtimeDataSO;

    private void Start()
    {
        Merage();
    }
    [Button("合并数据")]
    public void Merage()
    {
        runtimeDataSO.Data.Rows.Clear();
        tableOverviews.ForEach(x =>
        {
            if (x.name.Contains("Item"))
            {
                ItemsConfigTableOverview itemsConfig = (ItemsConfigTableOverview)x;
                    
                for (int i = 0; i < itemsConfig.Data.Rows.Count; i++)
                {
                    runtimeDataSO.Data.Rows.Add(new RuntimeDataTable.RuntimeDataRow()
                    {
                        Id = itemsConfig.Data.Rows[i].ID,
                        Key = $"Item_{itemsConfig.Data.Rows[i].Name}_CanPickup",
                        Type =$"{itemsConfig.Data.Rows[i].CanPickup.GetType()}",
                        Value = $"{itemsConfig.Data.Rows[i].CanPickup}",
                    });
                }
                    
            }
        });
    }
}