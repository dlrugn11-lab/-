using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.UI;

public class EndingManager : MonoBehaviour
{
    [Header("UI 연결")]
    public Image endingImage;
    public TextMeshProUGUI endingText;

    [Header("설정")]
    public float typingSpeed = 0.08f;
    public float waitBeforeExit = 3.0f;

    [Header("엔딩 문구")]
    [TextArea(3, 10)]
    public string[] endingLines;

    void Start()
    {
        StartCoroutine(PlayEndingSequence());
    }

    IEnumerator PlayEndingSequence()
    {
        yield return new WaitForSeconds(1.0f);
        foreach (string line in endingLines)
        {
            endingText.text = "";
            foreach (char letter in line.ToCharArray())
            {
                endingText.text += letter;
                yield return new WaitForSeconds(typingSpeed);
            }
            yield return new WaitForSeconds(2.0f);
        }
        yield return new WaitForSeconds(waitBeforeExit);

        Debug.Log("게임 종료!");
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}