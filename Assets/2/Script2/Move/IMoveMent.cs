using UnityEngine;
using UnityEngine.AI;

public abstract class MoveMentBase 
{
    protected Rigidbody2D rb;
    protected NavMeshAgent agent;

    // Hàm này để Controller đưa "xác" vào
    public void GetRigid(Rigidbody2D parentRb)
    {
        rb = parentRb;
        if (rb != null) rb.gravityScale = 0f;
    }
    public  virtual void GetAgent(NavMeshAgent agent)
    {
        this.agent = agent;
    }
    // Hàm này để Controller đưa "lệnh" vào
    public abstract void Move(Vector2 direction);
}