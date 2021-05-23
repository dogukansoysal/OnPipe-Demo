using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CoinManager : Singleton<CoinManager>
{
    public int currentCoins;
    public UnityAction<int> CoinsChanged;
    private void Start()
    {
        if(SaveManager.GetSaveDataInt("coinCount")== -1)
        {
            SaveManager.Save("coinCount", 0);
        }
        currentCoins = SaveManager.GetSaveDataInt("coinCount");
        CoinsChanged?.Invoke(currentCoins);
    }
    public void AddCoins(int value)
    {
        currentCoins += value;
        SaveManager.Save("coinCount", currentCoins);
        CoinsChanged?.Invoke(currentCoins);
    }
    public void RemoveCoins(int value)
    {
        currentCoins -= value;
        SaveManager.Save("coinCount", currentCoins);
        CoinsChanged?.Invoke(currentCoins);
    }
}
