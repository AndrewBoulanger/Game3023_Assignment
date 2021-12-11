using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class PlayerData
{

    public float[] position;
    public string sceneName;
    Scene m_Scene;
    public PlayerData (PlayerBehaviour player)
    {
        m_Scene = SceneManager.GetActiveScene();
        sceneName = m_Scene.name;
        position = new float[3];
        position[0] = player.transform.position.x;
        position[1] = player.transform.position.y;
        position[2] = player.transform.position.z;

    }
}
