using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Moq;
namespace Tests
{
    public class MoveControlBlocksServiceTest
    {
        private MoveControlBlocksService Service;
        private IBlocks IBlocks;
        [SetUp]
        public void SetUp()
        {
            var mockBoard = new Mock<IBoard>();
            mockBoard.Setup(m => m.Width).Returns(10);

            mockBoard.Setup(m => m.ExistPosition(9, 0)).Returns(true);

            var collisionDetection = new CollisionDetection(mockBoard.Object);
            var adjuster = new ControlBlocksAdjuster(collisionDetection);
            Service = new MoveControlBlocksService(adjuster);

            IBlocks = new Blocks(new List<IBlock>
            {
                new Block(0, 2),
                new Block(0, 1),
                new Block(0, 0),
                new Block(0, -1)
            });
        }

        [Test]
        public void MoveRightSuccessTest()
        {
            var controlBlocks = new ControlBlocks(5, 10, IBlocks);
            controlBlocks = Service.Execute(controlBlocks, 1, 0);
            Assert.AreEqual(6, controlBlocks.X);
            Assert.AreEqual(10, controlBlocks.Y);
        }

        [Test]
        public void MoveRightIfWallCollision()
        {
            var controlBlocks = new ControlBlocks(9, 10, IBlocks);
            controlBlocks = Service.Execute(controlBlocks, 1, 0);
            Assert.AreEqual(9, controlBlocks.X);
            Assert.AreEqual(10, controlBlocks.Y);
        }

        // ボードの9, 0座標にはブロックが存在するため、衝突判定が発生
        [Test]
        public void MoveRightIfPutBlocksCollision()
        {
            var controlBlocks = new ControlBlocks(8, 1, IBlocks);
            controlBlocks = Service.Execute(controlBlocks, 1, 0);
            Assert.AreEqual(8, controlBlocks.X);
            Assert.AreEqual(1, controlBlocks.Y);
        }

        [Test]
        public void MoveLeftSuccessTest()
        {
            var controlBlocks = new ControlBlocks(5, 10, IBlocks);
            controlBlocks = Service.Execute(controlBlocks, -1, 0);
            Assert.AreEqual(4, controlBlocks.X);
            Assert.AreEqual(10, controlBlocks.Y);
        }

        [Test]
        public void MoveLeftIfWallCollision()
        {
            var controlBlocks = new ControlBlocks(0, 10, IBlocks);
            controlBlocks = Service.Execute(controlBlocks, -1, 0);
            Assert.AreEqual(0, controlBlocks.X);
            Assert.AreEqual(10, controlBlocks.Y);
        }

        [Test]
        public void MoveDownSuccessTest()
        {
            var controlBlocks = new ControlBlocks(5, 10, IBlocks);
            controlBlocks = Service.Execute(controlBlocks, 0, -1);
            Assert.AreEqual(5, controlBlocks.X);
            Assert.AreEqual(9, controlBlocks.Y);
            Assert.IsFalse(controlBlocks.IsPutable);
        }

        // 下方向の衝突があったら、PuttableフラグがTrueになる
        [Test]
        public void MoveDownIfGroundCollision()
        {
            var controlBlocks = new ControlBlocks(0, 1, IBlocks);
            controlBlocks = Service.Execute(controlBlocks, 0, -1);
            Assert.AreEqual(0, controlBlocks.X);
            Assert.AreEqual(1, controlBlocks.Y);
            Assert.IsTrue(controlBlocks.IsPutable);
        }

        // 右下入力時の動き
        [Test]
        public void MoveRightDownSuccessTest()
        {
            var controlBlocks = new ControlBlocks(5, 10, IBlocks);
            controlBlocks = Service.Execute(controlBlocks, 1, -1);
            Assert.AreEqual(5, controlBlocks.X);
            Assert.AreEqual(9, controlBlocks.Y);
        }

        // 左下入力時の動き
        [Test]
        public void MoveLeftDownSuccessTest()
        {

            var controlBlocks = new ControlBlocks(5, 10, IBlocks);
            controlBlocks = Service.Execute(controlBlocks, 1, -1);
            Assert.AreEqual(5, controlBlocks.X);
            Assert.AreEqual(9, controlBlocks.Y);
        }
    }

}
