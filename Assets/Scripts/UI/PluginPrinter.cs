using System;
using System.Runtime.InteropServices;
using TMPro;
using UnityEngine;

public class PluginPrinter : MonoBehaviour
{
    [DllImport("TestPlugin")]
    private static extern IntPtr SayHello();

    [DllImport("TestPlugin")]
    private static extern int AddTwoIntegers(int a, int b);

    [DllImport("TestPlugin")]
    private static extern float AddTwoFloats(float a, float b);

    [SerializeField]
    private TextMeshProUGUI output = null;

    private void Start()
    {
        output.text = Marshal.PtrToStringAnsi(SayHello());
        //output.text = AddTwoIntegers(1, 2).ToString();
        //output.text = AddTwoFloats(1.3f, 2.4f).ToString();
    }
}