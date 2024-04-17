#if UNITY_EDITOR

using System;
using System.Collections;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using LitJson;
using UnityEngine;
using Sirenix.OdinInspector;
using System.Xml.Serialization;
using Pangoo.Common;
using MetaTable;

namespace Pangoo.MetaTable
{
    public partial class CharacterConfigDetailRowWrapper : MetaTableDetailRowWrapper<CharacterConfigOverview, UnityCharacterConfigRow>
    {

        [ShowInInspector]
        public float WalkSpeed
        {
            get
            {
                return UnityRow.Row.WalkSpeed;
            }
            set
            {
                UnityRow.Row.WalkSpeed = value;
                Save();
            }
        }

        [ShowInInspector]
        public float RunSpeed
        {
            get
            {
                return UnityRow.Row.RunSpeed;
            }
            set
            {
                UnityRow.Row.RunSpeed = value;
                Save();
            }
        }



        [ValueDropdown("@GameSupportEditorUtility.GetNoiseSettings()")]
        [ShowInInspector]
        public string NoiseProfile
        {
            get
            {
                return UnityRow.Row.NoiseProfile;
            }
            set
            {
                UnityRow.Row.NoiseProfile = value;
                Save();
            }
        }
    }
}
#endif

