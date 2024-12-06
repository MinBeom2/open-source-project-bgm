using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using WCP;
using UnityEngine.AI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public WChatPanel chatPanel; // WChatPanel을 Inspector에서 연결
    public List<string> dialogues; // 대화 내용 리스트
    public List<bool> isplayer;
    private int currentDialogueIndex = 0;
    private int startDialogueIndex=0;
    public GameObject Canvas;
    public GameObject Player;
    public GameObject[] Perdit;
    public GameObject sound;

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

    
    IEnumerator Set_eye()
    {
        yield return new WaitForSeconds(2.0f);
        Doll_eye.gameObject.SetActive(true);    
    }

    IEnumerator giveitem()
    {
        yield return new WaitForSeconds(0.7f);
        Item[0].gameObject.SetActive(true);
        Item[1].gameObject.SetActive(true);
    }
    IEnumerator offitem()
    {
        yield return new WaitForSeconds(0.7f);
        Item[0].gameObject.SetActive(false);
        Item[1].gameObject.SetActive(false);
    }
    IEnumerator givekey()
    {
        yield return new WaitForSeconds(0.7f);
        Item[2].gameObject.SetActive(true);
    }
    IEnumerator offkey()
    {
        yield return new WaitForSeconds(0.7f);
        Item[2].gameObject.SetActive(false);
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
            if (albedo < 1 && Dark) albedo += Time.deltaTime; 
            else if(albedo >= 0 && !Dark) albedo -= Time.deltaTime;
            Canvas.transform.Find("close_eye").GetComponent<Image>().color = new Color(0, 0, 0, albedo);
            if (albedo <= 0 && !Dark) boolval = false;
        }
    }

    private void ShowNextDialogue()
    {
        if (!Canvas.gameObject.activeSelf) { return; }
        int end_count = 23;
        if (Player.GetComponent<Interaction>().hasEye == true)
        {

            startDialogueIndex = 24;
            if (currentDialogueIndex == 0) currentDialogueIndex = startDialogueIndex;
            end_count = 55;
        }
        if(Player.GetComponent<Interaction>().has2heart == true)
        {
            startDialogueIndex = 56;
            if (currentDialogueIndex == 24) currentDialogueIndex = startDialogueIndex;
            end_count = 75;
        }
        if (currentDialogueIndex <= end_count)
        {
            // 다이얼로그 중의 트리거들

            if(currentDialogueIndex == 28) //눈알 끼우기
            {
                Doll.GetComponent<Animator>().SetTrigger("Set_eye");
                StartCoroutine(Set_eye());
            }

            else if(currentDialogueIndex == 38) //아이템 주기
            {
                Doll.GetComponent<Animator>().SetTrigger("give");
                StartCoroutine(giveitem());
            }

            else if (currentDialogueIndex == 39) //팔 내리기
            {
                Doll.GetComponent<Animator>().SetTrigger("idle");
                StartCoroutine(offitem());
            }

            else if (currentDialogueIndex == 44) //화면 어두워짐
            {
                Canvas.transform.Find("close_eye").gameObject.SetActive(true);
                boolval = true;
                Dark = true;
            }

            else if (currentDialogueIndex == 51)
            {
                Doll.gameObject.SetActive(false);
            }

            else if (currentDialogueIndex == 54) //화면 밝아짐
            {
                Dark = false;
                Player.GetComponent<Interaction>().hascardkey = true;
            }
            else if (currentDialogueIndex == 56) //마지막 대화 시작
            {
                Doll.GetComponent<Animator>().SetTrigger("Turn");
            }
            else if(currentDialogueIndex == 57)
            {
                Doll.GetComponent<Animator>().SetTrigger("idle2");
            }
            else if (currentDialogueIndex == 63) //열쇠 전달
            {
                Doll.transform.Find("Skirt_1").Find("Key").gameObject.SetActive(false);
                Doll.GetComponent<Animator>().SetTrigger("give");
                StartCoroutine(givekey());
            }
            else if (currentDialogueIndex == 64)
            {
                Doll.GetComponent<Animator>().SetTrigger("idle");
                Player.GetComponent <Interaction>().haslastkey = true;
                StartCoroutine(offkey());
            }
            else if(currentDialogueIndex == 69) //다시 웅크림
            {
                Doll.GetComponent<Animator>().SetTrigger("Crouching_idle2");
            }
            else if (currentDialogueIndex == 74) //피부 변환
            {
                Doll.GetComponent<ChangeMatarial_doll>().changematerial_doll();
            }
            else if (currentDialogueIndex == 75) //추격전 준비 완료
            {
                Doll.GetComponent<Animator>().SetTrigger("Zombie_walk");
            }
            chatPanel.AddChatAndUpdate(isplayer[currentDialogueIndex], dialogues[currentDialogueIndex], isplayer[currentDialogueIndex] ? 1 : 0);
            currentDialogueIndex++;
            
        }
        else
        {
            if (currentDialogueIndex == 76)
            {
                sound.GetComponent<Sound>().changebgm();
                Doll.GetComponent<AI>().enabled = true;
                Perdit[0].GetComponent<AI>().enabled = true;
                Perdit[1].GetComponent<AI>().enabled = true;
                Perdit[1].GetComponent<NavMeshAgent>().enabled = true;
                Canvas.transform.parent.Find("Image").gameObject.SetActive(true);
                Player.GetComponent<GameOver>().enabled = true;
                
            }
            Canvas.transform.parent.Find("press_E").GetComponent<TextMeshProUGUI>().color = new Color(1, 1, 1, 1);
            Canvas.transform.parent.Find("press_SpaceBar").GetComponent<TextMeshProUGUI>().color = new Color(1, 1, 1, 0);
            currentDialogueIndex = startDialogueIndex;
            Player.GetComponent<Movement>().enabled = true;
            Canvas.gameObject.SetActive(false);
        }
    }
}
