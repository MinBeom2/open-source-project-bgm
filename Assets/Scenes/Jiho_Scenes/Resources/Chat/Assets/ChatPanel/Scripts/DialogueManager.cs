using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WCP;

public class DialogueManager : MonoBehaviour
{
    public WChatPanel chatPanel; // WChatPanel을 Inspector에서 연결
    public List<string> dialogues; // 대화 내용 리스트
    private int currentDialogueIndex = 0;
    public GameObject Canvas;
    
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
    }

    private void ShowNextDialogue()
    {
        

        if (currentDialogueIndex < dialogues.Count)
        {
            // 대화창에 다음 대화 내용을 추가
            bool isPlayerSpeaking = (currentDialogueIndex % 2 == 0); // 예: 짝수는 플레이어, 홀수는 NPC
            chatPanel.AddChatAndUpdate(isPlayerSpeaking, dialogues[currentDialogueIndex], isPlayerSpeaking ? 1 : 0);
            currentDialogueIndex++;
        }
        else
        {
            currentDialogueIndex = 0;
            Canvas.gameObject.SetActive(false);
        }
    }
}
