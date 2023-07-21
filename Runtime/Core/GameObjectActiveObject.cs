using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using GameFramework.Event;

namespace Pangoo{

    public class GameObjectActiveObject : MonoBehaviour
    {
        public string ConditionString;

        private void OnEnable() {
            PangooEntry.Event.Fire( this,GameObejctActiveEventArgs.Create(ConditionString,true));
        }

        private void OnDisable()
        {
            PangooEntry.Event.Fire(this, GameObejctActiveEventArgs.Create(ConditionString, false));
        }
    }
}
