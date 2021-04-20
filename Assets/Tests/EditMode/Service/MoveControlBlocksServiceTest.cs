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
            mockBoard.Setup(m => m.ExistPosition(7, 0)).Returns(true);
            mockBoard.Setup(m => m.ExistPosition(0, 0)).Returns(true);

            var collisionDetection = new CollisionDetection(mockBoard.Object);
            var adjuster = new ControlBlocksAdjuster(collisionDetection);
            Service = new MoveControlBlocksService(adjuster);

            IBlocks = new IShapedBlocks();
        }

        [Test]
        public void 右移動成功テスト()
        {
            var controlBlocks = new ControlBlocks(5, 10, IBlocks);
            controlBlocks = Service.Execute(controlBlocks, 1, 0, false, false);
            Assert.AreEqual(6, controlBlocks.X);
            Assert.AreEqual(10, controlBlocks.Y);
        }

        [Test]
        public void 右移動で壁に衝突するテスト()
        {
            var controlBlocks = new ControlBlocks(9, 10, IBlocks);
            controlBlocks = Service.Execute(controlBlocks, 1, 0, false, false);
            Assert.AreEqual(9, controlBlocks.X);
            Assert.AreEqual(10, controlBlocks.Y);
        }

        // ボードの9, 0座標にはブロックが存在するため、衝突判定が発生
        [Test]
        public void 右移動でブロックに衝突するテスト()
        {
            var controlBlocks = new ControlBlocks(8, 1, IBlocks);
            controlBlocks = Service.Execute(controlBlocks, 1, 0, false, false);
            Assert.AreEqual(8, controlBlocks.X);
            Assert.AreEqual(1, controlBlocks.Y);
        }

        [Test]
        public void 左移動成功テスト()
        {
            var controlBlocks = new ControlBlocks(5, 10, IBlocks);
            controlBlocks = Service.Execute(controlBlocks, -1, 0, false, false);
            Assert.AreEqual(4, controlBlocks.X);
            Assert.AreEqual(10, controlBlocks.Y);
        }

        [Test]
        public void 左移動でブロックに衝突するテスト()
        {
            var controlBlocks = new ControlBlocks(0, 10, IBlocks);
            controlBlocks = Service.Execute(controlBlocks, -1, 0, false, false);
            Assert.AreEqual(0, controlBlocks.X);
            Assert.AreEqual(10, controlBlocks.Y);
        }

        [Test]
        public void 下移動成功テスト()
        {
            var controlBlocks = new ControlBlocks(5, 10, IBlocks);
            controlBlocks = Service.Execute(controlBlocks, 0, -1, false, false);
            Assert.AreEqual(5, controlBlocks.X);
            Assert.AreEqual(9, controlBlocks.Y);
            Assert.IsFalse(controlBlocks.IsPutable);
        }

        // 下方向の衝突があったら、PuttableフラグがTrueになる
        [Test]
        public void 下移動でボードの一番下に衝突するテスト()
        {
            var controlBlocks = new ControlBlocks(0, 1, IBlocks);
            controlBlocks = Service.Execute(controlBlocks, 0, -1, false, false);
            Assert.AreEqual(0, controlBlocks.X);
            Assert.AreEqual(1, controlBlocks.Y);
            Assert.IsTrue(controlBlocks.IsPutable);
        }

        [Test]
        public void 左回転で壁に衝突するテスト()
        {
            var controlBlocks = new ControlBlocks(0, 10, IBlocks);
            controlBlocks = Service.Execute(controlBlocks, 0, 0, true, false);
            Assert.AreEqual(2, controlBlocks.X);
            Assert.AreEqual(10, controlBlocks.Y);

            var controlBlocks2 = new ControlBlocks(1, 10, IBlocks);
            controlBlocks2 = Service.Execute(controlBlocks2, 0, 0, true, false);
            Assert.AreEqual(2, controlBlocks2.X);
            Assert.AreEqual(10, controlBlocks2.Y);
        }

        [Test]
        public void 右回転で壁に衝突するテスト()
        {
            var controlBlocks = new ControlBlocks(9, 10, IBlocks);
            controlBlocks = Service.Execute(controlBlocks, 0, 0, false, true);
            Assert.AreEqual(7, controlBlocks.X);
            Assert.AreEqual(10, controlBlocks.Y);

            var controlBlocks2 = new ControlBlocks(8, 10, IBlocks);

            controlBlocks2 = Service.Execute(controlBlocks2, 0, 0, false, true);
            Assert.AreEqual(7, controlBlocks2.X);
            Assert.AreEqual(10, controlBlocks2.Y);
        }

        [Test]
        public void 右回転で設置ブロックに衝突するテスト()
        {
            var controlBlocks = new ControlBlocks(6, 0, IBlocks);
            controlBlocks = Service.Execute(controlBlocks, 0, 0, false, true);
            Assert.AreEqual(4, controlBlocks.X);
            Assert.AreEqual(0, controlBlocks.Y);
        }

        [Test]
        public void 右回転でボードの一番下に衝突するテスト()
        {
            var controlBlocks = new ControlBlocks(5, 0, IBlocks);
            controlBlocks.RightSpin();
            controlBlocks = Service.Execute(controlBlocks, 0, 0, false, true);
            Assert.AreEqual(5, controlBlocks.X);
            Assert.AreEqual(2, controlBlocks.Y);
        }


        [Test]
        public void 回転時に両方向に衝突する際のテスト()
        {
            // 回転後の移動調整をしても衝突する場合、回転前のブロックが返る
            var controlBlocks = new ControlBlocks(8, 0, IBlocks);
            controlBlocks = Service.Execute(controlBlocks, 0, 0, false, true);
            Assert.AreEqual(8, controlBlocks.X);
            Assert.AreEqual(0, controlBlocks.Y);
        }

        [Test]
        public void 右下入力時の動きのテスト()
        {
            var controlBlocks = new ControlBlocks(5, 10, IBlocks);
            controlBlocks = Service.Execute(controlBlocks, 1, -1, false, false);
            Assert.AreEqual(5, controlBlocks.X);
            Assert.AreEqual(9, controlBlocks.Y);
        }

        [Test]
        public void 左下入力時の動きのテスト()
        {
            var controlBlocks = new ControlBlocks(5, 10, IBlocks);
            controlBlocks = Service.Execute(controlBlocks, 1, -1, false, false);
            Assert.AreEqual(5, controlBlocks.X);
            Assert.AreEqual(9, controlBlocks.Y);
        }

    }

}
