using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class PlayerBehaviour : MonoBehaviour
{

    [SerializeField]
    private float moveSpeed = 1.0f;
    [SerializeField] [Range(0f, 1f)]
    private float decay = 0.01f;

    private Rigidbody2D rigidbody;
    Animator animator;


    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float inputX = Input.GetAxisRaw("Horizontal");
        float inputY = Input.GetAxisRaw("Vertical");
       
        rigidbody.AddForce(new Vector2(inputX * moveSpeed, inputY * moveSpeed));

        rigidbody.velocity *= (1-decay);

        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();

        if(Mathf.Abs(rigidbody.velocity.x) < 0.01 && Mathf.Abs(rigidbody.velocity.y) < 0.01)
        {
            animator.SetBool("IsMoving", false);
        }
        else{
            animator.SetFloat("Vely", inputX);
            animator.SetFloat("Velx", inputY);
            animator.SetBool("IsMoving", true);
        }

    }


}

