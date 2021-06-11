/*           INFINITY CODE          */
/*     https://infinity-code.com    */

using System;
using System.Collections.Generic;
using InfinityCode.uContext;
using UnityEditor;
using UnityEngine;

namespace InfinityCode.uContextPro.Tools
{
    [InitializeOnLoad]
    public static class HierarchyErrors
    {
        static HierarchyErrors()
        {
            HierarchyItemDrawer.Register("HierarchyErrors", DrawHierarchyItem, -1);
        }

        private static Rect DrawHierarchyItem(int id, Rect rect)
        {
            if (!Prefs.hierarchyIcons || !Prefs.hierarchyErrorIcons) return rect;

            List<LogManager.Entry> entries = LogManager.GetEntries(id);
            if (entries == null || entries.Count <= 0) return rect;
            if (entries[0] == null || string.IsNullOrEmpty(entries[0].message)) return rect;

            GUIContent content = EditorIconContents.consoleErrorIconSmall;
            StaticStringBuilder.Clear();
            StaticStringBuilder.Append(entries[0].message.Split(new[] {"\n"}, StringSplitOptions.RemoveEmptyEntries)[0]);
            if (entries.Count > 1) StaticStringBuilder.Append("\n+").Append(entries.Count - 1).Append(" errors");

            content.tooltip = StaticStringBuilder.GetString(true);
            Rect localRect = new Rect(rect);
            localRect.xMin = localRect.xMax - 20;
            rect.width -= 22;

            if (GUI.Button(localRect, content, GUIStyle.none)) entries[0].Open();
            return rect;
        }
    }
}