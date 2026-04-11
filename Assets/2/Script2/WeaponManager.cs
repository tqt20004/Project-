using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public ItemDataSO currentWeapon;
    public ItemDataSO[] weaponArray;

    public static WeaponManager Instance;
    private void Awake()
    {
        // Kiểm tra nếu đã có Instance rồi thì tự hủy cái mới để tránh trùng lặp
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        // Nếu muốn cái Manager này sống xuyên suốt các Scene:
        // DontDestroyOnLoad(gameObject); 
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) { ChooseWeapon(0); }
        if (Input.GetKeyDown(KeyCode.Alpha2)) { ChooseWeapon(1); }
        if (Input.GetKeyDown(KeyCode.Alpha3)) { ChooseWeapon(2); }
        
    }
    public void ChooseWeapon(int index)
    {
        if (weaponArray[index] != null)
        {
            currentWeapon = weaponArray[index];
        }
        Weapon.Instance.SetSO(currentWeapon);
    }
    public bool ReceiveDataWeapon(ItemDataSO newData)
    {
        if (newData == null) return false;

        for (int i = 0; i < weaponArray.Length; i++)
        {
            // Nếu đã có vũ khí này trong mảng rồi
            if (newData == weaponArray[i])
            {
                Debug.Log("Món này có rồi, không lấy thêm nữa!");
                return false; // Trả về false để bên Inventory KHÔNG xóa item
            }
        }

        // Nếu chưa có, tiến hành thêm vào mảng
        AddToArray(newData);
        Debug.Log("Đã thêm vũ khí mới thành công!");
        return true; // Trả về true để bên Inventory thực hiện xóa item
    }
    public void AddToArray(ItemDataSO newData)
    {
        bool isFull = true;
        for (int i = 0; i < weaponArray.Length; i++)
        {
            if (weaponArray[i] == null)
            {
                weaponArray[i] = newData;
                isFull = false;
                Debug.Log("added in slot" + i);
                break;
            }
        }
        if (isFull) Debug.Log("Slots is Full");
    }
    public void MoveToInventory()
    {
        ///Get Image
        Sprite weaponSprite = null;
        foreach (var data in currentWeapon.allData)
        {
            if (data is VisualData visual)
            {
                weaponSprite = visual.weaponSprite;
                break; // Tìm thấy ảnh rồi thì "break" vòng lặp ngay cho nhẹ máy
            }
        }
        //ADD,Remove
        var temp = InventoryManager.instance.AddItem(currentWeapon.name, weaponSprite,currentWeapon.quantity,currentWeapon.description,currentWeapon);
        if (temp == 0)
        {
            for (int i = 0;i < weaponArray.Length;i++)
            {
                if (weaponArray[i] == currentWeapon)
                {
                    weaponArray[i] = currentWeapon =  null;
                    break;
                }
            }
        }
    }
    public void DropItem()
    {

    }
    
}
