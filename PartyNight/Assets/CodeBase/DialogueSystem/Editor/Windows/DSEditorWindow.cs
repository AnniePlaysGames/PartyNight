using CodeBase.DialogueSystem.Editor.Data.SaveLoad;
using CodeBase.DialogueSystem.Editor.Factories;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace CodeBase.DialogueSystem.Editor.Windows
{
    public class DSEditorWindow : EditorWindow
    {
        private DSElementFactory _elementFactory;
        private DSGraphSave _graphSaver;
        private DSGraphLoad _graphLoader;
        private DSNodeFactory _nodeFactory;

        [MenuItem("Window/Dialogue Graph")]
        public static void Open()
        {
            GetWindow<DSEditorWindow>("Dialogue Graph");
        }

        private void CreateGUI()
        {
            InitGraphView();
            CreateToolbar();
        }

        private void InitGraphView()
        {
            _elementFactory = new DSElementFactory();
            _nodeFactory = new DSNodeFactory(_elementFactory);
            DSGraphView graphView = new DSGraphView(_nodeFactory);
            _graphSaver = new DSGraphSave(_elementFactory, graphView);
            _graphLoader = new DSGraphLoad(_elementFactory, _nodeFactory, graphView);
            graphView.StretchToParentSize();
            rootVisualElement.Add(graphView);
        }

        private void CreateToolbar()
        {
            Toolbar toolbar = _elementFactory.CreateToolBar();
            AttachSaveButton(toolbar);
            AttachLoadButton(toolbar);
            rootVisualElement.Add(toolbar);
        }

        private void AttachSaveButton(Toolbar toolbar)
        {
            toolbar.Add(_elementFactory.CreateToolbarButton("Save",()
                => _graphSaver.SaveData(_nodeFactory.Nodes)));
        }      
        
        private void AttachLoadButton(Toolbar toolbar)
        {
            toolbar.Add(_elementFactory.CreateToolbarButton("Load",()
                => _graphLoader.Load()));
        }
    }
}