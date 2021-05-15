using System;
using UnityEngine;

[CreateAssetMenu( fileName = "ProjectReadme", menuName = "Codoodling/Readme", order = 1 )]
public class ProjectReadme : ScriptableObject
{
    #region Serialized Field
    
    [SerializeField] private Texture2D icon;
    public Texture2D Icon => icon;
    
    [SerializeField] private string title;
    public string Title => title ?? "NULL";
    
    [SerializeField] private Section[] sections;
    public Section[] Sections => sections;

    [SerializeField] private bool loadedLayout;
    public bool LoadedLayout { get => loadedLayout; set => loadedLayout = value; }

    [SerializeField] private Font font;
    public Font Font => font;
    
    #endregion

    [Serializable]
    public class Section
    {
        public string heading;
        public string text;
        public string linkText;
        public string url;
    }
}
