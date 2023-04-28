using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pangoo
{

    public static class GameObjectExtension
    {

        public static T GetOrAddComponent<T>(this GameObject origin) where T : Component
        {
            T component = origin.GetComponent<T>();
            if (component == null)
            {
                component = origin.AddComponent<T>();
            }
            return component;
        }



    }
}
