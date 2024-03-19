using UnityEngine;
using Pangoo.Core.Services;
using Sirenix.OdinInspector;
using Pangoo.Common;

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
            if (MoveInputUp || Character.MoveStepChanged)
            {
                applyInterval = MinInterval;

            }

            if (LastPlayTime + applyInterval < Time)
            {
                playSoundFlag = true;
                LastPlayTime = Time;
                MoveInputUp = false;
            }

            if (!playSoundFlag)
            {
                return;
            }

            bool playedFlag = false;


            if (!FootstepConfig.configFootstepsUuid.IsNullOrWhiteSpace() && FootstepConfig.footsteps.Length > 0)
            {
                var val = Character.GetVariable<int>(FootstepConfig.configFootstepsUuid);
                if (val > 0 && val <= FootstepConfig.footsteps.Length)
                {
                    var footstepEntry = FootstepConfig.footsteps[val - 1];
                    var footstepFootList = footstepEntry.FootList;
                    FootstepsIndex = FootstepsIndex % footstepFootList.Length;
                    var footList = footstepFootList[FootstepsIndex];
                    var uuid = footList.soundUuids?.Random();
                    if (!uuid.IsNullOrWhiteSpace())
                    {
                        // Debug.Log($"Play Footstep Sounc With Config:{uuid}");
                        Character.Main.Sound.PlaySound(uuid, volume: footstepEntry.volume);
                        Interval = Random.Range(footstepEntry.IntervalRange.x, footstepEntry.IntervalRange.y);
                        MinInterval = footstepEntry.MinInterval;
                        playedFlag = true;
                    }

                }
            }

            if (!playedFlag)
            {
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

                            foreach (var footstepEntry in FootstepConfig.textureFootSteps)
                            {
                                if (playedFlag) break;
                                if (!footstepEntry.texture.name.Equals(texture.name)) continue;
                                if (footstepEntry.footstepEntry.FootList == null || (footstepEntry.footstepEntry.FootList != null && footstepEntry.footstepEntry.FootList.Length == 0)) continue;


                                var footstepFootList = footstepEntry.footstepEntry.FootList;
                                FootstepsIndex = FootstepsIndex % footstepFootList.Length;
                                var footList = footstepFootList[FootstepsIndex];
                                var uuid = footList.soundUuids?.Random();
                                if (!uuid.IsNullOrWhiteSpace())
                                {
                                    // Debug.Log($"Play Footstep Sounc With Config:{uuid}");
                                    Character.Main.Sound.PlaySound(uuid, volume: footstepEntry.footstepEntry.volume);
                                    Interval = Random.Range(footstepEntry.footstepEntry.IntervalRange.x, footstepEntry.footstepEntry.IntervalRange.y);
                                    MinInterval = footstepEntry.footstepEntry.MinInterval;
                                    playedFlag = true;
                                }

                            }
                        }
                    }
                }

            }



            if (!playedFlag)
            {
                var enterScene = StaticScene.GetLastestEnterScene();
                if (enterScene != null && enterScene.UseSceneFootstep && enterScene.Footstep != null && enterScene.Footstep.Value.FootList != null && enterScene.Footstep.Value.FootList.Length > 0)
                {
                    var footList = enterScene.Footstep.Value.FootList;
                    FootstepsIndex = FootstepsIndex % footList.Length;
                    var footEntry = footList[FootstepsIndex];
                    var uuid = footEntry.soundUuids?.Random();
                    if (!uuid.IsNullOrWhiteSpace())
                    {
                        // Debug.Log($"Play Footstep Sounc With Scene:{footEntry}");
                        Character.Main.Sound.PlaySound(uuid, volume: enterScene.Footstep.Value.volume);
                        Interval = Random.Range(enterScene.Footstep.Value.IntervalRange.x, enterScene.Footstep.Value.IntervalRange.y);
                        MinInterval = enterScene.Footstep.Value.MinInterval;
                        playedFlag = true;
                    }

                }


            }


            if (!playedFlag && GameMainConfig.UseDefaultFootstepSound && GameMainConfig.FootstepEntry.FootList.Length > 0)
            {
                FootstepsIndex = FootstepsIndex % GameMainConfig.FootstepEntry.FootList.Length;
                var footList = GameMainConfig.FootstepEntry.FootList[FootstepsIndex];
                var uuid = footList.soundUuids?.Random();
                if (!uuid.IsNullOrWhiteSpace())
                {
                    // Debug.Log($"Play Footstep Sounc With Defaut Config:{uuid}");
                    Character.Main.Sound.PlaySound(uuid, volume: GameMainConfig.FootstepEntry.volume);
                    Interval = Random.Range(GameMainConfig.FootstepEntry.IntervalRange.x, GameMainConfig.FootstepEntry.IntervalRange.y);
                    MinInterval = GameMainConfig.FootstepEntry.MinInterval;
                    playedFlag = true;
                }

            }

            FootstepsIndex += 1;




        }







    }

}

