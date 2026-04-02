using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class GameData 
{
    public int playerID;
    public int playerMoney;
    public int level;
    public List<int> inventory;
    public List<int> buyedItem;
}
