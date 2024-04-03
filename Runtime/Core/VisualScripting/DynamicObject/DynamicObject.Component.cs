using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pangoo.Core.VisualScripting
{

    public partial class DynamicObject
    {
        private Dictionary<Tuple<string, int>, Component> Components = new Dictionary<Tuple<string, int>, Component>();

        public T GetComponent<T>(string path) where T : Component
        {
            return this.GetComponent<T>(path, this.Components, gameObject);
        }

        private TComponent GetComponent<TComponent>(string path,
            IDictionary<Tuple<string, int>, Component> dictionary, GameObject gameObject)
            where TComponent : Component
        {
            if (gameObject == null) return null;

            int hash = typeof(TComponent).GetHashCode();
            var ComponentKey = new Tuple<string, int>(path, hash);
            if (!dictionary.TryGetValue(ComponentKey, out Component value))
            {
                var trans = GetTransform(path);
                if (trans == null)
                {
                    return null;
                }
                value = trans.GetComponent<TComponent>();
                if (value == null)
                {
                    return null;
                }

                dictionary.Add(ComponentKey, value);
            }

            return value as TComponent;
        }
    }


}