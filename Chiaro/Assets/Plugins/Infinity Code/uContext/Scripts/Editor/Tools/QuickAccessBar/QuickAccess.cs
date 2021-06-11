﻿/*           INFINITY CODE          */
/*     https://infinity-code.com    */

using System;
using System.Linq;
using InfinityCode.uContext.Integration;
using UnityEditor;
using UnityEngine;

namespace InfinityCode.uContext
{
    [InitializeOnLoad]
    public static class QuickAccess
    {
        public static Action<QuickAccessItem> OnInvokeExternal;
        public static Action OnVisibleChanged;

        public static EditorWindow activeWindow;
        public static int activeWindowIndex = -1;
        public static int invokeItemIndex;
        public static int invokeItemVisibleIndex;
        public static Rect invokeItemRect;

        private static Texture2D background;
        private static Rect rect;
        private static GUIStyle contentStyle;
        private static GUIStyle activeContentStyle;
        private static bool _visible;
        private static Action invokeItemAction;

        public static bool visible
        {
            get
            {
                if (!Prefs.quickAccessBar) return false;

                SceneView sceneView = SceneView.lastActiveSceneView;
                if (sceneView == null) return false;

                bool maximized = WindowsHelper.IsMaximized(sceneView);
                return ReferenceManager.quickAccessItems.Any(i => i.Visible(maximized));
            }
        }

        public static float width
        {
            get { return 32; }
        }

        static QuickAccess()
        {
            SceneViewManager.OnValidateOpenContextMenu += OnValidateOpenContextMenu;
            SceneViewManager.AddListener(OnSceneGUI, 1000, true);
        }

        public static void CheckActiveWindow()
        {
            if (activeWindow == null) activeWindowIndex = -1;
        }

        public static void CloseActiveWindow()
        {
            if (activeWindow == null) return;

            if (uContextMenu.allowCloseWindow) activeWindow.Close(); 
            activeWindow = null;
            activeWindowIndex = -1;
        }

        private static void DrawBackground()
        {
            if (background == null)
            {
                background = new Texture2D(1, 1);
                background.SetPixel(0, 0, new Color(0, 0, 0, 0.6f));
                background.Apply();
            }

            float minIntend = GetMinIntend();
            float maxIntend = Prefs.quickAccessBarIndentMax;

            GUI.DrawTexture(new Rect(0, minIntend, rect.width, rect.height - minIntend - maxIntend), background, ScaleMode.StretchToFill);
        }

        private static void DrawContent(SceneView sceneView)
        {
            if (contentStyle == null || contentStyle.normal.background == null)
            {
                contentStyle = new GUIStyle(EditorStyles.toolbarButton)
                {
                    fontSize = 8,
                    fixedHeight = 32,
                    normal = {background = Resources.CreateSinglePixelTexture(0, 0.1f), textColor = Color.white},
                    hover = {background = Resources.CreateSinglePixelTexture(0, 0.2f)},
                    active = {background = Resources.CreateSinglePixelTexture(0, 0.3f)},
                    padding = new RectOffset()
                };

                activeContentStyle = new GUIStyle(contentStyle);
                activeContentStyle.normal.background = activeContentStyle.active.background;
            }

            CheckActiveWindow();

            Event e = Event.current;
            bool maximized = WindowsHelper.IsMaximized(sceneView);

            int index = -1;
            int visibleIndex = -1;

            float y = GetMinIntend();

            foreach (QuickAccessItem item in ReferenceManager.quickAccessItems)
            {
                index++;
                if (item.type == QuickAccessItemType.flexibleSpace)
                {
                    GUILayout.FlexibleSpace();
                    continue;
                }

                if (item.type == QuickAccessItemType.space)
                {
                    GUILayout.Space(item.intSettings[0]);
                    continue;
                }
                if (item.content == null) continue;
                if (!item.Visible(maximized)) continue;
                visibleIndex++;

                GUIStyle style = activeWindowIndex == index ? activeContentStyle : contentStyle;
                ButtonEvent buttonEvent = GUILayoutUtils.Button(item.content, style, GUILayout.Width(width), GUILayout.Height(width));
                if (buttonEvent == ButtonEvent.press)
                {
                    if (e.button == 0)
                    {
                        invokeItemRect = GUILayoutUtils.lastRect;
                        invokeItemRect.y += y;
                        invokeItemIndex = index;
                        invokeItemVisibleIndex = visibleIndex;
                        invokeItemAction = item.Invoke;
                        EditorApplication.update += InvokeItemAction;
                        e.Use();
                    }
                    else if (e.button == 1) ShowEditMenu();
                }
            }

            if (e.type == EventType.MouseDown && e.mousePosition.x < width && e.button == 1) ShowEditMenu();
        }

        private static float GetMinIntend()
        {
            float v = 0;

            if (ProGrids.isEnabled)
            {
                if (ProGrids.isMenuOpen) v = 205;
                else v = 25;
            }

            return Mathf.Max(v, Prefs.quickAccessBarIndentMin);
        }

        private static void InvokeItemAction()
        {
            EditorApplication.update -= InvokeItemAction;
            if (invokeItemAction == null) return;

            try
            {
                invokeItemAction();
            }
            catch
            {
                
            }

            invokeItemAction = null;
            invokeItemVisibleIndex = invokeItemIndex = -1;
        }

        private static void OnSceneGUI(SceneView sceneView)
        {
            if (Event.current.type == EventType.Layout)
            {
                bool v = visible;
                if (v != _visible)
                {
                    if (OnVisibleChanged != null) OnVisibleChanged();
                    _visible = v;
                }
            }
            if (!_visible) return;

            rect = new Rect(0, 0, width, sceneView.position.height - 20);

            try
            {
                Handles.BeginGUI();

                DrawBackground();

                float minIntend = GetMinIntend();
                float maxIntend = Mathf.Max(Prefs.quickAccessBarIndentMax, 0);

                GUILayout.BeginArea(new Rect(0, minIntend, rect.width, rect.height - minIntend - maxIntend));
                DrawContent(sceneView);
                GUILayout.EndArea();

                Handles.EndGUI();
            }
            catch
            {
                
            }
        }

        private static bool OnValidateOpenContextMenu()
        {
            return visible && Event.current.mousePosition.x > width;
        }

        private static void ShowEditMenu()
        {
            GenericMenu menu = new GenericMenu();
            menu.AddItem(new GUIContent("Edit"), false, () => Settings.OpenQuickAccessSettings());
            menu.ShowAsContext();
            Event.current.Use();
        }
    }
}