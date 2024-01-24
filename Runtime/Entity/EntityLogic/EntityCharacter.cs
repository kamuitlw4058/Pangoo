using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityGameFramework.Runtime;
using Sirenix.OdinInspector;
using GameFramework;
using Pangoo.Core.Characters;
using Pangoo.Core.Services;



namespace Pangoo
{
    public class EntityCharacter : EntityBase
    {
        [ShowInInspector]
        public EntityInfo Info
        {
            get
            {
                if (EntityData != null)
                {
                    return EntityData.EntityInfo;
                }
                return null;
            }
        }


        [ShowInInspector]
        public EntityCharacterData EntityData;

        [SerializeField]
        bool IsOpenProbe = true;

        [HideInEditorMode]
        [ShowInInspector]
        public Character character;

        protected override void OnInit(object userData)
        {
            base.OnInit(userData);

        }

        protected override void OnShow(object userData)
        {
            base.OnShow(userData);

            EntityData = userData as EntityCharacterData;
            if (EntityData == null)
            {
                LogError("Entity data is invalid.");
                return;
            }
            if (EntityData.IsPlayer)
            {
                tag = "Player";
            }

            Name = Utility.Text.Format("{0}[{1}]", EntityData.EntityInfo.AssetName, Id);
            var MainService = EntityData.Service.Parent as MainService;
            character = new Character(gameObject, EntityData.CameraOnly);
            character.SetIsPlayer(EntityData.IsPlayer);
            character.Main = MainService;
            character.IsInteractive = EntityData.IsInteractive;

            MotionInfo motionInfo = new MotionInfo();
            motionInfo.RotationSpeedX = 80;
            motionInfo.RotationSpeedY = 80;
            motionInfo.LinearSpeed = EntityData.InfoRow.m_CharacterRow.MoveSpeed;
            motionInfo.GravityDownwards = -9.8f;
            motionInfo.TerminalVelocity = -54f;
            motionInfo.InteractionRadius = 5;


            character.SetMotionInfo(motionInfo);
            character.CameraOffset = EntityData.InfoRow.m_CharacterRow.CameraOffset;
            if (EntityData.Height >= 0)
            {
                character.CameraOffset = new Vector3(0, EntityData.Height, 0);
            }
            character.xAxisMaxPitch = EntityData.InfoRow.m_CharacterRow.XMaxPitch;
            character.yAxisMaxPitch = EntityData.InfoRow.m_CharacterRow.YMaxPitch;
            character.IsControllable = true;

            character.Awake();
            character.Start();
            character.ResetCameraDirection();

            DriverInfo driverInfo = new DriverInfo
            {
                SlopeLimit = EntityData.InfoRow.m_CharacterRow.SlopeLimit,
                StepOffset = EntityData.InfoRow.m_CharacterRow.StepOffset,
                SkinWidth = EntityData.InfoRow.m_CharacterRow.SkinWidth,
                MinMoveDistance = EntityData.InfoRow.m_CharacterRow.MinMoveDistance,
                Center = EntityData.InfoRow.m_CharacterRow.Center,
                Radius = EntityData.InfoRow.m_CharacterRow.Radius,
                Height = EntityData.InfoRow.m_CharacterRow.Height,
            };
            character.SetDriverInfo(driverInfo);
            character.EnabledFootstep = MainService.GameConfig.GetGameMainConfig().DefaultEnabledFootstepSound;
        }

        protected override void OnUpdate(float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(elapseSeconds, realElapseSeconds);
            character?.Update();
        }


        private void OnDestroy()
        {
            character?.Destroy();

        }

        void OnDrawGizmos()
        {
            character?.DrawGizmos();
        }

        private void OnEnable()
        {
            Debug.Log($"On Enable");
            character?.Enable();
        }

        private void OnDisable()
        {
            character?.Disable();
        }
    }
}