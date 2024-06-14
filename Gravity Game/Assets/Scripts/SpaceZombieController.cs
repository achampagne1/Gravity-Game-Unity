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

    void Start()
    {
        calculateStart();
        pauseDuration.create(.1f, 1f);
        moveDuration.create(1f, 4f);
    }

    void FixedUpdate()
    {
        randomMovement();
        setMovement(moveInput);
        //setJump(Input.GetKeyDown(KeyCode.Space));
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
}   