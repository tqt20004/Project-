using UnityEngine;

public class Item : MonoBehaviour
{
    [Header("Data Config")]
    public ItemDataSO weaponDataSO;


    Rigidbody2D rb;
    private SpriteRenderer renderSprite;
    public string name;
    public string description;
    public Sprite sprite;
    public int quantity;

    public AIBase aiBase;

    void Awake()
    {
        renderSprite = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        UpdateVisual();
    }

    public void UpdateVisual()
    {
        if (weaponDataSO == null)
        {
            Debug.LogWarning($"Item trên {gameObject.name} chưa có WeaponDataSO!");
            return;
        }

        // Lấy dữ liệu visual từ SO
        var visualData = weaponDataSO.GetData<VisualData>();
        var z = visualData.weaponSprite;
        if (visualData != null && visualData.weaponSprite != null)
        {
            SetInfo(weaponDataSO.name, z, weaponDataSO.quantity, weaponDataSO.description);
            SetSprite(z);
        }
    }
    public void SetInfo(string name,Sprite sprite,int quantity , string description)
    {
        this.name = name;
        this.sprite = sprite;
        this.description = description;
        if (this.quantity == 0) this.quantity = quantity;

    }
    public void GetSO(ItemDataSO weaponDataSO)
    {
        this.weaponDataSO = weaponDataSO;
        //VisualData visualData =GetComponent<VisualData>();
        //Sprite sprite = visualData.weaponSprite;
        //SetInfo(weaponDataSO.name, sprite, weaponDataSO.quantity, weaponDataSO.description);
    }

    public void SetSprite(Sprite sprite)
    {
        if (renderSprite == null) renderSprite = GetComponent<SpriteRenderer>();
        renderSprite.sprite = sprite;
    }
   
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        // 1. Check xem cái va chạm vào mình có phải là Player không (Dùng Tag cho nhanh)
        if (collision.gameObject.CompareTag("Player"))
        {
            aiBase = collision.gameObject.GetComponent<AIBase>();
                // 3. Đẩy dữ liệu vào Inventory
                var temp = InventoryManager.instance.AddItem(name, sprite, quantity, description, weaponDataSO);
                if (temp == 0) Destroy(gameObject);
                else if (temp == quantity) return;
                else quantity = temp;
        }
    }

}