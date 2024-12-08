using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    [SerializeField] private GameObject gameoverPanel;
    [SerializeField] private GameObject deadPanel;
    public GameObject canvas;
    AudioManager audioManager;

    bool flag = false;
    private float albedo = 0;


    private void OnCollisionEnter(Collision collision)
    {
        if (GetComponent<GameOver>().enabled)
        {
            if (collision.gameObject.CompareTag("Doll"))
            {
                deadPanel.SetActive(true);
                flag = true;
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                transform.GetComponent<playgroundMovement>().enabled = false;
                audioManager.PlaySFX(audioManager.gameover);
                gameoverPanel.SetActive(true);
            }
        }

    }

    public void Start()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();

    }

    public void Update()
    {
        if (flag == true)
        {
            if (albedo < 1) albedo += Time.deltaTime;
            canvas.transform.Find("DeadScene").GetComponent<Image>().color = new Color(0, 0, 0, albedo);
        }
    }
}
