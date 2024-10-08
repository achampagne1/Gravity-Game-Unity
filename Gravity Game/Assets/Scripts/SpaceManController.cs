using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SpaceManController : CharacterController
{
    bool enemyCollideFlag = false;
    float health = 10f;

    Rigidbody2D Bullet;

    void Start()
    {
        GameObject BulletObject = GameObject.Find("Bullet");
        Bullet = BulletObject.GetComponent<Rigidbody2D>();
        for (var i = 0; i < 10; i++)
        {
            GameObject ShotBullet = Instantiate(BulletObject, new Vector3(i * 2.0f, 0, 0), Quaternion.identity);
            ShotBullet.GetComponent<BulletController>().setFirst();
        }
        calculateStart();
        
    }

    void FixedUpdate()
    {
        setMovement(inputSystemToGetAxis());
        Debug.Log(Keyboard.current.aKey.isPressed);
        setJump(Keyboard.current.spaceKey.isPressed);
        calculateUpdate();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "SpaceZombie" && enemyCollideFlag == false)
        {
            //Vector2 forceVector = new Vector2(-5f,0f);
            //addForceLocal(forceVector);
            health -= 1f;
            UIHandler.instance.setHealthValue(health);
            enemyCollideFlag = true;
        }
        else if (collision.gameObject.name != "SpaceZombie" && enemyCollideFlag == true)
            enemyCollideFlag = false;

        if (collision.gameObject.name == "MedPack")
        {
            health = 10f;
            UIHandler.instance.setHealthValue(health);
        }
            

    }

    int inputSystemToGetAxis()
    {
        if (Keyboard.current.aKey.isPressed)
            return -1;
        if (Keyboard.current.dKey.isPressed)
            return 1;
        else
            return 0;
    }

}