using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems; // Bắt buộc phải có cái này
public class ItemShopSlot : MonoBehaviour, IPointerClickHandler
{
    public Button buyedBTN;
    public string itemName;
    public Image image;
    public TextMeshProUGUI itemText;
    public bool selected = false;
    public GameObject selectedPanel ;
    public TextMeshProUGUI priceText;
    public GameObject isBuyedPanel;
    public GameObject isEquipedPanel;

    public ItemDataSO weaponDataSO;
    public bool isFull = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void ReceiveSO(ItemDataSO weaponDataSO)
    {
        this.weaponDataSO = weaponDataSO;
        if (weaponDataSO != null) isFull = true;
        PresentItemShopSlot();
    }
    public void PresentItemShopSlot()
    {
        VisualData visualData = weaponDataSO.GetData<VisualData>();
        image.sprite = visualData.weaponSprite;
        itemName = weaponDataSO.name;
        itemText.text = weaponDataSO.name;
        priceText.text = weaponDataSO.price.ToString();
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        // Kiểm tra nếu là Chuột Trái
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            Debug.Log("chose");
            ShopManager.instance.SelectWeaponFromSlot(this);
            selected = true;
        }
        // Kiểm tra nếu là Chuột Phải (Option thêm cho ngầu)
        //else if (eventData.button == PointerEventData.InputButton.Right)
        //{
        //    Debug.Log("Click chuột phải để xem thông tin chi tiết chẳng hạn");
        //}
    }
    public void SetSelected()
    {
        selectedPanel.SetActive(true);
    }
    public void SetSelectedOff()
    {
        selectedPanel.SetActive(false);
    }
    public void SetActiveIsBuyed(bool state)
    {
        if(state == true)
        isBuyedPanel.SetActive(true);
    }
    public void SetActiveIsEquiped(bool state)
    {
        if(state == true)
        isEquipedPanel.SetActive(true);
    }
    
}
