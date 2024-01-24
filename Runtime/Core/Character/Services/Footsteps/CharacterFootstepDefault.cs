using System;
using UnityEngine;
using Pangoo.Core.Services;
using Sirenix.OdinInspector;


namespace Pangoo.Core.Characters
{

    public class CharacterFootstepDefault : CharacterBaseService
    {
        public override int Priority => 1;

        float Interval = 1f;

        float MinInterval = 0.1f;

        float LastPlayTime;

        bool MoveInputUp;

        int FootstepsIndex;

        bool m_Enabled;

        public bool Enabled
        {
            get
            {
                return m_Enabled;
            }
            set
            {
                m_Enabled = value;
            }
        }

        public StaticSceneService StaticScene
        {
            get
            {
                return Character.Main.StaticScene;
            }
        }

        public SoundService Sound
        {
            get
            {
                return Character.Main.Sound;
            }

        }

        public GameMainConfig GameMainConfig
        {
            get
            {
                return Character.Main.GameConfig.GetGameMainConfig();
            }
        }


        public CharacterFootstepDefault(NestedBaseService parent) : base(parent)
        {

        }
        protected override void DoStart()
        {
        }


        protected override void DoAwake()
        {

        }

        protected override void DoUpdate()
        {

            if (!Character.IsMoveInputDown)
            {
                MoveInputUp = true;
                return;
            }

            bool playSoundFlag = false;
            var applyInterval = Interval;
            if (MoveInputUp)
            {
                applyInterval = MinInterval;

            }

            if (LastPlayTime + applyInterval < Time)
            {
                playSoundFlag = true;
                LastPlayTime = Time;
                MoveInputUp = false;
            }

            if (playSoundFlag)
            {
                bool playedFlag = false;
                var enterScene = StaticScene.GetLastestEnterScene();
                if (!playedFlag && enterScene != null)
                {
                    playedFlag = true;
                }


                if (GameMainConfig.UseDefaultFootstepSound && GameMainConfig.DefaultFootstepSoundEffectUuids.Length > 0)
                {
                    FootstepsIndex = FootstepsIndex % GameMainConfig.DefaultFootstepSoundEffectUuids.Length;
                    var uuid = GameMainConfig.DefaultFootstepSoundEffectUuids[FootstepsIndex];
                    Log($"Play Config Sound:{uuid}");
                    Character.Main.Sound.PlaySound(uuid, volume: GameMainConfig.DefaultFootstepSoundVolume);
                }

                FootstepsIndex += 1;

            }



        }





    }

}

