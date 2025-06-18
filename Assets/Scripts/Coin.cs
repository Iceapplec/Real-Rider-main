using UnityEngine;

public class Coin : MonoBehaviour
{
    private ParticleSystem particleSystem;
    private AudioSource audioSource;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // 같은 게임 오브젝트에 있는 Particle System, Audio Source 컴포넌트 가져오기
        particleSystem = GetComponent<ParticleSystem>();
        audioSource = GetComponent<AudioSource>();
    }

    // 트리거에 들어오면 파티클 시스템과 오디오 소스 재생
    private void OnTriggerEnter2D(Collider2D other)
    {
        particleSystem.Play();
        audioSource.Play();

        Destroy(gameObject, 0.5f);
    }
}
