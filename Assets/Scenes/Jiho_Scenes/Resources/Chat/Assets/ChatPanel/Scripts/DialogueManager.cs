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
    private float albedo = 0;

    private void Start()
    {
        // 첫 번째 대화 내용을 추가
        if (dialogues.Count > 0)
        {
            chatPanel.AddChatAndUpdate(true, dialogues[0], 0);
        }
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
            if (albedo < 1) albedo += Time.deltaTime; 
            Canvas.transform.Find("close_eye").GetComponent<Image>().color = new Color(0, 0, 0, albedo); 
        }
    }

    private void ShowNextDialogue()
    {
        int end_count = 27;
        if (Player.GetComponent<Interaction>().hasEye == true)
        {

            startDialogueIndex = 28;
            if (currentDialogueIndex == 0) currentDialogueIndex = startDialogueIndex;
            end_count = 47;
        }
        if (currentDialogueIndex <= end_count)
        {
            // 대화창에 다음 대화 내용을 추가

            chatPanel.AddChatAndUpdate(isplayer[currentDialogueIndex], dialogues[currentDialogueIndex], isplayer[currentDialogueIndex] ? 1 : 0);
            currentDialogueIndex++;
        }
        else
        {
            if (Player.GetComponent<Interaction>().hasEye == true)
            {
                
                Canvas.transform.Find("close_eye").gameObject.SetActive(true);
                boolval = true;                
            }
            currentDialogueIndex = startDialogueIndex;
            Player.GetComponent<Movement>().enabled = true;
            //Canvas.gameObject.SetActive(false);
        }
    }
}
