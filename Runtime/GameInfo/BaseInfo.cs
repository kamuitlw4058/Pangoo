using System;
using UnityEngine;
using Sirenix.OdinInspector;
using GameFramework;

namespace Pangoo
{

    public abstract class BaseInfo : IInfo
    {
        public virtual string Name
        {
            get
            {
                return this.GetType().ToString();
            }
        }

        public  void Init(){
            OnInit();
        }

        // public abstract void Preload();

        // public abstract void Load();

        // public abstract void Unload();

        public void Shutdown(){
            OnShutdown();
        }

        protected virtual void OnInit()
        {
        }

        protected virtual void OnShutdown(){

        }

    }

}