using UnityEngine;

public class PlaceCubeCommand : ICommand
{
    Vector3 position;
    Color color;
    Transform cube;

    public PlaceCubeCommand(Vector3 position, Color color, Transform cube)
    {
        this.position = position;
        this.color = color;
        this.cube = cube;
    }

    public void Execute()
    {
        ShapePlacer.PlaceObject(position, color, cube);
    }
}
