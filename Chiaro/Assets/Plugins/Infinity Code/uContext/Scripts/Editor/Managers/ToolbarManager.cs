/*           INFINITY CODE          */
/*     https://infinity-code.com    */

using System;
using System.Collections.Generic;
using InfinityCode.uContext.UnityTypes;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using Object = UnityEngine.Object;

namespace InfinityCode.uContext
{
    [InitializeOnLoad]
    public static class ToolbarManager
    {
        private const float space = 10;
        private const float largeSpace = 20;
        private const float buttonWidth = 32;
        private const float dropdownWidth = 80;
        private const float playPauseStopWidth = 140;

        private static int toolCount;
        private static GUIStyle style;

        private static Dictionary<string, Action> leftToolbarItems;
        private static Dictionary<string, Action> rightToolbarItems;

        private static ScriptableObject currentToolbar;

        static ToolbarManager()
        {
            toolCount = ToolbarRef.GetToolCount();
            EditorApplication.update -= CheckCurrentToolbar;
            EditorApplication.update += CheckCurrentToolbar;
        }

        public static void AddLeftToolbar(string key, Action action)
        {
            if (leftToolbarItems == null) leftToolbarItems = new Dictionary<string, Action>();
            leftToolbarItems[key] = action;
        }

        public static void AddRightToolbar(string key, Action action)
        {
            if (rightToolbarItems == null) rightToolbarItems = new Dictionary<string, Action>();
            rightToolbarItems[key] = action;
        }

        private static void CheckCurrentToolbar()
        {
            if (currentToolbar != null) return;

            Object[] toolbars = UnityEngine.Resources.FindObjectsOfTypeAll(ToolbarRef.type);
            if (toolbars.Length == 0)
            {
                currentToolbar = null;
                return;
            }

            currentToolbar = (ScriptableObject) toolbars[0];
            if (currentToolbar == null) return;

            VisualElement visualTree = Compatibility.GetVisualTree(currentToolbar);
            IMGUIContainer container = (IMGUIContainer)visualTree[0];

            Action handler = IMGUIContainerRef.GetGUIHandler(container);
            handler -= OnGUI;
            handler += OnGUI;
            IMGUIContainerRef.SetGUIHandler(container, handler);
        }

        private static void DrawLeftToolbar(float screenWidth, float playButtonsPosition)
        {
            if (leftToolbarItems == null) return;

            Rect rect = new Rect(0, 0, screenWidth, Screen.height);
            rect.xMin += space;
            rect.xMin += buttonWidth * toolCount;
            rect.xMin += largeSpace;
            rect.xMin += 64 * 2;
            rect.xMax = playButtonsPosition;
            rect.xMin += space;
            rect.xMax -= space;
#if UNITY_2019_3_OR_NEWER
            rect.y = 1;
#else
            rect.y = 7;
#endif
            rect.height = 24;

            if (rect.width <= 0) return;

            GUILayout.BeginArea(rect);
            GUILayout.BeginHorizontal();
            EditorGUILayout.Space();
            foreach (var pair in leftToolbarItems)
            {
                try
                {
                    if (pair.Value != null) pair.Value();
                }
                catch (Exception e)
                {
                    Log.Add(e);
                }
            }

            GUILayout.EndHorizontal();
            GUILayout.EndArea();
        }

        private static void DrawRightToolbar(float screenWidth, float playButtonsPosition)
        {
            if (rightToolbarItems == null) return;

            Rect rightRect = new Rect(0, 0, screenWidth, Screen.height);
            rightRect.xMin = playButtonsPosition;
            rightRect.xMin += style.fixedWidth * 3;
            rightRect.xMax = screenWidth;
            rightRect.xMax -= space;
            rightRect.xMax -= dropdownWidth;
            rightRect.xMax -= space;
            rightRect.xMax -= dropdownWidth;
            rightRect.xMax -= largeSpace;
            rightRect.xMax -= dropdownWidth;
            rightRect.xMax -= space;
            rightRect.xMax -= buttonWidth;
            rightRect.xMax -= space;
            rightRect.xMax -= 78;
            rightRect.xMin += space;
            rightRect.xMax -= space;
#if UNITY_2019_3_OR_NEWER
            rightRect.y = 4;
#else
            rightRect.y = 7;
#endif
            rightRect.height = 24;

            if (rightRect.width <= 0) return;

            GUILayout.BeginArea(rightRect);
            GUILayout.BeginHorizontal();

            foreach (var pair in rightToolbarItems)
            {
                try
                {
                    if (pair.Value != null) pair.Value();
                }
                catch (Exception e)
                {
                    Log.Add(e);
                }
            }

            GUILayout.EndHorizontal();
            GUILayout.EndArea();
        }

        static void OnGUI()
        {
            if (style == null) style = new GUIStyle("CommandLeft");

            float screenWidth = EditorGUIUtility.currentViewWidth;
            float playButtonsPosition = Mathf.RoundToInt((screenWidth - playPauseStopWidth) / 2);

            DrawLeftToolbar(screenWidth, playButtonsPosition);
            DrawRightToolbar(screenWidth, playButtonsPosition);
        }

        public static bool RemoveLeftToolbar(string key)
        {
            if (leftToolbarItems == null) return false;
            return leftToolbarItems.Remove(key);
        }

        public static bool RemoveRightToolbar(string key)
        {
            if (rightToolbarItems == null) return false;
            return rightToolbarItems.Remove(key);
        }
    }
}