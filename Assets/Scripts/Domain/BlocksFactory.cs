using System;
using System.Collections.Generic;
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
                return CreateIShapedBlocks();
            case 1:
                return CreateLShapedBlocks();
            case 2:
                return CreateJShapedBlocks();
            case 3:
                return CreateZShapedBlocks();
            case 4:
                return CreateSShapedBlocks();
            case 5:
                return CreateTShapedBlocks();
            case 6:
                return CreateOShapedBlocks();
            default:
                return CreateIShapedBlocks();
        }
    }

    private Blocks CreateIShapedBlocks()
    {
        return new Blocks(
                new List<IBlock>
                {
                    new Block(0, 2),
                    new Block(0, 1),
                    new Block(0, 0),
                    new Block(0, -1)
                }
        );
    }

    private Blocks CreateLShapedBlocks()
    {
        return new Blocks(
                new List<IBlock>
                {
                    new Block(0, 1),
                    new Block(0, 0),
                    new Block(0, -1),
                    new Block(1, -1)
                }
        );
    }

    private Blocks CreateJShapedBlocks()
    {
        return new Blocks(
                new List<IBlock>
                {
                    new Block(0, 1),
                    new Block(0, 0),
                    new Block(0, -1),
                    new Block(-1, -1)
                }
        );
    }

    private Blocks CreateZShapedBlocks()
    {
        return new Blocks(
                new List<IBlock>
                {
                    new Block(-1, 1),
                    new Block(0, 1),
                    new Block(0, 0),
                    new Block(1, 0)
                }
        );
    }

    private Blocks CreateSShapedBlocks()
    {
        return new Blocks(
                new List<IBlock>
                {
                    new Block(1, 1),
                    new Block(0, 1),
                    new Block(0, 0),
                    new Block(-1, 0)
                }
        );
    }

    private Blocks CreateTShapedBlocks()
    {
        return new Blocks(
                new List<IBlock>
                {
                    new Block(0, 1),
                    new Block(-1, 0),
                    new Block(0, 0),
                    new Block(1, 0)
                }
        );
    }

    private Blocks CreateOShapedBlocks()
    {
        return new Blocks(
                new List<IBlock>
                {
                    new Block(0, 1),
                    new Block(1, 1),
                    new Block(0, 0),
                    new Block(1, 0)
                }
        );
    }


}
