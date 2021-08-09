using System.Collections.Generic;
using UnityEngine;

public class ObjectPlaceCommandInvoker : MonoBehaviour
{
    #region Private Field
    private static Queue<IObjectPlaceCommand> commandBuffer = null;

    private static List<IObjectPlaceCommand> commandHistory = null;
    private static int counter = 0;
    #endregion

    private void Awake()
    {
        commandBuffer = new Queue<IObjectPlaceCommand>();
        commandHistory = new List<IObjectPlaceCommand>();
    }

    #region Controls
    public static void AddCommand(IObjectPlaceCommand command)
    {
        while (commandHistory.Count > counter)
        {
            commandHistory.RemoveAt(counter);
        }
        commandBuffer.Enqueue(command);
    }
    #endregion

    private void Update()
    {
        ExecuteCommands();
    }

    #region
    private static void ExecuteCommands()
    {
        if (commandBuffer.Count > 0)
        {
            IObjectPlaceCommand command = commandBuffer.Dequeue();
            command.Execute();
            commandHistory.Add(command);
            counter++;
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Z))
            {
                if (counter > 0)
                {
                    counter--;
                    commandHistory[counter].Undo();
                }
            }
            else if (Input.GetKeyDown(KeyCode.R))
            {
                if (counter < commandHistory.Count)
                {
                    commandHistory[counter].Execute();
                    counter++;
                }
            }
        }
    }
    #endregion
}
