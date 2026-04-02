using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameDatabase", menuName = "System/GameDatabase")]
public class GameDatabase : ScriptableObject
{

    [Header("Inventory Items")]
    public List<WeaponDataSO> allWeapons;
    public List<RoleConfig> rolePlayerConfigs;
    // Hàm tra cứu vũ khí
    public WeaponDataSO GetWeaponByID(int id)
    {
        return allWeapons.Find(x => x.id == id);
    }
    //ở trên nghĩa là: =))
    //foreach (var x in allWeapons)
    //{
    //    if (x.id == id)
    //    {
    //        return x; // Tìm thấy thì trả về ngay
    //    }
    //}
    //return null; // Duyệt hết mà không thấy thì trả về null


}