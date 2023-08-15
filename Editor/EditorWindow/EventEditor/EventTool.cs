
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Pangoo;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;


namespace Pangoo.Editor
{
    public class EventBuildTool
    {

        [ShowInInspector]
        [TableList(AlwaysExpanded = true)]
        List<EventEntry> EventList = new List<EventEntry>();

        const string ModuleName = "Event";


        List<string> Headers = new List<string>()
        {
            "System",
            "System.IO",
            "System.Collections.Generic",
            "LitJson",
            "Pangoo",
            "UnityEngine",
            "Sirenix.OdinInspector",
            "GameFramework",
            "GameFramework.Event",
        };



        public EventBuildTool()
        {
            Refresh();
        }

        [Button("刷新Excel列表")]
        void Refresh()
        {
            var configs = AssetDatabaseUtility.FindAsset<PackageConfig>().ToList();
            if (configs == null || configs.Count == 0)
            {
                throw new System.Exception("Load Config Failed!");
            }

            EventList.Clear();
            Debug.Log($"configs:{configs.Count}");
            foreach (var config in configs)
            {
                var scriptDir = Path.Join(config.PackageDir, config.ScriptsMainDir, ModuleName).Replace("\\", "/");
                var scriptGenerateDir = Path.Join(scriptDir, "Generate").Replace("\\", "/");
                
                DirectoryUtility.ExistsOrCreate(scriptDir);
                DirectoryUtility.ExistsOrCreate(scriptGenerateDir);

            //     foreach (PangooEventsTableOverview overview in config.EventOverviews)
            //     {
            //         if (overview == null)
            //         {
            //             continue;
            //         }
            //         foreach(var data in overview.Data.Rows){
            //                 var className = $"{data.EventName}EventArgs";
            //                 EventList.Add(new EventEntry
            //             {
            //                 NameSpace = config.MainNamespace,
            //                 EventName = data.EventName,
            //                 EventClassName = className,
            //                 Overview = overview,
            //                 ScriptPath = $"{scriptGenerateDir}/{className}.cs",

                            
            //             });
            //         }

            //     }
            }
        }
        
        [Button("生成代码")]
        void Build(){
            foreach(var PangooEvent in EventList){
                if(!File.Exists(PangooEvent.ScriptPath) || PangooEvent.Overwrite){
                    JsonClassGenerator.GeneratorCodeString("{}",PangooEvent.NameSpace,new CSharpEventCodeWriter(Headers),PangooEvent.EventClassName,PangooEvent.ScriptPath);
                }
                
            }
            AssetDatabase.Refresh();
        }


        public class EventEntry
        {
            [ShowInInspector]
            public string NameSpace;
            [HideInInspector]
            public string EventName;
            public string EventClassName;

            [HideInInspector]
            public string ScriptPath;

            [TableTitleGroup("强制重写")]
            [HideLabel]
            public bool Overwrite;


            // [HideInInspector]
            // public PangooEventsTableOverview Overview;


        }

    }
}