#if UNITY_TOOLBAR_EXTENDER
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.Compilation;
using UnityToolbarExtender;

#if UNITY_2019_1_OR_NEWER
using VisualElement = UnityEngine.UIElements.VisualElement;
#else
using VisualElement = UnityEngine.Experimental.UIElements.VisualElement;
#endif

namespace ASze.CustomPlayButton
{
    [InitializeOnLoad]
    public static class RightPart
    {
        static float m_sliderValue = 1f;
    
        static RightPart()
        {
            m_sliderValue = EditorPrefs.GetFloat("TimeScaleSlider", 1f);
            ToolbarExtender.RightToolbarGUI.Add(OnToolbarGUI);

            CompilationPipeline.compilationFinished += OnCompilationFinished;
        }

        static void OnToolbarGUI()
        {
            GUILayout.Space(CustomPlayButton.GuiSettings.MarginsWidth);

            GUILayout.Label($"TimeScale: {m_sliderValue:F2}",
                GUILayout.Width(CustomPlayButton.GuiSettings.TimeScaleLabelWidth)
            );

            EditorGUI.BeginChangeCheck();
            m_sliderValue = GUILayout.HorizontalSlider(
                m_sliderValue,
                0f,
                CustomPlayButton.GuiSettings.MaxTimeScaleValue,
                GUILayout.Width(CustomPlayButton.GuiSettings.TimeScaleSliderWidth)
            );
          
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

            if (CustomPlayButton.GuiSettings.ShouldShowCompileTime)
            {
                GUILayout.Label($"Last compile: {EditorPrefs.GetString("LastCompileTime", "—")}");
            }
        }

        static void ApplyTimeScale(float value)
        {
            m_sliderValue = value;
            Time.timeScale = value;
            // EditorPrefs.SetFloat("TimeScaleSlider", value);
        }

        private static void OnCompilationFinished(object context)
        {
            // Convert UTC time to local system timezone
            var localTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.Local);
            EditorPrefs.SetString("LastCompileTime", localTime.ToString("HH:mm:ss"));
        }
    }
}
#endif