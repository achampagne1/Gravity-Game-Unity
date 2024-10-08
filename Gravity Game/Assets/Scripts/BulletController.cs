using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{

    Rigidbody2D rb;
    CircleCollider2D circleColliderPlayer;
    Transform planetCenter;
    public GameObject prefab;

    public float gravityForceMag = 20f;
    float drag = .1f;
    bool first = true;

    //vectors
    Vector2 gravityDirection = new Vector2(0, 0);
    Vector2 gravityForce = new Vector2(0, 0);
    Vector2 initialForce = new Vector2(0, 0);




    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
       
        GameObject temp = GameObject.Find("Planet");
        planetCenter = temp.GetComponent<Transform>();

        rb.AddForce(initialForce, ForceMode2D.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        if (!first)
        {
            calculateGravity();
            calculateRotation();
            rb.AddForce(gravityForce);
            rb.velocity = calculateDrag(rb.velocity);
        }
    }

    void calculateGravity()
    {
        //Calculate gravitational force towards the planet
        gravityDirection = (planetCenter.position - transform.position).normalized;
        gravityForce = gravityDirection * gravityForceMag;
    }

    void calculateRotation()
    {
        // Create a quaternion representing the desired rotation angle around the y-axis
        float angle = Mathf.Atan2(rb.velocity.y, rb.velocity.x) * Mathf.Rad2Deg;
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

    public void setFirst()
    {
        first = false;
    }

}
