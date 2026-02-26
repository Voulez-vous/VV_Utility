using System.Runtime.CompilerServices;
using UnityEngine;

namespace VV.Utility
{
    public static class CameraExtensions
    {
        private const float Epsilon = 0.0001f;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 ScreenPointToPlaneIntersection(this Camera cam, Vector3 position, Plane plane)
        {
            return cam.ScreenPointToRay(position).GetIntersection(plane);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 ScreenPointToPlaneIntersection(this Camera cam, Vector3 position, Vector3 planeNormal, Vector3 planeOrigin)
        {
            return cam.ScreenPointToRay(position).GetIntersection(new Plane(planeNormal, planeOrigin));
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Rect BoxToScreenRect(this Camera cam, Vector3 center, Vector3 size)
        {
            Bounds bounds = new Bounds(center, size);

            Vector2 min = Vector2.positiveInfinity;
            Vector2 max = Vector2.negativeInfinity;

            bounds.ForEachVertices(vertex =>
            {
                Vector3 p = cam.WorldToViewportPoint(vertex);
                if (p.z < 0) return;

                min = new Vector2(Mathf.Min(p.x, min.x), Mathf.Min(p.y, min.y));
                max = new Vector2(Mathf.Max(p.x, max.x), Mathf.Max(p.y, max.y));
            });

            return new Rect(min.x , min.y, max.x - min.x, max.y - min.y);
        }
    }
}