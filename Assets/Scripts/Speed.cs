using UnityEngine;

public class Speed : MonoBehaviour
{
    public float boostAmount = 30f;
    public float boostDuration = 3f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController pc = other.GetComponent<PlayerController>();
            if (pc != null)
            {
                pc.BoostSpeed(boostAmount, boostDuration);
            }
            Destroy(gameObject);
        }
    }
}
