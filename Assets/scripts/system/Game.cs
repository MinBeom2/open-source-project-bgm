using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Game : MonoBehaviour
{
    public GameObject player; // 플레이어 GameObject

    private void Start()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }
    }

    public void SceneToSave()
    {
        // 현재 씬 이름 저장
        DataManager.instance.previousScene = SceneManager.GetActiveScene().name;

        if (player != null)
        {
            // 플레이어 위치 및 회전 정보를 PlayerPos에 저장
            Vector3 currentPosition = player.transform.position;
            DataManager.instance.nowPos.positionX = currentPosition.x;
            DataManager.instance.nowPos.positionY = currentPosition.y;
            DataManager.instance.nowPos.positionZ = currentPosition.z;

            float currentRotationY = player.transform.eulerAngles.y;
            DataManager.instance.nowPos.rotationY = currentRotationY;

            Debug.Log($"플레이어 위치 저장: {currentPosition}, 회전 Y: {currentRotationY}");
        }
        else
        {
            Debug.LogWarning("플레이어 오브젝트가 없습니다!");
        }

        // SAVE_SCENE으로 이동
        SceneManager.LoadScene("SAVE_SCENE");
    }

    public void SceneToNext()
    {
        SceneManager.LoadScene("AISLE2");
    }
}

