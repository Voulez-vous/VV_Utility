using System;
using UnityEngine;
using VV.Scoring.Data;

namespace VV.Scoring
{
    /// <summary>
    /// Wrapper used to make the SerializeReference work.
    /// </summary>
    [Serializable]
    public class SerializedValueWrapper
    {
        [SerializeReference]
        private ISerializedValue value;

        public object Value
        {
            get => value?.GetValue();
            set
            {
                if (value is not ISerializedValue serializedValue) return;
                this.value = serializedValue;
            }
        }
    }
}