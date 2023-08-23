using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using GameFramework.Resource;
using Pangoo;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityGameFramework.Runtime;

namespace Pangoo
{
    public class GameInfoComponent : GameFrameworkComponent
    {
        Dictionary<Type,IInfo> Infos = new Dictionary<Type, IInfo>();

        [TableList]
        public GameInfoItem[] GameInfoItems;


        public void Init()
        {
            for (int i = 0; i < GameInfoItems.Length; i++)
            {
                if(!GameInfoItems[i].Enable){
                    continue;
                }

                Type procedureType = TypeUtility.GetRuntimeType(GameInfoItems[i].GameInfoTypeName);
                if (procedureType == null)
                {
                    Log.Error("Can not find data type '{0}'.", GameInfoItems[i].GameInfoTypeName);
                    return;
                }

                var info = (IInfo)Activator.CreateInstance(procedureType);
                if (info == null)
                {
                    Log.Error("Can not create data instance '{0}'.", GameInfoItems[i].GameInfoTypeName);
                    return;
                }
                Infos.Add(procedureType, info);
            }

            foreach(var kv in Infos)
            {
                kv.Value.Init();
            }
        }

        private void Start()
        {
            


        }

        public T GetGameInfo<T>() where T :BaseInfo
        {
            if (Infos.TryGetValue(typeof(T), out var info))
            {
                return (T)info;
            }
            Log.Warning($"获取 GameInfo:{typeof(T).Name} 配置表失败！");
            return null;
        }



    }
}