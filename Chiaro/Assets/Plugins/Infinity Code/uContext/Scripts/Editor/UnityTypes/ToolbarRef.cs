/*           INFINITY CODE          */
/*     https://infinity-code.com    */

using System;
using System.Reflection;

namespace InfinityCode.uContext.UnityTypes
{
    public static class ToolbarRef
    {
        private static Type _type;

        public static Type type
        {
            get
            {
                if (_type == null) _type = Reflection.GetEditorType("Toolbar");
                return _type;
            }
        }

#if !UNITY_2020_1_OR_NEWER
        private static FieldInfo _toolCountField;

        private static FieldInfo toolCountField
        {
            get
            {
                if (_toolCountField == null) _toolCountField = type.GetField("k_ToolCount", Reflection.StaticLookup);
                return _toolCountField;
            }
        }
#endif

        public static int GetToolCount()
        {
#if !UNITY_2020_1_OR_NEWER
            return (int)toolCountField.GetValue(null);
#else
            return 7;
#endif
        }
    }
}