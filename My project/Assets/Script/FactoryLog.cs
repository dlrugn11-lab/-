using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.SceneManagement;
public class FactoryLog : MonoBehaviour
{
    [Header("UI 연결")]
    public TextMeshProUGUI logText;
    public GameObject scanLine;

    [Header("설정")]
    public float typingSpeed = 0.05f;
    public float lineDelay = 1.5f;
    public string nextSceneName = "BossBattleScene";

    [Header("로그 내용")]
    [TextArea(3, 10)]
    public string[] logs;

    void Start()
    {
        if (logText == null)
        {
            Debug.LogError("인스펙터에서 Log Text를 연결해주세요!");
            return;
        }

        StartCoroutine(PlayFactorySequence());
    }

    IEnumerator PlayFactorySequence()
    {
        yield return new WaitForSeconds(1.0f);

        foreach (string line in logs)
        {
            logText.text = "";
            foreach (char letter in line.ToCharArray())
            {
                logText.text += letter;
                yield return new WaitForSeconds(typingSpeed);
            }

            yield return new WaitForSeconds(lineDelay);
        }

        yield return new WaitForSeconds(1.0f);
        SceneManager.LoadScene(nextSceneName);
    }
}