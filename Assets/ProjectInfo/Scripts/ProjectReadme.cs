using System;
using UnityEngine;

public class ProjectReadme : ScriptableObject
{
    #region Serialized Field
    [SerializeField] private Texture2D icon = null;
    [SerializeField] private string title = null;
    [SerializeField] private Section[] sections = null;
    [SerializeField] private bool loadedLayout = false;

    [Serializable]
    public class Section
    {
        private string heading = null;
        private string text = null;
        private string linkText = null;
        private string url = null;

        public Section(string heading, string text, string linkText, string url)
        {
            this.heading = heading;
            this.text = text;
            this.linkText = linkText;
            this.url = url;
        }
    }
    #endregion

    #region Properties
    public Texture2D Icon { get => icon; set => icon = value; }
    public string Title { get => title != null ? title : "NULL"; set => title = value; }
    public Section[] Sections { get => sections; set => sections = value; }
    public bool LoadedLayout { get => loadedLayout; set => loadedLayout = value; }
    #endregion
}
