using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootManager : MonoBehaviour
{
    RunTimeData RTData => RunTimeData.instance;
    //public List<GameObject> itemList;
    public GameObject moneyPrefab;
    private int difficultyMultiplier = 1;
    // Start is called before the first frame update
    void Start()
    {
        difficultyMultiplier = LevelManager.Instance.difficultDigital;
    }
    
    private void OnEnable()
    {
        // Khi có bất kỳ AI nào chết
        LevelManager.OnEnemyKilled += DropItem;
    }

    // Khi Object này bị tắt hoặc Scene bị hủy
    private void OnDisable()
    {
        // Hủy đăng ký để tránh lỗi bộ nhớ (Quan trọng!)
        LevelManager.OnEnemyKilled -= DropItem;
    }
    // Hàm lấy hệ số nhân dựa trên độ khó
    

    public void DropItem(Vector2 pos)
    {
        
        int amount = Random.Range(1, 9);
        var random = Random.value;

        if (random < 0.4f) return;
        // 2. Spawn vật phẩm
        GameObject droppedObj = Instantiate(moneyPrefab, pos, Quaternion.identity);

        
        if (droppedObj.TryGetComponent<Item>(out var item))
        {
            item.quantity = amount * difficultyMultiplier ;
            Debug.Log("DropMoney");
            
        }
        
    }
}
