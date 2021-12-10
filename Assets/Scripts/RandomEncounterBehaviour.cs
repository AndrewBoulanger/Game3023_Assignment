using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;


public class RandomEncounterBehaviour : MonoBehaviour
{
     [Range(0, 100)]
    public float encounterRate = 0;
    public float timeDelayInSeconds = 0.5f;
    public float encounterRateIncrement = 0.1f;
    Timer checkEncounterTimer;

    //determines the minimum number of random encounter checks to be made before a random encounter can occur
    float guaranteedSafeChecks = 10;

   Rigidbody2D player_rb;

    string saveDataPath;

    bool isEnteringBattle = false;


    // Start is called before the first frame update
    void Start()
    {

        checkEncounterTimer = new Timer();
        player_rb = GetComponent<Rigidbody2D>();
        saveDataPath = Application.dataPath + Path.DirectorySeparatorChar + SceneManager.GetActiveScene().name + "position.txt";
        loadPosition();

    }


    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "RandomEncounterTile" && player_rb.velocity.sqrMagnitude > 0.1) //moving on a random encounter tile
        {
            CheckForRandomEncounter();

        }
    }
    

    //enter battle scene if random number is below encounter rate. Only checks once every couple of frames
    //if it fails add to encounter rate
    private void CheckForRandomEncounter()
    {
        if(checkEncounterTimer.CheckTimer(timeDelayInSeconds))
        {
            //the random number will never be below (checkFrequency*guaranteed safe checks) (the first 10 checks are always safe)
            int minRange = (int)(timeDelayInSeconds * guaranteedSafeChecks);
            float randomNum = Random.Range(minRange, 100);

            if (encounterRate > randomNum)
            { 
                //time for a random encounter
                savePosition(transform.position);
                isEnteringBattle = true;
                StartCoroutine("BattleEntrySequence");
            }
            else //no encounter this time, raise chances of one happening
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


    IEnumerator BattleEntrySequence()
    {
        player_rb.constraints = RigidbodyConstraints2D.FreezeAll;
        MusicManager.Instance.PlayTrack(MusicManager.TrackID.Battle, 1.5f);
        yield return new WaitForSeconds(1.5f);
        player_rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        SceneManager.LoadScene(tag);
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
