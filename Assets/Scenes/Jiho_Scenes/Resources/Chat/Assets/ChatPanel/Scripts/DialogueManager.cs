using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WCP;

public class DialogueManager : MonoBehaviour
{
    public WChatPanel chatPanel; // WChatPanel�� Inspector���� ����
    public List<string> dialogues; // ��ȭ ���� ����Ʈ
    private int currentDialogueIndex = 0;
    public GameObject Canvas;
    
    private void Start()
    {
        // ù ��° ��ȭ ������ �߰�
        if (dialogues.Count > 0)
        {
            chatPanel.AddChatAndUpdate(true, dialogues[0], 0);
        }
    }

    private void Update()
    {
        // Ư�� Ű �Է����� ���� ��ȭ�� �Ѿ��
        if (Input.GetKeyDown(KeyCode.Space)) // Space Ű�� ������ ��
        {
            ShowNextDialogue();
        }
    }

    private void ShowNextDialogue()
    {
        

        if (currentDialogueIndex < dialogues.Count)
        {
            // ��ȭâ�� ���� ��ȭ ������ �߰�
            bool isPlayerSpeaking = (currentDialogueIndex % 2 == 0); // ��: ¦���� �÷��̾�, Ȧ���� NPC
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
