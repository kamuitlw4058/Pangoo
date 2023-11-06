using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using GameFramework.Event;
using Pangoo.Core.Services;
using UnityEngine.Playables;
using Sirenix.OdinInspector;

namespace Pangoo
{

    public class TempPlayerDirector : MonoBehaviour
    {

        public PlayableDirector playableDirector;

        private void Start()
        {
            playableDirector = GetComponent<PlayableDirector>();

        }

        [ShowInInspector]
        public PlayState? playState
        {
            get
            {
                return playableDirector?.state;
            }
        }

        [ShowInInspector]
        public bool IsFinished
        {
            get
            {
                if (playableDirector?.playableAsset == null)
                {
                    return false;
                }


                return playableDirector.time == playableDirector.playableAsset.duration;
            }
        }

    }
}
