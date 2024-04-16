using Pangoo;
using System.Collections;
using System.Collections.Generic;
using GameFramework;
using System;
using UnityGameFramework.Runtime;
using System.Linq;
using UnityEngine;
using Sirenix.OdinInspector;
using Pangoo.MetaTable;

namespace Pangoo.Core.Services
{
    public class CharacterService : MainSubService
    {
        public override string ServiceName => "CharacterService";
        public override int Priority => 5;


        IEntityGroupRow m_EntityGroupRow;

        [ShowInInspector]
        Dictionary<string, EntityCharacter> m_LoadedEntityDict;

        public EntityCharacter Player = null;

        [ShowInInspector]
        List<string> m_LoadingEntityUuids;

        EntityLoader Loader = null;
        CharacterInfo m_CharacterInfo;


        public Tuple<int, int> m_SectionChange;


        public bool m_SectionInited = false;

        public Action OnInitSceneLoaded;

        protected override void DoAwake()
        {
            base.DoAwake();
            m_LoadedEntityDict = new Dictionary<string, EntityCharacter>();
            m_LoadingEntityUuids = new List<string>();

        }

        protected override void DoStart()
        {

            m_CharacterInfo = GameInfoSrv.GetGameInfo<CharacterInfo>();
            m_EntityGroupRow = EntityGroupRowExtension.CreateCharacterGroup();
        }

        public bool PlayerControllable
        {
            get
            {
                if (Player != null)
                {
                    return Player.character.IsControllable;
                }

                return false;
            }
            set
            {
                if (Player != null)
                {
                    Player.character.IsControllable = value;
                    Log($"SetPlayer Controllable:{value}");

                }
            }
        }

        public void SetPlayerControllable(bool val)
        {
            if (Player != null)
            {
                Player.character.IsControllable = val;
                Log($"SetPlayer IsControllable:{val}");
            }
        }

        [ShowInInspector]
        public bool PlayerEnabledHotspot
        {
            get
            {
                if (Player != null)
                {
                    return Player.character.EnabledHotspot;
                }
                return false;
            }
            set
            {
                Log($"Enabled Player Hotspot:{value}");
                if (Player != null)
                {
                    Player.character.EnabledHotspot = value;
                }
            }
        }

        [ShowInInspector]
        public bool PlayerIsInteractive
        {
            get
            {
                if (Player != null)
                {
                    return Player.character.IsInteractive;
                }
                return false;
            }
            set
            {
                if (Player != null)
                {
                    Player.character.IsInteractive = value;
                }
            }
        }

        public void SetPlayerSpeed(float walkVal, float runWalk)
        {
            if (Player != null)
            {
                Player.character.SetCharacterSpeed(walkVal, runWalk);
                Log($"SetPlayer WalkSpeed:{walkVal} RunSpeed:{runWalk}");
            }
        }


        public void SetPlayerCameraHeight(float height)
        {
            var character = Player?.character;
            if (character != null)
            {
                character.CameraHight = height;
            }
            else
            {
                LogError($"Player Is Null");
            }
        }

        public void SetPlayerColliderHeight(float height)
        {
            var character = Player?.character;
            if (character != null)
            {
                character.ColliderHight = height;
            }
            else
            {
                LogError($"Player Is Null");
            }
        }

        public string GetApplyCharacterUuid(string uuid)
        {
            if (uuid.IsNullOrWhiteSpace())
            {
                uuid = GameMainConfigSrv.DefaultPlayer;
                if (uuid.IsNullOrWhiteSpace())
                {
                    LogError($"Character Uuid Is Null!");
                    return null;
                }
                else
                {
                    return uuid;
                }
            }

            if (uuid.Equals(ConstString.LatestPlayer))
            {
                if (Player == null)
                {
                    LogError($"Character Latest Player Uuid Is Null!");
                    return null;
                }
                else
                {
                    return Player.character.Row.Uuid;
                }
            }

            return uuid;

        }


        public EntityCharacter GetLoadedEntity(string uuid)
        {
            uuid = GetApplyCharacterUuid(uuid);
            if (uuid == null)
            {
                return null;
            }

            if (m_LoadedEntityDict.TryGetValue(uuid, out EntityCharacter var))
            {
                return var;
            }
            return null;
        }



        public void ShowCharacter(string infoUuid, Vector3 positon, Vector3 rotation, float height = ConstFloat.InvaildCameraHeight, float colliderHeight = ConstFloat.InvaildColliderHeight, bool IsInteractive = true, bool NotMoveWhenPlayerCreated = false)
        {

            infoUuid = GetApplyCharacterUuid(infoUuid);
            if (infoUuid == null)
            {
                LogError($"Show Character uuid Is Null");
                return;
            }



            if (Loader == null)
            {
                Loader = EntityLoader.Create(this);
            }
            Debug.Log($"ShowCharacter:{infoUuid},{positon},{rotation},{m_LoadedEntityDict.ContainsKey(infoUuid)}");

            //通过路径ID去判断是否被加载。用来在不同的章节下用了不用的静态场景ID,但是使用不同的加载Ids
            var infoRow = m_CharacterInfo.GetRowByUuid<CharacterInfoRow>(infoUuid);
            Debug.Log($"infoRow:{infoRow} infoUuid:{infoUuid}");
            if (m_LoadedEntityDict.ContainsKey(infoUuid))
            {
                var character = m_LoadedEntityDict[infoUuid];
                character.character.IsInteractive = IsInteractive;
                if (height > ConstFloat.InvaildCameraHeight)
                {
                    character.character.CameraHight = height;
                }

                if (colliderHeight > ConstFloat.InvaildColliderHeight)
                {
                    character.character.ColliderHight = colliderHeight;
                }

                if (character == Player && NotMoveWhenPlayerCreated)
                {
                    Physics.SyncTransforms();
                    return;
                }
                character.transform.position = positon;
                character.transform.rotation = Quaternion.Euler(rotation);
                character.character.ResetCameraDirection();
                Physics.SyncTransforms();
                return;
            }

            Log($"Show Character Row Id:{infoUuid}");
            // 这边有一个假设，同一个时间不会反复加载不同的章节下的同一个场景。
            if (m_LoadingEntityUuids.Contains(infoUuid))
            {
                return;
            }
            else
            {
                EntityCharacterData data = EntityCharacterData.Create(this, infoRow, m_EntityGroupRow, positon, rotation);
                data.Height = height;
                data.IsInteractive = IsInteractive;
                m_LoadingEntityUuids.Add(infoUuid);
                Loader.ShowEntity(EnumEntity.Character,
                    (o) =>
                    {
                        Log($"Character Loaded:{infoUuid}");
                        if (m_LoadingEntityUuids.Contains(infoUuid))
                        {
                            m_LoadingEntityUuids.Remove(infoUuid);
                        }
                        m_LoadedEntityDict.Add(infoUuid, o.Logic as EntityCharacter);
                        if (infoRow.IsPlayer)
                        {
                            Player = o.Logic as EntityCharacter;
                        }
                    },
                    data,
                    data);
            }
        }

        public GameObject PlayerGameObject
        {
            get
            {
                if (Player != null)
                {
                    return Player.gameObject;
                }
                return null;
            }

        }



    }
}