using UnityEngine;

public class PlayerMove : MoveMentBase
{
    public float speed = 5f;
    private Vector2 currentDir;

    private void Start()
    {
        Debug.Log("zz");
    }
    // Lớp con bắt buộc phải thực thi hàm này từ cha
    public override void Move(Vector2 direction)
    {
        currentDir = direction;
        rb.velocity = currentDir * speed;
    }

    void FixedUpdate()
    {
        if (rb != null)
        {
            rb.velocity = currentDir * speed;
        }
    }
}