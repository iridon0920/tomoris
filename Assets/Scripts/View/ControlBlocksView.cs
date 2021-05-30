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
    [SerializeField]
    private AudioSource SpinSound;

    public void SetPlayerId(int playerId)
    {
        PlayerId = playerId;
    }

    public void DrawControlBlocks(IControlBlocks controlBlocks)
    {
        Cursor = CursorViewFactory.InstantiateCursor(PlayerId, controlBlocks, transform);

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

    public void PlaySpinSound()
    {
        SpinSound.PlayOneShot(SpinSound.clip);
    }
}
