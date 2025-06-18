using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 8f;            // 최대 속도
    public float acceleration = 2f;         // 가속도
    public float rotationTorque = 5f;
    public Rigidbody2D rb;
    public float slowSpeed = 0.5f;          // 왼쪽 화살표 시 느려지는 속도
    public float jumpForce = 7f;            // 점프 힘

    private bool isGrounded = false;
    private bool panelShown = false;

    public AudioClip destroySound;           // Inspector에서 할당
    private AudioSource audioSource;

    private Coroutine speedBoostCoroutine;
    private float originalMoveSpeed;

    private float prevSpeed = 0f;
    private float accelerationValue = 0f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        // 스페이스바를 누르면 점프 (바닥에 있을 때만)
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }

    void FixedUpdate()
    {
        // 속도 계산
        float currentSpeed = rb.velocity.x;
        // 가속도 계산
        accelerationValue = (currentSpeed - prevSpeed) / Time.fixedDeltaTime;
        prevSpeed = currentSpeed;

        // UI에 표시
        if (UIManager.Instance != null)
        {
            UIManager.Instance.UpdateCarSpeedText($"속도: {currentSpeed:F2} m/s");
            UIManager.Instance.UpdateSurfaceText($"가속도: {accelerationValue:F2} m/s²");
        }

        // 오른쪽 화살표 또는 마우스 클릭 시 가속
        if ((Input.GetMouseButton(0) || Input.GetKey(KeyCode.RightArrow)) && isGrounded)
        {
            float targetSpeed = moveSpeed;
            float speedDiff = targetSpeed - rb.velocity.x;
            float force = speedDiff * acceleration;
            rb.AddForce(new Vector2(force, 0f), ForceMode2D.Force);

            if (!isGrounded)
            {
                rb.AddTorque(rotationTorque, ForceMode2D.Force);
            }
        }
        // 왼쪽 화살표 시 속도 즉시 감소
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            Vector2 v = rb.velocity;
            v.x = slowSpeed;
            rb.velocity = v;
        }
        // 클릭/키 입력 없을 때는 물리엔진에 맡김
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Ground 태그와 닿았을 때만 isGrounded = true
        if (collision.collider.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        // Ground 태그에서 떨어지면 isGrounded = false
        if (collision.collider.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }

    public void BoostSpeed(float boostAmount, float duration)
    {
        if (speedBoostCoroutine != null)
            StopCoroutine(speedBoostCoroutine);
        speedBoostCoroutine = StartCoroutine(SpeedBoostRoutine(boostAmount, duration));
    }

    private IEnumerator SpeedBoostRoutine(float boostAmount, float duration)
    {
        originalMoveSpeed = moveSpeed;
        moveSpeed += boostAmount;
        yield return new WaitForSeconds(duration);
        moveSpeed = originalMoveSpeed;
        speedBoostCoroutine = null;
    }
}
