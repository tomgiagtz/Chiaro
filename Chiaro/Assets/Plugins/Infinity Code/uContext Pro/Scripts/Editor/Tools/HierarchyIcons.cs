/*           INFINITY CODE          */
/*     https://infinity-code.com    */

using System;
using System.Collections.Generic;
using System.Linq;
using InfinityCode.uContext;
using InfinityCode.uContext.Actions;
using InfinityCode.uContext.Integration;
using InfinityCode.uContext.Windows;
using UnityEditor;
using UnityEngine;

namespace InfinityCode.uContextPro.Tools
{
    [InitializeOnLoad]
    public static class HierarchyIcons
    {
        private static GameObject target;
        private static List<Item> items;
        private static bool ehVisible = true;
        private static int ehRightMargin;

        static HierarchyIcons()
        {
            items = new List<Item>();
            HierarchyItemDrawer.Register("HierarchyIcon", DrawHierarchyItem);
        }

        private static Rect DrawHierarchyItem(int id, Rect rect)
        {
            if (!Prefs.hierarchyIcons) return rect;

            Event e = Event.current;
            if (e.modifiers != Prefs.hierarchyIconsModifiers)
            {
                if (!ehVisible)
                {
                    EnhancedHierarchy.SetRightMargin(ehRightMargin);
                    ehVisible = true;
                }
                return rect;
            }

            if (ehVisible)
            {
                ehRightMargin = EnhancedHierarchy.GetRightMargin();
                EnhancedHierarchy.SetRightMargin(-10000);
                ehVisible = false;
            }

            if (!rect.Contains(e.mousePosition)) return rect;

            GameObject go = EditorUtility.InstanceIDToObject(id) as GameObject;

            if (go != target)
            {
                target = go;
                UpdateItems(rect);
            }

            if (target == null) return rect;

            Rect lastRect = new Rect(rect.xMax, rect.y, 0, rect.height);

            for (int i = items.Count - 1; i >= 0; i--)
            {
                Item item = items[i];
                float lastX = item.Draw(lastRect);
                lastRect.x = lastX;
            }

            return rect;
        }

        private static void ShowAddComponent(Rect hierarchyRect)
        {
            Event e = Event.current;
            Vector2 position = e.mousePosition;
            position.y = hierarchyRect.yMax;
            if (EditorWindow.focusedWindow != null) position += EditorWindow.focusedWindow.position.position;
            else position = HandleUtility.GUIPointToScreenPixelCoordinate(position);

            Vector2 size = Prefs.contextMenuWindowSize;
            Rect rect = new Rect(position + new Vector2(-size.x / 2, 36), size);

#if !UNITY_EDITOR_OSX
            if (rect.yMax > Screen.currentResolution.height - 10) rect.y -= rect.height - 50;

            if (rect.x < 5) rect.x = 5;
            else if (rect.xMax > Screen.currentResolution.width - 5) rect.x = Screen.currentResolution.width - 5 - rect.width;
#endif

            Selection.activeGameObject = target;
            AddComponent.ShowAddComponent(rect);
        }

        private static void ShowComponent(Component component, Rect hierarchyRect)
        {
            Event e = Event.current;
            Vector2 position = e.mousePosition;
            position.y = hierarchyRect.yMax;
            if (EditorWindow.focusedWindow != null) position += EditorWindow.focusedWindow.position.position;
            else position = HandleUtility.GUIPointToScreenPixelCoordinate(position);

            Vector2 size = Prefs.contextMenuWindowSize;
            Rect rect = new Rect(position + new Vector2(-size.x / 2, 36), size);

#if !UNITY_EDITOR_OSX
            if (rect.yMax > Screen.currentResolution.height - 10) rect.y -= rect.height - 50;

            if (rect.x < 5) rect.x = 5;
            else if (rect.xMax > Screen.currentResolution.width - 5) rect.x = Screen.currentResolution.width - 5 - rect.width;
#endif

            ComponentWindow wnd = ComponentWindow.Show(component);
            wnd.position = rect;
        }

        private static void ShowMore(IEnumerable<Component> components, Rect rect)
        {
            GenericMenu menu = new GenericMenu();
            bool useSeparator = false;

            foreach (Component c in components)
            {
                menu.AddItem(new GUIContent(c.GetType().Name), false, () =>
                {
                    SceneViewManager.OnNextGUI += () => ShowComponent(c, rect);
                    SceneView.RepaintAll();
                });
                useSeparator = true;
            }

            if (useSeparator) menu.AddSeparator("");

            menu.AddItem(new GUIContent("Add Component"), false, () =>
            {
                SceneViewManager.OnNextGUI += () => ShowAddComponent(rect);
                SceneView.RepaintAll();
            });

            menu.AddItem(new GUIContent("Add To Bookmark"), false, () =>
            {
                Bookmarks.Add(target);
                SceneView.RepaintAll();
            });
            menu.ShowAsContext();
        }

        private static void UpdateItems(Rect rect)
        {
            items.Clear();

            if (target == null) return;

            Component[] components = target.GetComponents<Component>();
            Item item;

            for (int i = 0; i < Mathf.Min(components.Length, Prefs.hierarchyIconsMaxItems); i++)
            {
                Component component = components[i];
                if (component == null) continue;
                Texture2D thumbnail = AssetPreview.GetMiniThumbnail(component);
                GUIContent content = new GUIContent(
                    thumbnail,
                    ObjectNames.NicifyVariableName(component.GetType().Name)
                );
                if (thumbnail.name == "cs Script Icon" || thumbnail.name == "d_cs Script Icon") GameObjectUtils.GetPsIconContent(content);

                item = new Item(content);
                item.invoke += () => ShowComponent(component, rect);
                items.Add(item);
            }

            int moreItems = components.Length - Prefs.hierarchyIconsMaxItems;

            item = new Item(new GUIContent(moreItems > 0? "+" + moreItems: "...", "More"));
            item.invoke += () => ShowMore(components.Skip(Prefs.hierarchyIconsMaxItems), rect);
            items.Add(item);
        }

        internal class Item
        {
            public Action invoke;
            public GUIContent content;

            public Item(GUIContent content)
            {
                this.content = content;
            }

            public float Draw(Rect rect)
            {
                bool useButton = !string.IsNullOrEmpty(content.text);
                rect.xMin -= useButton ? Styles.hierarchyIcon.CalcSize(content).x + 8 : 18;
                GUI.Box(rect, content, Styles.hierarchyIcon);
                Event e = Event.current;
                if (e.type == EventType.MouseDown && e.button == 0 && rect.Contains(e.mousePosition))
                {
                    if (invoke != null) invoke();
                    e.Use();
                }

                return rect.x;
            }
        }
    }
}