using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ManipulateComponent))]
[CanEditMultipleObjects]
public class ColorChangerEditor : Editor
{
    #region Private Field
    private ManipulateComponent manipulator = null;

    private bool changeColorFoldout = false;
    private bool colorChangePressed = false;
    #endregion

    private void OnEnable()
    {
        manipulator = (ManipulateComponent)target;
    }

    public override void OnInspectorGUI()
    {
        InitGUIStyles();

        GUILayout.Label("Component Manipulator", titleStyle);
        EditorGUILayout.Space();

        #region Change Color
        EditorGUI.indentLevel++;
        EditorGUILayout.BeginVertical(GUI.skin.GetStyle("HelpBox"));
        {
            changeColorFoldout = EditorGUILayout.Foldout(changeColorFoldout, "Change Color");
            if (changeColorFoldout)
            {
                manipulator.GetSelectedColor = EditorGUILayout.ColorField("Color", manipulator.GetSelectedColor);
                EditorGUILayout.Space(1);

                GUILayout.BeginHorizontal();
                {
                    GUILayout.Space(15);
                    EditorGUILayout.BeginVertical();
                    {
                        if (Selection.count > 1) GUI.enabled = false;
                        colorChangePressed = GUILayout.Button("Change");
                        if (colorChangePressed)
                        {
                            ChangeColor(ref manipulator);
                        }
                        GUI.enabled = true;
                    }
                    EditorGUILayout.EndVertical();
                }
                GUILayout.EndHorizontal();
                EditorGUILayout.Space(3);
            }
        }
        EditorGUILayout.EndVertical();
        EditorGUILayout.Space(6);
        EditorGUI.indentLevel--;
        #endregion
    }

    #region Manipulate Functions
    /// <summary>
    /// Change color of the GameObject selected.
    /// </summary>
    private void ChangeColor(ref ManipulateComponent manipulator)
    {
        var propertyBlock = new MaterialPropertyBlock();
        propertyBlock.SetColor("_BaseColor", manipulator.GetSelectedColor);

        if (manipulator.GetRenderer == null)
            manipulator.GetRenderer = manipulator.GetComponent<Renderer>();

        manipulator.GetRenderer.SetPropertyBlock(propertyBlock);
        SceneView.RepaintAll();
    }
    #endregion

    #region GUI Styles
    private GUIStyle titleStyle = null;
    
    private bool isGUIInitialized = false;
    #endregion

    #region GUI Initialize
    private void InitGUIStyles()
    {
        if (isGUIInitialized) return;

        titleStyle = new GUIStyle(EditorStyles.label);
        titleStyle.fontSize = 16;
        titleStyle.fontStyle = FontStyle.Bold;
        titleStyle.wordWrap = true;

        isGUIInitialized = true;
    }
    #endregion
}
