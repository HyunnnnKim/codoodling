using UnityEngine;

public class ManipulateComponent : MonoBehaviour
{
    private Renderer manipulatorRenderer = null;
    public Renderer GetRenderer { get => manipulatorRenderer; set => manipulatorRenderer = value; }

    private Color selectedColor = Color.white;
    public Color GetSelectedColor { get => selectedColor; set => selectedColor = value; }
}
