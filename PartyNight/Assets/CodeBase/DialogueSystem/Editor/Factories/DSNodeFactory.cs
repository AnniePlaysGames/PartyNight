using System.Collections.Generic;
using CodeBase.DialogueSystem.Editor.Data.SaveLoad;
using CodeBase.DialogueSystem.Editor.Elements;
using CodeBase.DialogueSystem.Editor.Utilities;
using CodeBase.Infrastructure.Services.Dialogues.ChoiceData;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;

namespace CodeBase.DialogueSystem.Editor.Factories
{
    public class DSNodeFactory
    {
        private const string PortTitle = "";
        private readonly DSElementFactory _elementFactory;
        private DSNode _node;
        public Dictionary<string, DSNode> NodesId { get; }
        public Dictionary<Node, DSNode> Nodes { get; }


        public DSNodeFactory(DSElementFactory elementFactory)
        {
            Nodes = new Dictionary<Node, DSNode>();
            NodesId = new Dictionary<string, DSNode>();
            _elementFactory = elementFactory;
        }


        public DSNode CreateNode(DSNodeSaveData data)
        {
            _node = new DSNode
            {
                Data = data,
                DialogueName = InitDialogueNameField(data.DialogueNameValue),
                CharacterDropdown = _elementFactory.CreateEnumField(data.CharacterValue),
                EmotionDropdown = _elementFactory.CreateEnumField(data.EmotionValue),
                CharacterPhrase = InitCharacterPhraseField(data.CharacterPhraseValue),
                Position = data.Position,
                ID = data.ID
            };
            _node.InputPort = InitInputPort();
            _node.OutputPorts = InitOutputPorts();
            _node.ChoiceElements = InitChoiceElements();
            _node.SetPosition(data.Position);

            DrawNodeContainers();
            Nodes.Add(_node, _node);
            NodesId.TryAdd(_node.ID, _node);
            return _node;
        }

        private TextField InitDialogueNameField(string value)
        {
            return _elementFactory.CreateTextField(value, null, callback =>
            {
                TextField target = (TextField) callback.target;
                target.value = callback.newValue.RemoveWhitespaces().RemoveSpecialCharacters();
                _node.DialogueName = target;
            });
        }

        private List<ChoiceElement> InitChoiceElements()
        {
            if (_node.Data.ChoiceData.Count == 0)
            {
                for (int i = 0; i < Utilities.Constants.DefaultChoiceCount; i++)
                {
                    _node.Data.ChoiceData.Add(new ChoiceData());
                }
            }
            
            return new List<ChoiceElement>()
            {
                new ChoiceElement(_elementFactory, this, _node.OutputPorts[0], _node.Data.ChoiceData[0]),
                new ChoiceElement(_elementFactory, this, _node.OutputPorts[1], _node.Data.ChoiceData[1])
            };
        }

        private TextField InitCharacterPhraseField(string value)
        {
            return _elementFactory.CreateTextArea(value, null, callback =>
            {
                TextField target = (TextField) callback.target;
                _node.CharacterPhrase = target;
            });;
        }

        private Port InitInputPort() 
            => _elementFactory.CreatePort(_node, "", Orientation.Horizontal, Direction.Input);

        private Port[] InitOutputPorts()
        {
            Port[] result = new Port[Utilities.Constants.DefaultChoiceCount];
            for (int i = 0; i < result.Length; i++)
            {
                Port port = _elementFactory.CreatePort(_node,PortTitle, Orientation.Horizontal, Direction.Output, Port.Capacity.Single);
                result[i] = port;
                _node.OutputPorts[i] = port;
            }

            return result;
        }

        private void DrawNodeContainers()
        {
            DrawTitleContainer();
            DrawInputContainer();
            DrawOutputContainers();
            DrawExtensionContainer();
            _node.RefreshExpandedState();
        }

        private void DrawTitleContainer() 
            => _node.titleContainer.Insert(0, _node.DialogueName);

        private void DrawInputContainer() 
            => _node.inputContainer.Add(_node.InputPort);

        private void DrawOutputContainers()
        {
            for (int i = 0; i < _node.OutputPorts.Length; i++)
            {
                VisualElement choiceWithPort = new VisualElement();
                choiceWithPort.Add(_node.ChoiceElements[i]);
                choiceWithPort.Add(_node.OutputPorts[i]);
                choiceWithPort.AddToClassList(Utilities.Constants.HorizontalGroupStyle);
                _node.outputContainer.Add(choiceWithPort);
            }
        }

        private void DrawExtensionContainer()
        {
            VisualElement customDataContainer = new VisualElement();
            Foldout textFoldout = _elementFactory.CreateFoldout();

            textFoldout.Add(_node.CharacterPhrase);
            textFoldout.Add(_node.CharacterDropdown);
            textFoldout.Add(_node.EmotionDropdown);
            customDataContainer.Add(textFoldout);

            _node.extensionContainer.Add(customDataContainer);
        }
    }
}