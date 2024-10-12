using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SpaceManController : CharacterController
{
    //object creation
    GunWrapper gunWrapper;

    //game variables
    bool enemyCollideFlag = false;
    float health = 10f;

    void Start()
    {
        calculateCharacterStart();
        gunWrapper = new GunWrapper();
        gunWrapper.Start();
    }

    void FixedUpdate()
    {
        setMovement(inputSystemToGetAxis());
        setJump(Keyboard.current.spaceKey.isPressed);
        calculateCharacterUpdate();
    }

    private void Update()
    {
        if (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame)
        {
            Vector2 location = new Vector2(0, 0);
            Vector2 direction = new Vector2(0, 0);
            gunWrapper.shoot(location, direction);
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