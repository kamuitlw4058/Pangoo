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
   public abstract class ExcelTableBase
    {
        public  virtual List<ExcelRowBase> BaseRows {
            get{
                return null;
            }
        }


        public Type  GetRowType(){
            Type ret  = null;
            var nestedTypes = this.GetType().GetNestedTypes();
            foreach(var nested in nestedTypes){
                if(nested.BaseType == typeof(ExcelRowBase)){
                    ret = nested;
                    JsonMapper.AddObjectMetadata(nested);
                    break;
                }
            }
            return ret;
        }


        public ExcelTableColAttribute[] GetColAttributes(){
            var rowType =  GetRowType();
            var objectMetadata = JsonMapper.GetObjectMetadata(rowType);
            List<ExcelTableColAttribute> rowAttributes = new List<ExcelTableColAttribute>();

            foreach (var propPair in objectMetadata.Properties)
            {
                var prop_data = propPair.Value;
                var excelTableRow = prop_data.Info.GetCustomAttribute<ExcelTableColAttribute>();
                if (excelTableRow != null )
                {
                    rowAttributes.Add(excelTableRow);
                }
            }

             rowAttributes.Sort((attr1,attr2) => attr1.Index.CompareTo(attr2.Index));
             return rowAttributes.ToArray();
        }

        
        public virtual string[] GetHeadNames()
        {
            return GetColAttributes().Select( o=> o.Head).ToArray();
        }

        public virtual string[] GetNameCn()
        {
            return GetColAttributes().Select( o=> o.NameCn).ToArray();
        }

        public virtual string[] GetTypeNames()
        {
            return GetColAttributes().Select( o=> o.ColType).ToArray();
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
  
        public virtual List<string[]> GetTableHeadList()
        {
            List<string[]> tableHeadList = new List<string[]>();
            tableHeadList.Add(GetHeadNames());
            tableHeadList.Add(GetTypeNames());
            tableHeadList.Add(GetNameCn());
            return tableHeadList;
        }
        public virtual List<string[]> GetTableRowDataList()
        {
            List<string[]> ret = new List<string[]>();
            var cols = GetColAttributes();

            foreach (var item in BaseRows)
            {
                string[] rowStrs = GetRowStrings(item,cols);
                ret.Add(rowStrs);
            }
            return ret;
        }

        public string[] GetRowStrings(object item,ExcelTableColAttribute[] cols)
        {
            string[] texts = new string[cols.Length];
            for (int i = 0; i < texts.Length; i++)
            {
                object valueText = item.GetType().GetField(cols[i].Name).GetValue(item);
                texts[i] = valueText != null ?valueText.ToString(): string.Empty;
            }
            return texts;
        }

#if UNITY_EDITOR
        public virtual List<T> LoadExcelFile<T>(string excelFilePath)where T:ExcelRowBase,new()
        {
            List<T> ret = new List<T>();
            var fileInfo = new FileInfo(excelFilePath);
            Debug.Log($"excelFilePath:{excelFilePath}");
            ExcelPackage excelPackage = new ExcelPackage(fileInfo);
            ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets[1];

            Type rowType = typeof(T);
            JsonMapper.AddObjectMetadata(rowType);
            ObjectMetadata  metadata = JsonMapper.GetObjectMetadata(rowType);
            var propertyDict = GetPropertyDict(metadata);

            Dictionary<int,string> headDict = new Dictionary<int, string>();
            for (int j = 0; j < worksheet.Dimension.Columns; j++)
            {
                var cellValue = worksheet.Cells[0 + 1, j + 1].Value;
                headDict.Add(j,cellValue.ToString());
            }

            var hasId = headDict.ContainsValue("Id");

            for (int row = 3; row < worksheet.Dimension.Rows; row++)
            {   
                var  eventsRow = new T();
                for (int col = 0; col < worksheet.Dimension.Columns; col++)
                {
                    string head;
                    PropertyMetadata propertyMetadata;
                     if(headDict.TryGetValue(col,out head)  && propertyDict.TryGetValue(head,out propertyMetadata)){
                        var excelTableRowAttribute = propertyMetadata.Info.GetCustomAttribute<ExcelTableColAttribute>();
                        if (excelTableRowAttribute != null && excelTableRowAttribute.Head == head)
                        {
                                var cellValue = worksheet.Cells[row + 1, col + 1].Value;
                              
                                if (propertyMetadata.IsField)
                                {
                                    var fieldInfo = propertyMetadata.Info as FieldInfo;
                                      var value = StringConvert.ToValue(fieldInfo.FieldType, cellValue!=null ? cellValue.ToString() : string.Empty);
                                    ((FieldInfo)propertyMetadata.Info).SetValue(
                                        eventsRow,value);
                                }
                                // 属性的设置暂时不考虑
                                // else
                                // {
                                //     PropertyInfo p_info = (PropertyInfo)propertyMetadata.Info;

                                //     if (p_info.CanWrite)
                                //         p_info.SetValue(eventsRow, cellValue, null);
                                // }
                            //    Debug.Log($"Set  cellValue:{cellValue} propertyMetadata.Info:{propertyMetadata.Info.GetType()} excelTableRowAttribute:{excelTableRowAttribute.Name}");
                        }

                    
                    }
                }
                if(!hasId){
                    eventsRow.Id = row -2;
                }
                ret.Add(eventsRow);
            }

            return ret;

        }

        public Dictionary<string,PropertyMetadata> GetPropertyDict(ObjectMetadata  metadata){
            Dictionary<string,PropertyMetadata> dic = new Dictionary<string, PropertyMetadata>();
            foreach (var propPair in metadata.Properties)
            {
                var prop_data = propPair.Value;
                var excelTableRowAttribute = prop_data.Info.GetCustomAttribute<ExcelTableColAttribute>();
                if (excelTableRowAttribute != null)
                {
                    dic.Add(excelTableRowAttribute.Head,prop_data);
                }
            }

            return dic;
        }


        public  void BuildExcelFile(string path,string sheetName = "Sheet1")
        {
            var rowsList = GetTableHeadList();
            rowsList.AddRange(GetTableRowDataList());
            WriteTextToExcel(path,sheetName,rowsList);
            AssetDatabase.ImportAsset(path);
        }

        private void WriteTextToExcel(string filePath ,string sheetName,List<string[]> RowsList)
        {
            var fileInfo = new FileInfo(filePath);
            using (ExcelPackage excelPackage=new ExcelPackage(fileInfo))
            {
                //添加一张表格.如果失败认为已经有了去获取一张。
                ExcelWorksheet worksheet;
                try
                {
                    worksheet = excelPackage.Workbook.Worksheets.Add(sheetName);
                }
                catch (Exception e)
                {
                    worksheet = excelPackage.Workbook.Worksheets[sheetName];
                }

                
                for (int row = 0; row < RowsList.Count; row++)
                {
                    for (int col = 0; col < RowsList[row].Length; col++)
                    {
                            worksheet.Cells[row+1, col+1].Value = RowsList[row][col];
                    }
                }
                excelPackage.Save();//写入后保存表格
            }
        }
    }
#endif

}
