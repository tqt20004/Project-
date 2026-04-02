using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractNPC3 : MonoBehaviour
{
    public GameObject ChooseCharPanel;
    public Image imageRole;
    public List<RoleConfig> roleConfigList;
    RoleConfig curRole;
    int index;
    public GameDatabase gameDatabase;
    // Start is called before the first frame update
    void Start()
    {
        if (gameDatabase != null)
        {
            roleConfigList = gameDatabase.rolePlayerConfigs;
        }
        index = 0;
        if (roleConfigList.Count == 0) return;
        curRole = roleConfigList[index];
        SetImage();
    }

    public void NextRole()
    {
        if (roleConfigList.Count == 0) return;
        index++;
        if (index >= roleConfigList.Count) index = 0;
        curRole = roleConfigList[index];
        SetImage();
    }
    public void SetImage()
    {
        imageRole.sprite = curRole.chacracterSprite;
    }
    public void Apply()
    {
        RunTimeData.instance.curRoleCfg = curRole;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        ChooseCharPanel.SetActive(true);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        ChooseCharPanel.SetActive(false);
    }
    
}
