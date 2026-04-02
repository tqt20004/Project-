using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualBehavior : MonoBehaviour,IWeaponComponent
{
    public VisualData visualData;
    private SpriteRenderer spriteRenderer;
    public void Init(WeaponDataSO curSO)
    {
        visualData = curSO.GetData<VisualData>();
        if(spriteRenderer == null)
        {
            spriteRenderer = GetComponentInParent<SpriteRenderer>();
            
        }
        if (visualData == null)
        {
            this.enabled = false;
            return;
        }
        CheckCondition();
    }

    private void CheckCondition()
    {
        SetVisual();
    }

    public void SetVisual()
    {
        spriteRenderer.sprite = visualData.weaponSprite;
    }
}
