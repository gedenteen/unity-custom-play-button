using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;

namespace ASze.CustomPlayButton
{
    public class SceneBookmark : ScriptableObject
    {
        public List<SceneAsset> bookmarks = new List<SceneAsset>();

        public bool HasBookmark()
        {
            return bookmarks != null && bookmarks.Count > 0;
        }

        public void RemoveNullValue()
        {
            bookmarks.RemoveAll(item => item == null);
        }
        
        // Adds the currently active scene to the list (if it isn't there yet)
        public void AddCurrentScene()
        {
            var scene = EditorSceneManager.GetActiveScene();
            if (!scene.IsValid()) return;

            var sceneAsset = AssetDatabase.LoadAssetAtPath<SceneAsset>(scene.path);
            if (sceneAsset == null) return;

            if (!bookmarks.Contains(sceneAsset))
            {
                Undo.RecordObject(this, "Add Scene Bookmark");
                bookmarks.Add(sceneAsset);
                EditorUtility.SetDirty(this); // mark SO dirty for saving
            }
        }
    }
}
