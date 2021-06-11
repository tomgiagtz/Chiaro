/*           INFINITY CODE          */
/*     https://infinity-code.com    */

using System;
using System.Reflection;
using UnityEditor;

namespace InfinityCode.uContext.UnityTypes
{
    public static class EditorGUIRef
    {
        private static FieldInfo _recycledEditorField;

        private static FieldInfo recycledEditorField
        {
            get
            {
                if (_recycledEditorField == null) _recycledEditorField = type.GetField("s_RecycledEditor", Reflection.StaticLookup);
                return _recycledEditorField;
            }
        }

        public static Type type
        {
            get { return typeof(EditorGUI); }
        }

        public static object GetRecycledEditor()
        {
            return recycledEditorField.GetValue(null);
        }
    }
}