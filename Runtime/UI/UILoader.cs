using GameFramework;
using GameFramework.Event;
using UnityGameFramework.Runtime;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Pangoo
{
    public class UILoader : IReference
    {
        private Dictionary<int, Action<UIFormLogic>> dicCallback;
        private Dictionary<int, Action<UIFormLogic>> dicCloseCallback;

        private Dictionary<int, UIFormLogic> dicSerial2Entity;

        private List<int> tempList;

        public object Owner
        {
            get;
            private set;
        }

        public UILoader()
        {
            dicSerial2Entity = new Dictionary<int, UIFormLogic>();
            dicCallback = new Dictionary<int, Action<UIFormLogic>>();
            dicCloseCallback = new Dictionary<int, Action<UIFormLogic>>();
            tempList = new List<int>();
            Owner = null;
        }


        public int ShowUI(UIPanelData info, Action<UIFormLogic> onShowSuccess = null, Action<UIFormLogic> onCloseSuccess = null)
        {
            int serialId = PangooEntry.UI.ShowUI(info);
            if (serialId != 0)
            {
                if (onShowSuccess != null)
                {
                    dicCallback.Add(serialId, onShowSuccess);
                }

                if (onCloseSuccess != null)
                {
                    dicCloseCallback.Add(serialId, onCloseSuccess);
                }
            }

            return serialId;
        }


        private void OnShowUIFormSuccess(object sender, GameEventArgs e)
        {
            OpenUIFormSuccessEventArgs ne = (OpenUIFormSuccessEventArgs)e;
            if (ne == null)
            {
                return;
            }

            Action<UIFormLogic> callback = null;
            if (!dicCallback.TryGetValue(ne.UIForm.SerialId, out callback))
            {
                return;
            }

            dicSerial2Entity.Add(ne.UIForm.SerialId, ne.UIForm.Logic);
            callback?.Invoke(ne?.UIForm?.Logic);
        }

        private void OnShowUIFormFail(object sender, GameEventArgs e)
        {
            OpenUIFormFailureEventArgs ne = (OpenUIFormFailureEventArgs)e;
            if (ne == null)
            {
                return;
            }

            if (dicCallback.ContainsKey(ne.SerialId))
            {
                dicCallback.Remove(ne.SerialId);
                Log.Warning("{0} Show entity failure with error message '{1}'.", Owner.ToString(), ne.ErrorMessage);
            }
        }

        private void OnCloseUIForm(object sender, GameEventArgs e)
        {
            CloseUIFormCompleteEventArgs ne = (CloseUIFormCompleteEventArgs)e;
            if (ne == null)
            {
                return;
            }

            Action<UIFormLogic> callback = null;
            if (!dicCloseCallback.TryGetValue(ne.SerialId, out callback))
            {
                return;
            }


            if (!dicSerial2Entity.TryGetValue(ne.SerialId, out UIFormLogic logic))
            {
                callback?.Invoke(logic);
            }


            if (dicCallback.ContainsKey(ne.SerialId))
            {
                dicCallback.Remove(ne.SerialId);
            }

            if (dicCloseCallback.ContainsKey(ne.SerialId))
            {
                dicCloseCallback.Remove(ne.SerialId);
            }
        }


        public static UILoader Create(object owner)
        {
            UILoader loader = ReferencePool.Acquire<UILoader>();
            loader.Owner = owner;
            PangooEntry.Event.Subscribe(OpenUIFormSuccessEventArgs.EventId, loader.OnShowUIFormSuccess);
            PangooEntry.Event.Subscribe(OpenUIFormFailureEventArgs.EventId, loader.OnShowUIFormFail);
            PangooEntry.Event.Subscribe(CloseUIFormCompleteEventArgs.EventId, loader.OnCloseUIForm);



            return loader;
        }

        public void Clear()
        {
            Owner = null;
            dicSerial2Entity.Clear();
            dicCallback.Clear();
            PangooEntry.Event.Unsubscribe(OpenUIFormSuccessEventArgs.EventId, OnShowUIFormSuccess);
            PangooEntry.Event.Unsubscribe(OpenUIFormFailureEventArgs.EventId, OnShowUIFormFail);
            PangooEntry.Event.Unsubscribe(CloseUIFormCompleteEventArgs.EventId, OnCloseUIForm);
        }
    }
}
