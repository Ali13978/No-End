using System.Collections;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerController : MonoBehaviour
{

    public static PlayerController instance;

    private void Awake()
    {
        instance = this;
    }

    // Config
    [SerializeField] float MovementSpeed = 5f;
    [SerializeField] int JumpSpeed = 5;
    [SerializeField] Transform RaycastPoint;
    [SerializeField] float rayLength;
    
    Rigidbody2D MyRigidBody;

    private void Start()
    {
        MyRigidBody = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        Jump();
        Movement();
        FlipSprite();
    }
    
    private void Movement()
    {
        float ControlThrow = Input.GetAxis("Horizontal");
        Vector2 PlayerVelocity = new Vector2(ControlThrow * MovementSpeed, MyRigidBody.velocity.y);
        MyRigidBody.velocity = PlayerVelocity;
    }

    private void FlipSprite()
    {
        //bool IfPlayerHasHorizontalSpeed = Mathf.Abs(MyRigidBody.velocity.x) > Mathf.Epsilon;

        //if (IfPlayerHasHorizontalSpeed)
        //{
        //    transform.localScale = new Vector3(-(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        //}
    }
    
    private void Jump()
    {
        if (!CheckGround()) return;
        if (Input.GetButtonDown("Jump"))
        {
            Vector2 JumpVelocity = new Vector2(0f, JumpSpeed);
            MyRigidBody.velocity += JumpVelocity;
        }
    }
    
    private bool CheckGround()
    {
        return Physics2D.Raycast(RaycastPoint.position, Vector2.down, rayLength, LayerMask.GetMask("Ground"));
    }

}