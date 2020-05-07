using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public enum States
    {
        Idle,
        Walk,
        Attack,
        Damage,
        Die
    }
    public States state;
    public Animator anim;
    public Rigidbody2D rdb;
    float horizontal;
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
            rdb.AddForce(transform.right*10);

            if (horizontal < 0.1f)
            {
                StartCoroutine(Idle());
            }
            
        }

    }
}
