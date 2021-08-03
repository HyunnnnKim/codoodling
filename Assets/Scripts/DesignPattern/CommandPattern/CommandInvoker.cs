using System.Collections.Generic;
using UnityEngine;

public class CommandInvoker : MonoBehaviour
{
    #region Private Field
    private static Queue<ICommand> commandBuffer;

    #endregion

    private void Awake()
    {
        commandBuffer = new Queue<ICommand>();
    }

    #region Controls
    public static void AddCommand(ICommand command)
    {
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
            commandBuffer.Dequeue().Execute();
        }
    }
    #endregion
}
