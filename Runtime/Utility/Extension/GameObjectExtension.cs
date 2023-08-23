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

        public static void ResetTransfrom(this GameObject origin,bool apply_scale= true){
            origin.transform.localPosition = Vector3.zero;
            origin.transform.localRotation = Quaternion.Euler(Vector3.zero);
            if(apply_scale){
                origin.transform.localScale = Vector3.one;
            }
            

        }


        public static string GetPath(this GameObject go){

           return go.transform.GetPath();
        }
    }
}
