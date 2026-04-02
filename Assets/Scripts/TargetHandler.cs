using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetHandler : MonoBehaviour
{
   

    private Transform playerTransform;
    public static TargetHandler instance;
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;

    }

    void Start()
    {
        playerTransform = GetComponentInParent<Transform>();
    }

    // Update is called once per frame
    public Transform pos()
    {
        return playerTransform;
    }
    
}
