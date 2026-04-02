using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class FixNotice
{


    /// triển khai hàm Raycast ở AIBase rồi truyền cho SkillBase 
 
    //Sửa Pooling ở chỗ tạo Effect , hiện tại đang là New
//1. Cơ chế hoạt động của Pooling
//Lần đầu cần Effect: Kiểm tra trong Kho xem có cái nào "nhàn rỗi" (Inactive) không.
//    Nếu không có: Lúc này mới new một cái mới.
//    Nếu có: Lôi cái cũ ra, Reset lại các chỉ số(máu, thời gian, target) rồi gắn vào AI.

//Khi hết hạn: Thay vì để nó bị xóa (Destroy/Remove), ông chỉ cần "cất" nó lại vào Kho.

//2. Code triển khai thử (Simple Pool)

//public static class EffectPool
//    {
//        // Kho chứa các Effect đã dùng xong
//        private static Stack<EffectBase> _pool = new Stack<EffectBase>();

//        public static EffectBase Get()
//        {
//            // Nếu kho có đồ, lấy ra dùng lại
//            if (_pool.Count > 0)
//            {
//                return _pool.Pop();
//            }
//            // Nếu kho trống, mới phải tạo mới (giảm bớt rác)
//            return new EffectBase();
//        }

//        public static void ReturnToPool(EffectBase effect)
//        {
//            // Thay vì để GC dọn, ta cất vào kho
//            _pool.Push(effect);
//        }
//    }
//3. Sửa lại hàm Apply trong AIBase
//Lúc này hàm Apply của ông sẽ cực kỳ sạch sẽ và tối ưu:

//C#
//public void Apply(EffectConfig so)
//    {
//        // Lấy từ kho
//        EffectBase effect = EffectPool.Get();

//        effect.Init(this, so);
//        AddEffect(effect);
//    }
//    Và trong Update của AIBase, chỗ RemoveAt ông sửa thành:

//C#
//if (activeEffects[i].IsExpired) 
//{
//    EffectPool.ReturnToPool(activeEffects[i]); // Trả lại kho
//    activeEffects.RemoveAt(i);
//}
//4.Ưu điểm cực lớn (Lý do để làm)
//Zero GC Spike: Game chạy mượt mà, không bao giờ bị khựng do dọn rác bộ nhớ.

//Quản lý tập trung: Ông kiểm soát được hiện tại đang có bao nhiêu Effect đang chạy trong toàn bộ Game.
}
