using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using WCP;

public class DialogueManager : MonoBehaviour
{
    public WChatPanel chatPanel; // WChatPanel�� Inspector���� ����
    public List<string> dialogues; // ��ȭ ���� ����Ʈ
    public List<bool> isplayer;
    private int currentDialogueIndex = 0;
    private int startDialogueIndex=0;
    public GameObject Canvas;
    public GameObject Player;
    private bool boolval = false;
    private float albedo = 0;

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
            // ��ȭâ�� ���� ��ȭ ������ �߰�

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
