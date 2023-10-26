using Pangoo;
using System.Collections;
using System.Collections.Generic;

using System;
using GameFramework;
using UnityGameFramework.Runtime;
using UnityEngine;
using Pangoo.Core.Common;
using Pangoo.Core.VisualScripting;

namespace Pangoo.Core.Services
{
    public class GameSectionService : BaseService
    {
        public override int Priority => 10;

        ExcelTableService m_ExcelTableService;
        StaticSceneService m_StaticSceneService;
        DynamicObjectService m_DynamicObjectService;

        GameMainConfigService m_GameMainConfigService;

        GameSectionTable m_GameSectionTable;

        InstructionTable m_InstructionTable;



        public int CurrentId = 1;

        public int LatestId = -1;


        protected override void DoAwake()
        {
            base.DoAwake();
            m_StaticSceneService = Parent.GetService<StaticSceneService>();
            m_ExcelTableService = Parent.GetService<ExcelTableService>();
            m_StaticSceneService = Parent.GetService<StaticSceneService>();
            m_GameMainConfigService = Parent.GetService<GameMainConfigService>();

            m_DynamicObjectService = Parent.GetService<DynamicObjectService>();

            Event.Subscribe(GameSectionChangeEventArgs.EventId, OnGameSectionChangeEvent);

        }

        void OnGameSectionChangeEvent(object sender, GameFrameworkEventArgs e)
        {
            var args = e as GameSectionChangeEventArgs;
            if (args.GameSectionId != 0)
            {
                CurrentId = args.GameSectionId;
            }
        }

        protected override void DoStart()
        {
            Debug.Log($"DoStart GameSectionService");
            m_GameSectionTable = m_ExcelTableService.GetExcelTable<GameSectionTable>();
            m_StaticSceneService.OnInitSceneLoaded += OnInitSceneLoaded;
            CurrentId = m_GameMainConfigService.GetGameMainConfig().EnterGameSectionId;
            UpdateStaticScene();
        }

        void OnInitSceneLoaded()
        {
            Debug.Log($"OnInitSceneLoaded");
            var GameSection = m_GameSectionTable.GetGameSectionRow(CurrentId);
            var instructionIds = GameSection.InitedInstructionIds.ToListInt();
            if (instructionIds.Count > 0)
            {
                var instructions = GetInstructionList(instructionIds);
                var args = new Args();
                args.Main = Parent as MainService;
                instructions.Start(args);

            }
        }



        void UpdateStaticScene()
        {
            if (CurrentId != LatestId)
            {
                LatestId = CurrentId;
                var GameSection = m_GameSectionTable.GetGameSectionRow(CurrentId);
                if (GameSection == null)
                {
                    Debug.LogError($"GameSection is null:{GameSection}");
                }

                Tuple<int, int> sectionChange = new Tuple<int, int>(0, 0);
                if (!string.IsNullOrEmpty(GameSection.SectionJumpByScene))
                {
                    var itemList = GameSection.SectionJumpByScene.ToListInt("#");
                    if (itemList.Count == 2)
                    {
                        sectionChange = new Tuple<int, int>(itemList[0], itemList[1]);
                    }
                }
                m_StaticSceneService.SetGameSectionChange(sectionChange);
                m_StaticSceneService.SetGameScetion(
                    GameSection.DynamicSceneIds.ToListInt(),
                    GameSection.KeepSceneIds.ToListInt(),
                    GameSection.InitSceneIds.ToListInt()
                    );

                var doIds = GameSection.DynamicObjectIds.ToListInt();
                foreach (var doId in doIds)
                {
                    m_DynamicObjectService.ShowDynamicObject(doId);
                }

                Log.Info($"Update Static Scene:{GameSection.Id} KeepSceneIds:{GameSection.KeepSceneIds} DynamicSceneIds:{GameSection.DynamicSceneIds}");
            }
        }

        public InstructionTable.InstructionRow GetInstructionRow(int id)
        {
            InstructionTable.InstructionRow instructionRow = null;

#if UNITY_EDITOR
            if (Application.isPlaying && m_InstructionTable != null)
            {
                Debug.Log($"GetRowByInstructionTable");
                instructionRow = m_InstructionTable.GetRowById(id);
            }
            else
            {
                instructionRow = GameSupportEditorUtility.GetExcelTableRowWithOverviewById<InstructionTableOverview, InstructionTable.InstructionRow>(id);
            }

#else
            instructionRow = m_InstructionTable.GetRowById(id);
#endif
            return instructionRow;
        }

        InstructionList GetInstructionList(List<int> ids)
        {
            List<Instruction> instructions = new();

            foreach (var instructionId in ids)
            {
                InstructionTable.InstructionRow instructionRow = GetInstructionRow(instructionId);
                if (instructionRow == null || instructionRow.InstructionType == null)
                {
                    continue;
                }

                var InstructionInstance = ClassUtility.CreateInstance<Instruction>(instructionRow.InstructionType);
                InstructionInstance.LoadParams(instructionRow.Params);

                instructions.Add(InstructionInstance);
            }

            return new InstructionList(instructions.ToArray());
        }


        protected override void DoUpdate()
        {
            UpdateStaticScene();
        }

        protected override void DoDestroy()
        {
            if (m_StaticSceneService != null)
            {
                m_StaticSceneService.OnInitSceneLoaded -= OnInitSceneLoaded;

            }
            base.DoDestroy();
        }


    }
}