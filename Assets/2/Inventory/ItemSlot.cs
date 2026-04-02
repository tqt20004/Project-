using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class ItemSlot : MonoBehaviour, IPointerClickHandler
{
    public WeaponDataSO infoSO;

    public int maxNumberOfItem;
    //----ItemData----//

    public Sprite itemSprite;
    public int quantity;
    public string itemName;
    public bool isFull;
    public string itemDescription;

    public Image imageDescription;
    //----ItemSlot----//

    public TextMeshProUGUI quantityText;
    public Image itemImage;

    //---ItemDescription---//
    public TextMeshProUGUI itemDescriptionText;
    public TextMeshProUGUI itemDescriptionName;

    public GameObject selectedItemSlotPanel;
    public bool isSelectedSlot;
    [SerializeField]
    private Sprite emptySprite;

    // Start is called before the first frame update
    public int AddItem(string itemName, Sprite itemSprite, int quantity, string itemDescription, WeaponDataSO infoSO)
    {
        if (isFull)
            return quantity;
        //this.itemName = itemName;

        this.itemSprite = itemSprite;
        itemImage.sprite = itemSprite;
        //this.itemDescription = itemDescription;
        this.infoSO = infoSO;

        this.quantity += quantity;
        if (this.quantity >= maxNumberOfItem)
        {
            quantityText.text = maxNumberOfItem.ToString();
            quantityText.enabled = true;

            isFull = true;

            int extraItems = this.quantity - maxNumberOfItem;
            this.quantity = maxNumberOfItem;
            return extraItems;
        }

        ///Quantity Text Update
        ///
        quantityText.text = this.quantity.ToString();
        quantityText.enabled = true;
        return 0;


    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            OnLeftClick();
        }
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            //OnRightClick();
        }
    }

    //private void OnRightClick()
    //{
    //    Debug.Log("đã nhấp chuột phải vào itemSlot");
    //    Config[] gunArray = ItemEquipManager.instance.gunSOArray;
    //    // check xem còn trống thì add vào ô đó

    //    int length = gunArray.Length;
    //    for (int i = 0; i < length; i++)
    //    {
    //        if (gunArray[i] == null || gunArray[i] == ListConfig.Instance.itemConfig[3])
    //        {
    //            if (infoSO == null)
    //            {
    //                Debug.Log("not available");
    //                return;
    //            }
    //            gunArray[i] = infoSO;
    //            ItemEquipManager.instance.EquipWeapon(i);
    //            RemoveItemSlot();
    //            return;

    //        }
    //        Debug.Log("full Slot at EquipManager");
    //    }
    //}

    private void OnLeftClick()
    {
        Debug.Log("z");
        InventoryManager.instance.DeselectedPanel();
        selectedItemSlotPanel.SetActive(true);
        isSelectedSlot = true;
        
        if(infoSO == null) { return; }
       
        this.itemDescriptionText.text = this.infoSO.description;
        this.itemDescriptionName.text = this.infoSO.name;
        
        imageDescription.sprite = itemSprite;
        
        if (imageDescription.sprite == null)
        {
            imageDescription.sprite = emptySprite;
        }
    }
    // Đặt hàm này trong class ItemSlot, ví dụ, sau hàm OnLeftClick()

    public void RemoveItemSlot()
    {
        // Chỉ cần xóa và cập nhật UI nếu slot đang có item
        if (quantity > 0)
        {
            // 1. Reset Dữ liệu
            quantity = 0;
            itemName = "";
            itemSprite = emptySprite; // Dùng sprite rỗng đã được khai báo
            itemDescription = "";
            isFull = false;
            this.infoSO = null; // **Rất quan trọng:** Reset cả tham chiếu SO Config

            // 2. Cập nhật UI
            itemImage.sprite = emptySprite;
            quantityText.enabled = false;
            quantityText.text = "";

            // 3. Xử lý UI chọn (nếu đang chọn thì ẩn đi)
            if (isSelectedSlot)
            {
                // Giả định InventoryManager.instance.DeselectedPanel() ẩn panel mô tả
                InventoryManager.instance.DeselectedPanel();
            }

            Debug.Log("ItemSlot đã được dọn dẹp.");
        }
    }
}
