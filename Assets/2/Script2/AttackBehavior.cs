using System;
using System.Collections;
using System.Collections.Generic;
using System.IO.Pipes;
using UnityEngine;

public class AttackBehavior : MonoBehaviour , IWeaponComponent
{
    public  AttackData attackData;
    public Transform shottingGunTransform;
    public GameObject trailEffectBullet;
    public float offset = 1f;
    public Vector2 dir;
    public float nextFireTime = 0f;
    public float fireRate;

    [SerializeField] private List<EffectConfig> effects = new List<EffectConfig>();    //sửa lại kiểu dữ liệu
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Do();
    }

    public void Init(ItemDataSO data)
    {
        Debug.Log("called Init in Attack");
        attackData = data.GetData<AttackData>();
        if (attackData == null )
        {
            this.enabled = false; // Data không có thì nghỉ làm việc luôn
            return;
        }
       
            this.enabled = true;
        
            SetInfo();
        Debug.Log("called Init in Attack After");
    }

    private void SetInfo()
    {
         fireRate = attackData.fireRate; 
    }

    void Do()
    {
        if (attackData == null) { return; }
        if (Input.GetKey(KeyCode.Mouse0) && Time.time > nextFireTime)
        {
            shottingGunTransform = GetPoint.instance.shottingPoint;
            DoRaycast();
            nextFireTime = Time.time+ 1/fireRate;
        }
    }
    void DoRaycast()
    {
        Debug.Log("Do Raycast");
        Vector3 shottingGunPoint = shottingGunTransform.position;
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 10f;
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePos);
        dir = (worldPos - shottingGunPoint).normalized;
        RaycastHit2D hit;
        int mask = ~(LayerMask.GetMask("Player") | LayerMask.GetMask("Sensor"));
        hit = Physics2D.Raycast(shottingGunPoint,dir,attackData.range,mask);
        Vector3 finalPoint;
        if (hit.collider != null)
        {
            // Nếu bắn trúng, điểm kết thúc là nơi viên đạn chạm vào quái
            finalPoint = hit.point;
        }
        else
        {
            // Nếu bắn hụt, điểm kết thúc là điểm xa nhất theo tầm bắn của súng
            // Công thức: Vị trí bắt đầu + (Hướng bắn * Độ dài tầm bắn)
            finalPoint = shottingGunPoint + (Vector3)(dir * attackData.range);
        }
        SpawnTrailEffect(shottingGunPoint, finalPoint, dir);

        if (hit.collider != null )
        {
            SoundManager.Instance.PlayShotEffect();
            Debug.Log(hit.collider.name + "current position of hit is :" + hit.transform.position);
            AIBase target = hit.collider.GetComponentInChildren<AIBase>();
            if (target != null)
            {
                target.ApplyStatEffect(StatType.health, attackData.damage);

                Debug.Log($"<color=red>[Gun Combat]</color> Đã nã {attackData.damage} dame vào {target.name}");

                // 2. Nạp toàn bộ hiệu ứng phụ (Chậm, Độc, Choáng...)
                // Chỗ này check list có phần tử thì mới chạy cho đỡ tốn tài nguyên
                if (effects.Count > 0)
                {
                    foreach (var effect in effects)
                    {
                        target.Apply(effect);
                        Debug.Log($"<color=blue>[Effect Applied]</color> {effect.name} to {target.name}");
                    }
                }
            }

        }
        else { Debug.Log("Miss Shot!!"); }
    }   
    public void SpawnTrailEffect(Vector3 start, Vector3 end, Vector2 direction)
    {
        // 1. Đẻ viên đạn giả
        GameObject bullet = Instantiate(trailEffectBullet, start, Quaternion.identity);

        // 2. Tính khoảng cách và thời gian cần thiết để bay tới đích
        float distance = Vector3.Distance(start, end);
        float speed = 100f; // Tốc độ đạn giả (nên để cao cho mượt)
        float timeToReach = distance / speed;

        // 3. Cho nó bay
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = direction * speed;
        }

        // 4. CHỐT HẠ: Hủy đúng lúc nó vừa chạm điểm end
        // Cộng thêm một chút thời gian (ví dụ 0.1s) để cái đuôi Trail kịp co lại
        Destroy(bullet, timeToReach + 0.05f);
    }
}
