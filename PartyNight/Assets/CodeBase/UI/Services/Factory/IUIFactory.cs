using CodeBase.Infrastructure.Services;
using CodeBase.UI.Windows;
using UnityEngine;

namespace CodeBase.UI.Services.Factory
{
    public interface IUIFactory : IService
    {
        RectTransform UIRoot { get; }
        DialogueWindow DialogueWindow { get; }
        
        void CreateDialogueWindow();
    }
}