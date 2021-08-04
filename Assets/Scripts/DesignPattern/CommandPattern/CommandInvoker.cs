using System.Collections.Generic;
using UnityEngine;

public class CommandInvoker : MonoBehaviour
{
    #region Private Field
    private static Queue<ICommand> commandBuffer = null;

    private static List<ICommand> commandHistory = null;
    private static int counter = 0;
    #endregion

    private void Awake()
    {
        commandBuffer = new Queue<ICommand>();
        commandHistory = new List<ICommand>();
    }

    #region Controls
    public static void AddCommand(ICommand command)
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
            ICommand command = commandBuffer.Dequeue();
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
