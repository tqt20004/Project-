using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//public class CircleSelector : ITargetSelector
//{
//    public AIBase GetTarget(Transform seeker, float range)
//    {
//        // Mượn seeker.position để quét Scene
//        var hit = Physics2D.OverlapCircle(seeker.position, range);
//        //if(hit) {Debug
//        return hit?.GetComponent<AIBase>();
//    }
//}
public class CircleSelector : ITargetSelector
{
    public AIBase GetTarget(Transform seeker, float range)
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(seeker.position, range);

        // Log xem thực tế nó quét trúng BAO NHIÊU thằng (kể cả đất đá)
        Debug.Log("Số lượng vật thể quét trúng: " + hits.Length);

        foreach (var hit in hits)
        {
            Debug.Log("Tên vật thể quét trúng: " + hit.name); // Xem nó có quét trúng con quái nào không

            AIBase target = hit.GetComponentInChildren<AIBase>();
            if (target != null && target.gameObject != seeker.gameObject && target.roleConfig.isPlayer == false)
            {
                return target;
            }
        }
        return null;
    }
}