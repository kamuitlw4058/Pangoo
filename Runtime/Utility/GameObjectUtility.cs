using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Pangoo
{

    public static class GameObjectUtility
    {


        public static List<GameObject> FindGameOjects(Type type, bool onlyUse = true)
        {
            List<GameObject> ret = new List<GameObject>();
            List<string> paths = new List<string>();
            var coms = Resources.FindObjectsOfTypeAll(type);
            if (coms == null)
            {
                return ret;
            }

            var gos = coms.Where(o => o is MonoBehaviour).Select(o => (o as MonoBehaviour).gameObject).ToList();
            foreach (var c in gos)
            {
                if (c.scene.name != null)
                {
                    if (onlyUse && c.gameObject.activeInHierarchy)
                    {
                        continue;
                    }

                    if (paths.Contains(c.transform.GetPath()))
                    {
                        continue;
                    }

                    ret.Add(c);
                    paths.Add(c.transform.GetPath());

                }
            }
            return ret;
        }

    }


}
