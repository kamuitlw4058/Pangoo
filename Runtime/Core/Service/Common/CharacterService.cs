using Pangoo;
using System.Collections;
using System.Collections.Generic;
using GameFramework;
using System;
using UnityGameFramework.Runtime;
using System.Linq;
using UnityEngine;
using Sirenix.OdinInspector;

namespace Pangoo.Core.Services
{
    [Serializable]
    public class CharacterService : BaseService
    {
        public override int Priority => 5;

        ExcelTableService m_ExcelTableService;

        GameInfoService m_GameInfoService;

        EntityGroupTable m_EntityGroupTable;

        EntityGroupTable.EntityGroupRow m_EntityGroupRow;

        [ShowInInspector]
        Dictionary<int, EntityCharacter> m_LoadedEntityDict = new Dictionary<int, EntityCharacter>();

        public EntityCharacter Player = null;

        [ShowInInspector]
        List<int> m_LoadingEntityIds = new List<int>();

        EntityLoader Loader = null;
        CharacterInfo m_CharacterInfo;


        public Tuple<int, int> m_SectionChange;


        public bool m_SectionInited = false;

        public Action OnInitSceneLoaded;

        protected override void DoAwake()
        {
            base.DoAwake();

            m_ExcelTableService = Parent.GetService<ExcelTableService>();
            m_GameInfoService = Parent.GetService<GameInfoService>();

        }


        protected override void DoStart()
        {

            // m_StaticSceneTable = m_ExcelTableService.GetExcelTable<StaticSceneTable>();
            m_CharacterInfo = m_GameInfoService.GetGameInfo<CharacterInfo>();
            m_EntityGroupTable = m_ExcelTableService.GetExcelTable<EntityGroupTable>();
            m_EntityGroupRow = EntityGroupRowExtension.CreateCharacterGroup();
        }


        public void SetPlayerHeight(float height)
        {
            Player?.character?.SetCameraOffset(new Vector3(0, height, 0));
        }



        public void ShowCharacter(int infoId, Vector3 positon, Vector3 rotation, float height = -1f)
        {
            if (infoId == 0)
            {
                Debug.LogError("ShowCharacter Id is 0");
                return;
            }

            if (Loader == null)
            {
                Loader = EntityLoader.Create(this);
            }
            Debug.Log($"ShowCharacter:{infoId},{positon},{rotation},{m_LoadedEntityDict.ContainsKey(infoId)}");

            //通过路径ID去判断是否被加载。用来在不同的章节下用了不用的静态场景ID,但是使用不同的加载Ids
            var infoRow = m_CharacterInfo.GetRowById<CharacterInfoRow>(infoId);
            if (m_LoadedEntityDict.ContainsKey(infoId))
            {
                var character = m_LoadedEntityDict[infoId];
                character.transform.position = positon;
                character.transform.rotation = Quaternion.Euler(rotation);
                character.character.ResetCameraDirection();
                if (height >= 0)
                {
                    character.character.SetCameraOffset(new Vector3(0, height, 0));
                }

                Physics.SyncTransforms();
                return;
            }

            Log.Info($"Show Character Row Id:{infoId}");
            // 这边有一个假设，同一个时间不会反复加载不同的章节下的同一个场景。
            if (m_LoadingEntityIds.Contains(infoId))
            {
                return;
            }
            else
            {
                EntityCharacterData data = EntityCharacterData.Create(infoRow.CreateEntityInfo(m_EntityGroupRow), this, infoRow, positon, rotation);
                data.Height = height;
                m_LoadingEntityIds.Add(infoId);
                Loader.ShowEntity(EnumEntity.Character,
                    (o) =>
                    {
                        Log.Info($"Character Loaded:{infoId}");
                        if (m_LoadingEntityIds.Contains(infoId))
                        {
                            m_LoadingEntityIds.Remove(infoId);
                        }
                        m_LoadedEntityDict.Add(infoId, o.Logic as EntityCharacter);
                        if (infoRow.IsPlayer)
                        {
                            Player = o.Logic as EntityCharacter;
                        }
                    },
                    data.EntityInfo,
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