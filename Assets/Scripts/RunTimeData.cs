using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunTimeData : MonoBehaviour
{
    public static RunTimeData instance;
    public GameData curGameData;
    public List<int> itemEquipedList;
    public RoleConfig curRoleCfg;
    public LevelData LevelData;
    //------------//
    public int playerHealthPercent;
    public int enemyHealthPercent;
    public int curLevel;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        Load();
    }
    public GameData Load()
    {
        curGameData = SaveLoadSystem.LoadGame();
        playerHealthPercent = curGameData.playerLevel;
        return curGameData;    
    }
    public void SaveGame()
    {
        
        SaveLoadSystem.SaveGame(curGameData);
    }
    public void AddMoney(int amount)
    {
        curGameData.playerMoney += amount;
    }
    public void AddExperience(int amount)
    {
        curGameData.player_Experience += amount;
    }
    public void SpendMoney(int amount)
    {
        curGameData.playerMoney -= amount;
    }
    public void AddItemEquipedList(int id)
    {
        if (itemEquipedList.Contains(id))
        {
            Debug.Log("this item is available");
            return;
        }
        if(itemEquipedList.Count >= 3)
        {
            itemEquipedList.RemoveAt(0);
        }
        itemEquipedList.Add(id);
    }
    public int GetPlayerHealthModifier()
    {
        return playerHealthPercent;
    }
    public int GetEnemyHealthModifier()
    {
        return 0;
    }
    public void ChangePlayerHealth(int x)
    {
        playerHealthPercent = x;
    }
    public void UpgradeLevel()
    {
        int x = curGameData.playerLevel += 1;
        ChangePlayerHealth(x);
    }
    public void SetLevel(LevelData level)
    {
        this.LevelData = level;   
    }
}
