using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardController : MonoBehaviour
{
    [SerializeField]
    private GameObject BlockPrefab;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void DrawBlocks(bool[,] statusByPositions)
    {
        for (var x = 0; x < statusByPositions.GetLength(0); x++)
        {
            for (var y = 0; y < statusByPositions.GetLength(1); y++)
            {
                Instantiate(BlockPrefab, new Vector3(x, y, 0), Quaternion.identity);
            }
        }
    }

    public void DrawBlocks(int x, int y, Blocks controlBlocks)
    {
        foreach (var controlBlock in controlBlocks.BlockList)
        {
            var blockPosition = new Vector3(x + controlBlock.X, y + controlBlock.Y, 0);
            Instantiate(BlockPrefab, blockPosition, Quaternion.identity);
        }
    }
}
