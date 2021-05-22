using UnityEngine;
using UnityEngine.AddressableAssets;
using System.Threading.Tasks;
public class CursorViewFactory : MonoBehaviour
{
    public async Task<CursorView> Instantiate(int playerId, Vector3 position, Transform transform)
    {
        var handle = Addressables.InstantiateAsync(
            GetAddressByPlayerId(playerId),
            position,
            Quaternion.identity,
            transform
        );

        await handle.Task;
        Debug.Log(handle.Result);
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
