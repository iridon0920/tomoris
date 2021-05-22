using UnityEngine;

public class BoardBlockViewPosition : MonoBehaviour
{
    public Vector3 GetPositionByBoardPutBlock(BoardPutBlock block, Transform transform)
    {
        var blockPosition = transform.position;
        blockPosition.x += block.GetX();
        blockPosition.y += block.GetY();

        return blockPosition;
    }
}
