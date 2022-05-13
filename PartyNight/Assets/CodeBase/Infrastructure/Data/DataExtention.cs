using UnityEngine;

namespace CodeBase.Infrastructure.Data
{
    public static class DataExtensions
    {
        public static T ToDeserealized<T>(this string json)
            => JsonUtility.FromJson<T>(json);
        
        public static string ToJson(this object obj)
            => JsonUtility.ToJson(obj);
    }
}