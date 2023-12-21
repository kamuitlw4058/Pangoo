using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Pangoo.Core.Common;
using Pangoo.Core.Services;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Pangoo.Core.VisualScripting
{
    [Common.Title("Debug Text")]
    [Category("Character/设置玩家相机限制状态")]
    [Serializable]
    public class InstructionSetPlayerCameraClampState : Instruction
    {
        [ShowInInspector]
        public Args LastestArgs { get; set; }

        public InstructionPlayerClampParams ParamsRaw = new InstructionPlayerClampParams();
        public override IParams Params => this.ParamsRaw;

        // PROPERTIES: ----------------------------------------------------------------------------

        public override string Title => $"Character: {this.ParamsRaw.CharacterUuid}";

        // PRIVATE: -------------------------------------------------------------------------------

        private CharacterService m_CharacterService;

        // CONSTRUCTORS: --------------------------------------------------------------------------

        protected override IEnumerator Run(Args args)
        {
            RunImmediate(args);
            yield break;
        }

        public override void RunImmediate(Args args)
        {
            LastestArgs = args;

            if (args?.Main != null)
            {
                m_CharacterService = args.Main.CharacterService;
                var characterUuid = this.ParamsRaw.CharacterUuid;

                if (characterUuid.IsNullOrWhiteSpace())
                {
                    characterUuid = args.Main?.GameConfig?.GetGameMainConfig()?.DefaultPlayer;
                    if (characterUuid.IsNullOrWhiteSpace())
                    {
                        Debug.LogError("Get Player Id failed!");
                        return;
                    }
                }

                SetPlayerClamp(ParamsRaw.OpenCameraClamp);
                ResetCameraRotation();
                SetCameraNoise(ParamsRaw.OpenCameraNoise);
            }
        }

        public void SetPlayerClamp(bool val)
        {
            if (val)
            {
                m_CharacterService.Player.character.xAxisMaxPitch = ParamsRaw.RotationClamp.x;
                m_CharacterService.Player.character.yAxisMaxPitch = ParamsRaw.RotationClamp.y;
            }
            else
            {
                m_CharacterService.Player.character.xAxisMaxPitch = 90f;
                m_CharacterService.Player.character.yAxisMaxPitch = 360;
            }
        }

        public void ResetCameraRotation()
        {
            m_CharacterService?.Player?.character?.CharacterCamera?.SetDirection(new Vector3(0, 0, 0));
        }

        public void SetCameraNoise(bool isOpen)
        {
            NoiseSettings noiseSettings = Resources.Load<NoiseSettings>($"NoiseSettings/{ParamsRaw.NoiseSettings}");
            m_CharacterService.Player.character.CharacterCamera.SetCameraNoise(isOpen, noiseSettings, ParamsRaw.AmplitudeGain, ParamsRaw.FrequencyGain);
        }
    }
}
