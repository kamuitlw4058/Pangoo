using System;
using System.Collections.Generic;
using System.Reflection;
using GameFramework.Resource;
using Pangoo;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityGameFramework.Runtime;

namespace Pangoo
{
    public class PlayerComponent : GameFrameworkComponent
    {
        public GameObject PlayerPrefab;

        private void Start() {
            if(PlayerPrefab != null){
                Instantiate(PlayerPrefab);
            }
        }
    }
}