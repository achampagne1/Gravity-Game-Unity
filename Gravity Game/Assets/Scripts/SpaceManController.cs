using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceManController : CharacterController
{
    bool enemyCollideFlag = false;
    float health = 10f;
    void Start()
    {
        calculateStart();
        
    }

    void FixedUpdate()
    {
        setMovement((int)Input.GetAxisRaw("Horizontal"));
        setJump(Input.GetKeyDown(KeyCode.Space));
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

}