using System.IO;
using System.Collections.Generic;
using Pangoo.Common;
using Pangoo.MetaTable;
using Sirenix.OdinInspector;
using UnityEngine;
using System;



namespace Pangoo.Core.Services
{
    public class SaveLoadService : MainSubService
    {
        public override int Priority => 0;

        public string JsonStr;

        protected override void DoStart()
        {
            base.DoStart();

        }

        public void Load(string data)
        {
            if (data.IsNullOrWhiteSpace() && !JsonStr.IsNullOrWhiteSpace())
            {
                data = JsonStr;
            }
            else
            {
                JsonStr = data;
            }
            RuntimeDataSrv.Deserialize(data);
        }

        string SaveDir
        {
            get
            {
                return Application.persistentDataPath + "/Save";
            }
        }



        [Button]
        public void Load()
        {
            var fileInfos = DirectoryUtility.GetFileInfos(SaveDir, new List<string>() { ".sav" });
            List<Tuple<DateTime, FileInfo>> listTuple = new List<Tuple<DateTime, FileInfo>>();
            foreach (var fileInfo in fileInfos)
            {
                var timeString = fileInfo.Name.Substring(0, 16);
                var dateTime = DateTimeUtility.ParseDateTimeNow(timeString);
                Log($"fileInfo:{fileInfo}, fileInfo:{fileInfo.Name} dateTime:{dateTime}");
                listTuple.Add(new Tuple<DateTime, FileInfo>(dateTime, fileInfo));
            }
            if (listTuple.Count == 0)
            {
                return;
            }

            listTuple.Sort((a, b) => a.Item1.CompareTo(b.Item1) * -1);
            foreach (var tuple in listTuple)
            {
                Log($" dateTime:{tuple.Item1}");
            }

            Log($"First:{listTuple[0].Item1}");

            JsonStr = File.ReadAllText(listTuple[0].Item2.FullName);

            RuntimeDataSrv.Deserialize(JsonStr);





        }

        [Button]
        public void Save()
        {
            JsonStr = RuntimeDataSrv.SerializeToString();
            Log($"json:{JsonStr}");
            Log($"SaveDir:{SaveDir}");
            var timeString = DateTimeUtility.FormatDateTimeNow();
            DirectoryUtility.ExistsOrCreateSystem(SaveDir);
            var savePath = $"{SaveDir}/{timeString}.sav";
            File.WriteAllText(savePath, JsonStr);
            Log($"savePath:{savePath}");
        }

    }
}