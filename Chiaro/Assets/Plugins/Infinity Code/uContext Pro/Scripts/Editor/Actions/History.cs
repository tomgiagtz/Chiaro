/*           INFINITY CODE          */
/*     https://infinity-code.com    */

using System.Collections.Generic;
using InfinityCode.uContext;
using InfinityCode.uContext.Actions;
using InfinityCode.uContext.Tools;
using InfinityCode.uContext.Windows;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace InfinityCode.uContextPro.Actions
{
    public class History : ActionItem
    {
        protected override bool closeOnSelect
        {
            get { return false; }
        }

        public override float order
        {
            get { return 800; }
        }

        protected override void Init()
        {
            guiContent = new GUIContent(Icons.history, "History");
        }

        public override void Invoke()
        {
            List<SelectionHistory.SelectionRecord> selectionItems = SelectionHistory.records;

            GenericMenu menu = new GenericMenu();

            List<SceneHistoryItem> sceneRecords = ReferenceManager.sceneHistory;
            Scene activeScene = SceneManager.GetActiveScene();
            for (int i = 0; i < sceneRecords.Count; i++)
            {
                SceneHistoryItem r = sceneRecords[i];
                if (r.path == activeScene.path) continue;

                menu.AddItem(new GUIContent("Scenes/" + r.name), false, () =>
                {
                    EditorSceneManager.OpenScene(r.path);
                    uContextMenu.Close();
                });
            }

            for (int i = 0; i < selectionItems.Count; i++)
            {
                int ci = i;
                string names = selectionItems[i].GetShortNames();
                if (string.IsNullOrEmpty(names)) continue;
                menu.AddItem(new GUIContent("Selection/" + names), SelectionHistory.activeIndex == i, () =>
                {
                    SelectionHistory.SetIndex(ci);
                    uContextMenu.Close();
                });
            }

            List<WindowHistory.WindowRecord> recentWindows = WindowHistory.recent;
            for (int i = 0; i < recentWindows.Count; i++)
            {
                WindowHistory.WindowRecord r = recentWindows[i];
                menu.AddItem(new GUIContent("Windows/" + r.title), false, () =>
                {
                    WindowHistory.RestoreRecentWindow(r);
                    uContextMenu.Close();
                });
            }

            menu.ShowAsContext();
        }
    }
}