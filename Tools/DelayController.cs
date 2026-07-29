using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace VV.Utility.Tools
{
    public class DelayController : MonoBehaviour
    {
        private enum DalayTrigger
        {
            Start,
            Awake,
            Enable,
            Disable,
            Destroy,
            None
        }
        
        [SerializeField] private float delay = 0.5f;
        [SerializeField] private DalayTrigger triggerOn = DalayTrigger.None;
        
        public UnityEvent onExecuteDelay;

        #region Unity Functions
        
        private void Awake()
        {
            if(triggerOn == DalayTrigger.Awake)
                StartCoroutine(ExecuteDelay());
        }

        public void Start()
        {
            if(triggerOn == DalayTrigger.Start)
                StartCoroutine(ExecuteDelay());
        }

        private void OnEnable()
        {
            if(triggerOn == DalayTrigger.Enable)
                StartCoroutine(ExecuteDelay());
        }

        private void OnDisable()
        {
            if(triggerOn == DalayTrigger.Disable)
                StartCoroutine(ExecuteDelay());
        }

        private void OnDestroy()
        {
            if(triggerOn == DalayTrigger.Destroy)
                StartCoroutine(ExecuteDelay());
        }

        public void TriggerDelay()
        {
            StartCoroutine(ExecuteDelay());
        }
        
        #endregion
        
        private IEnumerator ExecuteDelay()
        {
            yield return new WaitForSeconds(delay);
            onExecuteDelay?.Invoke();
        }
    }
}