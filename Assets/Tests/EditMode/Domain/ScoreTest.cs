using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
namespace Tests
{
    public class ScoreTest
    {
        [Test]
        public void 消去行数に応じたスコア計算結果テスト()
        {
            var score = new Score();
            score.AddPointFromErasedLines(1);
            Assert.AreEqual(100, score.TotalPoints);

            var score2 = new Score();
            score2.AddPointFromErasedLines(2);
            Assert.AreEqual(200, score2.TotalPoints);

            var score3 = new Score();
            score3.AddPointFromErasedLines(3);
            Assert.AreEqual(400, score3.TotalPoints);

            var score4 = new Score();
            score4.AddPointFromErasedLines(4);
            Assert.AreEqual(800, score4.TotalPoints);
        }

        [Test]
        public void 点数の加算テスト()
        {
            var score = new Score();
            score.AddPointFromErasedLines(1);
            Assert.AreEqual(100, score.TotalPoints);
            score.AddPointFromErasedLines(2);
            Assert.AreEqual(300, score.TotalPoints);
            score.AddPointFromErasedLines(3);
            Assert.AreEqual(700, score.TotalPoints);
            score.AddPointFromErasedLines(4);
            Assert.AreEqual(1500, score.TotalPoints);
        }
    }
}
