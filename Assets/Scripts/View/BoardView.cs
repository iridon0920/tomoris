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

    public void DeleteBoardPutBlock(BoardPutBlock block)
    {
        var deleteTargetBlock = transform.Find(PREFIX + block.Id.ToString());
        Destroy(deleteTargetBlock.gameObject);
    }

    public void ChangeBoardPutBlockPosition(BoardPutBlock block)
    {
        var changeTargetBlock = transform.Find(PREFIX + block.Id.ToString()).GetComponent<BlockView>();

        var newPosition = transform.position;
        newPosition.x += block.GetX();
        newPosition.y += block.GetY();
        changeTargetBlock.FallToTargetPosition(newPosition);
    }
}
