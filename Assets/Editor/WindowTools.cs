using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEditor.U2D;
using UnityEngine;
using UnityEngine.U2D;

namespace Hamster.TouchPuzzle.Editor {
    public class WindowTools : EditorWindow {
        [MenuItem("Tools/Editor/EventActionEditor")]
        static void OpenEventActionEditor() {
            List<Assembly> assemblies = new List<Assembly>{ typeof(BPAction_HasItem).Assembly};
            BlackPrintEditor.ShowEventActionEditor(assemblies);
        }

        [MenuItem("Assets/To Atlas")]
        static void PackageAtlas() {
            Debug.Log("Start Create Sprite Atlas");
            if (null == Selection.objects ||
                0 >= Selection.objects.Length ||
                0 >= Selection.assetGUIDs.Length) {
                Debug.Log("请选中图片所在的文件夹");
                return;
            }

            for (int index = 0; index < Selection.objects.Length; index++) {

                string path = AssetDatabase.GUIDToAssetPath(Selection.assetGUIDs[index]);

                if (string.IsNullOrEmpty(path)) {
                    Debug.Log("请选中图片所在的文件夹");
                    return;
                }

                string fileName = System.IO.Path.GetFileName(path);

                List<Object> spriteList = new List<Object>();
                string[] files = AssetDatabase.FindAssets("t:Texture", new string[] { path });
                for (int i = 0; i < files.Length; i++) {
                    string filePath = AssetDatabase.GUIDToAssetPath(files[i]);
                    string assetPath = filePath.Substring(filePath.IndexOf("Assets"));
                    Texture texture = AssetDatabase.LoadAssetAtPath<Texture>(assetPath);
                    if (texture) {
                        spriteList.Add(texture);
                    }
                }
                SpriteAtlas spriteAtlas = new SpriteAtlas();

                SpriteAtlasExtensions.Add(spriteAtlas, spriteList.ToArray());
                SpriteAtlasPackingSettings spriteAtlasPackingSettings = new SpriteAtlasPackingSettings {
                    enableRotation = false,
                    enableTightPacking = false,
                    padding = 4,
                    blockOffset = 1
                };
                SpriteAtlasExtensions.SetPackingSettings(spriteAtlas, spriteAtlasPackingSettings);
                string spritePath = "Assets/Res/SpriteAtlas/" + fileName + ".spriteatlas";
                spriteAtlas.SetIncludeInBuild(false);
                AssetDatabase.CreateAsset(spriteAtlas, spritePath);
                // AssetImporter importer = AssetImporter.GetAtPath(spritePath);
                // importer.assetBundleName = "atlas/" + fileName.ToLower();
            }
            AssetDatabase.Refresh();

            Debug.Log("End Create Sprite Atlas");
        }
    }
}
