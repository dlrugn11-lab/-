using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;

public class FinalBattle : MonoBehaviour
{
    [Header("전투 설정")]
    public float detectRadius = 2.5f;   // 플레이어 감지 거리
    public int playerHP = 100;
    public int enemyHP = 50;
    // 다음 목적지를 'FactoryScene'으로 기본 설정했습니다.
    public string nextSceneName = "FactoryScene";

    [Header("UI 연결")]
    public GameObject battlePanel;      // 전투 UI 패널
    public TextMeshProUGUI statusText;  // 전투 로그 텍스트
    public Slider playerHPBar;          // 플레이어 체력바

    private Transform player;
    private bool isBattleStarted = false;
    private bool isPlayerTurn = true;

    void Start()
    {
        // 씬에서 "Player" 태그를 가진 오브젝트 찾기
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null) player = playerObj.transform;

        // 시작할 때 UI는 숨김
        if (battlePanel != null) battlePanel.SetActive(false);

        // 체력바 초기화
        if (playerHPBar != null)
        {
            playerHPBar.maxValue = playerHP;
            playerHPBar.value = playerHP;
        }
    }

    void Update()
    {
        if (isBattleStarted || player == null) return;

        // 플레이어와의 거리 체크
        float distance = Vector2.Distance(transform.position, player.position);
        if (distance < detectRadius)
        {
            StartBattle();
        }
    }

    void StartBattle()
    {
        isBattleStarted = true;
        if (battlePanel != null) battlePanel.SetActive(true);
        StartCoroutine(BattleLoop());
    }

    IEnumerator BattleLoop()
    {
        statusText.text = "마왕성으로 가는 길을 몬스터가 막아섰다!";
        yield return new WaitForSeconds(1.5f);

        while (enemyHP > 0)
        {
            if (isPlayerTurn)
            {
                int damage = Random.Range(15, 25);
                enemyHP -= damage;
                statusText.text = $"나의 공격! 몬스터에게 {damage} 데미지!";
                StartCoroutine(FlashRed());
            }
            else
            {
                int damage = Random.Range(5, 12);
                if (playerHP - damage <= 0) playerHP = 1;
                else playerHP -= damage;

                if (playerHPBar != null) playerHPBar.value = playerHP;
                statusText.text = $"몬스터의 공격! {damage} 피해를 입었다!";
            }

            isPlayerTurn = !isPlayerTurn;
            yield return new WaitForSeconds(1.2f);

            if (enemyHP <= 0) break;
        }
        statusText.text = "몬스터를 물리쳤다! 다시 마왕성을 향해 간다.";
        yield return new WaitForSeconds(2.0f);
        SceneManager.LoadScene(nextSceneName);
    }
    IEnumerator FlashRed()
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if (sr != null)
        {
            sr.color = Color.red;
            yield return new WaitForSeconds(0.1f);
            sr.color = Color.white;
        }
    }
}