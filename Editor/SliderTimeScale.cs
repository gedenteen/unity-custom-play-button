using UnityEngine;
using UnityEditor;

#if UNITY_TOOLBAR_EXTENDER
using UnityToolbarExtender;
#else
using UnityEditor.PackageManager;
using UnityEditor.PackageManager.Requests;
#endif

#if UNITY_2019_1_OR_NEWER
using VisualElement = UnityEngine.UIElements.VisualElement;
#else
using VisualElement = UnityEngine.Experimental.UIElements.VisualElement;
#endif

namespace ASze.CustomPlayButton
{
    [InitializeOnLoad]
    public static class SliderTimeScale
    {
        static float m_sliderValue = 1f;
    
        static SliderTimeScale()
        {
            m_sliderValue = EditorPrefs.GetFloat("TimeScaleSlider", 1f);
            ToolbarExtender.RightToolbarGUI.Add(OnToolbarGUI);
        }

        static void OnToolbarGUI()
        {
            GUILayout.Label($"TimeScale: {m_sliderValue:F2}", GUILayout.Width(95f));
          
            EditorGUI.BeginChangeCheck();
            m_sliderValue = GUILayout.HorizontalSlider(m_sliderValue, 0f, 2f, GUILayout.Width(150f));
          
            if (EditorGUI.EndChangeCheck())
            {
                ApplyTimeScale(m_sliderValue);
            }
          
            // Sync slider with current Time.timeScale
            // (in case the timeScale has changed in another script)
            if (Mathf.Abs(Time.timeScale - m_sliderValue) > 0.01f)
            {
                m_sliderValue = Time.timeScale;
            }
            
            if (GUILayout.Button(new GUIContent("Reset", "Set TimeScale to 1f"), GUILayout.ExpandWidth(false)))
            {
                ApplyTimeScale(1f);
            }
        }    
        
        static void ApplyTimeScale(float value)
        {
            m_sliderValue = value;
            Time.timeScale = value;
            // EditorPrefs.SetFloat("TimeScaleSlider", value);
        }
    }
}