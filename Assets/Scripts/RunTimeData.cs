using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunTimeData : MonoBehaviour
{
    public static RunTimeData instance;
    public GameData curGameData;
    public List<int> itemEquipedList;
    public RoleConfig curRoleCfg;
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
        return curGameData;    
    }
    public void SaveGame()
    {
        // 1. Khởi tạo một cục Data mới để chứa
        GameData newData = new GameData();

        // 2. Đi gom dữ liệu từ các nơi (Chỉ gom những thằng đang tồn tại)
        if (ShopManager.instance != null)
        {
            newData.buyedItem = ShopManager.instance.buyedItem;
        }
        newData.playerMoney = curGameData.playerMoney;

        SaveLoadSystem.SaveGame(newData);
    }
    public void AddMoney(int amount)
    {
        curGameData.playerMoney += amount;
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
}
