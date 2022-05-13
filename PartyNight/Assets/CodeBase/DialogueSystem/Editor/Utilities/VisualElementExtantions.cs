using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;

namespace CodeBase.DialogueSystem.Editor.Utilities
{
    public static class VisualElementExtantions
    {
        public static void AddSeveral<T>(this VisualElement parent, T[] childArray) where T : VisualElement
        {
            foreach (T child in childArray)
            {
                parent.Add(child);
            }
        }

        [CanBeNull] public static Node FirstConnectedNode(this Port port) 
            => port.connections.FirstOrDefault()?.input.node;
        
        public static bool TryAdd<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, TKey key, TValue value)
        {
            if (dictionary.ContainsKey(key))
            {
                return false;
            }
            dictionary.Add(key, value);
            return true;
        }
        public static bool TryAdd<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, KeyValuePair<TKey,TValue> value)
        {
            return TryAdd(dictionary, value.Key, value.Value);
        }
    }
}