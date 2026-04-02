using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootManager : MonoBehaviour
{
    //public List<GameObject> itemList;
        public GameObject moneyPrefab;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnEnable()
    {
        // Khi có bất kỳ AI nào chết
        AIBase.OnDie += DropItem;
    }

    // Khi Object này bị tắt hoặc Scene bị hủy
    private void OnDisable()
    {
        // Hủy đăng ký để tránh lỗi bộ nhớ (Quan trọng!)
        AIBase.OnDie -= DropItem;
    }
    public void DropItem(Vector2 pos)
    {
        Instantiate(moneyPrefab,pos,Quaternion.identity);
    }
}
