using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour {

    public float speed;
    public float jumpPower;
    private float axisX;
    private Rigidbody2D rigidBody;
    private bool facingRight;
    private bool grounded;
    public LayerMask groundLayers;
    private BoxCollider2D collider2d;
    private Animator animator;

	void Start () {
        rigidBody = GetComponent<Rigidbody2D>();
        collider2d = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
        facingRight = true;
    }
	
	void Update () {

        if (axisX > 0 && !facingRight) {
            Flip();
        } else if (axisX < 0 && facingRight) {
            Flip();
		
        }

      
        animator.SetFloat("HorizontalSpeed", axisX);
        animator.SetFloat("VerticalSpeed", Input.GetAxis("Vertical"));
        animator.SetBool("Grounded", grounded);

    }

    void FixedUpdate()
    {
	    
        axisX = Input.GetAxis("Horizontal");
	grounded = Physics2D.OverlapArea(new Vector2(transform.position.x - (collider2d.size.x / 2), transform.position.y - 0.05f), new Vector2(transform.position.x + (collider2d.size.x / 2), transform.position.y - 0.06f), groundLayers);
	//Raycast2D hit = Physics2D.Raycast(transform.position, -Vector2.down);
	    
        if (axisX > 0f)
        {
            rigidBody.velocity = new Vector2(axisX * speed, rigidBody.velocity.y);
        }
        else if (axisX < 0f)
        {
            rigidBody.velocity = new Vector2(axisX * speed, rigidBody.velocity.y);
        }
        else
        {
            rigidBody.velocity = new Vector2(0, rigidBody.velocity.y);
        }

        if (Input.GetButtonDown("Jump") && grounded) //for RayCast grounded should be changed to hit.collider != null
        {
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, jumpPower);
        }


    }

    void Flip() {
        facingRight = !facingRight;

        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(0, 1, 0, 0.5f);
        Gizmos.DrawCube(new Vector2(transform.position.x, transform.position.y - 0.055f), new Vector2(collider2d.size.x, 0.01f));

    }
}
