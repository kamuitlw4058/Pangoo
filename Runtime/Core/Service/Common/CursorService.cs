
using UnityEngine;
using Sirenix.OdinInspector;
using Pangoo.Core.VisualScripting;
using Pangoo.Core.Common;

namespace Pangoo.Core.Services
{

    public class CursorService : MainSubService
    {
        public const int CursorTypeLenght = 2;

        public override int Priority => -1;

        CursorTypeEnum m_CursorType;

        [ShowInInspector]
        public CursorTypeEnum CursorType
        {
            get
            {
                return m_CursorType;
            }
            set
            {
                m_CursorType = value;
                UpdateCursor();
            }
        }

        public void UpdateCursor()
        {
            switch (m_CursorType)
            {
                case CursorTypeEnum.Hide:
                    Cursor.lockState = CursorLockMode.Locked;
                    Cursor.visible = false;
                    break;
                case CursorTypeEnum.Show:
                    Cursor.visible = true;
                    Cursor.lockState = CursorLockMode.Confined;
                    break;
            }
        }



        protected override void DoStart()
        {
            CursorType = CursorTypeEnum.Hide;
        }

        protected override void DoUpdate()
        {
            if (GameMainConfigSrv.GameMainConfig.CursorOnOffKeyCode == KeyCode.None) return;

            if (Input.GetKeyDown(GameMainConfigSrv.GameMainConfig.CursorOnOffKeyCode))
            {
                var cursorVal = (int)CursorType;
                cursorVal += 1;
                cursorVal %= CursorTypeLenght;
                CursorType = (CursorTypeEnum)cursorVal;
            }
        }


    }
}