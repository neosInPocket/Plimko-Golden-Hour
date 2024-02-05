using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreUpgrader : MonoBehaviour
{
    [SerializeField] private UpgraderHolder firstUpgrade;
    [SerializeField] private UpgraderHolder secondUpgrade;
    [SerializeField] private List<CurrentHolders> currentHolders;

    private void Start()
    {
        RefreshAllDependencies();
    }

    public void UpgradeFirst()
    {
        SaveSystem.Document.currency -= firstUpgrade.Price;
        SaveSystem.Document.ballSpeed++;
        SaveSystem.SetData();
        RefreshAllDependencies();
    }

    public void UpgradeSecond()
    {
        SaveSystem.Document.currency -= secondUpgrade.Price;
        SaveSystem.Document.ballSize++;
        SaveSystem.SetData();
        RefreshAllDependencies();
    }

    public void RefreshAllDependencies()
    {
        secondUpgrade.Refresh();
        firstUpgrade.Refresh();
        currentHolders.ForEach(x => x.Refresh());
    }
}
