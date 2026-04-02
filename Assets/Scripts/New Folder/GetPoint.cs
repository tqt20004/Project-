using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetPoint : MonoBehaviour
{
    public Transform shottingPoint;
    public static GetPoint instance;
    private void Awake()
    {
        // Kiểm tra nếu đã có một instance khác tồn tại thì hủy con này đi
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject); // Quan trọng: Phải hủy bản sao thừa
        }
    }
    // Start is called before the first frame update
    public Transform GetShottingPoint()
    {
        return shottingPoint;
    }
}
