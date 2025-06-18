using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishLine : MonoBehaviour
{
    [SerializeField] private float reloadDelay = 2f; // 2�� �Ŀ� ���� ���ε��մϴ�.
    [SerializeField] private ParticleSystem finishEffect; // ���� ȿ���� ���� ��ƼŬ �ý���

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            finishEffect.Play(); // ���� ȿ�� ���
            Invoke(nameof(ReloadScene), reloadDelay); // 2�� �Ŀ� ReloadScene �޼��� ȣ��
        }        
    }
    void ReloadScene()
    {
        SceneManager.LoadScene(0);
    }
}
