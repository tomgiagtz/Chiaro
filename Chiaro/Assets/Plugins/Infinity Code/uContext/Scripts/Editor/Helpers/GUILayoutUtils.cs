/*           INFINITY CODE          */
/*     https://infinity-code.com    */

using InfinityCode.uContext.UnityTypes;
using UnityEditor;
using UnityEngine;

namespace InfinityCode.uContext
{
    public static class GUILayoutUtils
    {
        public static Rect lastRect;
        public static int buttonHash = "Button".GetHashCode();
        public static int toggleHash = "Toggle".GetHashCode();
        public static int hoveredButtonID;

        public static ButtonEvent Button(GUIContent content)
        {
            return Button(content, GUI.skin.button);
        }

        public static ButtonEvent Button(Texture texture, GUIStyle style, params GUILayoutOption[] options)
        {
            return Button(GUIContentRef.TempContent(texture), style, options);
        }

        public static ButtonEvent Button(GUIContent content, GUIStyle style, params GUILayoutOption[] options)
        {
            Event e = Event.current;

            Rect rect = GUILayoutUtility.GetRect(content, style, options);
            lastRect = rect;
            int id = GUIUtility.GetControlID(buttonHash, FocusType.Passive, rect);
            bool isHover = rect.Contains(e.mousePosition);
            bool hasMouseControl = GUIUtility.hotControl == id;

            if (e.type == EventType.Repaint)
            {
                style.Draw(rect, content, id, false);
            }
            else if (e.type == EventType.MouseDrag)
            {
                if (hasMouseControl)
                {
                    GUIUtility.hotControl = 0;
                    return ButtonEvent.drag;
                }
            }
            else if (e.type == EventType.MouseMove)
            {
                if (isHover) hoveredButtonID = id;
            }
            else if (e.type == EventType.MouseDown)
            {
                if (isHover)
                {
                    GUIUtility.hotControl = id;
                    e.Use();
                    return ButtonEvent.press;
                }
            }
            else if (e.type == EventType.MouseUp)
            {
                if (hasMouseControl)
                {
                    GUIUtility.hotControl = 0;
                    e.Use();

                    if (isHover)
                    {
                        GUI.changed = true;
                        return ButtonEvent.click;
                    }
                }

                return ButtonEvent.release;
            }

            return ButtonEvent.none;
        }

        public static ButtonEvent ToggleButton(GUIContent content, GUIStyle style, bool isActive, params GUILayoutOption[] options)
        {
            Event e = Event.current;

            Rect rect = GUILayoutUtility.GetRect(content, style, options);
            int id = GUIUtility.GetControlID(buttonHash, FocusType.Passive, rect);
            bool isHover = rect.Contains(e.mousePosition);
            bool hasMouseControl = GUIUtility.hotControl == id;

            if (e.type == EventType.Repaint)
            {
                style.Draw(rect, content, id, false);
            }
            else if (e.type == EventType.MouseDrag)
            {
                if (hasMouseControl)
                {
                    GUIUtility.hotControl = 0;
                    return ButtonEvent.drag;
                }
            }
            else if (e.type == EventType.MouseMove)
            {
                if (isHover) hoveredButtonID = id;
            }
            else if (e.type == EventType.MouseDown)
            {
                if (isHover)
                {
                    GUIUtility.hotControl = id;
                    e.Use();
                    return ButtonEvent.press;
                }
            }
            else if (e.type == EventType.MouseUp)
            {
                if (hasMouseControl)
                {
                    GUIUtility.hotControl = 0;
                    e.Use();

                    if (isHover)
                    {
                        GUI.changed = true;
                        return ButtonEvent.click;
                    }
                }

                return ButtonEvent.release;
            }

            return ButtonEvent.none;
        }

        public static bool ToggleButton(ref bool toggled, GUIContent content, GUIStyle toggleButtonStyle, params GUILayoutOption[] options)
        {
            Rect rect = GUILayoutUtility.GetRect(content, toggleButtonStyle, options);
            EditorGUI.BeginChangeCheck();
            toggled = GUI.Toggle(rect, toggled, content, toggleButtonStyle);
            return EditorGUI.EndChangeCheck();
        }

        public static GUIContent TempContent(Texture texture)
        {
            return GUIContentRef.TempContent(texture);
        }
    }
}