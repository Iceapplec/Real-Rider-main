using UnityEngine;
using UnityEngine.SceneManagement; // 씬 전환을 위해 필요

public class SceneChanger : MonoBehaviour
{
    // Play 버튼에서 호출할 메서드
    public void ChangeToMainScene()
    {
        SceneManager.LoadScene("Main");
    }

}
