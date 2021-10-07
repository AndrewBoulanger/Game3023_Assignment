using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;


public class RandomEncounterBehaviour : MonoBehaviour
{
     [Range(0, 100)]
    public float encounterRate = 0;
    public float frameDelay = 10;
    public float encounterRateIncrement = 0.5f;

   Rigidbody2D player_rb;

    string saveDataPath;

    bool isEnteringBattle = false;

    // Start is called before the first frame update
    void Start()
    {
        player_rb = GetComponent<Rigidbody2D>();
        saveDataPath = Application.dataPath + Path.AltDirectorySeparatorChar + "SaveData" + Path.AltDirectorySeparatorChar + SceneManager.GetActiveScene().name + "position.txt";
        loadPosition();
    }


    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "TallGrass" && player_rb.velocity.sqrMagnitude > 0.1) //moving in tall grass
        {
            CheckForRandomEncounter();

        }
    }
    

    //enter battle scene if random number is below encounter rate. Only checks once every couple of frames
    //if it fails add to encounter rate
    private void CheckForRandomEncounter()
    {
        if(Time.frameCount % frameDelay == 0)
        {
            float randomNum = Random.Range(0, 100);
            if (encounterRate > randomNum)
            {
                savePosition(transform.position);
                SceneManager.LoadScene(tag);

            }
            else
                encounterRate += encounterRateIncrement;
        }
    }

    //save the last know position of the player, called before entering a random encounter
    void savePosition(Vector3 position)
    {
        StreamWriter sw = new StreamWriter(saveDataPath);
        sw.WriteLine(position.x + "," + position.y);

        sw.Close();
        isEnteringBattle = true;
    }

    //load position of player on the current maps last recorded player position
    void loadPosition()
    {
        
        if(File.Exists(saveDataPath))
        {

            StreamReader sr = new StreamReader(saveDataPath);
            string line = sr.ReadLine();
            sr.Close();
            string[] coordinates = line.Split(',');

            transform.position = new Vector2(float.Parse(coordinates[0]), float.Parse(coordinates[1]));
        }

    }

    private void OnDisable()
    {
        //delete save data if leaving scene for a reason other than a random battle
        if(isEnteringBattle == false && File.Exists(saveDataPath))
        {
            File.Delete(saveDataPath);
        }
    }
}
