using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunWrapper : MonoBehaviour
{
    //object creation
    GameObject bulletObject;
    public void Start()
    {
        bulletObject = GameObject.Find("Bullet");
    }

    void Update()
    {
        
    }

    public void shoot(Vector3 location,Vector3 direction)
    {
        GameObject ShotBullet = Instantiate(bulletObject, location, Quaternion.identity);
        ShotBullet.GetComponent<BulletController>().newInstance(getMouseDirection(direction));
        ShotBullet.GetComponent<BulletController>().Start();
    }
    private static Vector3 getMouseDirection(Vector3 mousePosition)
    {
        // Step 1: Get the screen's center position (player's position)
        Vector2 screenCenter = new Vector2(Screen.width / 2f, Screen.height / 2f);

        // Step 2: Calculate the direction vector from the center to the mouse position
        Vector2 direction = new Vector2(mousePosition.x, mousePosition.y) - screenCenter;

        // Step 3: Normalize the direction vector to get a unit vector
        Vector2 normalizedDirection = direction.normalized;

        // Return the normalized direction
        return normalizedDirection;
    }
}
