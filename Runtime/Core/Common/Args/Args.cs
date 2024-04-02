using System;
using System.Collections.Generic;
using UnityEngine;
using Pangoo;
using Pangoo.Core.VisualScripting;
using Pangoo.Core.Services;
using Sirenix.OdinInspector;
using UnityEngine.EventSystems;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace Pangoo.Core.Common
{
    [Serializable]
    public class Args
    {
        public static readonly Args EMPTY = new Args();

        // [NonSerialized] private readonly Dictionary<int, Component> selfComponents;
        // [NonSerialized] private readonly Dictionary<int, Component> targetComponents;


        [field: NonSerialized] public GameObject Self { get; private set; }
        [field: NonSerialized] public GameObject Target { get; private set; }

        [field: NonSerialized] public string TargetPath { get; private set; }


        public int TargetIndex { get; set; }

        [ShowInInspector]
        [field: NonSerialized] public DynamicObject dynamicObject { get; private set; }

        [field: NonSerialized] public PlayableDirector playableDirector { get; set; }

        [field: NonSerialized] public string signalAssetName { get; set; }


        [ShowInInspector]
        [field: NonSerialized] public TriggerEvent Trigger { get; private set; }

        [ShowInInspector]
        [field: NonSerialized] public MainService Main { get; set; }

        [field: NonSerialized] public PointerEventData PointerData { get; set; }

        [ShowInInspector]
        [field: NonSerialized] public string PointerPath { get; set; }
        
        [ShowInInspector]
        [field: NonSerialized] public string triggerPath { get; set; }


        public Args Clone => new Args(dynamicObject, this.Self, this.Target)
        {
            TargetPath = TargetPath,
            TargetIndex = TargetIndex,
            playableDirector = playableDirector,
            signalAssetName = signalAssetName,
            Trigger = Trigger,
            Main = Main,
            PointerData = PointerData,
            PointerPath = PointerPath,
            triggerPath=triggerPath,
        };


        public Args()
        {
            // this.selfComponents = new Dictionary<int, Component>();
            // this.targetComponents = new Dictionary<int, Component>();
        }
        public Args(DynamicObject triggerObject) : this(triggerObject, null as GameObject, null as GameObject)
        { }

        public Args(DynamicObject triggerObject, Component target) : this(triggerObject, target, target)
        { }

        public Args(DynamicObject triggerObject, GameObject target) : this(triggerObject, target, target)
        { }

        public Args(DynamicObject triggerObject, Component self, Component target) : this()
        {
            dynamicObject = triggerObject;
            this.Self = self == null ? null : self.gameObject;
            this.Target = target == null ? null : target.gameObject;
        }

        public Args(DynamicObject triggerObject, GameObject self, GameObject target) : this()
        {
            dynamicObject = triggerObject;
            this.Self = self;
            this.Target = target;
        }

        // PUBLIC METHODS: ------------------------------------------------------------------------

        // public T ComponentFromSelf<T>(bool inChildren = false) where T : Component
        // {
        //     return this.GetComponent<T>(this.selfComponents, this.Self, inChildren);
        // }

        // public T ComponentFromTarget<T>(bool inChildren = false) where T : Component
        // {
        //     return this.GetComponent<T>(this.targetComponents, this.Target, inChildren);
        // }

        // public void ChangeSelf(GameObject self)
        // {
        //     if (this.Self == self) return;

        //     this.Self = self;
        //     this.selfComponents.Clear();
        // }

        // public void ChangeSelf<T>(T self) where T : Component
        // {
        //     this.ChangeSelf(self != null ? self.gameObject : null);
        // }

        public void ChangeTarget(GameObject target, string path = null, int index = 0)
        {
            if (this.Target == target) return;

            this.Target = target;
            this.TargetIndex = index;
            this.TargetPath = path;
        }

        public void ChangeTarget<T>(T target) where T : Component
        {
            this.ChangeTarget(target != null ? target.gameObject : null);
        }

        // PRIVATE METHODS: -----------------------------------------------------------------------

        // private TComponent GetComponent<TComponent>(
        //     IDictionary<int, Component> dictionary, GameObject gameObject, bool inChildren)
        //     where TComponent : Component
        // {
        //     if (gameObject == null) return null;

        //     int hash = typeof(TComponent).GetHashCode();
        //     if (!dictionary.TryGetValue(hash, out Component value) || value == null)
        //     {
        //         value = inChildren
        //             ? gameObject.GetComponent<TComponent>()
        //             : gameObject.GetComponentInChildren<TComponent>();

        //         if (value == null) return null;
        //         dictionary[hash] = value;
        //     }

        //     return value as TComponent;
        // }
    }
}