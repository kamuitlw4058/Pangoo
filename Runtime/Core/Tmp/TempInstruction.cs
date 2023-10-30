using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using GameFramework.Event;
using Pangoo.Core.Services;

namespace Pangoo
{

    public class TempInstruction : MonoBehaviour
    {
        EventHelper eventHelper = null;

        MainService mainService = null;

        CharacterService characterService = null;


        public void ChangeGameSection(int id)
        {
            if (eventHelper == null)
            {
                eventHelper = EventHelper.Create(this);
            }
            eventHelper.Fire(this, GameSectionChangeEventArgs.Create(id));
        }

        public void SetPlayerHeight(float height)
        {
            if (mainService == null)
            {
                mainService = PangooEntry.Service.mainService;
            }

            if (characterService == null)
            {
                characterService = mainService?.CharacterService;
            }

            characterService?.Player?.character?.SetCameraOffset(new Vector3(0, height, 0));

        }
    }
}
