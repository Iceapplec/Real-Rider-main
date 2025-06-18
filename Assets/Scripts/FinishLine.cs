using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishLine : MonoBehaviour
{
    [SerializeField] private float reloadDelay = 2f; // 2초 후에 씬을 리로드합니다.
    [SerializeField] private ParticleSystem finishEffect; // 도착 효과를 위한 파티클 시스템

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            finishEffect.Play(); // 도착 효과 재생
            Invoke(nameof(ReloadScene), reloadDelay); // 2초 후에 ReloadScene 메서드 호출
        }        
    }
    void ReloadScene()
    {
        SceneManager.LoadScene(0);
    }
}
