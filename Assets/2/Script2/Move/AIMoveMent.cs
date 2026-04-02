using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIMoveMent : MoveMentBase
{
    // vì MoveMentBase đã có rồi (protected).
    public override void GetAgent(NavMeshAgent agent)
    {
        this.agent = agent;
        if (this.agent != null)
        {
            this.agent.updateRotation = false;
            this.agent.updateUpAxis = false;
            if (this.agent.transform.parent != null)
            {
                this.agent.transform.parent.rotation = Quaternion.identity;
            }

            this.agent.transform.rotation = Quaternion.identity;
        }
    }

    public override void Move(Vector2 targetPos)
    {
        if (agent == null) return;
        if (targetPos == null) return;
        if (agent.transform.parent != null)
            agent.transform.parent.rotation = Quaternion.identity;

        // NavMesh cần vị trí đích, không phải hướng (direction)
        agent.SetDestination(targetPos);
    }
}
