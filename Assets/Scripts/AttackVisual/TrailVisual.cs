using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailVisual : MonoBehaviour
{
    Rigidbody2D rb;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("cc");
        Destroy(gameObject);
    }
}
