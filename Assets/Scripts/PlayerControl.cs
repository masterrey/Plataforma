using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public float DragWoY=5;
    public float WalkForce = 10;
    public float JumpForce = 10;
    public enum States
    {
        Idle,
        Walk,
        Attack,
        Damage,
        Die,
        Jump

    }
    public States state;
    public Animator anim;
    public Rigidbody2D rdb;
    float horizontal;
    float jumpTime=1;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Idle());
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        if (horizontal > 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        if (horizontal < 0)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        horizontal = Mathf.Abs(horizontal);
        if (Input.GetButtonDown("Jump"))
        {
            StartCoroutine(Jump());
        }
        if (Input.GetButtonUp("Jump"))
        {
           jumpTime = 0;
        }
        anim.SetFloat("DistChao", Physics2D.Raycast(transform.position, Vector2.down, 10).distance);

    }

    void FixedUpdate()
    {
        //drag sem o y 
        rdb.AddForce(new Vector2(-rdb.velocity.x,0)* DragWoY);
    }

        
    IEnumerator Idle()
    {
        state = States.Idle;
        anim.SetFloat("Velocidade", 0);
        while (state == States.Idle)
        {
            yield return new WaitForEndOfFrame();
            if (horizontal > 0.1f)
            {
                StartCoroutine(Walk());

            }
            
        }
           
    }

    

    IEnumerator Walk()
    {
        state = States.Walk;
        anim.SetFloat("Velocidade", 1);
        while (state == States.Walk)
        {
            yield return new WaitForEndOfFrame();
            rdb.AddForce(transform.right* WalkForce);

            if (horizontal < 0.1f)
            {
                StartCoroutine(Idle());
            }
          

        }

    }

    IEnumerator Jump()
    {
        
        state = States.Jump;
        jumpTime = .3f;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 10);
        if (hit.distance > 1.3f)
        {
            StartCoroutine(Idle());
        }
        while (state == States.Jump)
        {
            yield return new WaitForFixedUpdate();
            rdb.AddForce(transform.up * JumpForce * jumpTime);
            
            jumpTime -= Time.fixedDeltaTime;

            if (jumpTime < 0.1f)
            {
                StartCoroutine(Idle());
            }

        }

    }
}
