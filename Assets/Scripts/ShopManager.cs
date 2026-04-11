using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
public class ShopManager : MonoBehaviour
{
    public ShopUI shopUI;
    public List<int> buyedItem;
    public GameDatabase gameDatabase;
    public List<ItemShopSlot> ItemShopSlotList;
    public static ShopManager instance;
    public Button buyedBTN;
    public ItemShopSlot curItemShopSlot;
    //--------//
    public ItemShopSlot pickedItemShopSlot;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        
        }
        else
        {
            Destroy(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        GetData();
       
        foreach(var i in buyedItem)
        {
        Debug.Log("add: " + i);
        }
        upLoadDataForShop();
    }
    void GetData()
    {
        GameData data = RunTimeData.instance.Load(); //
        this.buyedItem = data.buyedItem;
    }
  
    public void  AddBuyedItem(int id)
    {
        buyedItem.Add(id);
        RunTimeData.instance.SaveGame();
        upLoadDataForShop();
    }
    public void upLoadDataForShop()
    {
        foreach (var s in ItemShopSlotList) s.isFull = false;
        // Chạy 1 vòng lặp duy nhất
        for (int i = 0; i < gameDatabase.allWeapons.Count; i++)
        {
            // Né lỗi tràn mảng nếu số đồ mua vượt quá số ô UI
            if (i >= ItemShopSlotList.Count) break;

            ItemDataSO data = gameDatabase.allWeapons[i];
            if (data != null)
            {
                ItemShopSlotList[i].SetActiveIsEquiped(false);
                ItemShopSlotList[i].ReceiveSO(data);
                foreach(var checkExsist in buyedItem)
                {
                    if(checkExsist == ItemShopSlotList[i].weaponDataSO.id)
                     ItemShopSlotList[i].SetActiveIsBuyed(true);
                }
                bool isEquiped = RunTimeData.instance.itemEquipedList.Contains(data.id);
                Debug.Log(isEquiped+"-------------");
                ItemShopSlotList[i].SetActiveIsEquiped(isEquiped);
            }
        }
    }
    public void DeSelected()
    {
        foreach ( var s in ItemShopSlotList)
        {
            s.selected = false;
            s.SetSelectedOff();
        }
    }
    public void SelectWeaponFromSlot(ItemShopSlot clickedSlot)
    {
        SoundManager.Instance.PlaySelectEffect();
        // 1. Duyệt qua danh sách các Slot và bắt tụi nó bỏ chọn hết
        foreach (var slot in ItemShopSlotList)
        {
            slot.selected = false;
            slot.SetSelectedOff();
        }

        if (clickedSlot != null)
        { 
        clickedSlot.selected = true;
            clickedSlot.SetSelected();
            curItemShopSlot = clickedSlot;
        }
        
    }
    public void buyItem()
    {
        if (curItemShopSlot == null || curItemShopSlot.weaponDataSO == null)
        {
            Debug.Log("CurItemShopSlot Not Exsist");
            return; 
        }
        int id = curItemShopSlot.weaponDataSO.id;
        int price = curItemShopSlot.weaponDataSO.price;
        bool valid = ValidatePurchase(id,price);
        if (valid == true)
        {
            AddBuyedItem(id);
            RunTimeData.instance.SpendMoney(price);
            Debug.Log("purchased item id:" + id);
            shopUI.RefreshMoney();
        }
        else if(valid == false)
        {
            Debug.Log("can't buy it");
        }
    }
    public bool ValidatePurchase(int id ,int price)
    {
        GameData gameData = RunTimeData.instance.curGameData;
        if (gameData != null)
        {
            if (buyedItem.Contains(id))
            {
                Debug.Log("exsist" + id);
                return false;
            }
                
            if (gameData.playerMoney >= price)
            {
                return true;
            }
            else
            {
                Debug.Log("money is not enough");
                return false;
            }
        }
        
            Debug.Log("gameDataNull");
            return false;
        
    }
    public void EquipItem()
    {
        SoundManager.Instance.PlaySelectEffect();
        if (buyedItem.Contains(curItemShopSlot.weaponDataSO.id))
        { 
        RunTimeData.instance.AddItemEquipedList(curItemShopSlot.weaponDataSO.id);
        upLoadDataForShop();
        }
        
    }
}

