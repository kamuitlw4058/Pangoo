using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityGameFramework.Runtime;
using System;
using GameFramework;
using GameFramework.Event;

namespace Pangoo
{

    public class EventHelper : IReference
    {
        private GameFrameworkMultiDictionary<int, EventHandler<GameEventArgs>> dicEventHandler = new GameFrameworkMultiDictionary<int, EventHandler<GameEventArgs>>();

        public object Owner
        {
            get;
            private set;
        }

        public EventHelper()
        {
            dicEventHandler = new GameFrameworkMultiDictionary<int, EventHandler<GameEventArgs>>();
            Owner = null;
        }

        public void Fire(object sender, GameEventArgs e){
              PangooEntry.Event.Fire(sender, e);
        }


        public void FireNow(object sender, GameEventArgs e){
              PangooEntry.Event.FireNow(sender, e);
        }

        

        public void Subscribe(int id, EventHandler<GameEventArgs> handler)
        {
            if (handler == null)
            {
                throw new Exception("Event handler is invalid.");
            }

            dicEventHandler.Add(id, handler);
            PangooEntry.Event.Subscribe(id, handler);
        }
          

        public void UnSubscribe(int id, EventHandler<GameEventArgs> handler)
        {
            if (!dicEventHandler.Remove(id, handler))
            {
                throw new Exception(Utility.Text.Format("Event '{0}' not exists specified handler.", id.ToString()));
            }

            PangooEntry.Event.Unsubscribe(id, handler);
        }

        public void UnSubscribeAll()
        {
            if (dicEventHandler == null)
                return;

            foreach (var item in dicEventHandler)
            {
                foreach (var eventHandler in item.Value)
                {
                    PangooEntry.Event.Unsubscribe(item.Key, eventHandler);
                }
            }

            dicEventHandler.Clear();
        }

        public static EventHelper Create(object owner)
        {
            EventHelper eventSubscriber = ReferencePool.Acquire<EventHelper>();
            eventSubscriber.Owner = owner;

            return eventSubscriber;
        }

        public void Clear()
        {
            dicEventHandler.Clear();
            Owner = null;
        }
    }
}