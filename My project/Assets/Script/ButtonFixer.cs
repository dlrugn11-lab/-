using UnityEngine;
using System.Collections;

public class ButtonFixer : MonoBehaviour
{
    public GameObject stick;
    public GameObject targetButton;

    [Header("속도 설정 (낮을수록 느림)")]
    public float moveInSpeed = 1.5f;  // 날아오는 속도
    public float moveOutSpeed = 1.0f; // 퇴장하는 속도
    public float waitTime = 0.5f;     // 버튼 치고 잠시 멈추는 시간

    [Header("망치 타격 지점 설정")]
    public float offsetX = -1.5f; // 버튼 중심에서 왼쪽으로 얼마나 떨어질지
    public float offsetY = 1.5f;  // 버튼 중심에서 위쪽으로 얼마나 올라갈지

    void Start()
    {
        // 시작 위치를 버튼의 y축과 맞추되, 왼쪽 멀리 배치
        stick.transform.position = new Vector3(-15f, targetButton.transform.position.y + offsetY, stick.transform.position.z);
        Invoke("LaunchStick", 2.0f);
    }

    void LaunchStick()
    {
        StartCoroutine(FixSequence());
    }

    IEnumerator FixSequence()
    {
        // 1. 망치 타격 지점 계산
        float t = 0;
        Vector3 startPos = stick.transform.position;
        Vector3 hitTargetPos = targetButton.transform.position + new Vector3(offsetX, offsetY, 0);

        // 1-1. 망치 들어옴
        while (t < 1.0f)
        {
            t += Time.deltaTime * moveInSpeed;
            stick.transform.position = Vector3.Lerp(startPos, hitTargetPos, t);
            yield return null;
        }

        // 2. 버튼 수습 및 효과음
        targetButton.transform.rotation = Quaternion.identity;

        // AudioSource가 없을 경우를 대비한 체크
        AudioSource audio = GetComponent<AudioSource>();
        if (audio != null) audio.Play();

        Debug.Log("시스템: 버튼 수습 완료!");

        // 3. 잠시 멈춤
        yield return new WaitForSeconds(waitTime);

        // 4. 망치 빠짐
        t = 0;
        Vector3 currentPos = stick.transform.position;
        Vector3 exitPos = new Vector3(20, 10, stick.transform.position.z);

        while (t < 1.0f)
        {
            t += Time.deltaTime * moveOutSpeed;
            stick.transform.position = Vector3.Lerp(currentPos, exitPos, t);
            yield return null;
        }
    }
}