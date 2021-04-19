using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Zenject;
using Moq;

namespace Tests
{
    [TestFixture]
    public class CollisionDetectionTest : ZenjectUnitTestFixture
    {
        private CollisionDetection CollisionDetection;
        private IBlocks IBlocks;
        private IBlocks LBlocks;

        [SetUp]
        public void SetUp()
        {
            var mockBoard = new Mock<IBoard>();
            mockBoard.Setup(m => m.Width).Returns(10);
            mockBoard.Setup(m => m.Height).Returns(20);

            mockBoard.Setup(m => m.ExistPosition(8, 0)).Returns(true);
            mockBoard.Setup(m => m.ExistPosition(2, 0)).Returns(true);

            CollisionDetection = new CollisionDetection(mockBoard.Object);

            IBlocks = new IShapedBlocks();

            LBlocks = new LShapedBlocks();

        }


        [Test]
        public void IsCollisionRightWallTest()
        {
            var controlBlocks = new ControlBlocks(10, 19, LBlocks);
            Assert.IsTrue(CollisionDetection.IsCollision(controlBlocks));

            controlBlocks = new ControlBlocks(9, 19, LBlocks);
            Assert.IsTrue(CollisionDetection.IsCollision(controlBlocks));

            controlBlocks = new ControlBlocks(8, 19, LBlocks);
            Assert.IsFalse(CollisionDetection.IsCollision(controlBlocks));
        }

        [Test]
        public void IsCollisionLeftWallTest()
        {
            var controlBlocks = new ControlBlocks(-2, 19, LBlocks);
            Assert.IsTrue(CollisionDetection.IsCollision(controlBlocks));

            controlBlocks = new ControlBlocks(0, 19, LBlocks);
            Assert.IsFalse(CollisionDetection.IsCollision(controlBlocks));
        }

        [Test]
        public void IsCollisionGroundTest()
        {
            var controlBlocks = new ControlBlocks(4, -2, IBlocks);
            Assert.IsTrue(CollisionDetection.IsCollision(controlBlocks));

            controlBlocks = new ControlBlocks(4, 0, IBlocks);
            Assert.IsTrue(CollisionDetection.IsCollision(controlBlocks));

            controlBlocks = new ControlBlocks(4, 1, IBlocks);
            Assert.IsFalse(CollisionDetection.IsCollision(controlBlocks));
        }

        [Test]
        public void IsCollisionPutBlocksTest()
        {
            var controlBlocks = new ControlBlocks(7, 1, LBlocks);
            Assert.IsTrue(CollisionDetection.IsCollision(controlBlocks));

            controlBlocks = new ControlBlocks(2, 1, LBlocks);
            Assert.IsTrue(CollisionDetection.IsCollision(controlBlocks));

            controlBlocks = new ControlBlocks(1, 1, LBlocks);
            Assert.IsTrue(CollisionDetection.IsCollision(controlBlocks));
        }
    }
}
