/*           INFINITY CODE          */
/*     https://infinity-code.com    */

using InfinityCode.uContext;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace InfinityCode.uContextPro.Tools
{
    [InitializeOnLoad]
    public static class Highlighter
    {
        private static Texture2D _hoveredTexture;
        private static Color lastColor;

        private static Texture2D hoveredTexture
        {
            get
            {
                if (lastColor != Prefs.highlightHierarchyRowColor)
                {
                    if (_hoveredTexture != null)
                    {
                        Object.DestroyImmediate(_hoveredTexture);
                        _hoveredTexture = null;
                    }
                }

                if (_hoveredTexture == null)
                {
                    _hoveredTexture = uContext.Resources.CreateSinglePixelTexture(Prefs.highlightHierarchyRowColor);
                    lastColor = Prefs.highlightHierarchyRowColor;
                }
                return _hoveredTexture;
            }
        }

        static Highlighter()
        {
            HierarchyItemDrawer.Register("Highlighter2", DrawHierarchyItem, 1);
        }

        private static Rect DrawHierarchyItem(int id, Rect rect)
        {
            if (!Prefs.highlight) return rect;
            if (!(EditorWindow.mouseOverWindow is SceneView)) return rect;

            GameObject go = EditorUtility.InstanceIDToObject(id) as GameObject;

            Event e = Event.current;
            if (rect.Contains(e.mousePosition))
            {
                if (e.modifiers == Prefs.hierarchyIconsModifiers) uContext.Tools.Highlighter.Highlight(go);
                else uContext.Tools.Highlighter.Highlight(null);
            }
            if (Prefs.highlightHierarchyRow) HighlightRow(rect, go);

            return rect;
        }

        private static void HighlightRow(Rect rect, GameObject go)
        {
            if (uContext.Tools.Highlighter.lastGameObject == null || go == null) return;
            if (uContext.Tools.Highlighter.lastGameObject != go) return;

#if UNITY_2019_3_OR_NEWER
            GUI.DrawTexture(new Rect(32, rect.y, 2, rect.height), hoveredTexture, ScaleMode.StretchToFill);
#else
            GUI.DrawTexture(new Rect(16, rect.y, 2, rect.height), hoveredTexture, ScaleMode.StretchToFill);
#endif
        }
    }
}