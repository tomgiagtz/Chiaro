/*           INFINITY CODE          */
/*     https://infinity-code.com    */

using System;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace InfinityCode.uContext.UnityTypes
{
    public static class EditorGUIUtilityRef
    {
        private static MethodInfo _drawEditorHeaderItemsMethod;

        private static MethodInfo drawEditorHeaderItemsMethod
        {
            get
            {
                if (_drawEditorHeaderItemsMethod == null)
                {
#if UNITY_2019_3_OR_NEWER
                    _drawEditorHeaderItemsMethod = Reflection.GetMethod(type, "DrawEditorHeaderItems", new[] { typeof(Rect), typeof(Object[]), typeof(float) }, Reflection.StaticLookup);
#else
                    _drawEditorHeaderItemsMethod = Reflection.GetMethod(typeof(EditorGUIUtility), "DrawEditorHeaderItems", new[] { typeof(Rect), typeof(Object[])}, Reflection.StaticLookup);
#endif
                }

                return _drawEditorHeaderItemsMethod;
            }
        }

        public static Type type
        {
            get { return typeof(EditorGUIUtility); }
        }

        public static void DrawEditorHeaderItems(Rect rect, Object[] objects, int id)
        {
#if UNITY_2019_3_OR_NEWER
            drawEditorHeaderItemsMethod.Invoke(null, new object[] { rect, objects, id });
#else
            drawEditorHeaderItems.Invoke(null, new object[] { rect, objects });
#endif
        }
    }
}