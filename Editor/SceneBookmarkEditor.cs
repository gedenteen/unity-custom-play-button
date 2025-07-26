using UnityEditor;
using UnityEngine;

namespace ASze.CustomPlayButton
{
    // For buttons in Inspector (for SO SceneBookmark)
    [CustomEditor(typeof(SceneBookmark))]
    public class SceneBookmarkEditor : Editor
    {
        private GUIStyle _buttonStyle;

        private void InitStyles()
        {
            if (_buttonStyle != null) return;

            _buttonStyle = new GUIStyle(GUI.skin.button)
            {
                fixedHeight = 30,
                fontSize = 14,
                fontStyle = FontStyle.Bold
            };
        }

        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            GUILayout.Space(10);

            InitStyles();

            if (GUILayout.Button("Add Current Scene", _buttonStyle))
            {
                var bookmark = (SceneBookmark)target;
                bookmark.AddCurrentScene();
            }
        }
    }
}