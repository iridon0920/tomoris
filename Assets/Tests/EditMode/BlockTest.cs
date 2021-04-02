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
    }
}
