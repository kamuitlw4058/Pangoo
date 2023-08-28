using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using GameFramework.Event;

namespace Pangoo{

    public class GameObjectActiveSender : MonoBehaviour
    {
        public string ConditionString;
        public bool Active;

        private void OnEnable() {
            PangooEntry.Event.Fire( this.gameObject,GameObejctActiveEventArgs.Create(ConditionString,Active));
        }

    }
}
