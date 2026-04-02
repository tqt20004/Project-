using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShopUI : MonoBehaviour
{
    public TextMeshProUGUI moneyText;
    public GameObject shopPanel;
    public static ShopUI instance;
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }
    void Start()
    {
        RefreshMoney(); // Lần đầu mở Shop lên là phải hiện đúng số tiền
    }

    // Hàm này để gọi mỗi khi mua đồ xong hoặc được cộng tiền
    public void RefreshMoney()
    {
        if (RunTimeData.instance != null && RunTimeData.instance.curGameData != null)
        {
            moneyText.text = "$" + RunTimeData.instance.curGameData.playerMoney.ToString("N0");
            // "N0" để nó tự thêm dấu phẩy ngăn cách nghìn cho chuyên nghiệp (ví dụ: 1,000)
        }
    }
    public void SetActiveShopPanel(bool states)
    {
        shopPanel.SetActive(states);
    }
}
