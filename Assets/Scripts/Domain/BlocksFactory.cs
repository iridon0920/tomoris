using System;
using Zenject;

public class BlocksFactory
{
    private readonly Random Random;
    const int NUMBER_OF_TYPES = 7;

    [Inject]
    public BlocksFactory(Random random)
    {
        Random = random;
    }

    public Blocks Create()
    {
        switch (Random.Next(NUMBER_OF_TYPES))
        {
            case 0:
                return new IShapedBlocks();
            case 1:
                return new LShapedBlocks();
            case 2:
                return new JShapedBlocks();
            case 3:
                return new ZShapedBlocks();
            case 4:
                return new SShapedBlocks();
            case 5:
                return new TShapedBlocks();
            case 6:
                return new OShapedBlocks();
            default:
                return new IShapedBlocks();
        }
    }

}
