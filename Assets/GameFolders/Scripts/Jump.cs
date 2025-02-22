using Assets.GameFolders.Interfaces;
using DG.Tweening;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class Bounce : MonoBehaviour
{
    [SerializeField] private float jumpForce = 50f;
    private Rigidbody2D rb;
    [SerializeField] float speed = 1f;
    float horizontal;
    Vector2 newPos;

    [SerializeField]   PhysicsMaterial2D PhysicMaterial;
    [SerializeField] bool isCanInput = true;


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        newPos = Vector2.up;
        //   rb.AddForce(Vector2.up * jumpForce * Time.deltaTime, ForceMode2D.Impulse);

    }
    private void Update()
    {
        HorizontalMovement();
  //  Debug.Log(rb.velocity);

    }


    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.collider.CompareTag("platform"))

            rb.AddRelativeForce(newPos * jumpForce, ForceMode2D.Impulse);

        //        this.transform.position += Vector3.Lerp(this.transform.position, newPos, 0.5F);
        // this.transform.Translate(newPos*jumpForce);


        if (collision.collider.CompareTag("floor"))
        {
            Debug.Log("Floor collison");
            // this.transform.position += Vector3.Lerp(this.transform.position, newPos, 0.5F);
            //  rb.AddRelativeForceY(newPos.y * jumpForce * Time.deltaTime);
            rb.AddRelativeForce(newPos * jumpForce, ForceMode2D.Impulse);
        }

        if (collision.collider.CompareTag("SideCollider"))
        {
           
            Vector2 rev = new Vector2(rb.velocity.x*-1500, newPos.y);
            rb.AddRelativeForce(rev *5f, ForceMode2D.Impulse);
        }
        if (collision.collider.CompareTag("SideColliderRight"))
        {

            Vector2 rev = new Vector2(rb.velocity.x, newPos.y);
            rb.AddRelativeForce(rev * 5f, ForceMode2D.Impulse);

        }



    }
    void HorizontalMovement()
    {
        if(isCanInput==true)
         horizontal = Input.GetAxis("Horizontal");
            float targetX = transform.position.x + horizontal * speed;
            Vector3 pos=new Vector3(horizontal, this.rb.velocity.y,0);
            //transform.DOMoveX(targetX, 0.2f).SetEase(Ease.InBounce);
       //     this.transform.position += pos * speed * Time.deltaTime;
       rb.velocity=new Vector2(pos.x*speed, this.rb.velocity.y);  
        
    }


 



}