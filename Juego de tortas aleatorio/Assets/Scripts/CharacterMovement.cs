﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    int iJumpLeft;
    int iFrameDash;

    float timer;
    float fChrSwift, fChrJump;
    Vector2 v2ChrVel;


    bool bIsCollidingLeft, bIsCollidingRight;
    bool bIsTouchingGround;
    bool bIsDashing, bCanDashAgain;

    uint iSpeed = 6;

    // Start is called before the first frame update
    void Start()
    {
        iJumpLeft = 3;
        iFrameDash = 15;
        timer = 0.0f;
        fChrSwift = 20.0f;
        fChrJump = 300.0f;
        bIsTouchingGround = true;
        bCanDashAgain = true;

        if(gameObject.GetComponent<CharacterBehaviour>() != null)
            iSpeed = gameObject.GetComponent<CharacterBehaviour>().Speed;
    }

    // Update is called once per frame
    void Update()
    {

        v2ChrVel = gameObject.GetComponent<Rigidbody2D>().velocity;
        Move();
        Dash();
        //Debug.Log("I'm moving");
    }

    private void Move()
    {
        if (Input.GetKeyDown(KeyCode.W) && iJumpLeft > 0)
        {
            if (bIsCollidingRight && !bIsTouchingGround)
            {
                gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                gameObject.GetComponent<Rigidbody2D>().AddForce(Mathf.Sqrt(.5f) * fChrJump * (Vector2.up + Vector2.left));
            }
            else if (bIsCollidingLeft && !bIsTouchingGround)
            {
                gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                gameObject.GetComponent<Rigidbody2D>().AddForce(Mathf.Sqrt(.5f) * fChrJump * (Vector2.up + Vector2.right));
            }
            else
            {
                gameObject.GetComponent<Rigidbody2D>().velocity = v2ChrVel.x * Vector2.right;
                gameObject.GetComponent<Rigidbody2D>().AddForce(fChrJump * Vector2.up);
                iJumpLeft--;
            }

            v2ChrVel = gameObject.GetComponent<Rigidbody2D>().velocity;
        }

        if (Input.GetKey(KeyCode.A))
        {
            if(!(bIsCollidingLeft && !bIsTouchingGround))
                gameObject.GetComponent<Rigidbody2D>().AddForce(fChrSwift * Vector2.left);
            if(v2ChrVel.x < -0.3f * iSpeed && !bIsDashing)
            {
                gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(-0.3f * iSpeed, v2ChrVel.y);
            }

            v2ChrVel = gameObject.GetComponent<Rigidbody2D>().velocity;
        }

        if (Input.GetKey(KeyCode.D))
        {
            if (!(bIsCollidingRight && !bIsTouchingGround))
                gameObject.GetComponent<Rigidbody2D>().AddForce(fChrSwift * Vector2.right);
            if (v2ChrVel.x > 0.3f * iSpeed && !bIsDashing)
            {
                gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0.3f * iSpeed, v2ChrVel.y);
            }

            v2ChrVel = gameObject.GetComponent<Rigidbody2D>().velocity;
        }

        if (Input.GetKeyDown(KeyCode.S))
            gameObject.GetComponent<Rigidbody2D>().AddForce(fChrJump * Vector2.down);

        if (Input.GetKeyUp(KeyCode.S) && !bIsTouchingGround)
            gameObject.GetComponent<Rigidbody2D>().AddForce(fChrJump * Vector2.up);

        
    }

    private void Dash()
    {
        if (Input.GetKeyDown(KeyCode.L) && bCanDashAgain)
        {
            bIsDashing = true;
            bCanDashAgain = false;

            int movingLeft = (int)Mathf.Sign(gameObject.GetComponent<Rigidbody2D>().velocity.x);

            if (!(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)))
            {
                gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                gameObject.GetComponent<Rigidbody2D>().Sleep();
            } 
            else
                gameObject.GetComponent<Rigidbody2D>().AddForce(movingLeft * fChrJump * Vector2.right);
        }

        if (bIsDashing)
        {
            iFrameDash--;
            if (iFrameDash <= 0)
            {
                iFrameDash = 15;
                bIsDashing = false;
                if (bIsTouchingGround) bCanDashAgain = true;
            }
        }

        if (gameObject.GetComponent<Rigidbody2D>().IsSleeping())
        {
            timer += Time.deltaTime;
            if (timer > 0.3f)
            {
                timer = 0.0f;
                gameObject.GetComponent<Rigidbody2D>().WakeUp();
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        iJumpLeft = 3;
        bIsCollidingLeft = false;
        bIsCollidingRight = false;

        if (collision.GetContact(0).point.y == collision.GetContact(1).point.y)
        {
            bIsTouchingGround = true;
            bCanDashAgain = true;
            gameObject.GetComponent<Rigidbody2D>().gravityScale = 1.0f;
            gameObject.GetComponent<Rigidbody2D>().drag = 0.9f;
        }
            
        else
        {
            gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            gameObject.GetComponent<Rigidbody2D>().gravityScale = 0.3f;
            gameObject.GetComponent<Rigidbody2D>().drag = 1.0f;

            if (collision.contacts[0].point.x > transform.position.x)
            {
                bIsCollidingRight = true;
            }
            else
            {
                bIsCollidingLeft = true;
            }
        }

        
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        gameObject.GetComponent<Rigidbody2D>().gravityScale = 1.0f;
        gameObject.GetComponent<Rigidbody2D>().drag = 0.0f;

        bIsCollidingLeft = false;
        bIsCollidingRight = false;
        bIsTouchingGround = false;
    }
}
