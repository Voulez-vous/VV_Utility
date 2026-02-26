using System.Runtime.CompilerServices;
using UnityEngine;

namespace VV.Utility
{
    public static class Vector3Extensions
    {
        private const float Epsilon = 0.0001f;

        // Sets any values of the Vector3
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 With(this Vector3 vector, float? x = null, float? y = null, float? z = null)
        {
            return new Vector3(x ?? vector.x, y ?? vector.y, z ?? vector.z);
        }

        // Adds to any values of the Vector3
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 Add(this Vector3 vector, float? x = null, float? y = null, float? z = null)
        {
            return new Vector3(vector.x + (x ?? 0), vector.y + (y ?? 0), vector.z + (z ?? 0));
        }

        #region Comparison

        #region CompareWithVector

        // checks magnitude difference between 2 Vectors
        // returns : 1 if v1.magnitude > v1.magnitude
        // 0 if v1.magnitude == v2.magnitude
        // -1 if v1.magnitude < v2.magnitude
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Compare(this Vector3 v1, Vector3 v2)
        {
            if (v1.sqrMagnitude > v2.sqrMagnitude)
                return 1;
            else if (Mathf.Approximately(v1.sqrMagnitude, v2.sqrMagnitude))
                return 0;
            else if (v1.sqrMagnitude < v2.sqrMagnitude)
                return -1;

            return 0;
        }

        // checks magnitude is less than parameter vector
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool ShorterThan(this Vector3 v1, Vector3 v2)
        {
            return v1.sqrMagnitude < v2.sqrMagnitude;
        }

        // checks magnitude between 2 Vector3
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsSizeApproximately(this Vector3 v1, Vector3 v2)
        {
            return Mathf.Abs(v1.sqrMagnitude - v2.sqrMagnitude) < Epsilon;
        }

        // checks magnitude is greater than parameter vector
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool LongerThan(this Vector3 v1, Vector3 v2)
        {
            return v1.sqrMagnitude > v2.sqrMagnitude;
        }

        #endregion CompareWithVector

        #region CompareWithScalar

        // checks magnitude is shorter than given magnitude
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool ShorterThan(this Vector3 v2, float magnitude)
        {
            return v2.sqrMagnitude < (magnitude * magnitude);
        }

        // checks magnitude is almost zero
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsZero(this Vector3 v1)
        {
            return v1.sqrMagnitude < (Epsilon * Epsilon);
        }

        // checks magnitude between 2 Vector3
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsSizeApproximately(this Vector3 v1, float magnitude)
        {
            return Mathf.Approximately(v1.sqrMagnitude, (magnitude * magnitude));
        }

        // checks magnitude is greater than parameter vector
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool LongerThan(this Vector3 v1, float magnitude)
        {
            return v1.sqrMagnitude > (magnitude * magnitude);
        }

        #endregion CompareWithScalar

        #endregion Comparison

        #region Scale

        // Scales vector using a simple scalar
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Scale(this ref Vector3 v1, float scale)
        {
            v1.Scale(new Vector3(scale, scale, scale));
        }

        // Scales vector using a simple scalar
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 GetScaled(this Vector3 v1, Vector3 scale)
        {
            Vector3 v = new Vector3(v1.x, v1.y, v1.z);
            v.Scale(scale);
            return v;
        }

        // Scales vector using a simple scalar
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 GetScaled(this Vector3 v1, float scale)
        {
            Vector3 v = new Vector3(v1.x, v1.y, v1.z);
            v.Scale(new Vector3(scale, scale, scale));
            return v;
        }

        #endregion Scale

        #region Size

        // Resizes this vector with given magnitude
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Resize(this ref Vector3 v1, float magnitude)
        {
            v1.Scale(magnitude / v1.magnitude);
        }

        // returns new vector with given magnitude
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 GetWithSize(this Vector3 v1, float magnitude)
        {
            return (new Vector3(v1.x, v1.y, v1.z)).GetScaled(magnitude / v1.magnitude);
        }

        // Reduces vector by amount of magnitude
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ReduceBy(this ref Vector3 v1, float magnitude)
        {
            v1 -= (v1.normalized * magnitude);
        }

        // Get new vector with size reduced by given magnitude
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 GetReducedBy(this Vector3 v1, float magnitude)
        {
            return v1 - (v1.normalized * magnitude);
        }

        // Reduces vector by amount of magnitude
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Augment(this ref Vector3 v1, float magnitude)
        {
            v1 += (v1.normalized * magnitude);
        }

        // Get new vector with size reduced by given magnitude
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 GetAugmentedBy(this Vector3 v1, float magnitude)
        {
            return v1 + (v1.normalized * magnitude);
        }

        #endregion Size

        #region Conversion

        // Converts a Vector3 to a Vector3 losing the value of the y
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 ToVector2(this Vector3 v3)
        {
            return new Vector2(v3.x, v3.z);
        }

        #endregion Conversion
    }
    
    #region staticDirections

    public static class VVector3
    {
        public static readonly Vector3 zero      = new(0f, 0f, 0f);
        
        public static readonly Vector3 up      = new(0f, 1f, 0f);
        public static readonly Vector3 down    = new(0f, -1f, 0f);
        public static readonly Vector3 right   = new(1f, 0f, 0f);
        public static readonly Vector3 left    = new(-1f, 0f, 0f);
        public static readonly Vector3 forward = new(0f, 0f, 1f);
        public static readonly Vector3 back    = new(0f, 0f, -1f);

        // XY
        public static readonly Vector3 up_right   = new(1f, 1f, 0f);
        public static readonly Vector3 down_right = new(1f, -1f, 0f);
        public static readonly Vector3 down_left  = new(-1f, -1f, 0f);
        public static readonly Vector3 up_left    = new(-1f, 1f, 0f);

        // YZ
        public static readonly Vector3 up_forward   = new(0f, 1f, 1f);
        public static readonly Vector3 down_forward = new(0f, -1f, 1f);
        public static readonly Vector3 down_back    = new(0f, -1f, -1f);
        public static readonly Vector3 up_back      = new(0f, 1f, -1f);

        // XZ
        public static readonly Vector3 right_forward = new(1f, 0f, 1f);
        public static readonly Vector3 right_back    = new(1f, 0f, -1f);
        public static readonly Vector3 left_back     = new(-1f, 0f, -1f);
        public static readonly Vector3 left_forward  = new(-1f, 0f, 1f);

        // XYZ
        public static readonly Vector3 right_up_forward   = new(1f, 1f, 1f);
        public static readonly Vector3 right_down_forward = new(1f, -1f, 1f);
        public static readonly Vector3 right_up_back      = new(1f, 1f, -1f);
        public static readonly Vector3 right_down_back    = new(1f, -1f, -1f);
        public static readonly Vector3 left_up_forward    = new(-1f, 1f, 1f);
        public static readonly Vector3 left_down_forward  = new(-1f, -1f, 1f);
        public static readonly Vector3 left_up_back       = new(-1f, 1f, -1f);
        public static readonly Vector3 left_down_back     = new(-1f, -1f, -1f);
        
        
        // NORMALIZED

        public static readonly float root2Inv = 0.70710678118f;
        public static readonly float root3Inv = 0.577350259f;
        
        // XY
        public static readonly Vector3 up_right_normalized   = new(root2Inv, root2Inv, 0f);
        public static readonly Vector3 down_right_normalized = new(root2Inv, -root2Inv, 0f);
        public static readonly Vector3 down_left_normalized  = new(-root2Inv, -root2Inv, 0f);
        public static readonly Vector3 up_left_normalized    = new(-root2Inv, root2Inv, 0f);

        // YZ
        public static readonly Vector3 up_forward_normalized   = new(0f, root2Inv, root2Inv);
        public static readonly Vector3 down_forward_normalized = new(0f, -root2Inv, root2Inv);
        public static readonly Vector3 down_back_normalized    = new(0f, -root2Inv, -root2Inv);
        public static readonly Vector3 up_back_normalized      = new(0f, root2Inv, -root2Inv);

        // XZ
        public static readonly Vector3 right_forward_normalized = new(root2Inv, 0f, root2Inv);
        public static readonly Vector3 right_back_normalized    = new(root2Inv, 0f, -root2Inv);
        public static readonly Vector3 left_back_normalized     = new(-root2Inv, 0f, -root2Inv);
        public static readonly Vector3 left_forward_normalized  = new(-root2Inv, 0f, root2Inv);

        // XYZ
        public static readonly Vector3 right_up_forward_normalized   = new(root3Inv, root3Inv, root3Inv);
        public static readonly Vector3 right_down_forward_normalized = new(root3Inv, -root3Inv, root3Inv);
        public static readonly Vector3 right_up_back_normalized      = new(root3Inv, root3Inv, -root3Inv);
        public static readonly Vector3 right_down_back_normalized    = new(root3Inv, -root3Inv, -root3Inv);
        public static readonly Vector3 left_up_forward_normalized    = new(-root3Inv, root3Inv, root3Inv);
        public static readonly Vector3 left_down_forward_normalized  = new(-root3Inv, -root3Inv, root3Inv);
        public static readonly Vector3 left_up_back_normalized       = new(-root3Inv, root3Inv, -root3Inv);
        public static readonly Vector3 left_down_back_normalized     = new(-root3Inv, -root3Inv, -root3Inv);
    }

    #endregion
}