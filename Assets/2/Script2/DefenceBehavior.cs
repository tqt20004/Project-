using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenceBehavior : MonoBehaviour, IWeaponComponent
{
    public DefenceData defenceData; 
    public void Init(WeaponDataSO data)
    {
        defenceData = data.GetData<DefenceData>();
        if (defenceData != null)
        {
            Debug.Log("get: " + defenceData);
        } 
            
    }
}
