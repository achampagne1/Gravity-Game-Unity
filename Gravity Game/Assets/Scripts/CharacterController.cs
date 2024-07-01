﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour{
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
    float jumpMagnitude = 0;
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
    Vector2 additionalForce = new Vector2(0, 0);

    public void setMovement(int moveInput)
    {
        horizontalInput = moveInput;
    }

    public void setJump(bool jumpInput)
    {
        space = jumpInput;
    } 

    public CircleCollider2D getCharacterCollider()
    {
        return circleColliderPlayer;
    }

    public Vector2 getGravityDirection()
    {
        return gravityDirection;
    }

    public float getCharacterOrientation()
    {
        return transform.rotation.eulerAngles.z;
    }

    public bool getFacingLeft()
    {
        return facingLeft;
    }


    public void calculateStart()
    {
        rb = GetComponent<Rigidbody2D>();
        circleColliderPlayer = GetComponent<CircleCollider2D>();
        heightTestPlayer = circleColliderPlayer.bounds.extents.y + 0.05f;
        layerMaskPlanet = LayerMask.GetMask("Default");

        GameObject temp = GameObject.Find("Planet");
        planetCenter = temp.GetComponent<Transform>();

        rb.velocity = new Vector2(0, 0); //this can be moifie to have a starting velocity*/
    }

    public void calculateUpdate()
    {
        
        turnLeftRight();

        //Check if the player is grounded
        isGrounded = IsGrounded();

        calculateGravity();

        calculateJump();

        calculateMovement();

        calculateRotation();

        calculateDrag();

        rb.AddForce(gravityForce);
        rb.AddForce(jump, ForceMode2D.Impulse);
        rb.AddForce(moveDirection, ForceMode2D.Impulse);
        rb.AddForce(drag, ForceMode2D.Impulse);

        //for some reason aidng the aitional force oes nothing
        /*if (additionalForce != Vector2.zero)
        {
            Debug.Log("ouch");
            rb.AddForce(additionalForce, ForceMode2D.Impulse);
            Debug.Log(additionalForce);
            additionalForce = Vector2.zero;
        }*/

        if(!IsGrounded())
            rb.velocity += -jumpExtraction + jumpMagnitude * -gravityDirection; //this line is causing the glitch for them to fly up
        previousV = -rb.velocity;
        previousMove = -moveDirection;
    }

    public void addForceLocal(Vector2 force)
    {
        //Vector2 localForce = new Vector2(0, 0);

        float angle = Vector2.SignedAngle(gravityDirection, force);
        additionalForce = force;
       // Debug.Log(localForce);
        //rb.AddForce(localForce);
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

    void turnLeftRight()
    {
        if (horizontalInput == -1 && facingLeft == false)
        {
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
            facingLeft = true;
        }
        else if (horizontalInput == 1 && facingLeft == true)
        {
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
            facingLeft = false;
        }
    }

    void calculateJump()
    {
        if (!isGrounded)
            jumpExtraction = rb.velocity - gravityForce - moveDirection;
        else
            jumpExtraction = Vector2.zero;

        jumpMagnitude = CalculateMagnitude(jumpExtraction);

        // Jump if grounded and space is pressed
        rotatedX = -gravityDirection.x;
        rotatedY = -gravityDirection.y;
        if (space && isGrounded)
            jump = new Vector2(rotatedX * jumpForce, rotatedY * jumpForce);
        else
            jump = new Vector2(0, 0);
    }

    void calculateGravity()
    {
        //Calculate gravitational force towards the planet
        gravityDirection = (planetCenter.position - transform.position).normalized;
        gravityForce = gravityDirection * gravityForceMag;
    }

    void calculateMovement()
    {
        //movement
        rotatedX = -gravityDirection.y;
        rotatedY = gravityDirection.x;
        moveDirection = new Vector2((horizontalInput * moveSpeed * rotatedX), (horizontalInput * moveSpeed * rotatedY));
    }

    void calculateRotation()
    {
        // Create a quaternion representing the desired rotation angle around the y-axis
        float angle = Mathf.Atan2(gravityDirection.y, gravityDirection.x) * Mathf.Rad2Deg;
        Quaternion desiredRotation = Quaternion.Euler(0f, 0f, 90f + angle);
        transform.rotation = desiredRotation;
    }

    void calculateDrag()
    {
        //drag calculation
        if (isGrounded)
        {
            drag = previousV;
        }
        else
            drag = previousMove;

    }

    Vector2 rotate(Vector2 v, float angle)
    {
        float radian = angle * Mathf.Deg2Rad; // Convert angle to radians
        float cosTheta = Mathf.Cos(radian);
        float sinTheta = Mathf.Sin(radian);

        float newX = cosTheta * v.x - sinTheta * v.y;
        float newY = sinTheta * v.x + cosTheta * v.y;

        return new Vector2(newX, newY);
    }

}
