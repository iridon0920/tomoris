using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
namespace Tests
{
    public class ControlBlockTest
    {
        [Test]
        public void MoveTest()
        {
            var controlBlock = new ControlBlock(0, 1);

            Assert.AreEqual(-1, controlBlock.MoveX(-1).X);
            Assert.AreEqual(2, controlBlock.MoveY(1).Y);
        }

        [Test]
        public void EqualTest()
        {
            var controlBlock = new ControlBlock(-1, 1);
            Assert.IsTrue(controlBlock.Equals(new ControlBlock(-1, 1)));
        }
    }
}
