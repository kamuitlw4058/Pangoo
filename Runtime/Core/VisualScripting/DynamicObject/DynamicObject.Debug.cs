using System;
using UnityEngine;
using Pangoo.Core.Common;
using Pangoo.Core.Services;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Pangoo.Core.Characters;
using GameFramework;
using UnityEngine.Rendering;
using System.ComponentModel;


namespace Pangoo.Core.VisualScripting
{

    public partial class DynamicObject
    {

        public override string ServiceName => "DynamicObject";

        public override void Log(string message)
        {
            base.Log($"{Row.Name}[{Row.UuidShort}]:{message}");
        }

        public override void LogError(string message)
        {
            base.LogError($"{Row.Name}[{Row.UuidShort}]:{message}");
        }


    }


}