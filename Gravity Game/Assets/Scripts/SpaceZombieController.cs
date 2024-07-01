using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceZombieController : CharacterController
{
    //object creation
    RandomTimer pauseDuration = new RandomTimer();
    RandomTimer moveDuration = new RandomTimer(); 

    int moveInput = 0;
    bool pause = false;
    bool following = false;
    public bool movementToggle = true;

    void Start()
    {
        calculateStart();
        pauseDuration.create(.1f, 1f);
        moveDuration.create(1f, 4f);
    }

    void FixedUpdate()
    {
        if (movementToggle) //for debugging purposes
        {
            if (detectPlayer() && following == false)
            {
                if (getFacingLeft())
                    moveInput = -1;
                else
                    moveInput = 1;
                following = true;
            }
            else if (!detectPlayer() && following == true)
            {
                moveInput = 0;
                following = false;
            }
            else
                randomMovement();
            setMovement(moveInput);
        }
        calculateUpdate();
    }

    void randomMovement()
    {
        if (moveDuration.checkTimer()&&pause) //move state
        {
            pause = false;
            moveInput = UnityEngine.Random.Range(-1, 2);
            moveDuration.resetTimer();
            pauseDuration.resetTimer();
        }
        else if (pauseDuration.checkTimer()&&!pause) //!move state
        {
            pause = true;
            moveInput = 0;
            moveDuration.resetTimer();
            pauseDuration.resetTimer();
        }
    }

    bool detectPlayer()
    {
        for (int i = 0; i<60; i++)
        {
            float angle =  (getCharacterOrientation()+30 - i +(System.Convert.ToSingle(getFacingLeft()) *180)) % 360;
            Vector2 temp = new Vector2(Mathf.Cos(angle * Mathf.PI / 180), Mathf.Sin(angle * Mathf.PI / 180));
            RaycastHit2D lookForPlayer = Physics2D.Raycast(getCharacterCollider().bounds.center,temp, 100f, LayerMask.GetMask("player"));
            RaycastHit2D lookForObstacles = Physics2D.Raycast(getCharacterCollider().bounds.center, temp, 100f, LayerMask.GetMask("Default"));
            if (lookForPlayer.collider != null&& lookForObstacles.collider == null)
                return true;
        }
        return false;
    }
}   