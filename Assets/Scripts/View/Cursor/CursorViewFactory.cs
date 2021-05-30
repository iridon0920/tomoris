using UnityEngine;

[RequireComponent(typeof(CursorViewPosition))]
public class CursorViewFactory : MonoBehaviour
{
    [SerializeField]
    private CursorViewPosition CursorViewPosition;
    [SerializeField]
    private CursorView Cursor1P;
    [SerializeField]
    private CursorView Cursor2P;

    public CursorView InstantiateCursor(int playerId, IControlBlocks controlBlocks, Transform transform)
    {
        return Instantiate(
            GetCursorViewByPlayerId(playerId),
            CursorViewPosition.GetPositionByControlBlocks(controlBlocks, transform),
            Quaternion.identity,
            transform
        );
    }

    private CursorView GetCursorViewByPlayerId(int playerId)
    {
        if (playerId == 1)
        {
            return Cursor1P;
        }
        else
        {
            return Cursor2P;
        }
    }

}
