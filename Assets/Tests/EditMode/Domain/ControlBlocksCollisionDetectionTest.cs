using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Zenject;

namespace Tests
{
    [TestFixture]
    public class CollisionDetectionTest : ZenjectUnitTestFixture
    {
        private IBoard Board;
        private ICollisionDetection CollisionDetection;
        private IBlocks IBlocks;
        private IBlocks LBlocks;

        [SetUp]
        public void SetUp()
        {
            Board = new Board(10, 20);
            CollisionDetection = new CollisionDetection(Board);

            IBlocks = new Blocks(new List<IBlock>
            {
                new Block(0, 2),
                new Block(0, 1),
                new Block(0, 0),
                new Block(0, -1)
            });

            LBlocks = new Blocks(new List<IBlock>
            {
                new Block(0, 1),
                new Block(0, 0),
                new Block(0, -1),
                new Block(1, -1)
            });

            Board.PutBlocks(new ControlBlocks(0, 1, LBlocks));
            Board.PutBlocks(new ControlBlocks(8, 1, LBlocks));
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
            var controlBlocks = new ControlBlocks(7, 0, LBlocks);
            Assert.IsTrue(CollisionDetection.IsCollision(controlBlocks));

            controlBlocks = new ControlBlocks(2, 0, LBlocks);
            Assert.IsTrue(CollisionDetection.IsCollision(controlBlocks));

            controlBlocks = new ControlBlocks(1, 1, LBlocks);
            Assert.IsTrue(CollisionDetection.IsCollision(controlBlocks));
        }
    }
}
