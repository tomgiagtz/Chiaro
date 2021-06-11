/*           INFINITY CODE          */
/*     https://infinity-code.com    */

using System;
using UnityEditor;
using UnityEngine;

namespace InfinityCode.uContext.Windows
{
    public partial class CreateBrowser
    {
        public class PrefabItem : Item
        {
            public string path;
            [NonSerialized]
            private bool previewLoaded;
            private static Texture _prefabTexture;

            private static Texture prefabTexture
            {
                get
                {
                    if (_prefabTexture == null) _prefabTexture = EditorIconContents.prefab.image;
                    return _prefabTexture;
                }
            }

            public PrefabItem(string label, string path)
            {
                if (label.Length < 8) return;

                this.label = label.Substring(0, label.LastIndexOf("."));
                this.path = path;
            }

            public override void Dispose()
            {
                base.Dispose();

                path = null;
                previewEditor = null;
            }

            public override void Draw()
            {
                if (content.image == null || !previewLoaded)
                {
                    content.image = prefabTexture;
                    previewLoaded = false;

                    {
                        GameObject asset = AssetDatabase.LoadAssetAtPath<GameObject>(path);
                        if (AssetPreview.IsLoadingAssetPreview(asset.GetInstanceID()))
                        {
                            content.image = prefabTexture;
                            GUI.changed = true;
                        }
                        else
                        {
                            content.image = AssetPreview.GetAssetPreview(asset);
                            if (content.image != null && content.image.name != "Prefab Icon" && content.image.name != "d_Prefab Icon") previewLoaded = true;
                        }
                    }
                }

                base.Draw();
            }

            public void DrawPreview()
            {
                if (previewPrefab != this && previewEditor != null)
                {
                    DestroyImmediate(previewEditor);
                }

                if (previewEditor == null)
                {
                    previewPrefab = this;
                    previewEditor = Editor.CreateEditor(AssetDatabase.LoadAssetAtPath<GameObject>(path));
                }

                if (previewEditor != null) previewEditor.OnInteractivePreviewGUI(GUILayoutUtility.GetRect(128, 128), Styles.grayRow);
            }

            protected override void InitContent()
            {
                _content = new GUIContent(label);
            }

            public override void OnClick()
            {
                if (instance.OnSelectPrefab != null) instance.OnSelectPrefab(path);
                instance.Close();
            }
        }
    }
}