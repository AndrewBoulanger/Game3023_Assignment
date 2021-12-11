using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class PlayerBehaviour : MonoBehaviour
{
    [SerializeField]
    public Ability[] abilities;

    [SerializeField]
    private float moveSpeed = 1.0f;
    [SerializeField] [Range(0f, 1f)]
    private float decay = 0.01f;

    private Rigidbody2D rigidbody;
    Animator animator;
    bool isMoving = false;

    [SerializeField]
    private AudioClip footStep;
   

    [SerializeField]
    private AudioSource musicSrc;


    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        musicSrc = GetComponent<AudioSource>();
        if (SaveSystem.LoadPlayer() != null)
        {
            PlayerData data = SaveSystem.LoadPlayer();

            Vector3 position;
            position.x = data.position[0];
            position.y = data.position[1];
            position.z = data.position[2];
            transform.position = position;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float inputX = Input.GetAxisRaw("Horizontal");
        float inputY = Input.GetAxisRaw("Vertical");
       
        rigidbody.AddForce(new Vector2(inputX * moveSpeed, inputY * moveSpeed));

        rigidbody.velocity *= (1-decay);

        if (Mathf.Abs(rigidbody.velocity.x) < 0.01 && Mathf.Abs(rigidbody.velocity.y) < 0.01)
        {
           
            isMoving = false;
       
        }
        else{
          
            isMoving = true;
        }
        animator.SetFloat("Vely", inputY);
        animator.SetFloat("Velx", inputX);
        animator.SetBool("IsMoving", isMoving);
        if (isMoving)
        {
            if (!musicSrc.isPlaying)
            {
                musicSrc.Play();
            }
        }
        else
            musicSrc.Stop();

    }
    public void Save()
    {
        SaveSystem.SavePlayer(this);
    }

}

