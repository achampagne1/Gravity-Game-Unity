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
        if (bulletObject != null)
            Debug.Log("f");
    }

    void Update()
    {
        
    }

    public void shoot(Vector2 location,Vector2 direction)
    {
        GameObject ShotBullet = Instantiate(bulletObject, new Vector3(2, 2, 0), Quaternion.identity);
        ShotBullet.GetComponent<BulletController>().setNotFirst();
    }
}
