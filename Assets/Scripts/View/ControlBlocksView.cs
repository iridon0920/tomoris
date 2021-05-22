using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ControlBlockViewList))]
[RequireComponent(typeof(CursorViewFactory))]
[RequireComponent(typeof(AudioSource))]
public class ControlBlocksView : MonoBehaviour
{
    private int PlayerId;
    private CursorView Cursor;
    [SerializeField]
    private ControlBlockViewList BlockList;
    [SerializeField]
    private CursorViewFactory CursorViewFactory;
    [SerializeField]
    private AudioSource CollisionSound;

    public void SetPlayerId(int playerId)
    {
        PlayerId = playerId;
    }

    public async void DrawControlBlocks(IControlBlocks controlBlocks)
    {
        Cursor = await CursorViewFactory.Instantiate(PlayerId, controlBlocks, transform);

        BlockList.InstantiateBlocks(controlBlocks, transform);
    }

    public void DeleteControlBlocks()
    {
        if (Cursor)
        {
            Cursor.Remove();
        }

        BlockList.RemoveAll();
    }

    public void ChangeControlBlocksPosition(IControlBlocks controlBlocks)
    {
        if (Cursor)
        {
            Cursor.MoveToTargetPosition(controlBlocks, transform);
        }

        BlockList.MoveToTargetPosition(controlBlocks, transform);
    }

    public void PlayCollisionSound()
    {
        CollisionSound.PlayOneShot(CollisionSound.clip);
    }
}
