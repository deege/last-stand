using System.Collections;
using UnityEngine;

namespace Deege.Game.Tools
{
    public static class GameObjectTools
    {
        public static int GetRootInstanceId(GameObject childObject)
        {
            Transform current = childObject.transform;
            while (current.parent != null)
            {
                current = current.parent;
            }
            return current.gameObject.GetInstanceID();
        }
    }
}
