using UnityEngine;

public class CursorViewPosition : MonoBehaviour
{
    const int CURSOR_POSITION_Y = 3;

    public Vector3 GetPositionByControlBlocks(IControlBlocks controlBlocks, Transform transform)
    {
        Debug.Log(transform.position);
        var cursorPosition = transform.position;
        cursorPosition.x += controlBlocks.X;
        cursorPosition.y += controlBlocks.Y + CURSOR_POSITION_Y;

        return cursorPosition;
    }
}
