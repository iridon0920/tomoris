using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
namespace Tests
{

    public class ControlBlocksAdjusterTest
    {
        private CollisionDetection CollisionDetection;
        private ControlBlocksAdjuster Adjuster;

        [SetUp]
        public void SetUp()
        {
            var board = new Board(10, 20);
            CollisionDetection = new CollisionDetection(board);
            Adjuster = new ControlBlocksAdjuster(CollisionDetection);
        }

        [Test]
        public void AdjustControlBlocksSuccessTest()
        {
            var blockList = new List<IBlock> { new Block(0, 0) };
            var blocks = new Blocks(blockList);
            var controlBlocks = new ControlBlocks(5, 10, blocks);

            Adjuster.AdjustBlocksForMoveRight(controlBlocks);
            Assert.AreEqual(5, controlBlocks.X);
            Assert.AreEqual(10, controlBlocks.Y);

            Adjuster.AdjustBlocksForMoveLeft(controlBlocks);
            Assert.AreEqual(5, controlBlocks.X);
            Assert.AreEqual(10, controlBlocks.Y);

            Adjuster.AdjustBlocksForMoveDown(controlBlocks);
            Assert.AreEqual(5, controlBlocks.X);
            Assert.AreEqual(10, controlBlocks.Y);
        }

        [Test]
        public void AdjustControlBlocksRightCollisionTest()
        {
            var blockList = new List<IBlock> { new Block(0, 0) };
            var blocks = new Blocks(blockList);
            var controlBlocks = new ControlBlocks(10, 19, blocks);

            Adjuster.AdjustBlocksForMoveRight(controlBlocks);
            Assert.AreEqual(9, controlBlocks.X);
            Assert.AreEqual(19, controlBlocks.Y);
        }

        [Test]
        public void AdjustControlBlocksLeftCollisionTest()
        {
            var blockList = new List<IBlock> { new Block(0, 0) };
            var blocks = new Blocks(blockList);
            var controlBlocks = new ControlBlocks(-2, 15, blocks);

            Adjuster.AdjustBlocksForMoveLeft(controlBlocks);
            Assert.AreEqual(0, controlBlocks.X);
            Assert.AreEqual(15, controlBlocks.Y);
        }

        [Test]
        public void AdjustControlBlocksDownCollisionTest()
        {
            var blockList = new List<IBlock> { new Block(0, 0) };
            var blocks = new Blocks(blockList);
            var controlBlocks = new ControlBlocks(4, -2, blocks);

            Adjuster.AdjustBlocksForMoveDown(controlBlocks);
            Assert.AreEqual(4, controlBlocks.X);
            Assert.AreEqual(0, controlBlocks.Y);
        }
    }
}
