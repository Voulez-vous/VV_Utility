using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VV.Utility.Tools
{
    public class AnimationController : MonoBehaviour
    {
        [SerializeField] List<Animator> animators;
        [SerializeField] bool stopped = true;
        [SerializeField] float mooseDelayDuration = 2.0f;

        private void Start()
        {
            if (stopped)
                Stop();
        }

        public void Play()
        {
            StopAllCoroutines(); 
            StartCoroutine(PlayRoutine());
        }

        private IEnumerator PlayRoutine()
        {
            yield return new WaitForSeconds(mooseDelayDuration);
            foreach (Animator animator in animators)
            {
                animator.enabled = true;
            }
        }
        public void Stop()
        {
            StopAllCoroutines();
            foreach (Animator animator in animators)
            {
                animator.enabled = false;
            }
        }
    }
}