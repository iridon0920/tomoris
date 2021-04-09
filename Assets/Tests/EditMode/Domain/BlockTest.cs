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
        public void EqualTest()
        {
            var block = new Block(-1, 1);
            Assert.IsTrue(block.Equals(new Block(-1, 1)));
        }

        [Test]
        public void MoveDownTest()
        {
            var block = new Block(-1, 2);
            var block2 = block.MoveDown();

            Assert.AreEqual(-1, block2.X);
            Assert.AreEqual(1, block2.Y);
        }

        [Test]
        public void RotateLeft90DegreeTest()
        {
            var block = new Block(1, 2);

            var block2 = block.RotateLeft90Degree();
            Assert.AreEqual(-2, block2.X);
            Assert.AreEqual(1, block2.Y);

            var block3 = block2.RotateLeft90Degree();
            Assert.AreEqual(-1, block3.X);
            Assert.AreEqual(-2, block3.Y);

            var block4 = block3.RotateLeft90Degree();
            Assert.AreEqual(2, block4.X);
            Assert.AreEqual(-1, block4.Y);

            var block5 = block4.RotateLeft90Degree();
            Assert.IsTrue(block5.Equals(block));
        }

        [Test]
        public void RotateRight90DegreeTest()
        {
            var block = new Block(1, 2);

            var block2 = block.RotateRight90Degree();
            Assert.AreEqual(2, block2.X);
            Assert.AreEqual(-1, block2.Y);

            var block3 = block2.RotateRight90Degree();
            Assert.AreEqual(-1, block3.X);
            Assert.AreEqual(-2, block3.Y);

            var block4 = block3.RotateRight90Degree();
            Assert.AreEqual(-2, block4.X);
            Assert.AreEqual(1, block4.Y);

            var block5 = block4.RotateRight90Degree();
            Assert.IsTrue(block5.Equals(block));
        }
    }
}
