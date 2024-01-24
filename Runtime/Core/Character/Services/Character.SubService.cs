using System;
using UnityEngine;
using Pangoo.Core.Common;
using Pangoo.Core.Services;

namespace Pangoo.Core.Characters
{

    public partial class Character
    {

        CharacterInputService m_CharacterInputService;

        public CharacterInputService CharacterInput
        {
            get
            {
                return m_CharacterInputService;
            }
        }

        CharacterCameraService m_CharacterCameraService;

        public CharacterCameraService CharacterCamera
        {
            get
            {
                return m_CharacterCameraService;
            }
        }

        DriverService m_DriverService;


        public DriverService Driver
        {
            get
            {
                return m_DriverService;
            }
        }


        MotionActionService m_MotionActionService;


        public MotionActionService MotionAction
        {
            get
            {
                return m_MotionActionService;
            }
        }


        PlayerService m_PlayerService;


        public PlayerService PlayerService
        {
            get
            {
                return m_PlayerService;
            }
        }

        InteractionService m_InteractionService;

        public InteractionService InteractionService
        {
            get
            {
                return m_InteractionService;
            }
        }


        FootstepsService m_FootstepsService;
        public FootstepsService FootstepsService
        {
            get
            {
                return m_FootstepsService;
            }
        }





    }
}