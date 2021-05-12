using System.Collections.Generic;
using UnityEngine;
public class BoardView : MonoBehaviour
{
    const string PREFIX = "Board Block ";

    [SerializeField]
    private BlockViewFactory BlockViewFactory;

    private List<BlockView> Blocks = new List<BlockView>();
    public async void DrawBoardPutBlock(BoardPutBlock block)
    {
        var newPosition = transform.position;
        newPosition.x += block.GetX();
        newPosition.y += block.GetY();

        var blockObject = await BlockViewFactory.InstantiateBlock(
            block.GetBlockColor(),
            newPosition,
            transform
        );

        blockObject.name = PREFIX + block.Id.ToString();
        Blocks.Add(blockObject);
    }

    public void DeleteBoardPutBlock(BoardPutBlock boardBlock)
    {
        var deleteTargetBlock = Blocks.Find(block => block.name == PREFIX + boardBlock.Id.ToString());
        deleteTargetBlock.Erase();
    }

    public void ChangeBoardPutBlockPosition(BoardPutBlock boardBlock)
    {
        var newPosition = transform.position;
        newPosition.x += boardBlock.GetX();
        newPosition.y += boardBlock.GetY();

        var changeTargetBlock = Blocks.Find(block => block.name == PREFIX + boardBlock.Id.ToString());
        changeTargetBlock.FallToTargetPosition(newPosition);
    }
}
