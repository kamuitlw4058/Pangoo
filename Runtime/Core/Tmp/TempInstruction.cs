using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using GameFramework.Event;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Logical;
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

        CharacterService GetCharacterService()
        {
            if (mainService == null)
            {
                mainService = PangooEntry.Service.mainService;
            }
            
            return mainService?.CharacterService;
        }
        
        public Vector3 rotation;
        public void SetCameraRotation()
        {
            characterService=GetCharacterService();
            Debug.Log($":{rotation} :{characterService}  :{characterService?.Player} :{characterService?.Player?.character} :{characterService?.Player?.character?.CharacterCamera}");
            characterService?.Player?.character?.CharacterCamera?.SetDirection(rotation);
        }


        public void ResetCameraDirection()
        {
            characterService=GetCharacterService();
            characterService?.Player?.character?.ResetCameraDirection();
        }

        public void SetPlayerInput(bool val)
        {
            characterService=GetCharacterService();

            var player = characterService?.Player?.character;
            if (player != null) player.IsControllable = val;
        }
    }
}
