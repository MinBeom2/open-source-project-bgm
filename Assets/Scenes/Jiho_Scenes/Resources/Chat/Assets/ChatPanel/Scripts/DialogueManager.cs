using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using WCP;

public class DialogueManager : MonoBehaviour
{
    public WChatPanel chatPanel; // WChatPanel을 Inspector에서 연결
    public List<string> dialogues; // 대화 내용 리스트
    public List<bool> isplayer;
    private int currentDialogueIndex = 0;
    private int startDialogueIndex=0;
    public GameObject Canvas;
    public GameObject Player;

    private bool boolval = false;
    private bool Dark = false;
    private float albedo = 0;

    public GameObject Doll;
    public GameObject Doll_eye;
    public GameObject[] Item;

    private void Start()
    {
        // 첫 번째 대화 내용을 추가
        if (dialogues.Count > 0)
        {
            chatPanel.AddChatAndUpdate(true, dialogues[0], 0);
        }
    }

    void Set_eye()
    {
        Doll_eye.gameObject.SetActive(true);
    }
    void giveitem()
    {
        Item[0].gameObject.SetActive(true);
        Item[1].gameObject.SetActive(true);
    }
    void offitem()
    {
        Item[0].gameObject.SetActive(false);
        Item[1].gameObject.SetActive(false);
    }


    private void Update()
    {
        // 특정 키 입력으로 다음 대화로 넘어가기
        if (Input.GetKeyDown(KeyCode.Space)) // Space 키를 눌렀을 때
        {
            ShowNextDialogue();
        }
        if (boolval == true) 
        {
            Debug.Log("Calculating");
            if (albedo < 1 && Dark) albedo += Time.deltaTime; 
            else if(albedo >= 0 && !Dark) albedo -= Time.deltaTime;
            Canvas.transform.Find("close_eye").GetComponent<Image>().color = new Color(0, 0, 0, albedo);
            if (albedo <= 0 && !Dark) boolval = false;
        }
    }

    private void ShowNextDialogue()
    {
        int end_count = 27;
        if (Player.GetComponent<Interaction>().hasEye == true)
        {

            startDialogueIndex = 28;
            if (currentDialogueIndex == 0) currentDialogueIndex = startDialogueIndex;
            end_count = 59;
        }
        if (currentDialogueIndex <= end_count)
        {
            // 다이얼로그 중의 트리거들

            if(currentDialogueIndex == 32)
            {
                Doll.GetComponent<Animator>().SetTrigger("Set_eye");
                Invoke("Set_eye", 2.0f);
            }

            else if(currentDialogueIndex == 42)
            {
                Doll.GetComponent<Animator>().SetTrigger("give");
                Invoke("giveitem", 0.7f);

            }

            else if (currentDialogueIndex == 43)
            {
                Doll.GetComponent<Animator>().SetTrigger("idle");
                Invoke("offitem", 0.7f);
            }

            else if (currentDialogueIndex == 48)
            {
                Canvas.transform.Find("close_eye").gameObject.SetActive(true);
                boolval = true;
                Dark = true;
            }

            else if (currentDialogueIndex == 55)
            {
                Doll.gameObject.SetActive(false);
            }

            else if (currentDialogueIndex == 58)
            {
                Dark = false;
                Player.GetComponent<Interaction>().hascardkey = true;
            }
            chatPanel.AddChatAndUpdate(isplayer[currentDialogueIndex], dialogues[currentDialogueIndex], isplayer[currentDialogueIndex] ? 1 : 0);
            currentDialogueIndex++;
        }
        else
        {
            currentDialogueIndex = startDialogueIndex;
            Player.GetComponent<Movement>().enabled = true;
            Canvas.gameObject.SetActive(false);
        }
    }
}
