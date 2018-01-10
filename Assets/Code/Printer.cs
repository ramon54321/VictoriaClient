using UnityEngine;

class Printer : SharpLogger.Printer
{
    public override void Print(string message)
    {
        Debug.Log(message);
    }
}
