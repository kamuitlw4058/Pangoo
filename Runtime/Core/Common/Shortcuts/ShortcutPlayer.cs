using UnityEngine;
using Pangoo.Core.Characters;

namespace Pangoo.Core.Common
{
    public static class ShortcutPlayer
    {
        // PROPERTIES: ----------------------------------------------------------------------------

        public static Character Instance { get; private set; }

        public static Transform Transform => Instance != null ? Instance.CachedTransfrom : null;

        // PUBLIC METHODS: ------------------------------------------------------------------------

        // public static TComponent Get<TComponent>() where TComponent : Component
        // {
        //     return Instance != null ? Instance.Get<TComponent>() : null;
        // }

        public static void Change(Character character)
        {
            Instance = character != null ? character : null;
        }
    }
}