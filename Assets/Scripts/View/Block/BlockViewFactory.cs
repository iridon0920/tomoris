using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using UnityEngine.AddressableAssets;

public class BlockViewFactory : MonoBehaviour
{
    public async UniTask<BlockView> InstantiateBlock(BlockColor blockColor, Vector3 position, Transform transform)
    {
        var handle = Addressables.InstantiateAsync(
            GetAddressByBlockColor(blockColor),
            position,
            Quaternion.identity,
            transform
        );

        await handle.ToUniTask();
        return handle.Result.GetComponent<BlockView>();
    }

    private string GetAddressByBlockColor(BlockColor blockColor)
    {
        switch (blockColor)
        {
            case BlockColor.Red:
                return "Sprites/RedBlock";
            case BlockColor.Blue:
                return "Sprites/BlueBlock";
            case BlockColor.Green:
                return "Sprites/GreenBlock";
            case BlockColor.LightBlue:
                return "Sprites/LightBlueBlock";
            case BlockColor.Orange:
                return "Sprites/OrangeBlock";
            case BlockColor.Purple:
                return "Sprites/PurpleBlock";
            case BlockColor.Yellow:
                return "Sprites/YellowBlock";
        }
        return "";
    }
}
