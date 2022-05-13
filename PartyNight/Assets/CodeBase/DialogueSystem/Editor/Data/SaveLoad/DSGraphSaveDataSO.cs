using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.DialogueSystem.Editor.Data.SaveLoad
{
    public class DSGraphSaveDataSO : ScriptableObject
    {
        [field: SerializeField] public string FileName { get; set; }
        [field: SerializeField] public List<DSNodeSaveData> Nodes { get; set; } = new List<DSNodeSaveData>();
    }
}