using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IActor
{
    [SerializeField] string groundTag = "Ground";
    [SerializeField] BoxCollider2D myBody;

    Rigidbody2D myRigidbody;
    Animator myAnimator;
    Vector2 moveVector;

    // For smooth Movement
    CircleCollider2D myFeet;

    public Vector3 jumpForce;
    public float moveSpeed; 

    // Start is called before the first frame update
    void Start()
    {
        myFeet = GetComponent<CircleCollider2D>();
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("left") || Input.GetKey("right"))
        {
            Move();
        }
        else myAnimator.SetBool("Move", false);

        if (Input.GetKeyDown("space"))
        {
            Jump();
        }
        if (Input.GetKeyDown("z"))
        {
            Attack();
        }
    }

    private void Attack()
    {
        myAnimator.SetTrigger("Attack");
    }

    #region Jump
    private void Jump()
    {
        // Preventing air jumping
        if (!myFeet.IsTouchingLayers(LayerMask.GetMask("Ground"))) return;
        myAnimator.SetTrigger("Jump");
        myRigidbody.AddForce(jumpForce);
    }

    /// <summary>
    /// Anytime we finish touching on ground jump clip will be played
    /// </summary>
    /// <param name="collider"></param>
    private void OnCollisionExit2D(Collision2D collider)
    {
        if (collider.gameObject.CompareTag("Ground"))
        {
            
        }
    }
    #endregion

    private void Move()
    {
        int localScaleX = Input.GetKey("left") ? -1 : 1;

        float moveVector = localScaleX * moveSpeed;
        myRigidbody.velocity = new Vector2(moveVector * moveSpeed, myRigidbody.velocity.y);
        transform.localScale = new Vector3(Mathf.Sign(moveVector), transform.localScale.y, transform.localScale.z);

        if (myFeet.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            myAnimator.SetBool("Move", true);  
        }
        else
            myAnimator.SetBool("Move", false);

    }



    #region IActor Implementation
    public void OnDeath()
    {
        throw new NotImplementedException();
    }

    public void TakeDamage(float Damage)
    {
        throw new NotImplementedException();
    }
    #endregion
}
