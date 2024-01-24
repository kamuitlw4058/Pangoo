using UnityEngine;
using Pangoo.Core.Services;
using Sirenix.OdinInspector;


namespace Pangoo.Core.Characters
{

    public class CharacterFootstepDefault : CharacterBaseService
    {
        public override int Priority => 1;

        float Interval = 1f;

        float MinInterval = 0.3f;

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

            if (!Enabled)
            {
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
                if (!playedFlag && enterScene != null && enterScene.UseSceneFootstep && enterScene.SceneFootstepUuids.Length > 0)
                {
                    var footstepSoundList = enterScene.SceneFootstepUuids;
                    FootstepsIndex = FootstepsIndex % footstepSoundList.Length;
                    var uuid = footstepSoundList[FootstepsIndex];
                    Debug.Log($"Play Footstep Sounc With Scene:{uuid}");
                    Character.Main.Sound.PlaySound(uuid, volume: enterScene.SceneFootstepVolume);
                    Interval = Random.Range(enterScene.IntervalMin, enterScene.IntervalMax);
                    MinInterval = enterScene.MinInterval;
                    playedFlag = true;
                }


                if (!playedFlag && GameMainConfig.UseDefaultFootstepSound && GameMainConfig.DefaultFootstepSoundEffectUuids.Length > 0)
                {
                    FootstepsIndex = FootstepsIndex % GameMainConfig.DefaultFootstepSoundEffectUuids.Length;
                    var uuid = GameMainConfig.DefaultFootstepSoundEffectUuids[FootstepsIndex];
                    Debug.Log($"Play Footstep Sounc With Config:{uuid}");
                    Character.Main.Sound.PlaySound(uuid, volume: GameMainConfig.DefaultFootstepSoundVolume);
                    Interval = Random.Range(GameMainConfig.FootstepSoundInterval.x, GameMainConfig.FootstepSoundInterval.y);
                    MinInterval = GameMainConfig.FootstepSoundMinInterval;
                }

                FootstepsIndex += 1;

            }



        }





    }

}

