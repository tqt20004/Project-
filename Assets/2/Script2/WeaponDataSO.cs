using System.Collections.Generic;
using System.Linq; // Cần cái này để dùng OfType
using UnityEngine;

[CreateAssetMenu(fileName = "NewWeaponData", menuName = "ModularSystem/Weapon Data")]
public class WeaponDataSO : ItemDataSO
{
    public int price;
    public int id;
    public int quantity;
    public string description;
    public bool addInShop;
    //// [SerializeReference] cho phép Unity hiện menu chọn class con trong Inspector
    [SerializeReference]
    [SubclassSelector]

    public List<ComponentData> allData = new List<ComponentData>();

    // Hàm này để Behavior tự tìm đúng data nó cần trong cái List trên
    public T GetData<T>() where T : ComponentData
    {
        return allData.OfType<T>().FirstOrDefault();
    }
}
