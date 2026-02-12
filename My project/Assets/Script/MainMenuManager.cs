using UnityEngine;
using UnityEngine.SceneManagement; // 씬 이동 필수 라이브러리

public class MainMenuManager : MonoBehaviour
{
    // 게임 시작 버튼용
    public void GoToVillage()
    {
        SceneManager.LoadScene("Village");
    }
    // 게임 종료 버튼용
    public void AppQuit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;

#else
            Application.Quit();

#endif
    }
}