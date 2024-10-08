using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxController : MonoBehaviour
{
    //object creation
    Rigidbody2D rb;
    Transform planetCenter;

    //vectors
    Vector2 gravityDirection = new Vector2(0, 0);


    // Start is called before the first frame update
    void Start()
    {
        GameObject temp = GameObject.Find("Planet");
        planetCenter = temp.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        gravityDirection = (planetCenter.position - transform.position).normalized;

        float angle = Mathf.Atan2(gravityDirection.y, gravityDirection.x) * Mathf.Rad2Deg;
        Quaternion desiredRotation = Quaternion.Euler(0f, 0f, 90f + angle);
        transform.rotation = desiredRotation;
    }
}
