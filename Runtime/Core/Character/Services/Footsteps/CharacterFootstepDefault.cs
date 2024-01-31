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

        public FootstepAsset FootstepConfig
        {
            get
            {
                return Character.Main.GameConfig.GetFootstepAsset();
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
        public readonly RaycastHit[] m_HitsBuffer = new RaycastHit[5];

        public Renderer m_Renderer;

        public int m_HitNum;

        public RaycastHit m_Hit;


        private RaycastHit GetGroundHit(Vector3 position, float raydistance = 2.25f)
        {
            m_HitNum = Physics.RaycastNonAlloc(
                position, -Character.CachedTransfrom.up,
                this.m_HitsBuffer,
                raydistance,
                FootstepConfig.LayerMask,
                QueryTriggerInteraction.Ignore
            );

            RaycastHit hit = new RaycastHit();
            float minDistance = Mathf.Infinity;

            for (int i = 0; i < m_HitNum; ++i)
            {
                float distance = Vector3.Distance(
                    this.m_HitsBuffer[i].transform.position,
                    position
                );

                if (distance > minDistance) continue;

                hit = this.m_HitsBuffer[i];
                minDistance = distance;
            }

            return hit;
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
                m_Hit = GetGroundHit(Character.CachedTransfrom.position);
                if (m_Hit.collider != null)
                {
                    m_Renderer = m_Hit.collider.GetComponent<Renderer>();
                    if (m_Renderer != null)
                    {
                        foreach (Material material in m_Renderer.sharedMaterials)
                        {
                            Texture texture = material.mainTexture;
                            if (texture == null) continue;

                            foreach (var footstepEntry in FootstepConfig.footsteps)
                            {
                                if (playedFlag) break;
                                if (!footstepEntry.texture.name.Equals(texture.name)) continue;
                                if (footstepEntry.soundUuids == null || (footstepEntry.soundUuids != null && footstepEntry.soundUuids.Length == 0)) continue;


                                var footstepSoundList = footstepEntry.soundUuids;
                                FootstepsIndex = FootstepsIndex % footstepSoundList.Length;
                                var uuid = footstepSoundList[FootstepsIndex];
                                Debug.Log($"Play Footstep Sounc With Texture:{uuid}");
                                Character.Main.Sound.PlaySound(uuid, volume: footstepEntry.volume);
                                Interval = Random.Range(footstepEntry.IntervalRange.x, footstepEntry.IntervalRange.y);
                                MinInterval = footstepEntry.MinInterval;
                                playedFlag = true;
                            }
                        }
                    }
                }





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

