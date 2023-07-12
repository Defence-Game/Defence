using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.Experimental.U2D.Animation;

namespace Assets.PixelHeroes.Editor
{
    public class SpriteLibrarySetup : EditorWindow
    {
        public Texture2D SpriteSheet;
        public SpriteLibraryAsset SpriteLibrary;

        [MenuItem("Window/Pixel Heroes/Sprite Library Setup")]
        public static void Init()
        {
            var window = GetWindow<SpriteLibrarySetup>(false, "Sprite Library Setup");

            window.minSize = window.maxSize = new Vector2(300, 150);
            window.Show();
        }

        public void OnGUI()
        {
            EditorGUILayout.LabelField("Fill Sprite Library based on a Sprite Sheet texture", new GUIStyle(EditorStyles.label) { normal = { textColor = Color.yellow } });
            SpriteSheet = (Texture2D) EditorGUILayout.ObjectField(new GUIContent("Sprite Sheet"), SpriteSheet, typeof(Texture2D), false);
            SpriteLibrary = (SpriteLibraryAsset) EditorGUILayout.ObjectField(new GUIContent("Sprite Library"), SpriteLibrary, typeof(SpriteLibraryAsset), false);

            if (GUILayout.Button("Setup"))
            {
                if (SpriteSheet == null)
                {
                    Debug.LogWarning("Sprite Sheet is null");
                }
                else if (SpriteSheet == null)
                {
                    Debug.LogWarning("Sprite Library is null");
                }
                else
                {
                    var path = AssetDatabase.GetAssetPath(SpriteSheet);
                    var sprites = AssetDatabase.LoadAllAssetsAtPath(path).OfType<Sprite>().ToList();

                    foreach (var category in SpriteLibrary.GetCategoryNames().ToList())
                    {
                        foreach (var label in SpriteLibrary.GetCategoryLabelNames(category).ToList())
                        {
                            SpriteLibrary.RemoveCategoryLabel(category, label, true);
                        }
                    }

                    foreach (var sprite in sprites)
                    {
                        if (!sprite.name.Contains("_")) continue;

                        var split = sprite.name.Split('_');

                        SpriteLibrary.AddCategoryLabel(sprite, split[0], split[1]);
                    }

                    Debug.Log("Done!");
                }
            }
        }
    }
}