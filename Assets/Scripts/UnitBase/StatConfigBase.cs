using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class StatConfigBase
{
    public StatType type;

    // 4 Biến đúng y hệt trong hình
    public float baseValue;
    public float basePercentValue = 0f; // 0 tức là +0%
    public float otherValue = 0f;
    public float allPercentValue = 1f;  // 1 tức là 100%

    public float currentValue;
    // Constructor nhận data từ SO
    public StatConfigBase(StatConfigSO data)
    {
        this.type = data.type;
        this.baseValue = data.baseValue;
        //
        currentValue = GetFinalValue();
    }

    // Công thức tính toán (Hàm get trong hình)
    public float GetFinalValue()
    {
        // Công thức: (Gốc * %Gốc + Số_Cộng_Thẳng) * %Tổng
        // Ví dụ: (100 * 1 + 0) * 1 = 100
        return (baseValue * (1 + basePercentValue) + otherValue) * allPercentValue;
    }
}