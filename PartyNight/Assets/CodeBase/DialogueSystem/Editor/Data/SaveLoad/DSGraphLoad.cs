using System.IO;
using CodeBase.DialogueSystem.Editor.Elements;
using CodeBase.DialogueSystem.Editor.Factories;
using CodeBase.DialogueSystem.Editor.Windows;
using UnityEditor.Experimental.GraphView;
using UnityEditor;
using UnityEngine;

namespace CodeBase.DialogueSystem.Editor.Data.SaveLoad
{
    public class DSGraphLoad
    {
        private readonly DSElementFactory _elementFactory;
        private readonly DSNodeFactory _nodeFactory;
        private readonly DSGraphView _graphView;
        private DSGraphSaveDataSO _data;

        public DSGraphLoad(DSElementFactory elementFactory, DSNodeFactory nodeFactory, DSGraphView graphView)
        {
            _elementFactory = elementFactory;
            _graphView = graphView;
            _nodeFactory = nodeFactory;
        }

        public void Load()
        {
            LoadDataAsset();
            InstantiateNodes();
            foreach (DSNodeSaveData saveData in _data.Nodes)
            {
                DSNode node = _nodeFactory.NodesId[saveData.ID];
                for (int i = 0; i < node.ChoiceElements.Count; i++)
                {
                    TryToConnectPort(node, i);
                }
            }
        }

        private void InstantiateNodes()
        {
            foreach (DSNodeSaveData saveData in _data.Nodes)
            {
                _graphView.AddElement(_nodeFactory.CreateNode(saveData));
            }
        }

        private void TryToConnectPort(DSNode node, int i)
        {
            string connectedNodeId = node.Data.ChoiceData[i].ConnectedNodeId;
            if (NodeHaveConnection(with: connectedNodeId))
            {
                Port connectedPort = _nodeFactory.NodesId[connectedNodeId].InputPort;
                Edge connection = node.OutputPorts[i].ConnectTo(connectedPort);
                connectedPort.Connect(connection);
                node.Add(connection);
            }
        }

        private static bool NodeHaveConnection(string with) 
            => !string.IsNullOrEmpty(with);

        private void LoadDataAsset()
        {
            string assetPath = "Assets/CodeBase/DialogueSystem/GraphData/" + _elementFactory.FileNameTextField.value +
                               ".asset";
            string fullAssetFilePath = Application.dataPath + "/CodeBase/DialogueSystem/GraphData/" +
                                       _elementFactory.FileNameTextField.value;

            
            _data = ScriptableObject.CreateInstance<DSGraphSaveDataSO>();
            AssetDatabase.CreateAsset(_data, assetPath);
            JsonUtility.FromJsonOverwrite(File.ReadAllText(fullAssetFilePath), _data);
        }
    }
}