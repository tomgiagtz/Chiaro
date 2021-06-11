/*           INFINITY CODE          */
/*     https://infinity-code.com    */

using System;
using InfinityCode.uContext;
using InfinityCode.uContext.Actions;
using InfinityCode.uContext.Integration;
using InfinityCode.uContext.Tools;
using InfinityCode.uContext.Windows;
using UnityEditor;
using UnityEngine;

namespace InfinityCode.uContextPro.Actions
{
    public class Snapping : ActionItem
    {
        protected override bool closeOnSelect
        {
            get { return false; }
        }

        public override float order
        {
            get { return -990; }
        }

        private void AlignSelection(SnapAxis axis)
        {
            foreach (GameObject gameObject in Selection.gameObjects)
            {
                SnapHelper.Snap(gameObject.transform, axis);
            }

            uContextMenu.Close();
        }

        protected override void Init()
        {
            guiContent = new GUIContent(Icons.proGridsWhite, "Snapping");
        }

        public override void Invoke()
        {
            GenericMenu menu = new GenericMenu();

            if (!ProGrids.snapEnabled)
            {
                menu.AddItem(new GUIContent("Snap To Grid"), EditorSnapSettings.gridSnapEnabled, () => EditorSnapSettings.gridSnapEnabled = !EditorSnapSettings.gridSnapEnabled);
                menu.AddSeparator(string.Empty);
            }

            if (SelectionBoundsManager.hasBounds)
            {
                if (!ProGrids.snapEnabled)
                {
                    menu.AddItem(new GUIContent("Align Selection To Grid/All Axis"), false, () => AlignSelection(SnapAxis.All));
                    menu.AddItem(new GUIContent("Align Selection To Grid/X"), false, () => AlignSelection(SnapAxis.X));
                    menu.AddItem(new GUIContent("Align Selection To Grid/Y"), false, () => AlignSelection(SnapAxis.Y));
                    menu.AddItem(new GUIContent("Align Selection To Grid/Z"), false, () => AlignSelection(SnapAxis.Z));
                }

                Vector3 boundsSize = SelectionBoundsManager.bounds.size;
                boundsSize.x = Mathf.Round(boundsSize.x * 1000) / 1000;
                boundsSize.y = Mathf.Round(boundsSize.y * 1000) / 1000;
                boundsSize.z = Mathf.Round(boundsSize.z * 1000) / 1000;

                menu.AddItem(new GUIContent("Selection/X (" + boundsSize.x + ")"), false, () => SetSnapValue(boundsSize.x));
                menu.AddItem(new GUIContent("Selection/Y (" + boundsSize.y + ")"), false, () => SetSnapValue(boundsSize.y));
                menu.AddItem(new GUIContent("Selection/Z (" + boundsSize.z + ")"), false, () => SetSnapValue(boundsSize.z));
            }

            float[] values = {0.1f, 0.25f, 0.5f, 1, 1.5f, 2, 3, 4, 8};
            float snapValue = SnapHelper.value;
            foreach (float v in values)
            {
                menu.AddItem(new GUIContent(v.ToString()), Math.Abs(snapValue - v) < float.Epsilon, () => SetSnapValue(v));
            }

            menu.AddItem(new GUIContent("Set Custom Value"), false, () =>
            {
                InputDialog.Show("Enter snapping value", snapValue.ToString(), OnSnapValueEntered);
            });

            menu.ShowAsContext();
        }

        private void OnSnapValueEntered(string s)
        {
            float v;
            if (float.TryParse(s, out v)) SetSnapValue(v);
        }

        private void SetSnapValue(float value)
        {
            SnapHelper.value = value;
            uContextMenu.Close();
        }
    }
}