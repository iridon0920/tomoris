using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(BoardBlockViewPosition))]
public class BoardBlockViewList : MonoBehaviour
{
    const string PREFIX = "Board Block ";

    private List<BlockView> Blocks = new List<BlockView>();

    [SerializeField]
    private BlockViewFactory BlockViewFactory;
    [SerializeField]
    private BoardBlockViewPosition BoardBlockViewPosition;

    public void InstantiateBlock(BoardPutBlock boardPutBlock, Transform transform)
    {
        var blockObject = BlockViewFactory.InstantiateBlock(
            boardPutBlock.GetBlockColor(),
            BoardBlockViewPosition.GetPositionByBoardPutBlock(boardPutBlock, transform),
            transform
        );

        blockObject.name = GetBoardPutBlockId(boardPutBlock);
        Blocks.Add(blockObject);
    }

    public void RemoveBlock(BoardPutBlock boardPutBlock)
    {
        var deleteTargetBlock = Blocks.Find(
            block => block.name == GetBoardPutBlockId(boardPutBlock)
        );

        deleteTargetBlock.Erase();
        Blocks.Remove(deleteTargetBlock);
    }

    public void MoveToTargetPosition(BoardPutBlock boardPutBlock, Transform transform)
    {
        var changeTargetBlock = Blocks.Find(
            block => block.name == GetBoardPutBlockId(boardPutBlock)
        );

        changeTargetBlock.FallToTargetPosition(
            BoardBlockViewPosition.GetPositionByBoardPutBlock(boardPutBlock, transform)
        );
    }

    private string GetBoardPutBlockId(BoardPutBlock boardPutBlock)
    {
        return PREFIX + boardPutBlock.Id.ToString();
    }
}
