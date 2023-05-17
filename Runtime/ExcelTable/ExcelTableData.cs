using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using Excel;
using UnityEngine;


namespace Pangoo
{
    public class ExcelTableData
    {

        public class ExcelTableColInfo
        {
            public string Name;
            public string Type;

            public string CnName;

            public string Desc;
        }

        public DataTable DataTable;

        public string ClassBaseName;
        public string ClassName => $"{ClassBaseName}Table";

        public int HeadCount;

        public int Rows;

        public int Cols;

        public List<Tuple<string, string, string, string>> ColsList;

        public List<string> TypeList;
        public List<string> NameList;
        public List<string> CnNameLst;

        Dictionary<string, ExcelTableColInfo> ColInfnDict;

        public ExcelTableColInfo GetColInfo(string key)
        {
            ExcelTableColInfo ret;
            if (ColInfnDict.TryGetValue(key, out ret))
            {
                return ret;
            }
            return null;
        }


        public static ExcelTableData Parser(string excelFile, string className, int headCount = 3)
        {
            FileStream mStream;

            mStream = File.Open(excelFile, FileMode.Open, FileAccess.Read);
            IExcelDataReader mExcelReader = ExcelReaderFactory.CreateOpenXmlReader(mStream);
            DataSet mResultSet = mExcelReader.AsDataSet();

            //Debug.Log("json 写入地址："+JsonPath);
            //判断Excel文件中是否存在数据表
            if (mResultSet.Tables.Count < 1)
            {
                return null;
            }


            //默认读取第一个数据表
            DataTable mSheet = mResultSet.Tables[0];

            //判断数据表内是否存在数据
            if (mSheet.Rows.Count < 1)
            {
                return null;
            }


            //读取数据表行数和列数
            int rowCount = mSheet.Rows.Count;       //行
            int colCount = mSheet.Columns.Count;    //列

            List<string> typesLst = new List<string>();
            List<string> namesLst = new List<string>();
            List<string> cnNameLst = new List<string>();
            List<string> descList = new List<string>();
            Dictionary<string, ExcelTableColInfo> colInfoDict = new Dictionary<string, ExcelTableColInfo>();

            for (int i = 0; i < colCount; i++)
            {
                var name = mSheet.Rows[0][i].ToString();
                var type = mSheet.Rows[1][i].ToString();
                if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(type))
                {
                    throw new Exception($"ExcelTableData 文件:{excelFile} ColsIndex:{i} 名字或者类型为空:{name},{type}");
                }
                namesLst.Add(name);
                typesLst.Add(type);
                cnNameLst.Add(mSheet.Rows[2][i].ToString());
                descList.Add(string.Empty);
                if (colInfoDict.ContainsKey(name))
                {
                    throw new Exception($"ExcelTableData 文件:{excelFile} Cols:{colCount} 有重名列:{name}");
                }
                colInfoDict.Add(name, new ExcelTableColInfo()
                {
                    Name = mSheet.Rows[0][i].ToString(),
                    Type = mSheet.Rows[1][i].ToString(),
                    CnName = mSheet.Rows[2][i].ToString(),
                    Desc = string.Empty,
                });
            }

            return new ExcelTableData()
            {
                DataTable = mSheet,
                Rows = rowCount,
                Cols = colCount,
                TypeList = typesLst,
                NameList = namesLst,
                CnNameLst = cnNameLst,
                ColInfnDict = colInfoDict,
                HeadCount = headCount,
                ClassBaseName = className,
            };
        }

    }
}