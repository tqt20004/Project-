using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class VisualData : ComponentData
{
    public VisualData visualData;
    public Sprite weaponSprite;
    public Vector3 scale = Vector3.one;

    public void Init(ItemDataSO data)
    {
        visualData = data.GetData<VisualData>();
        if (visualData != null)
        {
            Debug.Log("Đã lấy được dữ liệu");
        }
    }
}
