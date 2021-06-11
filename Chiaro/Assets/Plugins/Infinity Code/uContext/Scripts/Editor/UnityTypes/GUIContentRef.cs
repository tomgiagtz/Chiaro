/*           INFINITY CODE          */
/*     https://infinity-code.com    */

using System;
using System.Reflection;
using UnityEngine;

namespace InfinityCode.uContext.UnityTypes
{
    public static class GUIContentRef
    {
        private static MethodInfo _tempContentMethod;

        private static MethodInfo tempContentMethod
        {
            get
            {
                if (_tempContentMethod == null) _tempContentMethod = Reflection.GetMethod(type, "Temp", new[] { typeof(Texture) });
                return _tempContentMethod;
            }
        }

        public static Type type
        {
            get { return typeof(GUIContent); }
        }

        public static GUIContent TempContent(Texture texture)
        {
            return (GUIContent)tempContentMethod.Invoke(null, new object[] {texture});
        }
    }
}