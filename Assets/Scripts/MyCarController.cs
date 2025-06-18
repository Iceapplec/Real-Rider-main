using UnityEngine;

public class MyCarController : MonoBehaviour
{
    private SurfaceEffector2D surfaceEffector2D;
    private Rigidbody2D rb;
    private bool onGround = false;

    public float jumpForce = 7f;
    public float defaultSpeed = 7f;      // 기본 속도
    public float maxSpeed = 15f;         // 최대 속도
    public float minSpeed = 2f;          // 최소 속도
    public float speedStep = 5f;         // 속도 변화량(가속/감속)

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<SurfaceEffector2D>(out var effector))
        {
            onGround = true;
            surfaceEffector2D = effector;

            // 충돌 시 기본 속도로 설정
            surfaceEffector2D.speed = defaultSpeed;
        }
    }

    private void Update()
    {
        if (surfaceEffector2D == null) return;

        // 오른쪽 화살표를 누르고 있으면 속도 증가
        if (Input.GetKey(KeyCode.RightArrow))
        {
            surfaceEffector2D.speed += speedStep * Time.deltaTime;
            surfaceEffector2D.speed = Mathf.Clamp(surfaceEffector2D.speed, minSpeed, maxSpeed);
        }
        // 왼쪽 화살표를 누르고 있으면 속도 감소
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            surfaceEffector2D.speed -= speedStep * Time.deltaTime;
            surfaceEffector2D.speed = Mathf.Clamp(surfaceEffector2D.speed, minSpeed, maxSpeed);
        }
        // 아무 키도 누르지 않으면 기본 속도로 유지
        else
        {
            surfaceEffector2D.speed = Mathf.MoveTowards(surfaceEffector2D.speed, defaultSpeed, speedStep * Time.deltaTime);
        }

        // 점프
        if (Input.GetKeyDown(KeyCode.Space) && onGround)
        {   
            Jump();
        }
    }

    private void Jump()
    {
        onGround = false;

        if (rb == null) return;

        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }
}
