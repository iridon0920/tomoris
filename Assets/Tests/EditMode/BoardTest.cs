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

        // 移動先でブロックがゲーム盤の外側へ衝突する場合、移動不可としてfalseを返すテスト
        // 右側移動の場合
        [Test]
        public void ControlBlocksRightMoveCollisionOutSideTest()
        {
            var board = new Board(5, 20);
            var IBlocks = new ControlBlocks(CreateIBlockList());
            board.InsertBlocks(IBlocks);

            // 衝突しない場合True, 衝突する場合false
            Assert.IsTrue(board.MoveBlocksRight());
            Assert.IsTrue(board.MoveBlocksRight());
            Assert.IsFalse(board.MoveBlocksRight());

            Assert.AreEqual(4, board.CurrentControlBlocksPositionX);
            Assert.AreEqual(19, board.CurrentControlBlocksPositionY);
        }


        // 移動先でブロックがゲーム盤の外側へ衝突する場合、移動不可としてfalseを返すテスト
        // 左側移動の場合
        [Test]
        public void ControlBlocksLeftMoveCollisionOutSideTest()
        {
            var board = new Board(5, 20);
            var IBlocks = new ControlBlocks(CreateIBlockList());
            board.InsertBlocks(IBlocks);

            // 衝突しない場合True, 衝突する場合false
            Assert.IsTrue(board.MoveBlocksLeft());
            Assert.IsTrue(board.MoveBlocksLeft());
            Assert.IsFalse(board.MoveBlocksLeft());

            Assert.AreEqual(0, board.CurrentControlBlocksPositionX);
            Assert.AreEqual(19, board.CurrentControlBlocksPositionY);
        }

        // 移動先でブロックがゲーム盤の外側へ衝突する場合、移動不可としてfalseを返すテスト
        // 回転した上で左側移動の場合
        [Test]
        public void ControlBlocksSpinAndMoveCollisionOutSideTest()
        {
            var board = new Board(5, 20);
            var IBlocks = new ControlBlocks(CreateIBlockList());
            board.InsertBlocks(IBlocks);

            // I字ブロックが横向きの状態で、左移動は衝突し、右移動は衝突しない
            Assert.IsTrue(board.SpinBlocks());
            Assert.IsFalse(board.MoveBlocksLeft());
            Assert.IsTrue(board.MoveBlocksRight());

            Assert.AreEqual(3, board.CurrentControlBlocksPositionX);
            Assert.AreEqual(19, board.CurrentControlBlocksPositionY);
        }

        [Test]
        public void ControlBlockSpinCollisionOutSideTest()
        {
            var board = new Board(5, 20);
            var IBlocks = new ControlBlocks(CreateIBlockList());
            board.InsertBlocks(IBlocks);

            Assert.IsTrue(board.MoveBlocksLeft());
            // 移動した先で回転したら衝突する
            Assert.IsFalse(board.SpinBlocks());
        }

        [Test]
        public void ControlBlocksPutTest()
        {
            var board = new Board(10, 10);
            var IBlocks = new ControlBlocks(CreateIBlockList());
            board.InsertBlocks(IBlocks);

            // まだブロックは置かれていない。
            Assert.IsFalse(board.StatusByPositions[4, 3]);
            Assert.IsFalse(board.StatusByPositions[4, 2]);
            Assert.IsFalse(board.StatusByPositions[4, 1]);
            Assert.IsFalse(board.StatusByPositions[4, 0]);

            // ブロックが一度右移動後一番下に着地するように置かれる。
            Assert.IsTrue(board.MoveBlocksRight());
            Assert.IsTrue(board.MoveBlocksDown());
            Assert.IsTrue(board.MoveBlocksDown());
            Assert.IsTrue(board.MoveBlocksDown());
            Assert.IsTrue(board.MoveBlocksDown());
            Assert.IsTrue(board.MoveBlocksDown());
            Assert.IsTrue(board.MoveBlocksDown());
            Assert.IsTrue(board.MoveBlocksDown());
            Assert.IsTrue(board.MoveBlocksDown());
            // 接地するとFalseが返る。
            Assert.IsFalse(board.MoveBlocksDown());

            // I字ブロックがゲーム盤に適用されている。
            Assert.IsTrue(board.StatusByPositions[5, 3]);
            Assert.IsTrue(board.StatusByPositions[5, 2]);
            Assert.IsTrue(board.StatusByPositions[5, 1]);
            Assert.IsTrue(board.StatusByPositions[5, 0]);

            // CurrentBlocks関係のプロパティが初期化されている
            Assert.IsNull(board.CurrentControlBlocks);
            Assert.AreEqual(4, board.CurrentControlBlocksPositionX);
            Assert.AreEqual(9, board.CurrentControlBlocksPositionY);

            // 2つ目のブロック投入
            IBlocks = new ControlBlocks(CreateIBlockList());
            board.InsertBlocks(IBlocks);

            // 1つ目に置いたブロックの上に置くようにブロックを動かす
            Assert.IsTrue(board.MoveBlocksRight());
            Assert.IsTrue(board.MoveBlocksDown());
            Assert.IsTrue(board.MoveBlocksDown());
            Assert.IsTrue(board.MoveBlocksDown());
            Assert.IsTrue(board.MoveBlocksDown());
            // 既に存在するブロックの上に置こうとするとFalseが返る。
            Assert.IsFalse(board.MoveBlocksDown());

            // 1個目の上にI字ブロックが新たにゲーム盤に適用されている。
            Assert.IsTrue(board.StatusByPositions[5, 7]);
            Assert.IsTrue(board.StatusByPositions[5, 6]);
            Assert.IsTrue(board.StatusByPositions[5, 5]);
            Assert.IsTrue(board.StatusByPositions[5, 4]);

            // CurrentBlocks関係のプロパティが初期化されている
            Assert.IsNull(board.CurrentControlBlocks);
            Assert.AreEqual(4, board.CurrentControlBlocksPositionX);
            Assert.AreEqual(9, board.CurrentControlBlocksPositionY);
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
