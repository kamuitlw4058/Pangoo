using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Pangoo;
using Sirenix.OdinInspector;
using UnityEngine;
using Pangoo.Common;
using Pangoo.Core.VisualScripting;

public class HotSpotSpriteState : MonoBehaviour
{
    [Serializable]
    public struct HotspotStateMapperEntry
    {
        public int HotspotState;
        public string FsmStateName;

    }

    public List<HotspotStateMapperEntry> HotspotStateMapperList = new List<HotspotStateMapperEntry>();

    Dictionary<int, string> HotspotStateMapperDict = new Dictionary<int, string>();

    public int RunningHotspoState
    {
        get
        {
            foreach (var kv in HotspotStateMapperDict)
            {
                if (kv.Value.Equals(RunningState))
                {
                    return kv.Key;
                }
            }

            return 0;
        }
        set
        {
            if (HotspotStateMapperDict.TryGetValue(value, out string state))
            {
                RunningState = state;
            }
        }
    }



    public List<HotSpotFsmState> StateList = new();

    public HotspotFsmManager uIFsmManager = new HotspotFsmManager();

    [ShowInInspector]
    public INamedFsm<HotspotFsmManager> namedFsm;

    [ValueDropdown("StateDropdown")]
    public string startState;

    [ShowInInspector]
    [ValueDropdown("StateDropdown")]
    public string RunningState
    {
        get
        {
            return namedFsm?.CurrentState.FullName;
        }
        set
        {
            if (value == namedFsm?.CurrentState.FullName)
            {
                return;
            }

            namedFsm?.ChangeState(value);
        }
    }

    public IEnumerable StateDropdown()
    {
        ValueDropdownList<string> dropdownItems = new ValueDropdownList<string>();
        foreach (var state in StateList)
        {
            dropdownItems.Add(state.FullName);
        }
        return dropdownItems;
    }


    void Start()
    {
        namedFsm = NamedFsm<HotspotFsmManager>.Create("Test", uIFsmManager, StateList);
        namedFsm.Start(startState);
        HotspotStateMapperDict.Clear();
        foreach (var item in HotspotStateMapperList)
        {
            HotspotStateMapperDict.Add(item.HotspotState, item.FsmStateName);
        }


    }

    // Update is called once per frame
    void Update()
    {
        namedFsm.Update();
    }
}
