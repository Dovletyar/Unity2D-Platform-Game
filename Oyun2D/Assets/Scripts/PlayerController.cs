using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{

    Rigidbody2D playerRB;
    Animator playerAnimator;

    public float moveSpeed = 1f;
    public float jumpSpeed = 1f, jumpFrequency = 1f, nextJumpTime;

    bool facingRight = true;
    public bool isGrounded = false;

    //zemin kontrol değişkenleri
    public Transform groundCheckPosition;
    public float groundCheckRadius;
    public LayerMask groundCheckLayer;


    void Awake()
    {
        
    }

    
    // Start is called before the first frame update
    void Start()
    {
        playerRB = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        HorizontalMove();
        onGroundCheck();


        if (playerRB.velocity.x < 0 && facingRight)
        {
            //yuzunu çevir
            FlipFace();

        }
        else if(playerRB.velocity.x > 0 && !facingRight)
        {
            //yuzunu çevir
            FlipFace();

        }

        //Zıplama
        if (Input.GetAxis("Vertical") > 0 && isGrounded && (nextJumpTime < Time.timeSinceLevelLoad))
        {
            nextJumpTime = Time.timeSinceLevelLoad + jumpFrequency;
            Jump();
        }



    }

    void FixedUpdate()
    {

    }

    //yurume metodu
    void HorizontalMove()
    {
        playerRB.velocity = new Vector2(Input.GetAxis("Horizontal") * moveSpeed, playerRB.velocity.y);
        playerAnimator.SetFloat("playerSpeed", Mathf.Abs(playerRB.velocity.x));

    }

    //yuzunu cevir metodu
    void FlipFace()
    {
        facingRight = !facingRight;
        Vector3 tempLocalScale = transform.localScale;
        tempLocalScale.x *= -1;
        transform.localScale = tempLocalScale;
    }

    //zıplama metodu
    void Jump()
    {
        playerRB.AddForce(new Vector2(0f, jumpSpeed));

    }

    //zemin kontrolu metodu
    void onGroundCheck()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheckPosition.position, groundCheckRadius, groundCheckLayer);
        playerAnimator.SetBool("isGroundedAnim", isGrounded);

    }

    //level atlama
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("LevelUp"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
        }
    }



    }
