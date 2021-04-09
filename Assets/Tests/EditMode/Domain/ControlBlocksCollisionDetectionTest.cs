using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Moq;
using Zenject;

namespace Tests
{
    [TestFixture]
    public class ControlBlocksCollisionDetectionTest : ZenjectUnitTestFixture
    {
        private Mock<IBoard> MockBoard;
        private Mock<IBlocks> MockLBlocks;
        private Mock<IBlocks> MockIBlocks;

        [SetUp]
        public void SetUp()
        {
            MockBoard = new Mock<IBoard>();
            MockBoard.Setup(m => m.Width).Returns(10);
            MockBoard.Setup(m => m.Height).Returns(20);


            var statusByPositions = new bool[10, 20];
            for (var x = 0; x < 10; x++)
            {
                for (var y = 0; y < 20; y++)
                {
                    statusByPositions[x, y] = false;
                }
            }
            statusByPositions[0, 0] = true;
            statusByPositions[1, 0] = true;
            statusByPositions[8, 0] = true;
            statusByPositions[9, 0] = true;
            MockBoard.Setup(m => m.StatusByPositions).Returns(statusByPositions);

            MockLBlocks = new Mock<IBlocks>();
            MockLBlocks.Setup(m => m.BlockList).Returns(
                CreateLBlockList()
            );

            MockIBlocks = new Mock<IBlocks>();
            MockIBlocks.Setup(m => m.BlockList).Returns(
                CreateIBlockList()
            );
        }


        [Test]
        public void IsCollisionRightWallTest()
        {
            var collisionDetection = new ControlBlocksCollisionDetection(MockBoard.Object);

            var controlBlocks = new ControlBlocks(10, 19, MockLBlocks.Object);
            Assert.IsTrue(collisionDetection.IsCollision(controlBlocks));
            controlBlocks = new ControlBlocks(9, 19, MockLBlocks.Object);
            Assert.IsTrue(collisionDetection.IsCollision(controlBlocks));
            controlBlocks = new ControlBlocks(8, 19, MockLBlocks.Object);
            Assert.IsFalse(collisionDetection.IsCollision(controlBlocks));
        }


        [Test]
        public void IsCollisionLeftWallTest()
        {
            var collisionDetection = new ControlBlocksCollisionDetection(MockBoard.Object);

            var controlBlocks = new ControlBlocks(-2, 19, MockLBlocks.Object);
            Assert.IsTrue(collisionDetection.IsCollision(controlBlocks));
            controlBlocks = new ControlBlocks(0, 19, MockLBlocks.Object);
            Assert.IsFalse(collisionDetection.IsCollision(controlBlocks));
        }

        [Test]
        public void IsCollisionGroundTest()
        {
            var collisionDetection = new ControlBlocksCollisionDetection(MockBoard.Object);

            var controlBlocks = new ControlBlocks(4, -2, MockIBlocks.Object);
            Assert.IsTrue(collisionDetection.IsCollision(controlBlocks));
            controlBlocks = new ControlBlocks(4, 0, MockIBlocks.Object);
            Assert.IsTrue(collisionDetection.IsCollision(controlBlocks));
            controlBlocks = new ControlBlocks(4, 1, MockIBlocks.Object);
            Assert.IsFalse(collisionDetection.IsCollision(controlBlocks));
        }

        [Test]
        public void IsCollisionPutBlocksTest()
        {
            var collisionDetection = new ControlBlocksCollisionDetection(MockBoard.Object);

            var controlBlocks = new ControlBlocks(7, 0, MockLBlocks.Object);
            Assert.IsTrue(collisionDetection.IsCollision(controlBlocks));

            controlBlocks = new ControlBlocks(2, 0, MockLBlocks.Object);
            Assert.IsTrue(collisionDetection.IsCollision(controlBlocks));

            controlBlocks = new ControlBlocks(1, 1, MockIBlocks.Object);
            Assert.IsTrue(collisionDetection.IsCollision(controlBlocks));
        }

        // I字ブロックのリスト作成
        private List<IBlock> CreateIBlockList()
        {
            return new List<IBlock>
            {
                new Block(0, 2),
                new Block(0, 1),
                new Block(0, 0),
                new Block(0, -1)
            };
        }

        // L字ブロックのリスト作成
        private List<IBlock> CreateLBlockList()
        {
            return new List<IBlock>
            {
            new Block(0, 1),
            new Block(0, 0),
            new Block(0, -1),
            new Block(1, -1)
            };
        }
    }
}
