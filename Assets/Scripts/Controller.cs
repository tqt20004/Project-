using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Controller : MonoBehaviour
{
    [SerializeReference]
    public MoveMentBase moveMent;
    public string myTag;
    public NavMeshAgent agent;
    public Vector3 target;
    public AIBase owner;
    private ITargetSelector targetSelector;

    void Start()
    {
        GetAiBase();
        GetITargetSelectorMethod();
        CheckAndAttach();
    }
    void CheckAndAttach()
    {
        Rigidbody2D parentRb = GetComponentInParent<Rigidbody2D>();
        if (transform.parent == null) { Debug.Log("CantFindParent"); return; }
        myTag = transform.parent.tag;
        if (myTag == "Player") moveMent = new PlayerMove();
        else if (myTag == "Enemy")
        {
            agent = GetComponentInParent<NavMeshAgent>();
            moveMent = new AIMoveMent();
            if (moveMent != null)
            {
                moveMent.GetAgent(agent);
            }else { Debug.Log("0"); }
        }
        if (moveMent != null && parentRb != null)
        {
            moveMent.GetRigid(parentRb);
        }
    }
    

    void Update()
    {
        if (moveMent == null) return;
        if (myTag == null) return;
        if (myTag == "Player") PlayerMove();
        else if(myTag == "Enemy") EnemyMove();
        else return;
        CheckRay();

    }
    void GetAiBase()
    {
       owner = transform.root.GetComponentInChildren<AIBase>();
    }
    void GetITargetSelectorMethod()
    {
        if(owner == null) return;
        this.targetSelector = owner.targetSelector;
    }
    void CheckRay()
    {
        if(owner.roleConfig.isPlayer == false) return;
        if (!Input.GetKeyDown(KeyCode.Space)) return;
        if(targetSelector == null) GetITargetSelectorMethod();
        AIBase z = targetSelector.GetTarget(transform, 3f);
        owner.SetTargetForSkill(0,z);
        owner.RequestUseSkill(0);
        Debug.Log(target);
    }
    void PlayerMove()
    {
        Vector2 input;
        input.x = Input.GetAxisRaw("Horizontal");
        input.y = Input.GetAxisRaw("Vertical");

        // Logic Xoay Hướng (Flip)
        if (input.x > 0)
        {
            // Nhìn sang phải
            transform.parent.localScale = new Vector3(1, 1, 1);
        }
        else if (input.x < 0)
        {
            // Nhìn sang trái -> Lật cả GunPoint đi theo
            transform.parent.localScale = new Vector3(-1, 1, 1);
        }

        if (input.sqrMagnitude > 1) input.Normalize();
        moveMent.Move(input);
    }
    void EnemyMove()
    {
        // BƯỚC 1: Kiểm tra xem "Đài phát thanh" có hoạt động không
        if (TargetHandler.instance != null)
        {
            var playerTransform = TargetHandler.instance.pos();

            if (playerTransform != null)
            {
                // BƯỚC 2: Cập nhật tọa độ MỚI NHẤT của Player vào biến target
                target = playerTransform.position;

                // BƯỚC 3: Ra lệnh cho NavMesh đuổi theo tọa độ mới này
                // Hàm Move bên AIMoveMent sẽ gọi agent.SetDestination(target)
                moveMent.Move(target);
            }
        }
    }
}
