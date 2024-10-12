using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : ObjectController
{

    CircleCollider2D circleColliderPlayer;
    public GameObject prefab;

    float drag = .1f;
    bool first = true;

    //vectors
    Vector2 initialForce = new Vector2(10, 0);




    // Start is called before the first frame update
    void Start()
    {
        calculateStart();
    }

    // Update is called once per frame
    void Update()
    {
        if (!first)
        {
            calculateRotation();
            calculateUpdate();
            rb.velocity = calculateDrag(rb.velocity);
        }
    }

    protected override void calculateRotation()
    {
        // Create a quaternion representing the desired rotation angle around the y-axis
        // bullet rotation is slightly different from other object rotations. it must take into account its velocity
        // due to this, the calculateRotation() parent function is overidden
        float angle = Mathf.Atan2(rb.velocity.y+gravityDirection.y, rb.velocity.x+gravityDirection.x) * Mathf.Rad2Deg;
        Quaternion desiredRotation = Quaternion.Euler(0f, 0f,angle);
        transform.rotation = desiredRotation;
    }

    Vector2 calculateDrag(Vector2 input)
    {
        float magnitude = input.magnitude;
        Vector2 unitVector = input.normalized;
        magnitude -= drag;
        return (magnitude * unitVector);
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(this.gameObject);
    }

    public void setNotFirst()
    {
        first = false;
    }

}
