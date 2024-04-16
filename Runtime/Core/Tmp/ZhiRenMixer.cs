using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening.Core.Easing;
using JetBrains.Annotations;
using Pangoo;
using Pangoo.Common;
using Pangoo.Core.Characters;
using Sirenix.OdinInspector;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Audio;

public class ZhiRenMixer : MonoBehaviour
{
    public const float NormalVolume = 0;
    public const float MuteVolume = -80;

    [SerializeField]
    AudioMixer Mixer;

    public enum MixerState
    {
        None,
        RoomOnly,
        MainOnly,
        BlendRoomMain,
    }

    MixerState lastestMixerState;


    [SerializeField]
    MixerState mixerState;

    public string MainKey;

    public float MainMaxVolume = 0;

    public string RoomKey;

    public float RoomMaxVolume = 0;

    [Range(0, 1)]
    public float Value;


    public float WeightPower = 0.2f;

    public Vector2 DistanceRange;


    [SerializeField]
    bool UserMainGroup;

    void Start()
    {

    }
    [SerializeField]
    EntityCharacter character;


    public float Distance;

    public bool AtRoom;

    public bool IsOpenedDoor;

    public bool NotOptState;


    void Update()
    {
        if (Mixer == null)
        {
            return;
        }

        // if (PangooEntry.Service.mainService.StaticScene.EnterAssetCountDict.ContainsKey("7726c64ee90449a2807e1004da5efb53"))
        // {
        //     AtRoom = true;
        // }
        // else
        // {
        //     AtRoom = false;
        // }
        var dynamicObejctEntity = PangooEntry.Service.mainService.DynamicObject.GetLoadedEntity("a9dd0b0ec78d44efa0652f260aa15f80");
        if (dynamicObejctEntity != null)
        {
            var val = dynamicObejctEntity.DynamicObj.GetVariable<bool>("f6d3e5b39e7341198d9013799605b7e2");
            if (val)
            {
                IsOpenedDoor = true;
            }
            else
            {
                IsOpenedDoor = false;
            }
        }
        else
        {
            IsOpenedDoor = false;
        }

        if (!NotOptState)
        {
            if (AtRoom && IsOpenedDoor)
            {
                mixerState = MixerState.BlendRoomMain;
            }
            else if (AtRoom && !IsOpenedDoor)
            {
                mixerState = MixerState.RoomOnly;
            }
            else if (!AtRoom)
            {
                mixerState = MixerState.MainOnly;
            }
        }


        if (mixerState != lastestMixerState)
        {
            switch (mixerState)
            {
                case MixerState.MainOnly:
                    Mixer.SetFloat(MainKey, MainMaxVolume);
                    Mixer.SetFloat(RoomKey, MuteVolume);
                    break;
                case MixerState.RoomOnly:
                    Mixer.SetFloat(MainKey, MuteVolume);
                    Mixer.SetFloat(RoomKey, RoomMaxVolume);
                    break;
            }


            lastestMixerState = mixerState;
        }

        if (mixerState == MixerState.BlendRoomMain)
        {
            if (character == null)
            {
                if (PangooEntry.Service.mainService?.CharacterService?.Player != null)
                {
                    character = PangooEntry.Service.mainService?.CharacterService?.Player;

                }
            }


            if (character != null)
            {
                var direction = transform.position - character.transform.position;
                Distance = direction.magnitude;

                if (Distance < DistanceRange.x)
                {
                    Value = 0;
                }
                else if (Distance >= DistanceRange.x && Distance <= DistanceRange.y)
                {
                    Value = MathUtility.Remap(Distance, DistanceRange, new Vector2(0, 1));

                }
                else
                {
                    Value = 1;
                }
            }

            var currentRoom = MathUtility.Remap(Mathf.Pow(1 - Value, WeightPower), new Vector2(0, 1), new Vector2(-80, RoomMaxVolume));
            var currentMain = MathUtility.Remap(Mathf.Pow(Value, WeightPower), new Vector2(0, 1), new Vector2(-80, MainMaxVolume));

            Mixer.SetFloat(MainKey, currentMain);
            Mixer.SetFloat(RoomKey, currentRoom);
        }



    }
    [Button("Swap")]
    public void Swap()
    {
        if (UserMainGroup)
        {
            Mixer.SetFloat("Volume1", -80);
            Mixer.SetFloat("Volume2", 0);
            UserMainGroup = false;
        }
        else
        {
            Mixer.SetFloat("Volume1", 0);
            Mixer.SetFloat("Volume2", -80);
            UserMainGroup = true;

        }

    }
}
