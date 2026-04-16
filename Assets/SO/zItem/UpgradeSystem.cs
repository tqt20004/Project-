using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class UpgradeSystem 
{
    public static void UpgradeHealth()
    {

    }
    public static bool Buy(int price , ref int money)
    {
        bool isBuy = CheckValid(price, money);
        if (isBuy)
        {
            money -= price;
            return true;
        }
        return false;
    }
    public static bool CheckValid(int price , int money)
    {
        if (money < price) return false;
        return true;
    }
   
}
