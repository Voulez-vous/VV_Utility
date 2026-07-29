using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace VV.Utility.Tools
{
    /// <summary>
    /// Place this script on the same GameObject as the Animator.
    /// Use Animation Events to call Trigger("EventName").
    /// </summary>
    [RequireComponent(typeof(Animator))]
    public class AnimationTrigger : MonoBehaviour
    {
        [Serializable]
        public class AnimationEventEntry
        {
            public string eventName;
            public UnityEvent onTrigger = new();
            public bool onlyTriggerOnce;
        
            [HideInInspector] public bool triggered;
        }

        [SerializeField]
        private List<AnimationEventEntry> animationEvents = new();

        private Dictionary<string, AnimationEventEntry> eventLookup;

        private void Awake()
        {
            eventLookup = new Dictionary<string, AnimationEventEntry>();

            foreach (AnimationEventEntry entry in animationEvents)
            {
                if (string.IsNullOrEmpty(entry.eventName))
                    continue;

                eventLookup.TryAdd(entry.eventName, entry);
            }
        }

        /// <summary>
        /// Called from Animation Events
        /// </summary>
        public void Trigger(string eventName)
        {
            if (!eventLookup.TryGetValue(eventName, out AnimationEventEntry entry))
            {
                Debug.LogWarning($"[AnimationTrigger] No animation event named '{eventName}' on {name}");
                return;
            }

            if (entry.onlyTriggerOnce && entry.triggered)
                return;

            entry.triggered = true;
            entry.onTrigger?.Invoke();
        }

        public void ResetTriggers()
        {
            foreach (AnimationEventEntry entry in animationEvents)
                entry.triggered = false;
        }
    }
}