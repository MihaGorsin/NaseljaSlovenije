using UnityEngine;
using UnityEditor;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

public class TownManagerTest {

    [Test]
    public void TownManagerCircleTest()
    {
        for (int i = 0; i <= TownManager.NumberOfAllTowns; i++) TownManager.GetNextTown();
        Assert.IsNotNull(TownManager.selectedTown);
    }

    [Test]
    public void TownManagerRandomizeTownsTest()
    {
        Town[] firstTownOrder = new Town[TownManager.NumberOfAllTowns];
        for(int i=0; i<TownManager.NumberOfAllTowns; i++) {
            firstTownOrder[i] = TownManager.selectedTown;
            TownManager.GetNextTown();
        }
        Town[] secondTownOrder = new Town[TownManager.NumberOfAllTowns];
        for (int i = 0; i < TownManager.NumberOfAllTowns; i++) {
            secondTownOrder[i] = TownManager.selectedTown;
            TownManager.GetNextTown();
        }
        Assert.AreNotEqual(firstTownOrder, secondTownOrder);
    }
}
