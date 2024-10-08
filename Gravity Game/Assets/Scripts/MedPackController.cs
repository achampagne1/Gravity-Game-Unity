using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MedPackController : MonoBehaviour
{
    //object creation
    Rigidbody2D rb;
    Transform planetCenter;

    //vectors
    Vector2 gravityDirection = new Vector2(0, 0);
    //Vector2 originalPosition = new Vector2(0, 0);

    float floatCounter = 360f;


    // Start is called before the first frame update
    void Start()
    {
        GameObject temp = GameObject.Find("Planet");
        planetCenter = temp.GetComponent<Transform>();
        rb = GetComponent<Rigidbody2D>();
        //originalPosition = transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        gravityDirection = (planetCenter.position - transform.position).normalized;
        Vector2 newPosition = new Vector2((transform.position.x + (Mathf.Sin(floatCounter) * .015f)*-gravityDirection.x), (transform.position.y+ (Mathf.Sin(floatCounter) * .015f) * -gravityDirection.y));
        rb.MovePosition(newPosition);
        floatCounter -=.05f;
        if (floatCounter <= 0)
            floatCounter = 360;

        float angle = Mathf.Atan2(gravityDirection.y, gravityDirection.x) * Mathf.Rad2Deg;
        Quaternion desiredRotation = Quaternion.Euler(0f, 0f, 90f + angle);
        transform.rotation = desiredRotation;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(this.gameObject);

    }
}
