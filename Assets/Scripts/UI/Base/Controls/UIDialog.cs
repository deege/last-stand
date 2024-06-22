using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Deege.Game.UI.Controls;
using UnityEngine;
using UnityEngine.UIElements;

namespace Deege.UI.Controls
{
    public class UIDialog<T> : UIScreen
    {
        private readonly Dictionary<Button, T> buttonResults;
        private TaskCompletionSource<T> taskCompletionSource;
        private readonly VisualElement buttonContainer = new();
        private readonly VisualElement header = new();
        private readonly VisualElement content = new();

        public VisualElement DialogContent => content;
        public VisualElement ButtonContainer => buttonContainer;
        public VisualElement Header => header;


        public UIDialog() : base()
        {
            buttonResults = new Dictionary<Button, T>();
        }

        protected override void ConstructUI(UIDocument uiDocument, string styleResource = "")
        {
            base.ConstructUI(uiDocument, styleResource);

            AddToClassList("dialog");

            header.AddToClassList("header");
            hierarchy.Add(header);

            content.AddToClassList("content");
            hierarchy.Add(content);

            buttonContainer.AddToClassList("button-container");
            hierarchy.Add(buttonContainer);
        }

        public void SetPosition(Vector2 position)
        {
            style.left = position.x;
            style.top = position.y;
        }

        public void SetSize(Vector2 size)
        {
            style.width = size.x;
            style.height = size.y;
        }

        public Task<T> ShowAsync(UIDocument uiDocument)
        {
            taskCompletionSource = new TaskCompletionSource<T>();
            Show(uiDocument);
            return taskCompletionSource.Task;
        }

        public override void Hide()
        {
            base.Hide();
            taskCompletionSource?.TrySetCanceled();
        }

        public void AddButton(string localizationKey, T result, string className = "button")
        {
            var button = LocalizedButton.CreateButton(localizationKey, className);
            buttonContainer.Add(button);
            button.clicked += () => OnButtonClicked(button);
            buttonResults[button] = result;
        }

        private void OnButtonClicked(Button button)
        {
            if (buttonResults.TryGetValue(button, out T result))
            {
                taskCompletionSource?.SetResult(result);
                Hide();
            }
        }
    }
}
