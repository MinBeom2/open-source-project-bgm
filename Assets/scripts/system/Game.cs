using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
    public GameObject player;

    private void Start()
    {
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

        SceneManager.LoadScene("SAVE_SCENE");
    }



    public void SceneToNext()
    {
        DataManager.instance.nowPos.positionX = 1.1f;
        DataManager.instance.nowPos.positionY = 0;
        DataManager.instance.nowPos.positionZ = 11.13f;
        DataManager.instance.nowPos.rotationY = 0;

        SceneManager.LoadScene("AISLE2");
    }
}

