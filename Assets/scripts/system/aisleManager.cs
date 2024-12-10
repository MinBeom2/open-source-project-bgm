using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class aisleManager : MonoBehaviour
{
    public GameObject player;
    AudioManager audioManager;

    private void Start()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        audioManager.musicSource.clip = audioManager.DefaultBackground;
        audioManager.musicSource.Play();

        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }
    }

    public void SceneToSave()
    {
        DataManager.instance.previousScene = SceneManager.GetActiveScene().name;

        if (player != null)
        {
            Vector3 currentPosition = player.transform.position;
            DataManager.instance.nowPos.positionX = currentPosition.x;
            DataManager.instance.nowPos.positionY = currentPosition.y;
            DataManager.instance.nowPos.positionZ = currentPosition.z;

            float currentRotationY = player.transform.eulerAngles.y;
            DataManager.instance.nowPos.rotationY = currentRotationY;

            Debug.Log($"플레이어 위치 {currentPosition} 회전 Y: {currentRotationY}");
        }
    }



    public void SceneToNext()
    {
        string nowAisle = SceneManager.GetActiveScene().name;
        if (nowAisle == "AISLE1")
        {
            SceneManager.LoadScene("Backrooms_Scene");
        }
        else if (nowAisle == "AISLE2")
        {
            SceneManager.LoadScene("MyScene");
        }
        else if (nowAisle == "AISLE3")
        {
            SceneManager.LoadScene("PSW_MainMap");
        }
        else
        {
            SceneManager.LoadScene("GAMEOVER");
        }
    }
}

