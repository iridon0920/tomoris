using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
namespace Tests
{
    public class BlockTest
    {
        [Test]
        public void MoveTest()
        {
            var block = new Block(0, 1);

            Assert.AreEqual(-1, block.MoveX(-1).X);
            Assert.AreEqual(2, block.MoveY(1).Y);
        }

        [Test]
        public void EqualTest()
        {
            var block = new Block(-1, 1);
            Assert.IsTrue(block.Equals(new Block(-1, 1)));
        }
    }
}
