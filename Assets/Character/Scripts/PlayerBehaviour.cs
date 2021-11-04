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


    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float inputX = Input.GetAxisRaw("Horizontal");
        float inputY = Input.GetAxisRaw("Vertical");
       
        rigidbody.AddForce(new Vector2(inputX * moveSpeed, inputY * moveSpeed));

        rigidbody.velocity *= (1-decay);

        if(Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();
    }


}

