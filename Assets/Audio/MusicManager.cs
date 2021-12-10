using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    private MusicManager() {}

    private static MusicManager instance = null;

    public static MusicManager Instance
    {
        get
        {
            if(instance == null)
            {
                instance = FindObjectOfType<MusicManager>();
                DontDestroyOnLoad(instance);
            }
            return instance;
        }
        private set {instance = value; }
    }


    public enum TrackID
    {
        Overworld = 0,
        Battle = 1
    }

    
    [SerializeField]
    List<AudioClip> musicTracks;
    [SerializeField]
    AudioSource musicSource;

    TrackID currentTrackId;

    public void PlayTrack(TrackID id, float fadeInDuration = 0.0f )
    {
        musicSource.clip = musicTracks[(int)id];
        musicSource.Play();
        currentTrackId = id;

        if(fadeInDuration > 0.0f)
            StartCoroutine(FadeInMusic(fadeInDuration));
    }



    IEnumerator FadeInMusic(float fadeInDuration)
    {
        float timer = 0f;
        while(timer < fadeInDuration)
        {
            timer += Time.deltaTime;
            float fadeValue = timer/ fadeInDuration;
            musicSource.volume = Mathf.SmoothStep(0.0f, 1.0f, fadeValue);

            yield return new WaitForEndOfFrame();
        }
        
    }

}
