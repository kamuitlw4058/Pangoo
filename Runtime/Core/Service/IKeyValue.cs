using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pangoo.Core.Services
{
    public interface IKeyValue
    {
        public bool TryGet<T>(string key, out T outValue);
        public void Set<T>(string key, T value);
    }
}

