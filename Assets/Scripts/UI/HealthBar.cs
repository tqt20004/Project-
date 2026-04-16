using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [Header("UI Reference")]
    public Image healthBarFill; // Cái Image đang để Fill Method là Horizontal

    [Header("Data Source")]
    public AIBase owner; // Kéo thằng AIBase (Player hoặc Enemy) vào đây

    private void Start()
    {
        owner = transform.root.GetComponentInChildren<AIBase>();
    }
    private void OnEnable()
    {
        if(owner == null) { owner = transform.root.GetComponentInChildren<AIBase>(); }
        if (owner != null)
        {
            // 1. Đăng ký sự kiện
            owner.OnHealthChanged += UpdateHealthBar;

            // 2. Lấy dữ liệu máu từ roleStats để cập nhật lần đầu
            var hpStat = owner.roleStats.GetStat(StatType.health);  
            if (hpStat != null)
            {
                // Truyền Máu hiện tại và Máu tối đa (con số cụ thể)
                UpdateHealthBar(hpStat.currentValue, hpStat.GetFinalValue());
            }
        }
    }

    private void OnDisable()
    {
        // Khi Object bị tắt/hủy, phải hủy đăng ký để tránh lỗi Memory Leak
        if (owner != null)
        {
            owner.OnHealthChanged -= UpdateHealthBar;
        }
    }

    // Hàm này sẽ tự động chạy mỗi khi owner.TakeDamage() được gọi
    private void UpdateHealthBar(float current, float max)
    {
        //Debug.Log($"[UI] {gameObject.name} của {owner.name} update: {current}/{max}");
        Debug.Log("current and Max health " + current+"/" + max);
        if (healthBarFill != null && max > 0)
        {
            Debug.Log(current+"?" + max + "/" + current/max);
            // Fill Amount nhận giá trị từ 0.0 đến 1.0
            healthBarFill.fillAmount = current / max;
        }
    }

    private void LateUpdate()
    {
        // Nếu đây là thanh máu trên đầu Enemy (World Space), 
        // dòng này giúp nó không bị lật ngược khi Enemy quay mặt (Flip)
        if (transform.parent != null && transform.parent.lossyScale.x < 0)
        {
            transform.rotation = Quaternion.identity;
        }
    }
}