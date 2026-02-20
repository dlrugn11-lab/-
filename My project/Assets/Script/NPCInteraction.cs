using UnityEngine;
using TMPro;

public class NPCInteraction : MonoBehaviour
{
    [Header("UI 연결")]
    public TextMeshProUGUI dialogueText;
    public GameObject dialogueBox;

    [Header("대사 설정")]
    [TextArea(3, 10)]
    public string[] sentences;

    private int currentIndex = 0;

    void Start()
    {
        if (dialogueBox != null)
            dialogueBox.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            dialogueBox.SetActive(true);
            Talk();
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (dialogueBox != null)
                dialogueBox.SetActive(false);
            currentIndex = 0;
        }
    }
    void Talk()
    {
        if (sentences.Length > 0 && dialogueText != null)
        {
            dialogueText.text = sentences[currentIndex];
            currentIndex++;
            if (currentIndex >= sentences.Length)
            {
                currentIndex = 0;
            }
        }
    }
}