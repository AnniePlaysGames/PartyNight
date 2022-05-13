using CodeBase.Infrastructure.Services.AssetManagement;
using CodeBase.UI.Windows;
using UnityEngine;

namespace CodeBase.UI.Services.Factory
{
    class UIFactory : IUIFactory
    {
        private const string DialogueWindowPath = "UI/DialogueWindow";
        
        private readonly IAssetProvider _assetProvider;
        
        public DialogueWindow DialogueWindow { get; private set; }
        public RectTransform UIRoot { get; }

        public UIFactory(IAssetProvider assetProvider, RectTransform uiRoot)
        {
            _assetProvider = assetProvider;
            UIRoot = uiRoot;
        }

        public void CreateDialogueWindow()
        {
            GameObject window = _assetProvider.Instantiate(DialogueWindowPath);
            window.transform.SetParent(UIRoot, false);
            DialogueWindow = window.GetComponent<DialogueWindow>();
        }
    }
}