/*           INFINITY CODE          */
/*     https://infinity-code.com    */

using System.Collections.Generic;
using UnityEditor;

namespace InfinityCode.uContext
{
    public static partial class Prefs
    {
        public static bool renameByShortcut = true;

        private class RenameManager : PrefManager, IHasShortcutPref
        {
            public override float order
            {
                get { return -45; }
            }

            public override void Draw()
            {
                renameByShortcut = EditorGUILayout.ToggleLeft("Rename By Shortcut (F2)", renameByShortcut, EditorStyles.boldLabel);
            }

            public IEnumerable<Shortcut> GetShortcuts()
            {
                return new[]
                {
                    new Shortcut("Rename Selected Items", "Everywhere", "F2"),
                };
            }
        }
    }
}