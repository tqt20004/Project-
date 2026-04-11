using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttackBehavior : MonoBehaviour, IWeaponComponent
{
    public Transform holdingWeaponPoint;
    public CircleSelector circleRayCast;
    public float damage;
    public float fireRate;
    public MeleeAtackData meleeAtackData;
    Transform finalPos;
    AIBase target;
    // Start is called before the first frame update
    void Start()
    {
        circleRayCast = new CircleSelector();
        Debug.Log("zz");
    }
   
    // Update is called once per frame
    void Update()
    {
        Do();
        //Debug.Log("Đang chạy MeleeBehave");
    }

    private void CheckRay()
    {
       target =  circleRayCast.GetTarget(transform,5f);
        Debug.Log("zzz");

    }

    private void Do()
    {
        if (meleeAtackData == null) { return; }

        if (Input.GetKey(KeyCode.Z))
        {
            holdingWeaponPoint = GetPoint.instance.shottingPoint;

            CheckRay();

            if (target != null)
            {
                Debug.Log("scaned and found by Melee: " + target);
            }
            if (target == null) { Debug.Log("no"); }
        }
        
    }

    public void Init(ItemDataSO data)
    {
        meleeAtackData = data.GetData<MeleeAtackData>();
        if (meleeAtackData == null )
        {
            this.enabled = false;
            return;
        }
       
            this.enabled=true;
        
            Debug.Log("đã gắn Melee");
        SetInfo();
    }

    private void SetInfo()
    {
        damage = meleeAtackData.damage;
        fireRate = meleeAtackData.fireRate;
    }

    
}
