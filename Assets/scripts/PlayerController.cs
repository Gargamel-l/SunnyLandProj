using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{

    public float speed;
    public float JumpForce;
    public float moveInput;
    public bool loose;
    public Joystick joystick;
    public float health;
    public int numHealth;
    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;
    public bool death = false;

    private Rigidbody2D rb;

    private bool facingR = true;
    private bool isGround;
    public Transform GroundCheck;
    public float groundClose;
    public LayerMask Ground;
    private Animator anim;
    private bool dearHome;
    public Transform HomeCheck;
    public LayerMask home;
    public float homeClose;

    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (health > numHealth)
        {
            health = numHealth;
        }
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < Mathf.RoundToInt(health))
            {
                hearts[i].sprite = fullHeart;
            }
            else
            {
                hearts[i].sprite = emptyHeart;
            }
            if ((health > 0) && (loose))
            {
                TryAgain();
            }
            else if ((health <= 0) && (loose))
            {
                death = true;
            }
        }
        
        if (!death)
        {
            moveInput = joystick.Horizontal;
            rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);
        }

        if (facingR == false && moveInput > 0 && !loose )
        {
            Flip();
        }
        else if (facingR == true && moveInput < 0 && !loose)
        {
            Flip();
        }
        isGround = Physics2D.OverlapCircle(GroundCheck.position, groundClose, Ground);
        anim.SetBool("isJump", !isGround);
        dearHome = Physics2D.OverlapCircle(HomeCheck.position, homeClose, home);
        anim.SetBool("win", dearHome);
        death = dearHome;
        
        
        if (moveInput == 0)
        {
            anim.SetBool("isRun", false);
        }
        else
        {
            anim.SetBool("isRun", true);
        }
        
    }

    public void OnJumpBottomDown()
    {
        if (isGround == true && !death)
        {
            rb.velocity = Vector2.up * JumpForce;
        }
    }

    void Flip()
    {
        facingR = !facingR;
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        anim.SetBool("loose", true);
        loose = true;
        health -= 1;
    }

    void TryAgain()
    {
        //SceneManager.LoadScene("SunnyLand", LoadSceneMode.Single);
        var position = transform.position;
        var x = position.x;
        var y = position.y;
        transform.position = new Vector3(-7, -2, 0);
        loose = false; 
        anim.SetBool("loose", false);
    }

}
