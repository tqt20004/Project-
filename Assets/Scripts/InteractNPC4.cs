using TMPro;
using UnityEngine;

public class InteractNPC4 : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private TextMeshProUGUI experienceText;
    [SerializeField] private GameObject upgradePanel;

    [Header("Settings")]
    [SerializeField] private int upgradeCost;
    [SerializeField] private int healthLimit;

    // Shortcut tới Data cho gọn code
    private RunTimeData Data => RunTimeData.instance;

    private void OnEnable() => Invoke(nameof(RefreshUI), 0.05f); // Delay 0.05 giây rồi gọi hàm

    private void OnTriggerEnter2D(Collider2D col) => SetPanelActive(col, true);
    private void OnTriggerExit2D(Collider2D col) => SetPanelActive(col, false);

    private void SetPanelActive(Collider2D col, bool isActive)
    {
        if (col.CompareTag("Player")) upgradePanel.SetActive(isActive);
    }

    public void UpgradeHealth()
    {
        
        // 1. Check điều kiện giới hạn
        if (Data.playerHealthPercent >= healthLimit)
        {
            Debug.Log("You are Strong Enough! :33");
            return;
        }

        // 2. Thực hiện giao dịch
        bool isSuccess = UpgradeSystem.Buy(upgradeCost, ref Data.curGameData.player_Experience);

        if (!isSuccess)
        {
            Debug.Log("Not enough Experience!");
            return;
        }
        Debug.Log(Data.curLevel + " // " + Data.playerHealthPercent);
        // 3. Save
        Data.UpgradeLevel();
        Data.SaveGame();

        // 4. Update UI
        RefreshUI();
    }

    public void RefreshUI()
    {
        healthText.text = Data.playerHealthPercent.ToString();
        experienceText.text = Data.curGameData.player_Experience.ToString();
    }
}