using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UniRx;
namespace Tests
{

    public class BoardTest
    {
        private IBoard Board;
        private IBlocks LBlocks;
        private IBlocks IBlocks;


        [SetUp]
        public void SetUp()
        {
            Board = new Board(10, 20);

            IBlocks = new IShapedBlocks();

            LBlocks = new LShapedBlocks();
        }

        [Test]
        public void インスタンス生成時にブロック挿入位置が適切に設定されるかテスト()
        {
            Assert.AreEqual(4, Board.GetInsertPositionX());
            Assert.AreEqual(21, Board.GetInsertPositionY());
        }

        [Test]
        public void ブロックをボードに設置するテスト()
        {
            var controlBlocks = new ControlBlocks(0, 1, LBlocks);

            Assert.AreEqual(0, Board.Blocks.Count);

            Board.PutBlocks(controlBlocks);

            Assert.AreEqual(4, Board.Blocks.Count);

            Assert.AreEqual(1, Board.Blocks[0].Id);
            Assert.AreEqual(0, Board.Blocks[0].GetX());
            Assert.AreEqual(2, Board.Blocks[0].GetY());

            Assert.AreEqual(2, Board.Blocks[1].Id);
            Assert.AreEqual(0, Board.Blocks[1].GetX());
            Assert.AreEqual(1, Board.Blocks[1].GetY());

            Assert.AreEqual(3, Board.Blocks[2].Id);
            Assert.AreEqual(0, Board.Blocks[2].GetX());
            Assert.AreEqual(0, Board.Blocks[2].GetY());

            Assert.AreEqual(4, Board.Blocks[3].Id);
            Assert.AreEqual(1, Board.Blocks[3].GetX());
            Assert.AreEqual(0, Board.Blocks[3].GetY());
        }

        [Test]
        public void ライン揃い時の消去テスト()
        {
            var board = new Board(8, 20);
            // 1つ目のブロックを左下へ
            var controlBlocks = new ControlBlocks(2, 0, IBlocks);
            controlBlocks.LeftSpin();
            board.PutBlocks(controlBlocks);

            // 2つ目のブロックを左下二段目へ
            controlBlocks = new ControlBlocks(2, 1, IBlocks);
            controlBlocks.LeftSpin();
            board.PutBlocks(controlBlocks);

            var result1 = board.EraseIfAlign();
            Assert.AreEqual(0, result1.Count);

            // まだブロックは揃っていない
            Assert.IsTrue(board.ExistPosition(0, 1));
            Assert.IsTrue(board.ExistPosition(1, 1));
            Assert.IsTrue(board.ExistPosition(2, 1));
            Assert.IsTrue(board.ExistPosition(3, 1));
            Assert.IsTrue(board.ExistPosition(0, 0));
            Assert.IsTrue(board.ExistPosition(1, 0));
            Assert.IsTrue(board.ExistPosition(2, 0));
            Assert.IsTrue(board.ExistPosition(3, 0));
            Assert.IsFalse(board.ExistPosition(4, 0));
            Assert.IsFalse(board.ExistPosition(5, 0));
            Assert.IsFalse(board.ExistPosition(6, 0));
            Assert.IsFalse(board.ExistPosition(7, 0));

            // 4つ目のブロックを右下へ
            controlBlocks = new ControlBlocks(6, 0, IBlocks);
            controlBlocks.LeftSpin();
            board.PutBlocks(controlBlocks);
            var result2 = board.EraseIfAlign();


            // ブロックが揃ったことで一番下の段が消える
            Assert.IsTrue(board.ExistPosition(0, 1));
            Assert.IsTrue(board.ExistPosition(1, 1));
            Assert.IsTrue(board.ExistPosition(2, 1));
            Assert.IsTrue(board.ExistPosition(3, 1));
            Assert.IsFalse(board.ExistPosition(0, 0));
            Assert.IsFalse(board.ExistPosition(1, 0));
            Assert.IsFalse(board.ExistPosition(2, 0));
            Assert.IsFalse(board.ExistPosition(3, 0));
            Assert.IsFalse(board.ExistPosition(4, 0));
            Assert.IsFalse(board.ExistPosition(5, 0));
            Assert.IsFalse(board.ExistPosition(6, 0));
            Assert.IsFalse(board.ExistPosition(7, 0));

            // 戻り値には消えたブロックが入っている
            Assert.AreEqual(0, result2[0].GetX());
            Assert.AreEqual(0, result2[0].GetY());
            Assert.AreEqual(1, result2[1].GetX());
            Assert.AreEqual(0, result2[1].GetY());
            Assert.AreEqual(2, result2[2].GetX());
            Assert.AreEqual(0, result2[2].GetY());
            Assert.AreEqual(3, result2[3].GetX());
            Assert.AreEqual(0, result2[3].GetY());
            Assert.AreEqual(4, result2[4].GetX());
            Assert.AreEqual(0, result2[4].GetY());
            Assert.AreEqual(5, result2[5].GetX());
            Assert.AreEqual(0, result2[5].GetY());
            Assert.AreEqual(6, result2[6].GetX());
            Assert.AreEqual(0, result2[6].GetY());
            Assert.AreEqual(7, result2[7].GetX());
            Assert.AreEqual(0, result2[7].GetY());
        }

        [Test]
        public void 何もブロックの存在しないラインにブロックを落下させるテスト()
        {
            var board = new Board(8, 20);
            // 1つ目のブロック設置
            var controlBlocks = new ControlBlocks(3, 0, IBlocks);
            controlBlocks.LeftSpin();
            board.PutBlocks(controlBlocks);

            // 2つ目のブロック設置
            var controlBlocks2 = new ControlBlocks(2, 4, IBlocks);
            controlBlocks2.LeftSpin();
            board.PutBlocks(controlBlocks2);

            // 3つ目のブロック設置
            var sevenSideBlocks = new Blocks(new List<IBlock>
            {
                new Block(0, 0),
                new Block(1, 0),
                new Block(2, 0),
                new Block(3, 0),
                new Block(4, 0),
                new Block(5, 0),
                new Block(6, 0)
            });
            var controlBlocks3 = new ControlBlocks(0, 5, sevenSideBlocks);
            board.PutBlocks(controlBlocks3);

            // 4つ目のブロックを接地
            var controlBlocks4 = new ControlBlocks(4, 6, IBlocks);
            controlBlocks4.LeftSpin();
            board.PutBlocks(controlBlocks4);

            var result = board.FallToEmptyLine();

            // 移動したブロックが戻り値に入っている
            Assert.AreEqual(15, result.Count);

            // 空間にブロックが落下している
            Assert.IsFalse(board.ExistPosition(0, 0));
            Assert.IsTrue(board.ExistPosition(1, 0));
            Assert.IsTrue(board.ExistPosition(2, 0));
            Assert.IsTrue(board.ExistPosition(3, 0));
            Assert.IsTrue(board.ExistPosition(4, 0));
            Assert.IsFalse(board.ExistPosition(5, 0));
            Assert.IsFalse(board.ExistPosition(6, 0));
            Assert.IsFalse(board.ExistPosition(7, 0));

            Assert.IsTrue(board.ExistPosition(0, 1));
            Assert.IsTrue(board.ExistPosition(1, 1));
            Assert.IsTrue(board.ExistPosition(2, 1));
            Assert.IsTrue(board.ExistPosition(3, 1));
            Assert.IsFalse(board.ExistPosition(4, 1));
            Assert.IsFalse(board.ExistPosition(5, 1));
            Assert.IsFalse(board.ExistPosition(6, 1));
            Assert.IsFalse(board.ExistPosition(7, 1));

            Assert.IsTrue(board.ExistPosition(0, 2));
            Assert.IsTrue(board.ExistPosition(1, 2));
            Assert.IsTrue(board.ExistPosition(2, 2));
            Assert.IsTrue(board.ExistPosition(3, 2));
            Assert.IsTrue(board.ExistPosition(4, 2));
            Assert.IsTrue(board.ExistPosition(5, 2));
            Assert.IsTrue(board.ExistPosition(6, 2));
            Assert.IsFalse(board.ExistPosition(7, 2));

            Assert.IsFalse(board.ExistPosition(0, 3));
            Assert.IsFalse(board.ExistPosition(1, 3));
            Assert.IsTrue(board.ExistPosition(2, 3));
            Assert.IsTrue(board.ExistPosition(3, 3));
            Assert.IsTrue(board.ExistPosition(4, 3));
            Assert.IsTrue(board.ExistPosition(5, 3));
            Assert.IsFalse(board.ExistPosition(6, 3));
            Assert.IsFalse(board.ExistPosition(7, 3));


        }

        [Test]
        public void ボードの高さを超えた位置にブロックが設置されたらゲームオーバー判定()
        {
            var board = new Board(10, 10);
            var controlBlocks = new ControlBlocks(4, 1, IBlocks);
            board.PutBlocks(controlBlocks);
            Assert.IsFalse(board.IsGameOver());

            controlBlocks = new ControlBlocks(4, 5, IBlocks);
            board.PutBlocks(controlBlocks);
            Assert.IsFalse(board.IsGameOver());

            controlBlocks = new ControlBlocks(4, 9, IBlocks);
            board.PutBlocks(controlBlocks);
            Assert.IsTrue(board.IsGameOver());
        }
    }


}
