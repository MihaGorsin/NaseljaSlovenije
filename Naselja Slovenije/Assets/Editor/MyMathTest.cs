using UnityEngine;
using UnityEditor;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

public class MyMathTest {

    [Test]
    public void ZoomScaleTest()
    {
        Assert.AreEqual(5f, MyMath.ZoomScale(10, 20, 10));
    }
}
