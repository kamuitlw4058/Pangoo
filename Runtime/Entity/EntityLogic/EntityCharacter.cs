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
                Log.Error("Entity data is invalid.");
                return;
            }
            if (EntityData.IsPlayer)
            {
                tag = "Player";
            }

            Name = Utility.Text.Format("{0}[{1}]", EntityData.EntityInfo.AssetName, Id);

            character = new Character(gameObject, EntityData.CameraOnly);
            character.SetIsPlayer(EntityData.IsPlayer);
            character.Main = EntityData.Service.Parent as MainService;

            MotionInfo motionInfo = new MotionInfo();
            motionInfo.RotationSpeedX = 80;
            motionInfo.RotationSpeedY = 80;
            motionInfo.LinearSpeed = EntityData.InfoRow.m_CharacterRow.MoveSpeed;
            motionInfo.GravityDownwards = -9.8f;
            motionInfo.TerminalVelocity = -54f;
            motionInfo.InteractionRadius = 5;


            character.SetMotionInfo(motionInfo);
            character.CameraOffset = EntityData.InfoRow.m_CharacterRow.CameraOffset;
            character.xAxisMaxPitch = EntityData.InfoRow.m_CharacterRow.XMaxPitch;
            character.yAxisMaxPitch = EntityData.InfoRow.m_CharacterRow.YMaxPitch;
            character.IsControllable = true;

            character.Awake();
            character.Start();
            character.ResetCameraDirection();
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