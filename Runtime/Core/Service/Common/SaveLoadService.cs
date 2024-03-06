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
            Load();
        }



        public List<Tuple<DateTime, FileInfo>> GetSaveFiles()
        {
            if (!Directory.Exists(SaveDir))
            {
                Directory.CreateDirectory(SaveDir);
            }

            var fileInfos = DirectoryUtility.GetFileInfos(SaveDir, new List<string>() { ".sav" });
            List<Tuple<DateTime, FileInfo>> listTuple = new List<Tuple<DateTime, FileInfo>>();
            foreach (var fileInfo in fileInfos)
            {
                var timeString = fileInfo.Name.Substring(0, 16);
                var dateTime = DateTimeUtility.ParseDateTimeNow(timeString);
                // Log($"fileInfo:{fileInfo}, fileInfo:{fileInfo.Name} dateTime:{dateTime}");
                listTuple.Add(new Tuple<DateTime, FileInfo>(dateTime, fileInfo));
            }
            if (listTuple.Count == 0)
            {
                return listTuple;
            }

            listTuple.Sort((a, b) => a.Item1.CompareTo(b.Item1) * -1);
            if (listTuple.Count > 10)
            {
                for (int i = 10; i < listTuple.Count; i++)
                {
                    var tuple = listTuple[i];
                    listTuple.Remove(tuple);
                    File.Delete(tuple.Item2.FullName);
                }
            }

            return listTuple;
        }


        [Button]
        public void Load()
        {
            var listTuple = GetSaveFiles();
            if (listTuple.Count > 0)
            {
                Load(listTuple[0]);
            }
        }

        public void Load(Tuple<DateTime, FileInfo> tuple)
        {
            JsonStr = File.ReadAllText(tuple.Item2.FullName);
            RuntimeDataSrv.Deserialize(JsonStr);
        }

        string SaveDir
        {
            get
            {
                return Application.persistentDataPath + "/Save";
            }
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