using UnityEngine;
using UnityEngine.Events;

namespace VV.Utility.Tools
{
    public class MaterialController : MonoBehaviour
    {
        private static int _shaderPropertyID;
            
        [SerializeField] private Material material;
        [SerializeField] private string propertyName;
        
        [SerializeField] private float value;
        
        public float Value
        {
            get => value;
            set
            {
                this.value = value;
                UpdateMaterial();
            }
        }

        public UnityEvent OnInitialized = new();

        private void OnValidate()
        {
            _shaderPropertyID = Shader.PropertyToID(propertyName);
            UpdateMaterial();
        }

        public void Init()
        {
            OnInitialized?.Invoke();
        }

        private void UpdateMaterial()
        {
            if(!material || !material.HasProperty(_shaderPropertyID)) return;
            
            material.SetFloat(_shaderPropertyID, value);
        }
    }
}