using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    private float elapsedTime = 0f;
    private float fatestTime = float.MaxValue;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        elapsedTime += Time.deltaTime;

        UIManager.Instance.UpdateTimeText(FormatElapsedTime(elapsedTime));
    }

    public void GameStop()
    {
        //게임중지
        Time.timeScale = 0f;

        //시간저장

        if (elapsedTime < fatestTime)
        {
            fatestTime = elapsedTime;
            Debug.Log("New fastest time: " + FormatElapsedTime(fatestTime));
        }

        UIManager.Instance.UpdateCurrentTimeText("Current Time : " + FormatElapsedTime(elapsedTime));
        UIManager.Instance.UpdateFastTimeText("Fastest Time : " + FormatElapsedTime(fatestTime));

        //패널 활성화
        UIManager.Instance.ShowPanel();
    }


    public void GameRestart() 
    {
        elapsedTime = 0f;

        UIManager.Instance.HidePanel();

        Time.timeScale = 1f; // 게임 재시작

        UnityEngine.SceneManagement.SceneManager.LoadScene
            (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name); // 현재 씬 재시작
    } 
    private string FormatElapsedTime(float time)
    {
        int minutes = (int)(time / 60f);
        int seconds = (int)(time % 60f);
        int milliseconds = (int)((time * 1000) % 1000);
        return string.Format("{0:00}:{1:00}:{2:000}", minutes, seconds, milliseconds);
    }
}
