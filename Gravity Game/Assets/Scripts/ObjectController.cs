using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectController : MonoBehaviour
{
    //object creation
    public Rigidbody2D rb;
    public Transform planetCenter;

    //constants
    public float gravityForceMag = 20f;

    //game variables
    public int layerMaskPlanet = 0;

    //vectors
    public Vector2 gravityDirection = new Vector2(0, 0);
    public Vector2 gravityForce = new Vector2(0, 0);

    public void calculateStart()
    {
        rb = GetComponent<Rigidbody2D>();
        layerMaskPlanet = LayerMask.GetMask("Default");

        GameObject temp = GameObject.Find("Planet");
        planetCenter = temp.GetComponent<Transform>();

        rb.velocity = new Vector2(0, 0); //this can be moifie to have a starting velocity*/
    }

    public void calculateUpdate()
    {
        calculateGravity();

        calculateRotation();

        rb.AddForce(gravityForce);
    }
    
    //should gravity be virtual as well? I dont think so because all objects get affected by gravity equally
    //unless some object have a different effect on gravity. for now we will leave it
    void calculateGravity() 
    {
        //Calculate gravitational force towards the planet
        gravityDirection = (planetCenter.position - transform.position).normalized;
        gravityForce = gravityDirection * gravityForceMag;
    }

    public virtual void calculateRotation()
    {
        // Create a quaternion representing the desired rotation angle around the y-axis
        float angle = Mathf.Atan2(gravityDirection.y, gravityDirection.x) * Mathf.Rad2Deg;
        Quaternion desiredRotation = Quaternion.Euler(0f, 0f, 90f + angle);
        transform.rotation = desiredRotation;
    }


}
