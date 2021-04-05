using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Moq;
namespace Tests
{

    public class ControlBlocksAdjusterTest
    {
        private Mock<IControlBlocksCollisionDetection> MockCollisionDetection;
        private Mock<IBlocks> MockBlocks;

        [SetUp]
        public void SetUp()
        {
            MockCollisionDetection = new Mock<IControlBlocksCollisionDetection>();
            MockBlocks = new Mock<IBlocks>();
        }

        [Test]
        public void AdjustControlBlocksSuccessTest()
        {
            var blockHandling = new ControlBlocksAdjuster(
                MockCollisionDetection.Object
            );

            var controlBlocks = new ControlBlocks(1, 5, 10, MockBlocks.Object);

            blockHandling.AdjustBlocksForMoveRight(controlBlocks);
            Assert.AreEqual(5, controlBlocks.X);
            Assert.AreEqual(10, controlBlocks.Y);
            blockHandling.AdjustBlocksForMoveLeft(controlBlocks);
            Assert.AreEqual(5, controlBlocks.X);
            Assert.AreEqual(10, controlBlocks.Y);
            blockHandling.AdjustBlocksForMoveDown(controlBlocks);
            Assert.AreEqual(5, controlBlocks.X);
            Assert.AreEqual(10, controlBlocks.Y);
        }

        [Test]
        public void AdjustControlBlocksRightCollisionTest()
        {
            var controlBlocks = new ControlBlocks(1, 10, 19, MockBlocks.Object);
            MockCollisionDetection
                .SetupSequence(m => m.IsCollision(controlBlocks))
                .Returns(true)
                .Returns(true)
                .Returns(false);
            var blockHandling = new ControlBlocksAdjuster(MockCollisionDetection.Object);

            blockHandling.AdjustBlocksForMoveRight(controlBlocks);
            Assert.AreEqual(8, controlBlocks.X);
            Assert.AreEqual(19, controlBlocks.Y);
        }

        [Test]
        public void AdjustControlBlocksLeftCollisionTest()
        {
            var controlBlocks = new ControlBlocks(1, -2, 15, MockBlocks.Object);
            MockCollisionDetection
                .SetupSequence(m => m.IsCollision(controlBlocks))
                .Returns(true)
                .Returns(true)
                .Returns(false);
            var blockHandling = new ControlBlocksAdjuster(MockCollisionDetection.Object);

            blockHandling.AdjustBlocksForMoveLeft(controlBlocks);
            Assert.AreEqual(0, controlBlocks.X);
            Assert.AreEqual(15, controlBlocks.Y);
        }

        [Test]
        public void AdjustControlBlocksDownCollisionTest()
        {
            var controlBlocks = new ControlBlocks(1, 4, -2, MockBlocks.Object);
            MockCollisionDetection
                .SetupSequence(m => m.IsCollision(controlBlocks))
                .Returns(true)
                .Returns(false);
            var blockHandling = new ControlBlocksAdjuster(MockCollisionDetection.Object);

            blockHandling.AdjustBlocksForMoveDown(controlBlocks);
            Assert.AreEqual(4, controlBlocks.X);
            Assert.AreEqual(-1, controlBlocks.Y);
        }
    }
}
