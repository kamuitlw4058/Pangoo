using System;
using UnityEngine;
using Sirenix.OdinInspector;
using GameFramework;

namespace Pangoo
{


    public interface IInfo
    {
        string Name { get; }

        void Init();

        // void Preload();

        // void Load();

        // void Unload();

        void Shutdown();
    }


}
