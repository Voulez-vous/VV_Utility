using System;
using UnityEngine;

namespace VV.ID
{
    [Serializable]
    public struct VVGuid : IEquatable<Guid>
    {
        private Guid guid;
        public readonly Guid GetGUID => guid;
        
        [SerializeField]
        private string vvidSerialized;
        public VVGuid(Guid aNewGuid)
        {
            guid = aNewGuid;
            vvidSerialized = guid.ToString("N");
        }

        public VVGuid(string aNewGuid) 
        { 
            Guid.TryParse(aNewGuid, out guid);
            vvidSerialized = guid.ToString("N");
        }

        public void Init()
        {
#if UNITY_EDITOR
            if (string.IsNullOrEmpty(vvidSerialized) || vvidSerialized.Equals(Guid.Empty.ToString()))
            {
                guid = Guid.NewGuid();
                vvidSerialized = guid.ToString("N");
            }
            else
            {
#endif
                Guid.TryParse(vvidSerialized, out var newguid);
                guid = newguid;
#if UNITY_EDITOR
            }
#endif
        }
        
#if UNITY_EDITOR
        public void Reset()
        {
            guid = Guid.NewGuid();
            vvidSerialized = guid.ToString("N");
        }
#endif

        public static implicit operator VVGuid(string value) => new(value);
        public static implicit operator VVGuid(Guid value) => new(value);
        public static implicit operator Guid(VVGuid other) => new(other.ToString());

        public static bool operator ==(VVGuid lhs, VVGuid rhs) => lhs.Equals(rhs);
        public static bool operator ==(VVGuid lhs, Guid rhs) => lhs.Equals(rhs);
        public static bool operator !=(VVGuid obj1, VVGuid obj2) => !(obj1 == obj2);
        public static bool operator !=(VVGuid obj1, Guid obj2) => !(obj1 == obj2);


        public bool Equals(VVGuid other)
        {
            return guid.Equals(other.GetGUID);
        }
        public bool Equals(Guid other)
        {
            return guid.Equals(other);
        }

        public override bool Equals(object obj)
        {
            return obj is Guid other && Equals(other);
        }

        public override int GetHashCode()
        {
            return guid.GetHashCode();
        }

        public override string ToString()
        {
            return guid.ToString();
        }

        public string ToString(string param)
        {
            return guid.ToString(param);
        }

        public bool IsEmpty()
        {
            return guid == Guid.Empty;
        }
    }
}
