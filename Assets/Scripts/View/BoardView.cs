using System.Collections.Generic;
using UnityEngine;
public class BoardView : MonoBehaviour
{
    const string PREFIX = "Board Block ";

    [SerializeField]
    private GameObject BlockPrefab;
    private List<GameObject> Blocks = new List<GameObject>();
    public void DrawBoardPutBlock(BoardPutBlock block)
    {
        var newPosition = transform.position;
        newPosition.x += block.GetX();
        newPosition.y += block.GetY();
        var newBlock = Instantiate(BlockPrefab, newPosition, Quaternion.identity, transform);
        newBlock.name = PREFIX + block.Id.ToString();
        Blocks.Add(newBlock);
    }

    public void DeleteBoardPutBlock(BoardPutBlock block)
    {
        var deleteTargetBlock = transform.Find(PREFIX + block.Id);
        Destroy(deleteTargetBlock.gameObject);
    }

    public void ChangeBoardPutBlockPosition(BoardPutBlock block)
    {
        var changeTargetBlock = transform.Find(PREFIX + block.Id);

        var newPosition = transform.position;
        newPosition.x += block.GetX();
        newPosition.y += block.GetY();
        changeTargetBlock.position = newPosition;
    }
}
