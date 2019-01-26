using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    Animator animator;
    MovementController controller;
    ThrowScript throwing;

    int direction = -1; // 0 is up; 1 is left; 2 is right; 3 is down;

    private void Start()
    {
        animator = GetComponent<Animator>();
        controller = gameObject.GetComponentInParent<MovementController>();
        throwing = gameObject.GetComponentInParent<ThrowScript>();
    }

    private void LateUpdate()
    {
        if (controller.directionVector2.y > 0.5f && controller.directionVector2.x > -0.5f && controller.directionVector2.x < 0.5f)
            direction = 0;
        else if (controller.directionVector2.y < 0.5f && controller.directionVector2.y > -0.5f && controller.directionVector2.x < -0.5f)
            direction = 1;
        else if (controller.directionVector2.y < 0.5f && controller.directionVector2.y > -0.5f && controller.directionVector2.x > 0.5f)
            direction = 2;
        else if (controller.directionVector2.y < -0.5f && controller.directionVector2.x > -0.5f && controller.directionVector2.x < 0.5f)
            direction = 3;

        //Debug.Log("Direction = " + direction);
        animator.SetInteger("direction", direction);
        animator.SetBool("isMoving", controller.isMoving);

        if (throwing.holdsSth)
        {
            if (controller.isMoving)
            {
                //walk with holding
            }
            else
            {
                //stand with holding
            }
        }
        else
        {
            if (controller.isMoving)
            {
                //walk without holding
            }
            else
            {
                //stand without holding
            }
        }
    }

    public void CarryState()
    {
        animator.SetBool("pickup", true);
        animator.Play("PickUp");
    }
}
