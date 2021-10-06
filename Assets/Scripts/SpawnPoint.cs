using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    [SerializeField]
    public GameObject playerPrefab;

    static PlayerBehaviour playerReference = null;

    // Start is called before the first frame update
    void Start()
    {
        if(playerReference == null)
        {
            GameObject newPlayerObject = Instantiate(playerPrefab, transform.position, transform.rotation);
            playerReference = newPlayerObject.GetComponent<PlayerBehaviour>();
        }
    }


}
