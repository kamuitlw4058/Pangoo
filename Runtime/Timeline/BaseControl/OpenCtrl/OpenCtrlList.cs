using System;
using System.Collections.Generic;
using DG.Tweening;

using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace Pangoo.Timeline
{

    [DisallowMultipleComponent]
    public class OpenCtrlList : OpenCtrlBase
    {
        public List<OpenCtrlBase> Openers;

        public override bool IsOpening
        {
            get
            {
                if (Openers == null)
                {
                    return true;
                }
                return m_IsOpening;
            }
            set
            {
                m_IsOpening = value;
                if (Openers == null)
                {
                    return;
                }

                foreach (var opener in Openers)
                {
                    opener.IsOpening = value;
                }
            }
        }

        [ShowInInspector]
        public override bool IsOpened
        {
            get
            {
                if (Openers == null)
                {
                    return true;
                }

                bool flag = true;
                foreach (var opener in Openers)
                {
                    if(opener == null){
                        flag = false;
                        continue;
                    }
                    if (!opener.IsOpened)
                    {
                        flag = false;
                    }
                }

                return flag;
            }
        }

        [ShowInInspector]
        public override bool IsClosed
        {
            get
            {
                if (Openers == null)
                {
                    return true;
                }

                bool flag = true;
                foreach (var opener in Openers)
                {
                    if(opener == null){
                        flag = false;
                        continue;
                    }
                    if (!opener.IsClosed)
                    {
                        flag = false;
                    }
                }

                return flag;
            }
        }


        [ShowInInspector]
        public override float Progress
        {
            set
            {
                if (Openers == null)
                {
                    return;
                }
                foreach (var opener in Openers)
                {
                    if(opener != null )
                    {
                        opener.Progress = value;
                    }
                }
            }
        }
    }
}