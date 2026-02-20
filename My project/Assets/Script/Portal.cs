using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    public Transform player;
    public float radius = 1.5f;

    void Update()
    {
        if (player == null) return;
        float distance = Vector2.Distance(transform.position, player.position);

        if (distance < radius)
        {
            Debug.Log("거리 감지로 씬 이동 시도!");
            SceneManager.LoadScene("BattleScene");
        }
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}