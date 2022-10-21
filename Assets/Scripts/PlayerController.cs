using System.Collections;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{

    public static PlayerController instance;

    private void Awake()
    {
        instance = this;
    }

    [SerializeField] ParticleSystem dust;

    // Config
    [SerializeField] float MovementSpeed = 5f;
    [SerializeField] int JumpSpeed = 5;
    [SerializeField] Transform RaycastPoint;
    [SerializeField] float rayLength;
    [SerializeField] GameObject GFX;
    
    Rigidbody2D MyRigidBody;
    Animator Anim;

    private void Start()
    {
        MyRigidBody = GetComponent<Rigidbody2D>();
        Anim = GetComponent<Animator>();
    }
    private void Update()
    {
        Anim.SetFloat("yVelocity", MyRigidBody.velocity.y);
        Anim.SetBool("onGround", CheckGround());
        Jump();
        Movement();
        FlipSprite();
    }
    
    private void Movement()
    {
        float ControlThrow = CrossPlatformInputManager.GetAxis("Horizontal");
        Vector2 PlayerVelocity = new Vector2(ControlThrow * MovementSpeed, MyRigidBody.velocity.y);
        MyRigidBody.velocity = PlayerVelocity;
        bool IfPlayerHasHorizontalSpeed = Mathf.Abs(MyRigidBody.velocity.x) > Mathf.Epsilon;
        Anim.SetBool("isRunning", IfPlayerHasHorizontalSpeed);
        if(IfPlayerHasHorizontalSpeed)
        {
            CreateDust();
        }
    }

    private void FlipSprite()
    {
        bool IfPlayerHasHorizontalSpeed = Mathf.Abs(MyRigidBody.velocity.x) > Mathf.Epsilon;

        if (IfPlayerHasHorizontalSpeed)
        {
            transform.localScale = new Vector2(Mathf.Sign(MyRigidBody.velocity.x), 1f);
        }
    }
    
    private void Jump()
    {
        if (!CheckGround()) return;
        if (CrossPlatformInputManager.GetButtonDown("Jump"))
        {
            Anim.SetBool("isJumping", true);
            Vector2 JumpVelocity = new Vector2(0f, JumpSpeed);
            MyRigidBody.velocity += JumpVelocity;
            CinemachineShake.Instance.ShakeCamera(0.2f, 0.3f);
        }
    }

    private void JumpAnimOff()
    {
        Anim.SetBool("isJumping", false);
    }

    
    private bool CheckGround()
    {
        return Physics2D.Raycast(RaycastPoint.position, Vector2.down, rayLength, LayerMask.GetMask("Ground"));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            if(collision.collider == collision.gameObject.GetComponent<Enemy>().EnemyDeathCollider)
            {
                collision.gameObject.GetComponent<Enemy>().DeathAnimTrigger();
            }
            else
            {
                Died();
            }
        }

        else if(collision.gameObject.tag == "Spikes")
        {
            Died();
        }
    }

    private void Died()
    {
        MyRigidBody.bodyType = RigidbodyType2D.Static;
        Anim.SetTrigger("Died");
    }

    private void DeathComplete()
    {
        SceneManager.LoadScene(0);
    }

    private void CreateDust()
    {
        dust.Play();
    }
}