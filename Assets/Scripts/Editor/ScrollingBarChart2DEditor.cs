using UnityEditor;
using UnityEngine;

namespace u2vis.InfoVis
{
    [CustomEditor(typeof(ScrollingBarChart2D))]
    public class ScrollingBarChart2DEditor : BaseVisualizationViewEditor
    {
        protected ScrollingBarChart2D _barChart2D = null;

        protected SerializedProperty
            dataItemMesh_prop,
            barThickness_prop;

        protected override void OnEnable()
        {
            base.OnEnable();
            dataItemMesh_prop = serializedObject.FindProperty("_dataItemMesh");
            barThickness_prop = serializedObject.FindProperty("_barThickness");

            _barChart2D = (ScrollingBarChart2D)serializedObject.targetObject;
        }

        protected override void DrawGUIItems()
        {
            base.DrawGUIItems();
            EditorGUILayout.PropertyField(dataItemMesh_prop, new GUIContent("Data Item Mesh"));
            EditorGUILayout.PropertyField(barThickness_prop, new GUIContent("Bar Thickness"));
        }
    }
}
