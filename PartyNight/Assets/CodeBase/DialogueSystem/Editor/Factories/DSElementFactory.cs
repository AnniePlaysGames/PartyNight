using System;
using CodeBase.DialogueSystem.Editor.Elements;
using CodeBase.DialogueSystem.Editor.Utilities;
using UnityEditor.Experimental.GraphView;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace CodeBase.DialogueSystem.Editor.Factories
{
    public class DSElementFactory
    {
        private const string DefaultFileName = "DialoguesFileName";
        public TextField FileNameTextField { get; private set; }

        public Toolbar CreateToolBar()
        {
            Toolbar toolbar = new Toolbar();
            FileNameTextField = CreateTextField(DefaultFileName, "File Name:",
                callback =>
                {
                    FileNameTextField.value = callback.newValue.RemoveWhitespaces().RemoveSpecialCharacters();
                });
            toolbar.Add(FileNameTextField);
            return toolbar;
        }

        public ToolbarButton CreateToolbarButton(string text, Action clickEvent)
        {
            ToolbarButton button = new ToolbarButton(clickEvent)
            {
                text = text
            };
            return button;
        }

        public TextField CreateTextField(string value = null, string label = null,
            EventCallback<ChangeEvent<string>> onValueChanged = null)
        {
            TextField textField = new TextField()
            {
                value = value,
                label = label
            };
            
            if (onValueChanged != null)
            {
                textField.RegisterValueChangedCallback(onValueChanged);
            }

            return textField;
        }

        public TextField CreateTextArea(string value = null, string label = null,
            EventCallback<ChangeEvent<string>> onValueChanged = null)
        {
            TextField textArea = CreateTextField(value, label, onValueChanged);
            textArea.multiline = true;
            return textArea;
        }

        public Foldout CreateFoldout(string title = "Dialogue Test", bool collapsed = false)
        {
            Foldout foldout = new Foldout()
            {
                text = title,
                value = !collapsed
            };
            return foldout;
        }

        public EnumField CreateEnumField(Enum enumType)
        {
            return new EnumField(enumType);
        }

        public Port CreatePort(DSNode node, string portName = "",
            Orientation orientation = Orientation.Horizontal,
            Direction direction = Direction.Output,
            Port.Capacity capacity = Port.Capacity.Multi)
        {
            Port port = node.InstantiatePort(orientation, direction, capacity, typeof(bool));
            port.portName = portName;
            return port;
        }

        public T[] CreateSeveral<T>(int count) where T : VisualElement, new()
        {
            T[] result = new T[count];
            for (int i = 0; i < count; i++)
            {
                result[i] = new T();
            }

            return result;
        }
    }
}