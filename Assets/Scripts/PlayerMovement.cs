using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerMovement : MonoBehaviour
{
    //Config
    [SerializeField] float runSpeed, jumpHeight, climbSpeed; 
    [SerializeField] Vector2 deathKick = new Vector2(0f , 25f); 

    //State
   public bool isAlive = true;

    // Cached component references
    Rigidbody2D myRigidBody;
    Animator myAnimator;
    Collider2D myCollider;
    int extraJumps = 1;

    // Start is called before the first frame update
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        myRigidBody.gravityScale = 1;
        myAnimator = GetComponent<Animator>();
        myCollider = GetComponent<CapsuleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (!isAlive) { return; }
 
        //Keyboard controls, disable for onScreen Joystick
        Run(CrossPlatformInputManager.GetAxis("Horizontal")); 
        ClimbLadder(CrossPlatformInputManager.GetAxis("Vertical"));

        Jump();
        FlipSprite();
        Die();
    } 

    public void Run(float horizontal)
    { 
        float  controlThrow = horizontal; 
         
        Vector2 playerVelocity = new Vector2(controlThrow * runSpeed, myRigidBody.velocity.y);
        myRigidBody.velocity = playerVelocity;

        //Check if the player is moving or not to set animation accordingly.
        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidBody.velocity.x) > Mathf.Epsilon;
        myAnimator.SetBool("isRunning", playerHasHorizontalSpeed);

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            runSpeed = 8;
             playerVelocity = new Vector2(controlThrow * runSpeed, myRigidBody.velocity.y);
            myRigidBody.velocity = playerVelocity;
        }

        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            runSpeed = 4;
        }
    }

    public void ClimbLadder(float vertical)
    {
        //If we're not touching the ladders then do nothing, else, enable the player to climb.
        if (!myCollider.IsTouchingLayers(LayerMask.GetMask("Climbing")))
        {
            myAnimator.SetBool("isClimbing", false);
            myRigidBody.gravityScale = 1;
            return;
        }

        float controlThrow = vertical;
        Vector2 climbVelocity = new Vector2(myRigidBody.velocity.x, controlThrow * climbSpeed);
        myRigidBody.velocity = climbVelocity;

        bool playerHasVerticalSpeed = Mathf.Abs(myRigidBody.velocity.y) > Mathf.Epsilon;
        myAnimator.SetBool("isClimbing", playerHasVerticalSpeed);
        myRigidBody.gravityScale = 0;
    }

    private void Jump()
    {
       

        //If we're not touching the ground then jump, else don't jump
        if (!myCollider.IsTouchingLayers(LayerMask.GetMask("Ground")) && extraJumps == 0)
        {
            return;
        }
          
        if (Input.GetButtonDown("Jump") && myCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            Debug.Log("Jump!");
            myRigidBody.velocity += Vector2.up * jumpHeight;
        }
        else if (Input.GetButtonUp("Jump"))
        {
            if (myRigidBody.velocity.y > 0)
            {
                myRigidBody.velocity = Vector2.up * 0.5f;
            }
        }

         /// Code below is for double jumping.

        //if (myCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
        // {
        //    extraJumps = 1;
        //}

        // if (CrossPlatformInputManager.GetButtonDown("Jump"))
        // { 
        //      Vector2 jumpVelocityToAdd = Vector2.up * jumpHeight;
        //       myRigidBody.velocity += jumpVelocityToAdd;
        //      extraJumps--;  
        // }
    }

    private void Die()
    {
        if (myCollider.IsTouchingLayers(LayerMask.GetMask("Enemy", "Hazards")))
        {
            myAnimator.SetTrigger("Die");
            GetComponent<Rigidbody2D>().velocity = deathKick;
            isAlive = false;
            StartCoroutine(DeathTransition());
           
        }
    }


    //When Player is moving back, flip the sprite by scaling it to -1.
    private void FlipSprite()
    {
        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidBody.velocity.x) > Mathf.Epsilon;
        if (playerHasHorizontalSpeed)
        {
            transform.localScale = new Vector2(Mathf.Sign(myRigidBody.velocity.x), 1f);
        } 
    }

    IEnumerator DeathTransition()
    {
        yield return new WaitForSeconds(3f);
        FindObjectOfType<GameController>().PlayerDeath();

    }
}
