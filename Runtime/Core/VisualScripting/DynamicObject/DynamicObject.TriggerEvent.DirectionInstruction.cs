using System;
using System.Collections.Generic;

using UnityEngine;
using Pangoo.Core.Common;
using Pangoo.Core.Characters;
using Sirenix.OdinInspector;
using UnityEngine.Playables;
using UnityEditor;



namespace Pangoo.Core.VisualScripting
{

    public partial class DynamicObject
    {

        Instruction GetSelfTriggerEnabledInstruction(bool val)
        {
            var instruction = Activator.CreateInstance<InstructionSetSelfTriggerEnabled>();
            instruction.ParamsRaw = new InstructionBoolParams();
            instruction.ParamsRaw.Val = val;
            return instruction;
        }

        Instruction GetDynamicObjectPlayTimelineInstruction(int dynamicObjectId)
        {
            var instruction = Activator.CreateInstance<InstructionDynamicObjectPlayTimeline>();
            instruction.ParamsRaw.Val = dynamicObjectId;
            return instruction;
        }

        Instruction GetSetVariableBoolInstruction(int VariableId, bool val)
        {
            var instruction = Activator.CreateInstance<InstructionSetVariableBool>();
            instruction.ParamsRaw.VariableId = VariableId;
            instruction.ParamsRaw.Val = val;
            return instruction;
        }

        Instruction GetSetPlayerIsControllable(bool val)
        {
            var instruction = Activator.CreateInstance<InstructionSetPlayerControllable>();
            instruction.ParamsRaw.Val = val;
            return instruction;
        }

        Instruction GetSetGameObjectActive(string path, bool val)
        {
            var instruction = Activator.CreateInstance<InstructionGameObjectActive>();
            instruction.ParamsRaw.Path = path;
            instruction.ParamsRaw.Val = val;
            return instruction;
        }

        Instruction GetActiveCameraGameObject(string path, bool val)
        {
            var instruction = Activator.CreateInstance<InstructionActiveCameraGameObject>();
            instruction.ParamsRaw.Path = path;
            instruction.ParamsRaw.Val = val;
            return instruction;
        }

        Instruction GetUnactiveCameraGameObject(string path, bool val)
        {
            var instruction = Activator.CreateInstance<InstructionUnactiveCameraGameObject>();
            instruction.ParamsRaw.Path = path;
            instruction.ParamsRaw.Val = val;
            return instruction;
        }


        Instruction GetSubGameObjectPlayTimeline(string path, bool val)
        {
            var instruction = Activator.CreateInstance<InstructionSubGameObjectPlayTimeline>();
            instruction.ParamsRaw.Path = path;
            instruction.ParamsRaw.Val = val;
            return instruction;
        }





        Instruction GetChangeGameSectionInstruction(int val)
        {
            var instruction = Activator.CreateInstance<InstructionChangeGameSection>();
            instruction.ParamsRaw.Val = val;
            return instruction;
        }

        public InstructionList GetDirectInstructionList(DirectInstructionGroup directInstructionGroup, TriggerEvent trigger)
        {
            List<Instruction> ret = new();

            foreach (var directInstruction in directInstructionGroup.DirectInstructionList)
            {
                switch (directInstruction.InstructionType)
                {
                    case DirectInstructionTypeEnum.DynamicObjectPlayTimeline:
                        var instruction = GetDynamicObjectPlayTimelineInstruction(directInstruction.Int1);
                        ret.Add(instruction);
                        break;
                    case DirectInstructionTypeEnum.ChangeGameSection:
                        var instructionGameSection = GetChangeGameSectionInstruction(directInstruction.Int1);
                        ret.Add(instructionGameSection);
                        break;
                    case DirectInstructionTypeEnum.SetBoolVariable:
                        var InstructionSetVariableBool = GetSetVariableBoolInstruction(directInstruction.Int1, directInstruction.Bool1);
                        ret.Add(InstructionSetVariableBool);
                        break;
                    case DirectInstructionTypeEnum.SetPlayerIsControllable:
                        var InstructionSetPlayerIsIsControllable = GetSetPlayerIsControllable(directInstruction.Bool1);
                        ret.Add(InstructionSetPlayerIsIsControllable);
                        break;
                    case DirectInstructionTypeEnum.SetGameObjectActive:
                        var InstructionSetGameObjectActive = GetSetGameObjectActive(directInstruction.String1, directInstruction.Bool1);
                        ret.Add(InstructionSetGameObjectActive);
                        break;
                    case DirectInstructionTypeEnum.ActiveCameraGameObject:
                        var InstructionActiveCameraGameObject = GetActiveCameraGameObject(directInstruction.String1, directInstruction.Bool1);
                        ret.Add(InstructionActiveCameraGameObject);
                        break;
                    case DirectInstructionTypeEnum.UnactiveCameraGameObject:
                        var InstructionUnactiveCameraGameObject = GetUnactiveCameraGameObject(directInstruction.String1, directInstruction.Bool1);
                        ret.Add(InstructionUnactiveCameraGameObject);
                        break;
                    case DirectInstructionTypeEnum.SubGameObjectPlayTimeline:
                        var InstructionSubGameObjectPlayTimeline = GetSubGameObjectPlayTimeline(directInstruction.String1, directInstruction.Bool1);
                        ret.Add(InstructionSubGameObjectPlayTimeline);
                        break;
                }
            }

            if (directInstructionGroup.DisableOnFinish)
            {
                var instruction = GetSelfTriggerEnabledInstruction(false);
                ret.Add(instruction);
            }
            foreach (var instruction in ret)
            {
                instruction.Trigger = trigger;
            }
            return new InstructionList(ret.ToArray());
        }

        public TriggerEvent BuildTriggerEvent(int i, DirectInstructionGroup directInstructionGroup)
        {

            if (directInstructionGroup.DirectInstructionList == null || directInstructionGroup.DirectInstructionList.Length == 0) return null;

            TriggerEvent ret = null;
            TriggerEventTable.TriggerEventRow row = new TriggerEventTable.TriggerEventRow();

            row.Id = (i * -1) - 1;
            row.Name = $"DI_{directInstructionGroup.TriggerType}_{directInstructionGroup.DirectInstructionList?.Length ?? 0}";
            row.Params = "{}";
            row.Targets = string.Empty;
            row.Enabled = directInstructionGroup.InitEnabled;
            Debug.Log($"Try add row:{row.Id}");

            switch (directInstructionGroup.TriggerType)
            {
                case TriggerTypeEnum.OnTriggerEnter3D:
                    ret = CreateTriggerEvent<TriggerEventOnTriggerEnter3d>(row);
                    TriggerEnter3dEvent -= OnTriggerEnter3dEvent;
                    TriggerEnter3dEvent += OnTriggerEnter3dEvent;
                    break;
                case TriggerTypeEnum.OnInteract:
                    ret = CreateTriggerEvent<TriggerEventOnInteraction>(row);
                    m_Tracker = CachedTransfrom.GetOrAddComponent<InteractionItemTracker>();
                    ret.EventRunInstructionsEnd -= OnInteractEnd;
                    ret.EventRunInstructionsEnd += OnInteractEnd;
                    break;
            }

            if (ret != null)
            {
                ret.RunInstructions = GetDirectInstructionList(directInstructionGroup, ret);
                TriggerEvents.Add(row.Id, ret);
            }

            return ret;
        }


        void DoAwakeDirectionInstruction()
        {
            var directInstructionGroups = DirectInstructionGroup.CreateArray(Row?.DirectInstructions);
            if (directInstructionGroups == null || directInstructionGroups.Length == 0) return;


            for (int i = 0; i < directInstructionGroups.Length; i++)
            {
                var directInstructionGroup = directInstructionGroups[i];
                BuildTriggerEvent(i, directInstructionGroup);
            }
        }




    }
}