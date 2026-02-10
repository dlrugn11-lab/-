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
    void Start()
        {
            // 시작하자마자 막대기를 화면 왼쪽 멀리(-20 위치) 보이지 않게 배치
            stick.transform.position = new Vector3(-15f, targetButton.transform.position.y, stick.transform.position.z);

            Invoke("LaunchStick", 2.0f);
        }
    void LaunchStick()
    {
        StartCoroutine(FixSequence());
    }
    IEnumerator FixSequence()
    {
        // 1. 막대기 입장
        float t = 0;
        Vector3 startPos = stick.transform.position;
        Vector3 targetPos = targetButton.transform.position;

        while (t < 1.0f)
        {
            t += Time.deltaTime * moveInSpeed;
            stick.transform.position = Vector3.Lerp(startPos, targetPos, t);
            yield return null;
        }
        // 2. 버튼 수습
        targetButton.transform.rotation = Quaternion.identity;
        Debug.Log("시스템: 버튼 수습 완료!");
        // 3. 잠시 멈춤
        yield return new WaitForSeconds(waitTime);
        // 4. 막대기 퇴장
        t = 0;
        Vector3 exitPos = new Vector3(20, 10, stick.transform.position.z);
        while (t < 1.0f)
        {
            t += Time.deltaTime * moveOutSpeed;
            stick.transform.position = Vector3.Lerp(targetPos, exitPos, t);
            yield return null;
        }
    }
}