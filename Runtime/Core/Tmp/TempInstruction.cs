using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.Events;
using GameFramework.Event;
using Pangoo.Core.Services;
using Sirenix.OdinInspector;

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

            characterService?.SetPlayerHeight(height);

        }

        CharacterService GetCharacterService()
        {
            if (mainService == null)
            {
                mainService = PangooEntry.Service.mainService;
            }

            return mainService?.CharacterService;
        }
        [Title("设置相机旋转角度")]
        public Vector3 rotation;
        public void SetCameraRotation()
        {
            characterService = GetCharacterService();
            characterService?.Player?.character?.CharacterCamera?.SetDirection(rotation);
        }


        public void ResetCameraDirection()
        {
            characterService = GetCharacterService();
            characterService?.Player?.character?.ResetCameraDirection();
        }

        [Title("限制相机旋转角度")]
        public Vector2 rotationClamp;
        public void SetPlayerClamp(bool val)
        {
            characterService = GetCharacterService();

            if (val)
            {
                characterService.Player.character.xAxisMaxPitch = rotationClamp.x;
                characterService.Player.character.yAxisMaxPitch = rotationClamp.y;
            }
            else
            {
                characterService.Player.character.xAxisMaxPitch = 90f;
                characterService.Player.character.yAxisMaxPitch = 360;
            }
        }

        [Title("选择相机抖动的方式")]
        public NoiseSettings noiseSettings;

        public float amplitudeGain = 1;
        public float frequencyGain = 1;
        public void SetCameraNoise(bool isOpen)
        {
            characterService = GetCharacterService();
            characterService.Player.character.CharacterCamera.SetCameraNoise(isOpen, noiseSettings, amplitudeGain, frequencyGain);
        }

        public void SetPlayerInput(bool val)
        {
            characterService = GetCharacterService();

            var player = characterService?.Player?.character;
            if (player != null) player.IsControllable = val;
        }
    }
}
