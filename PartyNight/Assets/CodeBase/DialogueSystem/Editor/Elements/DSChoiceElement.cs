using CodeBase.DialogueSystem.Editor.Factories;
using CodeBase.DialogueSystem.Editor.Utilities;
using CodeBase.Infrastructure.Services.Dialogues.ChoiceData;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;

namespace CodeBase.DialogueSystem.Editor.Elements
{
    public class ChoiceElement : VisualElement
    {
        private readonly DSNodeFactory _nodeFactory;
        private readonly Port _outputPort;
        private readonly TextField _choiceText;
        private readonly ImpactContainer _impactContainer;
        private readonly ChoiceData _data;

        public ChoiceElement(DSElementFactory elementFactory, DSNodeFactory nodeFactory, Port outputPort, ChoiceData data)
        {
            _nodeFactory = nodeFactory;
            _outputPort = outputPort;
            _data = data;
            _choiceText = elementFactory.CreateTextArea(_data.ChoiceTextValue);
            _impactContainer = new ImpactContainer(_data.Impact);

            Add(_choiceText);
            Add(_impactContainer);
        }

        public ChoiceData GetChoiceElementData()
        {
            _data.Impact = _impactContainer.GetImpactData();
            _data.ChoiceTextValue = _choiceText.value;
            _data.ConnectedNodeId = FindConnectedNodeId();
            return _data;
        }

        private string FindConnectedNodeId()
        {
            Node firstConnectedNode = _outputPort.FirstConnectedNode();
            return firstConnectedNode != null ? _nodeFactory.Nodes[firstConnectedNode].ID : null;
        }
    }
}