using UnityEngine;
using UnityEngine.UIElements;
using System.Collections.Generic;

namespace Deege.UI.Controls
{
    public class UIWindow : UIScreen
    {
        private List<UIWindow> children = new List<UIWindow>();

        public UIWindow()
        {
            // Initialization logic will be moved to Build method
        }

        public override void ConstructUI(UIDocument uiDocument)
        {
            // Add specific build logic for UIWindow
        }

        public void AddChild(UIWindow child)
        {
            children.Add(child);
            hierarchy.Add(child);
        }

        public override void Render()
        {
            base.Render();
            foreach (var child in children)
            {
                child.Render();
            }
        }
    }
}