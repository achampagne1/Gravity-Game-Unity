using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SpaceManController : CharacterController
{
    bool enemyCollideFlag = false;
    float health = 10f;
    GameObject BulletObject;

    void Start()
    {
        BulletObject = GameObject.Find("Bullet");
        calculateStart();
        
    }

    void FixedUpdate()
    {
        setMovement(inputSystemToGetAxis());
        setJump(Keyboard.current.spaceKey.isPressed);
        calculateUpdate();
    }

    private void Update()
    {
        if (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame)
        {
            GameObject ShotBullet = Instantiate(BulletObject, new Vector3(2, 2, 0), Quaternion.identity);
            ShotBullet.GetComponent<BulletController>().setFirst();
        }
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