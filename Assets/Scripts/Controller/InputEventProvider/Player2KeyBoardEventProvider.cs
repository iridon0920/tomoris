using UnityEngine;

public class Player2KeyBoardEventProvider : IInputEventProvider
{
    public bool LeftInput()
    {
        return Input.GetKey(KeyCode.LeftArrow);
    }
    public bool RightInput()
    {
        return Input.GetKey(KeyCode.RightArrow);
    }
    public bool DownInput()
    {
        return Input.GetKey(KeyCode.DownArrow);
    }
    public bool LeftSpinInput()
    {
        return Input.GetKeyDown(KeyCode.RightAlt);
    }
    public bool RightSpinInput()
    {
        return Input.GetKeyDown(KeyCode.RightControl);
    }
}
