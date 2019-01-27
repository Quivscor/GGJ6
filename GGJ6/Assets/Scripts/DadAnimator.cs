using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DadAnimator : MonoBehaviour
{
    NPC me;
    MovementController player;
    Animator animator;

    int direction = -1;

    private void Start()
    {
        animator = GetComponent<Animator>();
        me = GetComponentInParent<NPC>();
    }

    public void PickedUp()
    {
        animator.SetBool("isCarried", true);
        player = GetComponentInParent<MovementController>();
        if (player.directionVector2.y > 0.5f && player.directionVector2.x > -0.5f && player.directionVector2.x < 0.5f)
            direction = 0;
        else if (player.directionVector2.y < 0.5f && player.directionVector2.y > -0.5f && player.directionVector2.x < -0.5f)
            direction = 1;
        else if (player.directionVector2.y < 0.5f && player.directionVector2.y > -0.5f && player.directionVector2.x > 0.5f)
            direction = 2;
        else if (player.directionVector2.y < -0.5f && player.directionVector2.x > -0.5f && player.directionVector2.x < 0.5f)
            direction = 3;
    }

    private void LateUpdate()
    {
        if(direction >= 0)
        {
            if (player.directionVector2.y > 0.5f && player.directionVector2.x > -0.5f && player.directionVector2.x < 0.5f)
                direction = 0;
            else if (player.directionVector2.y < 0.5f && player.directionVector2.y > -0.5f && player.directionVector2.x < -0.5f)
                direction = 1;
            else if (player.directionVector2.y < 0.5f && player.directionVector2.y > -0.5f && player.directionVector2.x > 0.5f)
                direction = 2;
            else if (player.directionVector2.y < -0.5f && player.directionVector2.x > -0.5f && player.directionVector2.x < 0.5f)
                direction = 3;
            animator.SetInteger("direction", direction);
        }
    }

    public void Thrown(Vector2 direction)
    {
        if (direction.x > 0)
            animator.SetInteger("flyDir", 1);
        else
            animator.SetInteger("flyDir", -1);
    }

}
