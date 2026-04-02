using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public static Weapon Instance { get; private set; }
    public WeaponDataSO weaponDataSO;
    // Start is called before the first frame update
    private void Awake()
    {
        // Khởi tạo Singleton
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        
    }
    public void SetSO(WeaponDataSO curSO)
    {
        Debug.Log("called SetSo in Weapon class");
        weaponDataSO = curSO;
        SetUp();
    }
    void SetUp()
    {
        var components = GetComponentsInChildren<IWeaponComponent>();
        Debug.Log($"Tìm thấy tất cả: {components.Length} components con"); // Thêm dòng này
        foreach (var component in components)
        {
            component.Init(weaponDataSO);
        } 
            
    }

}
