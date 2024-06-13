using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceManController : MonoBehaviour
{
    //object creation
    Rigidbody2D rb;
    CircleCollider2D circleColliderPlayer;
    Transform planetCenter;

    //constants
    public float moveSpeed = 20f;
    public float jumpForce = 11f;
    public float gravityForceMag = 20f;

    //game variables
    float heightTestPlayer = 0;
    float horizontalInput = 0;
    float rotatedX = 0;
    float rotatedY = 0;
    int layerMaskPlanet = 0;
    bool isGrounded = false;
    bool space = false;
    bool facingLeft = false;


    //vectors
    Vector2 gravityDirection = new Vector2(0, 0);
    Vector2 gravityForce = new Vector2(0, 0);
    Vector2 moveDirection = new Vector2(0, 0);
    Vector2 jump = new Vector2(0, 0);
    Vector2 previousV = new Vector2(0, 0);
    Vector2 drag = new Vector2(0, 0);
    Vector2 previousMove = new Vector2(0, 0);
    Vector2 jumpExtraction = new Vector2(0, 0);




    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        circleColliderPlayer = GetComponent<CircleCollider2D>();
        heightTestPlayer = circleColliderPlayer.bounds.extents.y + 0.05f;
        layerMaskPlanet = LayerMask.GetMask("Default");

        GameObject temp = GameObject.Find("Planet");
        planetCenter = temp.GetComponent<Transform>();

        rb.velocity = new Vector2(0, 0); //this can be moifie to have a starting velocity
    }

    void FixedUpdate()
    {
        //inputs
        horizontalInput = Input.GetAxisRaw("Horizontal");
        space = Input.GetKeyDown(KeyCode.Space);

        if(horizontalInput == -1 && facingLeft == false)
        {
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
            facingLeft = true;
        }
        else if(horizontalInput == 1 && facingLeft == true)
        {
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
            facingLeft = false;
        }

        //Check if the player is grounded
        isGrounded = IsGrounded();

        if (!isGrounded)
            jumpExtraction = rb.velocity - gravityForce - moveDirection;
        else
            jumpExtraction = Vector2.zero;

        float jumpMagnitude = CalculateMagnitude(jumpExtraction);
        
        //Calculate gravitational force towards the planet
        gravityDirection = (planetCenter.position - transform.position).normalized;
        gravityForce = gravityDirection * gravityForceMag;

        //movement
        rotatedX = -gravityDirection.y;
        rotatedY = gravityDirection.x;
        moveDirection = new Vector2((horizontalInput * moveSpeed * rotatedX), (horizontalInput * moveSpeed * rotatedY));
               

        // Create a quaternion representing the desired rotation angle around the y-axis
        float angle = Mathf.Atan2(gravityDirection.y, gravityDirection.x) * Mathf.Rad2Deg;
        Quaternion desiredRotation = Quaternion.Euler(0f, 0f, 90f + angle);
        transform.rotation = desiredRotation;

        // Jump if grounded and space is pressed
        rotatedX = -gravityDirection.x;
        rotatedY = -gravityDirection.y;
        if (space && isGrounded)
            jump = new Vector2(rotatedX * jumpForce, rotatedY * jumpForce);
        else
            jump = new Vector2(0, 0);
        
        
        //drag calculation
        if (isGrounded)
        {
            drag = previousV;
        }
        else
            drag = previousMove;

       
        rb.AddForce(gravityForce);
        rb.AddForce(jump + moveDirection + drag, ForceMode2D.Impulse);

        rb.velocity += -jumpExtraction + jumpMagnitude*-gravityDirection;
        previousV = -rb.velocity;        
        previousMove = -moveDirection;
    }

    bool IsGrounded()
    {
        RaycastHit2D hit = Physics2D.Raycast(circleColliderPlayer.bounds.center, gravityDirection, heightTestPlayer, layerMaskPlanet);
        bool ground = hit.collider != null;
        return ground;
    }

    float CalculateMagnitude(Vector2 vector)
    {
        return Mathf.Sqrt(vector.x * vector.x + vector.y * vector.y);
    }
}