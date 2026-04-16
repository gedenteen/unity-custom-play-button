using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

namespace ASze.CustomPlayButton
{
    public class GuiSettings : ScriptableObject
    {
        [Header("Custom Play Button")]
        public float DropdownWidth = 250f;
        public float ColumnWidth = 350f;

        [Header("Slider timeScale")]
        public float TimeScaleLabelWidth = 100f;
        public float TimeScaleSliderWidth = 150f;
        public float MaxTimeScaleValue = 2f;

        [Header("Compile time")]
        public bool ShouldShowCompileTime = true;
    }
}