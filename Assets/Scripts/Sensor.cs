using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sensor : MonoBehaviour
{
    [Header("Settings")]
    public string targetTag = "Player"; // Chỉ khóa mục tiêu là Player

    [Header("References")]
    public AIBase owner; // Kéo thằng AIBase (Cha hoặc Ngang hàng) vào đây

    Rigidbody2D rb;

    private void Start()
    {
        // Nếu quên kéo tay ngoài Inspector, nó sẽ tự tìm thằng AIBase ở Object cha
        if (owner == null)
        {
            owner = GetComponentInParent<AIBase>();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(targetTag))
        {
            AIBase targetAI = other.GetComponentInChildren<AIBase>();
            owner.target = targetAI;
            Debug.Log($"<color=yellow>[Sensor]</color> Đã thấy: {other.name}");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        // Khi Player chạy mất tiêu
        if (other.CompareTag(targetTag))
        {
            owner.target = null;
            Debug.Log("<color=red>[Sensor]</color> Mất dấu mục tiêu!");
        }
    }
}
