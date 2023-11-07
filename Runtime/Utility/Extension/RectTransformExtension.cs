using System.Collections.Generic;
using UnityEngine;

namespace Pangoo
{
    public static class RectTransformExtension
    {

        public static void SetUIPanelDefault(this RectTransform t)
        {
            t.anchorMin = Vector2.zero;
            t.anchorMax = Vector2.one;
            t.offsetMin = Vector2.zero;
            t.offsetMax = Vector2.zero;
        }

    }
}
