using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UINotificationManager : MonoBehaviour
{
    private Coroutine _currentDelay;
    public GameObject AnnoucePanel;
    public TextMeshProUGUI ex_text;
    private void OnEnable()
    {
        LevelManager.OnExperienceChanged += UpdateUI;
    }
    private void OnDisable()
    {
        LevelManager.OnExperienceChanged -= UpdateUI;

    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void UpdateUI(int ex_amount)
    {   
        ex_text.text ="+" + ex_amount.ToString();
        TurnOnPanel();
    }
    public void TurnOnPanel()   
    {
        AnnoucePanel.SetActive(true);
        // If there are old command that is running , turn it off then run new one  
        if (_currentDelay != null) StopCoroutine(_currentDelay);

        _currentDelay = StartCoroutine(TurnOffForSecond(1.5f));
    }
    public void TurnOffPanel()
    {
        AnnoucePanel.SetActive(false);
    }

    IEnumerator TurnOffForSecond(float second)
    {
        yield return new WaitForSeconds(second);
        TurnOffPanel();
    }
}
