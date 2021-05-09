using UnityEditor;

[CustomEditor(typeof(ProjectReadme))]
[InitializeOnLoad]
public class ProjectReadmeEditor : Editor
{
    /* Static Constructor  */
    static ProjectReadmeEditor()
    {

    }

    protected override void OnHeaderGUI()
    {
        base.OnHeaderGUI();
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
    }


}
