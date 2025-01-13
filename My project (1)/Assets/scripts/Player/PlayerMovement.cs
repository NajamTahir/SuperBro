using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask wallLayer;
    [SerializeField] private float speed;
    [SerializeField] private float jumpPower;
    [Header("Coyote time")]
    [SerializeField] private float coyoteTime;// how much time can player be in the air before jumping
    private float coyoteTimeCounter;// how much time has passed since player last touched the edge
    [Header("Multiple Jumps")]
    [SerializeField] private int extraJumps;
    private int jumpCounter;
    [Header("Wall Jump")]
    [SerializeField] private float wallJumpX;//Horizontal force of wall jump
    [SerializeField] private float wallJumpY;//Vertical force of wall jump
    private Rigidbody2D body;
    private Animator anim;
    private BoxCollider2D boxCollider;
    private float horizontalInput;
    [Header ("SFX")]
    [SerializeField] private AudioClip jumpSound;

    private void Awake(){
        //grabs reference to the rigidbody and animator
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }
    private void Update(){  
        horizontalInput = Input.GetAxis("Horizontal");
        
        //flip the player when moving left or right
        if(horizontalInput> 0.01f){
            transform.localScale = new UnityEngine.Vector2(1, 1);
        }
        else if(horizontalInput < -0.01f){
            transform.localScale = new UnityEngine.Vector2(-1, 1);
        }

        
        anim.SetBool("run", horizontalInput != 0);
        anim.SetBool("grounded", isGrounded());

        // jump
        if(Input.GetKeyDown(KeyCode.Space)){
            jump();
        }

        // Adjustable jump height
        if(Input.GetKeyUp(KeyCode.Space) && body.velocity.y > 0){
            body.velocity = new UnityEngine.Vector2(body.velocity.x, body.velocity.y * 0.5f);
        }
        if(onWall()){
            body.gravityScale = 0;
            body.velocity = UnityEngine.Vector2.zero;
        }
        else{
            body.gravityScale = 7;
            body.velocity = new UnityEngine.Vector2(horizontalInput * speed, body.velocity.y);

            if(isGrounded()){
                coyoteTimeCounter = coyoteTime; // reset coyote counter when grounded
                jumpCounter = extraJumps; // reset jump counter when grounded 
            }
            else{
                coyoteTimeCounter -= Time.deltaTime; // decrement coyote counter when not grounded
            }
        }
        
    }
    private void jump(){
        if(coyoteTimeCounter <= 0 && onWall() && jumpCounter <= 0) return;//if coyote counter is 0 or less and not on wall dont do anything and dont have extra jumps 

        SoundManager.instance.PlaySound(jumpSound);
        if(onWall()){
            WallJump();
        }
        else{
            if(isGrounded()){
                body.velocity = new UnityEngine.Vector2(body.velocity.x, jumpPower);
            }
            else{
                if(coyoteTimeCounter > 0){
                    body.velocity = new UnityEngine.Vector2(body.velocity.x, jumpPower);
                }
                else{
                    if(jumpCounter > 0){//if we have extra jumps then jump and decrement jump counter
                        body.velocity = new UnityEngine.Vector2(body.velocity.x, jumpPower);
                        jumpCounter -= 1;
                    }
                }
            }
            coyoteTimeCounter = 0;
        }
        
    }
    private void WallJump(){
        body.AddForce(new UnityEngine.Vector2(-Mathf.Sign(transform.localScale.x) * wallJumpX, wallJumpY));
    }
    private bool isGrounded(){
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center,boxCollider.bounds.size,0,UnityEngine.Vector2.down,0.1f, groundLayer);
        return raycastHit.collider != null;
    }
    private bool onWall(){
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center,boxCollider.bounds.size,0,new UnityEngine.Vector2(transform.localScale.x,0),0.1f, wallLayer);
        return raycastHit.collider != null;
    }
    public bool canAttack(){
        return horizontalInput == 0 && isGrounded() && !onWall();
    }
}
