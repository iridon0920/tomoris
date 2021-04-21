using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using UniRx;
using UniRx.Triggers;

public class ControlBlocksPresenter : MonoBehaviour
{
    [SerializeField]
    private ControlBlocksView ControlBlocksView;

    public void DrawControlBlocks(IControlBlocks controlBlocks)
    {
        ControlBlocksView.DrawControlBlocks(controlBlocks);
    }
}
