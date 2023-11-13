using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    // Component References
    Rigidbody2D rb;
    SpriteRenderer sr; 

    // Movement Variables
    public float speed = 5.0f;
    public float jumpForce = 300.0f;

    //GroundedCheck 
    public bool isGrounded;
    public Transform groundCheck;
    public LayerMask isGroundLayer;
    public float groundCheckRadius = 0.02f;

    // Start is called before the first frame update
    void Start()
    {
        // Getting Our Component References
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();


        //Checking variables for Dirty Data
        if (rb == null) Debug.Log("No Rigidbody Reference");
        if (sr == null) Debug.Log("No Sprite Renderer Reference");

        if (groundCheckRadius <=0)
        {
            groundCheckRadius = 0.02f;
            Debug.Log("groundCheck set to default value");
        }

        if (speed <= 0)
        {
            speed = 5.0f;
            Debug.Log("speed set to default value");
        }

        if (jumpForce <= 0)
        {
            jumpForce = 300.0f;
            Debug.Log("jumpForce set to default value");
        }

        if(groundCheck == null)
        {
            GameObject obj = new GameObject();
            obj.transform.SetParent(gameObject.transform);
            obj.transform.localPosition = Vector3.zero;
            obj.name = "GroundCheck";
            groundCheck = obj.transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        float hInput = Input.GetAxisRaw("Horizontal");

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, isGroundLayer);

        if(isGrounded && Input.GetButtonDown("Jump"))
        {
            rb.AddForce(Vector2.up * jumpForce);
        }

        Vector2 moveDirection = new Vector2(hInput * speed, rb.velocity.y);
        rb.velocity = moveDirection;
    }
}
