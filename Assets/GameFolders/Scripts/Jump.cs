using DG.Tweening;
using UnityEngine;
using System.Collections;

public class Bounce : MonoBehaviour
{
    [SerializeField] private float jumpForce = 50f;
    [SerializeField] private float speed = 1f;
    [SerializeField] private bool isCanInput = true;

    private Rigidbody2D rb;
    private float horizontal;
    [SerializeField] private float bounceValue;


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        HorizontalMovement();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("platform") || collision.collider.CompareTag("floor"))
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }

        if (collision.collider.CompareTag("SideCollider"))
        {
             Vector2 bounceDirection = new Vector2(1.5f, 2f).normalized;
            isCanInput = false; 
        rb.velocity = Vector2.zero;
        rb.AddForce(bounceDirection * bounceValue, ForceMode2D.Impulse);
        StartCoroutine(BounceEffect());

        }
     

        if (collision.collider.CompareTag("SideColliderRight"))
        {
             Vector2 bounceDirection = new Vector2(-1.5f, 2f).normalized;
            isCanInput = false; 
        rb.velocity = Vector2.zero;
        rb.AddForce(bounceDirection * bounceValue, ForceMode2D.Impulse);
        StartCoroutine(BounceEffect());
           
        }
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
