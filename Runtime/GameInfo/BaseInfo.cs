using System;
using UnityEngine;
using Sirenix.OdinInspector;
using GameFramework;
using System.Collections.Generic;

namespace Pangoo
{

    public abstract class BaseInfo : IInfo
    {
        protected Dictionary<int, object> IdDict = new Dictionary<int, object>();

        public virtual string Name
        {
            get
            {
                return this.GetType().ToString();
            }
        }

        public void Init()
        {
            OnInit();
        }

        // public abstract void Preload();

        // public abstract void Load();

        // public abstract void Unload();

        public void Shutdown()
        {
            OnShutdown();
        }

        protected virtual void OnInit()
        {
        }

        protected virtual void OnShutdown()
        {

        }


        public T GetRowById<T>(int id) where T : BaseInfoRow
        {
            object ret;
            if (IdDict.TryGetValue(id, out ret))
            {
                return (T)ret;
            }

            return null;
        }


    }

}