using System.Collections.Generic;
using CodeBase.DialogueSystem.Editor.Data.SaveLoad;
using CodeBase.DialogueSystem.Editor.Factories;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace CodeBase.DialogueSystem.Editor.Windows
{
    public class DSGraphView : GraphView
    {
        private const string GraphViewStylesPath = "DialogueSystem/DSGraphViewStyles.uss";
        private const string DSElementsStylesPath = "DialogueSystem/DSElementStyles.uss";
        private const string AddNodeActionName = "Add Node";

        private readonly DSNodeFactory _nodeFactory;

        public DSGraphView(DSNodeFactory nodeFactory)
        {
            _nodeFactory = nodeFactory;
            InitManipulators();
            AddGridBackground();
            AddStyles();
        }

        private void InitManipulators()
        {
            this.AddManipulator(new ContentDragger());
            this.AddManipulator(new SelectionDragger());
            this.AddManipulator(new RectangleSelector());
            this.AddManipulator(CreateNodeContextualMenu());
            SetupZoom(ContentZoomer.DefaultMinScale, ContentZoomer.DefaultMaxScale);
        }
        

        private IManipulator CreateNodeContextualMenu()
        {
            ContextualMenuManipulator contextualMenuManipulator = new ContextualMenuManipulator(
                menuEvent => menuEvent.menu.AppendAction(AddNodeActionName, actionEvent =>
                {
                    AddElement(_nodeFactory.CreateNode(new DSNodeSaveData
                    {
                        Position = new Rect(actionEvent.eventInfo.localMousePosition, Vector2.zero)
                    }));
                }));

            return contextualMenuManipulator;
        }

        private void AddGridBackground()
        {
            GridBackground gridBackground = new GridBackground();
            gridBackground.StretchToParentSize();
            Insert(0, gridBackground);
        }

        private void AddStyles()
        {
            styleSheets.Add((StyleSheet) EditorGUIUtility.Load(GraphViewStylesPath));
            styleSheets.Add((StyleSheet) EditorGUIUtility.Load(DSElementsStylesPath));
        }

        public override List<Port> GetCompatiblePorts(Port startPort, NodeAdapter nodeAdapter)
        {
            List<Port> compatiblePorts = new List<Port>();

            ports.ForEach(port =>
            {
                if (startPort == port || startPort.node == port.node || startPort.direction == port.direction)
                {
                    return;
                }

                compatiblePorts.Add(port);
            });

            return compatiblePorts;
        }
    }
}