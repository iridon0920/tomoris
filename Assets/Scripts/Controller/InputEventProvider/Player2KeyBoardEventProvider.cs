using UnityEngine;

public class Player2KeyBoardEventProvider : IInputEventProvider
{
    public bool LeftInput()
    {
        return Input.GetKey(KeyCode.A);
    }
    public bool RightInput()
    {
        return Input.GetKey(KeyCode.D);
    }
    public bool DownInput()
    {
        return Input.GetKey(KeyCode.S);
    }
    public bool LeftSpinInput()
    {
        return Input.GetKeyDown(KeyCode.Q);
    }
    public bool RightSpinInput()
    {
        return Input.GetKeyDown(KeyCode.E);
    }
}
