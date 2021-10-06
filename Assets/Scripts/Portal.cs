using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {

        WorldTraveler worldTravelerObject = collision.GetComponent<WorldTraveler>();
        if(worldTravelerObject != null)
        {
            SceneManager.LoadScene(tag);
        }
    }
}
