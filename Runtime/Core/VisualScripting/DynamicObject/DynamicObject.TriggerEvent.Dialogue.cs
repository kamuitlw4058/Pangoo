using System;
using System.Collections.Generic;

using UnityEngine;
using Pangoo.Core.Common;
using Pangoo.Core.Characters;
using Sirenix.OdinInspector;
using UnityEngine.Playables;
using GameFramework.Event;
using LitJson;


namespace Pangoo.Core.VisualScripting
{

    public partial class DynamicObject
    {

        public void DialogueSignal(string assetName, float signalTme)
        {
            CurrentArgs.signalAssetName = assetName;
            CurrentArgs.SignalTime = signalTme;
            TriggerInovke(TriggerTypeEnum.OnDialogueSignal);
        }



    }
}