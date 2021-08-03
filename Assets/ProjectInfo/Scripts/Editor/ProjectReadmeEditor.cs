using System.IO;
using System.Reflection;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ProjectReadme))]
[InitializeOnLoad]
public class ProjectReadmeEditor : Editor
{
    #region Private Field
    private static string showedreadmeSessionStateName = "ProjectReadmeEditor.showedReadme"; 
    private static float space = 16f;
    #endregion
    
    static ProjectReadmeEditor()
    {
        /* delayCall is called once after all inspectors update. */
        EditorApplication.delayCall += SelectReadmeAutomatically;
    }

    #region Setup
    private static void SelectReadmeAutomatically()
    {
        /* SessionState is Key-Value Store intended for storing and retrieving Editor session state. */
        if (SessionState.GetBool(showedreadmeSessionStateName, false)) return;
        var readme = SelectReadme();
        SessionState.SetBool(showedreadmeSessionStateName, true);

        if (!readme || readme.LoadedLayout) return;
        LoadLayout();
        readme.LoadedLayout = true;
    }

    [MenuItem("Codoodling/Show Readme")]
    private static ProjectReadme SelectReadme()
    {
        /* Search the asset database using the search filter string. keyword t is type */
        var ids = AssetDatabase.FindAssets("ProjectReadme t:ProjectReadme");
        if (ids.Length > 0)
        {
            var readmeObject = AssetDatabase.LoadMainAssetAtPath(AssetDatabase.GUIDToAssetPath(ids[0]));
            /* Access to the selection in the editor. */
            Selection.objects = new []{readmeObject};
            return (ProjectReadme)readmeObject;
        }
        else
        {
            Debug.Log("[Readme] Couldn't find readme for this project.");
            return null;
        }
    }

    private static void LoadLayout()
    {
        var assembly = typeof(EditorApplication).Assembly;
        var windowLayoutType = assembly.GetType("UnityEditor.WindowLayout", true);
        var method = windowLayoutType.GetMethod("LoadWindowLayout", BindingFlags.Static);
        method?.Invoke(null, new object[]{Path.Combine(Application.dataPath, "ProjectInfo/Layout.wlt"), false});
    }
    #endregion
    
    #region GUIStyle Field
    [SerializeField] private GUIStyle linkStyle;
    [SerializeField] private GUIStyle titleStyle;
    [SerializeField] private GUIStyle headingStyle;
    [SerializeField] private GUIStyle bodyStyle;

    private bool isInitialized;
    #endregion

    /* OnHeaderGUI controls how the header of the inspector is rendered. */
    protected override void OnHeaderGUI()
    {
        var readme = (ProjectReadme)target;
        InitGUIStyle(readme);

        var iconWidth = Mathf.Min(EditorGUIUtility.currentViewWidth / 3f - 20f, 128f);
        GUILayout.BeginHorizontal("In BigTitle");
        {
            GUILayout.Label(readme.Icon, GUILayout.Width(iconWidth), GUILayout.Height(iconWidth));
            GUILayout.Label(readme.Title, titleStyle);
        }
        GUILayout.EndHorizontal();
    }

    /* OnInspectorGUI is called everytime the inspector is drawn. */
    public override void OnInspectorGUI()
    {
        var readme = (ProjectReadme)target;
        InitGUIStyle(readme);

        foreach (var section in readme.Sections)
        {
            if (!string.IsNullOrEmpty(section.heading))
            {
                GUILayout.Label(section.heading, headingStyle);
            }
            if (!string.IsNullOrEmpty(section.text))
            {
                GUILayout.Label(section.text, bodyStyle);
            }
            if (!string.IsNullOrEmpty(section.linkText))
            {
                if (LinkLabel(new GUIContent(section.linkText)))
                {
                    Application.OpenURL(section.url);
                }
            }
            GUILayout.Space(space);
        }
    }
    
    #region Initialize GUIStyle
    private void InitGUIStyle(ProjectReadme readme)
    {
        if (isInitialized) return;
        /* GUIStyle is a styling information of a GUI element. */
        bodyStyle = new GUIStyle(EditorStyles.label);
        bodyStyle.fontSize = 23;
        bodyStyle.font = readme.Font;
        bodyStyle.wordWrap = true;
        
        titleStyle = new GUIStyle(bodyStyle);
        titleStyle.fontSize = 80;
        titleStyle.font = readme.Font;
        
        headingStyle = new GUIStyle(bodyStyle);
        headingStyle.fontSize = 36;
        headingStyle.font = readme.Font;

        linkStyle = new GUIStyle(bodyStyle);
        bodyStyle.fontSize = 23;
        bodyStyle.font = readme.Font;
        linkStyle.wordWrap = false;
        linkStyle.normal.textColor = new Color(0x00 / 255f, 0x78 / 255f, 0xDA / 255f, 1f);
        linkStyle.stretchWidth = false;

        isInitialized = true;
    }

    private bool LinkLabel(GUIContent label, params GUILayoutOption[] options)
    {
        var position = GUILayoutUtility.GetRect(label, linkStyle, options);

        Handles.BeginGUI();
        Handles.color = linkStyle.normal.textColor;
        Handles.DrawLine(new Vector3(position.xMin, position.yMax), new Vector3(position.xMax, position.yMax));
        Handles.color = Color.white;
        Handles.EndGUI();
        
        EditorGUIUtility.AddCursorRect(position, MouseCursor.Link);
        return GUI.Button(position, label, linkStyle);
    }
    #endregion
}
