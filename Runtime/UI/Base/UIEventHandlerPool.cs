using System;
using System.Collections.Generic;
using GameFramework;
using UnityGameFramework.Runtime;

namespace SteamClient.Hotfix
{
    public class UIEventHandlerPool
    {
        private readonly GameFrameworkMultiDictionary<UIEventId, Action<object>> m_EventHandlers;
        private readonly Dictionary<UIEventId, LinkedListNode<Action<object>>> m_CachedNodes;
        private readonly Dictionary<UIEventId, LinkedListNode<Action<object>>> m_TempNodes;

        public UIEventHandlerPool()
        {
            m_EventHandlers = new GameFrameworkMultiDictionary<UIEventId, Action<object>>();
            m_CachedNodes = new Dictionary<UIEventId, LinkedListNode<Action<object>>>();
            m_TempNodes = new Dictionary<UIEventId, LinkedListNode<Action<object>>>();
        }

        public bool Check(UIEventId id, Action<object> handler)
        {
            if (handler == null)
            {
                throw new Exception("Event handler is invalid.");
            }

            return m_EventHandlers.Contains(id, handler);
        }

        public void Subscribe(UIEventId id, Action<object> handler)
        {
            if (Check(id, handler))
            {
                Log.Error($"不能重复订阅相同的UI事件回调Action id: {id}");
            }
            else
            {
                m_EventHandlers.Add(id, handler);
            }
        }

        public void Unsubscribe(UIEventId id, Action<object> handler)
        {
            if (handler == null)
            {
                throw new Exception("Event handler is invalid.");
            }

            if (m_CachedNodes.Count > 0)
            {
                foreach (KeyValuePair<UIEventId, LinkedListNode<Action<object>>> cachedNode in m_CachedNodes)
                {
                    if (cachedNode.Value != null && cachedNode.Value.Value == handler)
                    {
                        m_TempNodes.Add(cachedNode.Key, cachedNode.Value.Next);
                    }
                }

                if (m_TempNodes.Count > 0)
                {
                    foreach (KeyValuePair<UIEventId, LinkedListNode<Action<object>>> cachedNode in m_TempNodes)
                    {
                        m_CachedNodes[cachedNode.Key] = cachedNode.Value;
                    }

                    m_TempNodes.Clear();
                }
            }

            if (!m_EventHandlers.Remove(id, handler))
            {
                Log.Error($"UI事件id : {id} 不存在指定的回调，移除失败");
            }
        }

        public void HandleEvent(UIEventId id, object eventData)
        {
            GameFrameworkLinkedListRange<Action<object>> range;
            if (m_EventHandlers.TryGetValue(id, out range))
            {
                LinkedListNode<Action<object>> current = range.First;
                while (current != null && current != range.Terminal)
                {
                    m_CachedNodes[id] = current.Next != range.Terminal ? current.Next : null;
                    current.Value(eventData);
                    current = m_CachedNodes[id];
                }

                m_CachedNodes.Remove(id);
            }
        }
    }
}