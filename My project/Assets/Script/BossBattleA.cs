using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BossBattleSimple : MonoBehaviour
{
    [Header("UI 연결")]
    public GameObject dialogueBox;  
    public TextMeshProUGUI dialogueText;
    public Slider bossHPBar; 

    [Header("설정")]
    public string nextSceneName = "EndingScene";
    public float typingSpeed = 0.05f;
    public int bossHP = 400; 

    [Header("마왕 대사")]
    [TextArea(3, 10)]
    public string[] bossLines;

    private bool isTalking = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !isTalking)
        {
            isTalking = true;
            StartCoroutine(StartBossEvent());
        }
    }

    IEnumerator StartBossEvent()
    {
        dialogueBox.SetActive(true);
        if (bossHPBar != null)
        {
            bossHPBar.gameObject.SetActive(true);
            bossHPBar.maxValue = bossHP;
            bossHPBar.value = bossHP;
        }
        foreach (string line in bossLines)
        {
            dialogueText.text = "";
            foreach (char letter in line.ToCharArray())
            {
                dialogueText.text += letter;
                yield return new WaitForSeconds(typingSpeed);
            }
            yield return new WaitForSeconds(1.5f);
        }
        while (bossHP > 0)
        {
            int damage = Random.Range(50, 80); 
            bossHP -= damage;

            if (bossHPBar != null) bossHPBar.value = bossHP;
            transform.position += new Vector3(0.1f, 0.1f, 0);
            yield return new WaitForSeconds(0.05f);
            transform.position -= new Vector3(0.1f, 0.1f, 0);

            yield return new WaitForSeconds(0.6f);
        }
        SceneManager.LoadScene(nextSceneName);
    }
}