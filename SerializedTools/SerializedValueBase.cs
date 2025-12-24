using System;
using UnityEngine;

namespace VV.Scoring.Data
{
    /// <summary>
    /// SerializedValue classes are a set of class to allow Editor SerializedReference usages.
    /// Used for editor tools.
    /// Unity needs an implemented inheritor to show correctly the types through SerializedReference.
    /// </summary>
    [Serializable]
    public abstract class SerializedValueBase
    {
        public abstract object GetValue();
    }
    
    /// <summary>
    /// Specific implementation for integers.
    /// </summary>
    [Serializable]
    public class IntValue : SerializedValueBase, ISerializedValue
    {
        public int value;
        public override object GetValue() => value;
    }

    /// <summary>
    /// Specific implementation for floats.
    /// </summary>
    [Serializable]
    public class FloatValue : SerializedValueBase, ISerializedValue
    {
        public float value;
        public override object GetValue() => value;
    }

    /// <summary>
    /// Specific implementation for strings.
    /// </summary>
    [Serializable]
    public class StringValue : SerializedValueBase, ISerializedValue
    {
        public string value;
        public override object GetValue() => value;
    }

    /// <summary>
    /// Specific implementation for booleans.
    /// </summary>
    [Serializable]
    public class BoolValue : SerializedValueBase, ISerializedValue
    {
        public bool value;
        public override object GetValue() => value;
    }
    
    /// <summary>
    /// Specific implementation for Unity Objects (ScriptableObject, Monobehaviour, etc).
    /// </summary>
    [Serializable]
    public class ObjectValue : SerializedValueBase, ISerializedValue
    {
        public UnityEngine.Object value;
        public override object GetValue() => value;
    }
    
    /// <summary>
    /// Specific implementation for Vector3.
    /// </summary>
    [Serializable]
    public class Vector3Value : SerializedValueBase, ISerializedValue
    {
        public Vector3 value;
        public override object GetValue() => value;
    }
}