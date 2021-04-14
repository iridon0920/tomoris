using Zenject;
using UnityEngine;
public class ControlBlocksAdjuster
{
    private CollisionDetection CollisionDetection { get; }
    private bool isPutBlocksCollision = false;

    [Inject]
    public ControlBlocksAdjuster(CollisionDetection collisionDetection)
    {
        CollisionDetection = collisionDetection;
    }

    public ControlBlocks AdjustBlocksForSideMove(ControlBlocks currentControlBlocks, ControlBlocks newControlBlocks)
    {
        if (CollisionDetection.IsCollision(newControlBlocks))
        {
            return currentControlBlocks;
        }
        return newControlBlocks;
    }
    public ControlBlocks AdjustBlocksForDownMove(ControlBlocks currentControlBlocks, ControlBlocks newControlBlocks)
    {
        if (CollisionDetection.IsCollisionPutPosition(newControlBlocks))
        {
            currentControlBlocks.SetTruePutable();
            return currentControlBlocks;
        }
        return newControlBlocks;
    }

    public ControlBlocks AdjustBlocksForSpin(ControlBlocks currentControlBlocks, ControlBlocks newControlBlocks)
    {
        newControlBlocks = AdjustBlocksForSpinLoop(newControlBlocks);

        if (CollisionDetection.IsCollision(newControlBlocks))
        {
            return currentControlBlocks;
        }
        return newControlBlocks;
    }

    private ControlBlocks AdjustBlocksForSpinLoop(ControlBlocks controlBlocks)
    {
        controlBlocks.Blocks.BlockList.ForEach(block =>
          {
              while (true)
              {
                  if (CollisionDetection.IsCollisionBlockForLeftWall(controlBlocks, block))
                  {
                      controlBlocks.MoveRight();
                  }
                  else if (CollisionDetection.IsCollisionBlockForRightWall(controlBlocks, block))
                  {
                      controlBlocks.MoveLeft();
                  }
                  else if (CollisionDetection.IsCollisionBlockForGround(controlBlocks, block))
                  {
                      controlBlocks.MoveUp();
                  }
                  else if (CollisionDetection.IsCollisionBlockForPutBlock(controlBlocks, block))
                  {
                      if (block.X > 0)
                      {
                          controlBlocks.MoveLeft();
                          controlBlocks.Blocks.BlockList.ForEach(block2 =>
                          {
                              if (block2.X > 0 && block.Y == block2.Y && !block.Equals(block2))
                              {
                                  controlBlocks.MoveLeft();
                              }
                          });
                      }
                      else
                      {
                          controlBlocks.MoveRight();
                          controlBlocks.Blocks.BlockList.ForEach(block2 =>
                        {
                            if (block2.X < 0 && block.Y == block2.Y && !block.Equals(block2))
                            {
                                controlBlocks.MoveRight();
                            }
                        });
                      }
                      break;
                  }
                  else
                  {
                      break;
                  }
              }
          });
        return controlBlocks;
    }
}
