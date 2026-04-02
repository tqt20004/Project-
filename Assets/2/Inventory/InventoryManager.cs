
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public GameDatabase gameDatabase;
    public static InventoryManager instance;
    public bool isInventory = false;
    public GameObject inventoryBoard;
    public ItemSlot[] itemSlot;
    public WeaponDataSO infoSO;

    public Button equipBotton;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(instance);
        }
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (isInventory == false)
            {
                Time.timeScale = 0f;
                inventoryBoard.SetActive(true);
                isInventory = true;
                //Debug.Log("Mở inventory");
            }
            else if (isInventory == true)
            {
                {
                    Time.timeScale = 1f;
                    inventoryBoard.SetActive(false);
                    isInventory = false;
                    //Debug.Log("Tắt inventory");
                }
            }
        }
    }
    public int AddItem(string name, Sprite image, int quantity, string itemDescription, WeaponDataSO infoSO)
    {
        for (int i = 0; i < itemSlot.Length; i++)
        {
            if ((itemSlot[i].isFull == false && itemSlot[i].itemName == name) || itemSlot[i].quantity == 0)
            {
                int leftOverItem = itemSlot[i].AddItem(name, image, quantity, itemDescription, infoSO);
                //Sound(name);
                Debug.Log(image);
                if (leftOverItem > 0)
                    leftOverItem = AddItem(name, image, leftOverItem, itemDescription, infoSO);
                return leftOverItem;
            }
        }
        return quantity;

    }
    public void DeselectedPanel()
    {
        for (int i = 0; i < itemSlot.Length; i++)
        {
            itemSlot[i].selectedItemSlotPanel.SetActive(false);
            itemSlot[i].isSelectedSlot = false;
        }
    }
    public void EquipItem()
    {
        for (int i = 0; i < itemSlot.Length; i++)
        {
            if(itemSlot[i].isSelectedSlot == true)
            {
                bool temp = WeaponManager.Instance.ReceiveDataWeapon(itemSlot[i].infoSO);
                if (temp == false) break;
                itemSlot[i].RemoveItemSlot();
            }
        }
    }
    //public List<int> SaveItem()
    //{
    //    List<int> IDItemlist = new List<int>();
    //    foreach(var item in itemSlot)
    //    {
    //        if(item != null && item.infoSO != null) 
    //        {
    //        Debug.Log("id" + item.infoSO.id);
    //        IDItemlist.Add(item.infoSO.id);
    //        }
    //    }
    //    return IDItemlist;
    //}
    //public void LoadItemToInventory(GameData data)
    //{
    //    // 2. Xóa sạch túi đồ cũ trước khi đổ đồ mới vào
    //    foreach (var slot in itemSlot)
    //    {
    //        slot.RemoveItemSlot();
    //    }

    //    // 3. Duyệt qua danh sách ID trong file Save
    //    foreach (var id in data.inventory)
    //    {
    //        // 4. Hứng dữ liệu tra cứu được vào biến 'foundWeapon'
    //        WeaponDataSO foundWeapon = gameDatabase.GetWeaponByID(id);

    //        // 5. Kiểm tra xem có tìm thấy đồ không, rồi mới Add vào túi
    //        if (foundWeapon != null)
    //        {
    //            var visual =  foundWeapon.GetData<VisualData>();
    //            Sprite sprite = visual.weaponSprite;
    //            AddItem(foundWeapon.name, sprite, foundWeapon.quantity, foundWeapon.description, foundWeapon);
    //        }
    //        else
    //        {
    //            Debug.LogWarning("Không tìm thấy ID " + id + " trong GameDatabase!");
    //        }
    //    }
    //}
    //public void Sound(string name)
    //{
    //    if (SoundManager.Instance == null) return;
    //    if (name == "Coin") { SoundManager.Instance.Equip(); }
    //    else { SoundManager.Instance.Equip(); }
    //}
}
