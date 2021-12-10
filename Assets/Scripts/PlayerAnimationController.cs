using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WalkDirection
{
    Down,
    Left,
    Up,
    Right
}

public class PlayerAnimationController : MonoBehaviour
{
    Rigidbody2D rigidbody;
    Animator animator;
    bool isWalking = false;
    WalkDirection walkDirection;

    const float minimumWalkVelocity = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 velocity = rigidbody.velocity;
        isWalking = velocity.SqrMagnitude() > minimumWalkVelocity * minimumWalkVelocity;

        //walk direction favours horizontal movement. just thought it looked better than swapping between them
        bool isMovingHorizontally = Mathf.Abs(velocity.x) > minimumWalkVelocity;
        if(isMovingHorizontally)
            walkDirection = (velocity.x > 0) ? WalkDirection.Right : WalkDirection.Left;
        else
            walkDirection = (velocity.y > 0) ? WalkDirection.Up : WalkDirection.Down;

        animator.SetBool("isWalking", isWalking);
        animator.SetInteger("walkDirection", (int)walkDirection);

    }
}
