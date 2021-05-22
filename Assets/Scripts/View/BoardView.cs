using UnityEngine;

[RequireComponent(typeof(BoardBlockViewList))]
[RequireComponent(typeof(AudioSource))]
public class BoardView : MonoBehaviour
{

    [SerializeField]
    private BoardBlockViewList BlockList;

    [SerializeField]
    private AudioSource EraseSound;

    public void DrawBoardPutBlock(BoardPutBlock boardPutBlock)
    {
        BlockList.InstantiateBlock(boardPutBlock, transform);
    }

    public void DeleteBoardPutBlock(BoardPutBlock boardPutBlock)
    {
        BlockList.RemoveBlock(boardPutBlock);
    }

    public void PlayEraseSound()
    {
        EraseSound.PlayOneShot(EraseSound.clip);
    }

    public void ChangeBoardPutBlockPosition(BoardPutBlock boardPutBlock)
    {
        BlockList.MoveToTargetPosition(boardPutBlock, transform);
    }
}
