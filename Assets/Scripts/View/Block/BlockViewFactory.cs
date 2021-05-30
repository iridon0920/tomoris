using UnityEngine;

public class BlockViewFactory : MonoBehaviour
{
    [SerializeField]
    private BlockView RedBlock;
    [SerializeField]
    private BlockView BlueBlock;
    [SerializeField]
    private BlockView GreenBlock;
    [SerializeField]
    private BlockView LightBlueBlock;
    [SerializeField]
    private BlockView OrangeBlock;
    [SerializeField]
    private BlockView PurpleBlock;
    [SerializeField]
    private BlockView YellowBlock;

    public BlockView InstantiateBlock(BlockColor blockColor, Vector3 position, Transform transform)
    {
        return Instantiate(
            GetBlockViewByBlockColor(blockColor),
            position,
            Quaternion.identity,
            transform
        );
    }

    private BlockView GetBlockViewByBlockColor(BlockColor blockColor)
    {
        switch (blockColor)
        {
            case BlockColor.Red:
                return RedBlock;
            case BlockColor.Blue:
                return BlueBlock;
            case BlockColor.Green:
                return GreenBlock;
            case BlockColor.LightBlue:
                return LightBlueBlock;
            case BlockColor.Orange:
                return OrangeBlock;
            case BlockColor.Purple:
                return PurpleBlock;
            case BlockColor.Yellow:
                return YellowBlock;
        }
        return null;
    }
}
