using UnityEngine;

namespace CodeBase.Extensions
{
    public static class VectorExtensions
    {
        public static Vector3 Clamp01(this Vector3 vector) 
            => new Vector3(Mathf.Clamp01(vector.x), Mathf.Clamp01(vector.y), Mathf.Clamp01(vector.z));

        public static Vector2 Clamp01(this Vector2 vector) 
            => new Vector3(Mathf.Clamp01(vector.x), Mathf.Clamp01(vector.y));
    }
}