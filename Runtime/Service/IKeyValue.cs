using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pangoo.Service
{
    public interface IKeyValue
    {
        public T Get<T>(string key);
        public void Set<T>(string key,T value);
    }
}

