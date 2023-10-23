using System;
using System.Collections.Generic;
using UnityEngine;
using Pangoo;
using LitJson;

namespace Pangoo.Core.Common
{
    public interface IParams
    {
        void Load(string val);
        string Save();
    }
}