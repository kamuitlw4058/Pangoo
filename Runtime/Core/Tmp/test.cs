using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using GameFramework.Event;

namespace Pangoo
{

    public interface ITestDat
    {
        public int Data1 { get; set; }
    }

    public class TestData : ITestDat
    {
        public int Data1;

        int ITestDat.Data1 { get => Data1; set => Data1 = value; }
    }
}
