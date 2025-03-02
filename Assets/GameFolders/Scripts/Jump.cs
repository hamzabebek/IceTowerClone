using DG.Tweening;
using UnityEngine;
using System.Collections;

public class Bounce : MonoBehaviour
{
    [SerializeField] public float jumpForce = 50f;
    [SerializeField] public float speed = 1f;
    [SerializeField] private bool isCanInput = true;
    [SerializeField] private GameObject deathScreen;

    private Rigidbody2D rb;
    private float horizontal;
    [SerializeField] private float bounceValue;
    public int value = 0;


    private void Start()
    {
        Time.timeScale = 1;
        deathScreen.SetActive(false);
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        HorizontalMovement();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Vector2 normal = collision.contacts[0].normal;
        if (normal.y > 0.5f)
        {
            rb.velocity = new Vector2(rb.velocity.x, 0);
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }

        if (collision.collider.CompareTag("SideWallLeft"))
        {
            SideBounce(1.5f);
        }

        if (collision.collider.CompareTag("SideWallRight"))
        {
            SideBounce(-1.5f);  
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "GameOverFloor")
        {
            GameOver();
        }
    }

    private void GameOver()
    {
        Time.timeScale = 0;
        deathScreen.SetActive(true);
    }
    private void SideBounce(float bounce)
    {
        Vector2 bounceDirection = new Vector2(bounce, 2f).normalized;
        isCanInput = false;
        rb.velocity = Vector2.zero;
        rb.AddForce(bounceDirection * bounceValue, ForceMode2D.Impulse);
        StartCoroutine(BounceEffect());
    }

    private void HorizontalMovement()
    {
        if (!isCanInput) return;
        horizontal = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y); 
    }

    private IEnumerator BounceEffect()
    {
        yield return new WaitForSeconds(0.75f);
        isCanInput = true; 
    }
}