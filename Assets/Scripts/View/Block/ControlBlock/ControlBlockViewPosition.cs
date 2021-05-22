using UnityEngine;

public class ControlBlockViewPosition : MonoBehaviour
{
    public Vector3 GetPositionByControlBlocks(IControlBlocks controlBlocks, IBlock block, Transform transform)
    {
        var blockPosition = transform.position;
        blockPosition.x += controlBlocks.X + block.X;
        blockPosition.y += controlBlocks.Y + block.Y;

        return blockPosition;
    }
}
