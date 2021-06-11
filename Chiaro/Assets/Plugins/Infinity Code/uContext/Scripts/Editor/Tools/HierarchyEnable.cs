/*           INFINITY CODE          */
/*     https://infinity-code.com    */

using UnityEditor;
using UnityEngine;

namespace InfinityCode.uContext.Tools
{
    [InitializeOnLoad]
    public static class HierarchyEnable
    {
        private static int activeID;

        static HierarchyEnable()
        {
            HierarchyItemDrawer.Register("HierarchyEnable", OnHierarchyItem, -1);
        }

        private static Rect OnHierarchyItem(int id, Rect rect)
        {
            if (!Prefs.hierarchyEnableGameObject) return rect;

            Event e = Event.current;
            if (e.modifiers != EventModifiers.Control && e.modifiers != EventModifiers.Command) return rect;

            float mx = e.mousePosition.x;

            if (mx < 0 || mx > rect.width)
            {
                if (activeID == id) activeID = -1;
                return rect;
            }

            float my = e.mousePosition.y;

            if (e.type == EventType.Layout)
            {
                if (my >= rect.y && my <= rect.yMax) activeID = id;
                else if (activeID == id) activeID = -1;
            }

            if (activeID != id) return rect;

            GameObject go = EditorUtility.InstanceIDToObject(id) as GameObject;
            if (go != null)
            {
                EditorGUI.BeginChangeCheck();
                bool v = EditorGUI.Toggle(new Rect(32, rect.y, 16, rect.height), GUIContent.none, go.activeSelf);
                if (EditorGUI.EndChangeCheck()) go.SetActive(v);
            }

            return rect;
        }
    }
}