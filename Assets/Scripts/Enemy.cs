using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] float MovementSpeed = 1f;
    Rigidbody2D MyRigidBody;

    [SerializeField] GameObject RayCastPoint;
    [SerializeField] float RayLength;
    [SerializeField] LayerMask mask;

    public Collider2D EnemyDeathCollider;
    public Collider2D PlayerDeathCollider;

    void Start()
    {
        MyRigidBody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Movement();
    }
    
    private void Movement()
    {
        if (CheckRayCastGround() && !CheckRayCastWall())
        {
            if (IsFacingRight())
            {
                MyRigidBody.velocity = new Vector2(MovementSpeed, 0f);
            }

            else
            {
                MyRigidBody.velocity = new Vector2(-MovementSpeed, 0f);
            }
        }

        else if(!CheckRayCastGround() || CheckRayCastWall())
        {
            transform.localScale = new Vector2(-(Mathf.Sign(MyRigidBody.velocity.x)) , 1);
        }
    }

    private bool IsFacingRight()
    {
        return transform.localScale.x > 0;
    }

    public bool CheckRayCastGround()
    {
        return Physics2D.Raycast(RayCastPoint.transform.position, Vector2.down, RayLength, LayerMask.GetMask("Ground"));
    }

    public bool CheckRayCastWall()
    {
        return Physics2D.Raycast(RayCastPoint.transform.position, Vector2.right, RayLength, mask);
    }

    public void DeathAnimTrigger()
    {
        MyRigidBody.bodyType = RigidbodyType2D.Static;
        CinemachineShake.Instance.ShakeCamera(2f, 0.1f);
        Destroy(EnemyDeathCollider);
        Destroy(PlayerDeathCollider);
        GetComponent<Animator>().SetTrigger("Died");
    }

    public void Died()
    {
        Destroy(gameObject);
    }
}