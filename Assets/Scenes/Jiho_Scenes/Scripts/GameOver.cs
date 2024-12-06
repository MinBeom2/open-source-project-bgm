using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    public GameObject canvas;
    public GameObject soundmanager;
    bool flag = false; 
    private float albedo = 0;


    private void OnCollisionEnter(Collision collision)
    {
        if (GetComponent<GameOver>().enabled)
        {
            if (collision.gameObject.CompareTag("Doll"))
            {
                flag = true;
                transform.GetComponent<Movement>().enabled = false;
                soundmanager.GetComponent<Sound>().gameover();
            }
        }
    }

    public void Start() { }

    public void Update()
    {
        if (flag == true)
        {
            if (albedo < 1) albedo += Time.deltaTime;
            canvas.transform.Find("DeadScene").GetComponent<Image>().color = new Color(0, 0, 0, albedo);
        }
    }
}
