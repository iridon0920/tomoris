using UnityEngine;
using UnityEngine.AddressableAssets;
using System.Threading.Tasks;

[RequireComponent(typeof(CursorViewPosition))]
public class CursorViewFactory : MonoBehaviour
{
    [SerializeField]
    private CursorViewPosition CursorViewPosition;
    public async Task<CursorView> Instantiate(int playerId, IControlBlocks controlBlocks, Transform transform)
    {
        var handle = Addressables.InstantiateAsync(
            GetAddressByPlayerId(playerId),
            CursorViewPosition.GetPositionByControlBlocks(controlBlocks, transform),
            Quaternion.identity,
            transform
        );

        await handle.Task;
        return handle.Result.GetComponent<CursorView>();
    }

    private string GetAddressByPlayerId(int playerId)
    {
        if (playerId == 1)
        {
            return "Sprites/Cursor/1p";
        }
        else
        {
            return "Sprites/Cursor/2p";
        }
    }

}
