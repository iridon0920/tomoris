using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Moq;
namespace Tests
{

    public class BlockHandlingTest
    {
        private Mock<IBoard> MockBoard;
        private Mock<IBlocksQueue> MockBlockQueue;
        private Mock<IBlocks> MockLBlocks;
        private Mock<IBlocks> MockIBlocks;

        [SetUp]
        public void SetUp()
        {
            MockBoard = new Mock<IBoard>();
            MockBoard.Setup(m => m.Width).Returns(10);
            MockBoard.Setup(m => m.Height).Returns(20);
            MockBoard.Setup(m => m.InsertPositionX).Returns(4);

            MockLBlocks = new Mock<IBlocks>();
            MockLBlocks.Setup(m => m.BlockList).Returns(
                CreateLBlockList()
            );

            MockIBlocks = new Mock<IBlocks>();
            MockIBlocks.Setup(m => m.BlockList).Returns(
                CreateIBlockList()
            );

            MockBlockQueue = new Mock<IBlocksQueue>();
            MockBlockQueue.Setup(m => m.Dequeue()).Returns(MockIBlocks.Object);
        }

        [Test]
        public void AdjustControlBlocksSuccessTest()
        {
            var blockHandling = new BlockHandling(
                MockBoard.Object,
                MockBlockQueue.Object
            );

            var controlBlocks = new ControlBlocks(1, 5, 10, MockLBlocks.Object);

            blockHandling.AdjustControlBlocksPosition(controlBlocks);
            Assert.AreEqual(5, controlBlocks.X);
            Assert.AreEqual(10, controlBlocks.Y);
        }

        [Test]
        public void AdjustControlBlocksRightCollisionTest()
        {
            var blockHandling = new BlockHandling(MockBoard.Object, MockBlockQueue.Object);

            var controlBlocks = new ControlBlocks(1, 11, 19, MockLBlocks.Object);

            blockHandling.AdjustControlBlocksPosition(controlBlocks);
            Assert.AreEqual(8, controlBlocks.X);
            Assert.AreEqual(19, controlBlocks.Y);
        }

        [Test]
        public void AdjustControlBlocksLeftCollisionTest()
        {
            var blockHandling = new BlockHandling(MockBoard.Object, MockBlockQueue.Object);

            var controlBlocks = new ControlBlocks(1, -2, 15, MockLBlocks.Object);

            blockHandling.AdjustControlBlocksPosition(controlBlocks);
            Assert.AreEqual(0, controlBlocks.X);
            Assert.AreEqual(15, controlBlocks.Y);
        }

        [Test]
        public void AdjustControlBlocksDownCollisionTest()
        {
            var blockHandling = new BlockHandling(MockBoard.Object, MockBlockQueue.Object);

            var controlBlocks = new ControlBlocks(1, 4, -2, MockLBlocks.Object);
            var PutControlBlocks = new ControlBlocks(1, 4, 1, MockLBlocks.Object);

            blockHandling.AdjustControlBlocksPosition(controlBlocks);
            Assert.AreEqual(MockBoard.Object.InsertPositionX, controlBlocks.X);
            Assert.AreEqual(MockBoard.Object.Height - 1, controlBlocks.Y);
            Assert.AreEqual(MockIBlocks.Object, controlBlocks.Blocks);

            MockBoard.Verify(m => m.PutBlocks(controlBlocks), Times.Once());
        }

        // I字ブロックのリスト作成
        private List<Block> CreateIBlockList()
        {
            return new List<Block>
            {
                new Block(0, 2),
                new Block(0, 1),
                new Block(0, 0),
                new Block(0, -1)
            };
        }

        // L字ブロックのリスト作成
        private List<Block> CreateLBlockList()
        {
            return new List<Block>
            {
            new Block(0, 1),
            new Block(0, 0),
            new Block(0, -1),
            new Block(1, -1)
            };
        }

    }
}
