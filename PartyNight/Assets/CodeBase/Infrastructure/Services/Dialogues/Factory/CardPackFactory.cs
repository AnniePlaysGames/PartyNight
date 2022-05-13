using System.Collections.Generic;
using System.Linq;
using CodeBase.Infrastructure.Services.Dialogues.Scriptable_Objects;
using UnityEngine;

namespace CodeBase.Infrastructure.Services.Dialogues.Factory
{
    public class CardPackFactory : ICardPackFactory
    {
        private const string SavedDialoguesPath = "DialogueSystem/SavedDialogues";

        public List<Dialogue> GeneratePack()
            => LoadAllDialogues();

        private List<Dialogue> LoadAllDialogues() 
            => Resources.LoadAll<Dialogue>(SavedDialoguesPath).ToList();
    }
}