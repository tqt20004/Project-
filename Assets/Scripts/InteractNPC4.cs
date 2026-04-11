using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InteractNPC4 : MonoBehaviour
{
    public TextMeshProUGUI healthTimeUpGrade;
    public GameObject upgradePanel;
    // Start is called before the first frame update
    private void Start()
    {
        int x = RunTimeData.instance.playerHealthPercent;
        UpdateUI(x);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            upgradePanel.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            upgradePanel.SetActive(false);
        }
    }
    public void Increase_One_PlayerHealthPercent()
    {
        int x = RunTimeData.instance.playerHealthPercent + 1;
        RunTimeData.instance.ChangePlayerHealth(x);
        UpdateUI(x);
    }
    public void UpdateUI(int x)
    {
        healthTimeUpGrade.text = x.ToString();
    }
}
