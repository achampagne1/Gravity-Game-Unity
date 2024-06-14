using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceManController : CharacterController
{
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
}