using System;
using System.Collections;
using System.Collections.Generic;
using LitJson;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;

namespace Pangoo.Core.VisualScripting
{
    public class ConditionInputMouseParams : ConditionParams
    {
        [JsonMember("MouseKey")]
        public string MouseKey
        {
            get
            {
                return m_Button.ToString();
            }
            set
            {
                m_Button =Enum.Parse<Button>(value);
            }
        }
        public Button m_Button = Button.Left;
        
        [JsonMember("CheckBool")]
        [LabelText("检测目标")]
        public bool CheckBool;
        
        public override void Load(string val)
        {
            var par = JsonMapper.ToObject<ConditionInputMouseParams>(val);
            m_Button = par.m_Button;
            CheckBool = par.CheckBool;
        }
        
        
        [Serializable]
        public enum Button
        {
            Left = MouseButton.Left,
            Right = MouseButton.Right,
            Middle = MouseButton.Middle,
            Forward = MouseButton.Forward,
            Back = MouseButton.Back
        }
    }
}
