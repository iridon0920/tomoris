using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
namespace Tests
{

    public class BoardTest
    {

        [Test]
        public void InsertBlocksTest()
        {
            var board = new Board(10, 20);
            board.InsertBlocks(new ControlBlocks(CreateIBlockList()));

            Assert.AreEqual(4, board.CurrentControlBlocksPositionX);
            Assert.AreEqual(19, board.CurrentControlBlocksPositionY);
        }

        [Test]
        public void ControlBlocksMoveTest()
        {
            var board = new Board(10, 20);
            var IBlocks = new ControlBlocks(CreateIBlockList());
            board.InsertBlocks(IBlocks);
            Assert.IsTrue(board.MoveBlocks(5, 20));

            Assert.AreEqual(5, board.CurrentControlBlocksPositionX);
            Assert.AreEqual(20, board.CurrentControlBlocksPositionY);
        }

        // 移動先でブロックがゲーム盤の外側へ衝突する場合、移動不可としてfalseを返すテスト
        [Test]
        public void ControlBlocksMoveCollisionOutSideTest()
        {
            var board = new Board(10, 20);
            var IBlocks = new ControlBlocks(CreateIBlockList());
            board.InsertBlocks(IBlocks);

            // 衝突しない場合True, 衝突する場合false
            Assert.IsTrue(board.MoveBlocks(9, 20));
            Assert.IsFalse(board.MoveBlocks(10, 20));
            Assert.IsTrue(board.MoveBlocks(0, 20));
            Assert.IsFalse(board.MoveBlocks(-1, 20));

            // I字ブロックを横回転させた場合、異なる結果となる
            Assert.IsTrue(board.MoveBlocks(4, 20));
            board.SpinBlocks();
            Assert.IsTrue(board.MoveBlocks(8, 20));
            Assert.IsFalse(board.MoveBlocks(9, 20));
            Assert.IsTrue(board.MoveBlocks(2, 20));
            Assert.IsFalse(board.MoveBlocks(1, 20));
            Assert.IsFalse(board.MoveBlocks(0, 20));
        }

        [Test]
        public void ControlBlockSpinCollisionOutSideTest()
        {
            var board = new Board(10, 20);
            var IBlocks = new ControlBlocks(CreateIBlockList());
            board.InsertBlocks(IBlocks);
            board.MoveBlocks(9, 20);
            Assert.IsFalse(board.SpinBlocks());
        }

        [Test]
        public void ControlBlocksPutTest()
        {
            var board = new Board(10, 20);
            var IBlocks = new ControlBlocks(CreateIBlockList());
            board.InsertBlocks(IBlocks);

            // まだブロックは置かれていない。
            Assert.IsFalse(board.StatusByPositions[4, 0]);
            Assert.IsFalse(board.StatusByPositions[4, 1]);
            Assert.IsFalse(board.StatusByPositions[4, 2]);
            Assert.IsFalse(board.StatusByPositions[4, 3]);

            // ブロックが一番下に着地するように置かれる。
            board.MoveBlocks(4, 1);

            // I字ブロックがゲーム盤に適用されている。
            Assert.IsTrue(board.StatusByPositions[4, 0]);
            Assert.IsTrue(board.StatusByPositions[4, 1]);
            Assert.IsTrue(board.StatusByPositions[4, 2]);
            Assert.IsTrue(board.StatusByPositions[4, 3]);

            board.InsertBlocks(IBlocks);
        }


        // I字ブロックのリスト作成
        private List<ControlBlock> CreateIBlockList()
        {
            return new List<ControlBlock>
            {
                new ControlBlock(0, 2),
                new ControlBlock(0, 1),
                new ControlBlock(0, 0),
                new ControlBlock(0, -1)
            };
        }

    }
}
