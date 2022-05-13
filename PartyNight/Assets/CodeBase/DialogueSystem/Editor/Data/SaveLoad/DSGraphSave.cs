using System.Collections.Generic;
using System.IO;
using System.Linq;
using CodeBase.DialogueSystem.Editor.Elements;
using CodeBase.DialogueSystem.Editor.Factories;
using CodeBase.DialogueSystem.Editor.Windows;
using CodeBase.Infrastructure.Data;
using CodeBase.Infrastructure.Services.Dialogues;
using CodeBase.Infrastructure.Services.Dialogues.Scriptable_Objects;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace CodeBase.DialogueSystem.Editor.Data.SaveLoad
{
    public class DSGraphSave
    {
        private readonly DSElementFactory _elementFactory;
        private readonly DSGraphView _graphView;

        private DSGraphSaveDataSO _data;
        private string _fullAssetFilePath;
        private string _resourcesFilePath;

        public DSGraphSave(DSElementFactory elementFactory, DSGraphView graphView)
        {
            _elementFactory = elementFactory;
            _graphView = graphView;
        }

        public void SaveData(Dictionary<Node, DSNode> nodes)
        {
            List<DSNode> actualNodes = RemoveDeletedNodes(nodes);
            CreateInstance();
            AttachValues(_data, actualNodes);
            CreateAssets(_data);
            File.WriteAllText(_fullAssetFilePath, _data.ToJson());

            AssetDatabase.SaveAssets();
        }

        private List<DSNode> RemoveDeletedNodes(Dictionary<Node, DSNode> nodes)
            => _graphView.nodes.ToList().Select(graphViewNode => nodes[graphViewNode]).ToList();

        private void CreateInstance()
        {
            _fullAssetFilePath = Application.dataPath + "/CodeBase/DialogueSystem/GraphData/" +
                                 _elementFactory.FileNameTextField.value;
            _resourcesFilePath = "Assets/Resources/DialogueSystem/SavedDialogues/" +
                                 _elementFactory.FileNameTextField.value + ".asset";
            _data = ScriptableObject.CreateInstance<DSGraphSaveDataSO>();
        }

        private void CreateAssets(DSGraphSaveDataSO data)
        {
            Dialogue dialogue = ScriptableObject.CreateInstance<Dialogue>();
            AssetDatabase.CreateAsset(dialogue, _resourcesFilePath);
            List<DialogueCard> cards = dialogue.DialogueCards;
            foreach (DSNodeSaveData node in data.Nodes)
            {
                cards.Add(node.ConvertToDialogueCard());
            }
            SetInitCardAsFirst(cards);
            EditorUtility.SetDirty(dialogue);
            EditorUtility.FocusProjectWindow();
            Selection.activeObject = dialogue;
        }

        private static void SetInitCardAsFirst(List<DialogueCard> cards)
        {
            int initCardIndex = cards.FindIndex(card => card.DialogueNameValue == "init");
            cards.Insert(0, cards[initCardIndex]);
            cards.RemoveAt(initCardIndex + 1);
        }

        private void AttachValues(DSGraphSaveDataSO data, List<DSNode> nodes)
        {
            _data.FileName = _elementFactory.FileNameTextField.value;
            foreach (DSNode node in nodes)
            {
                data.Nodes.Add(node.CollectNodeData());
            }
        }
    }
}