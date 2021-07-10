using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomScript : MonoBehaviour
{
    float horizontal;
    float vertical;
    public float swimSpeed;

    public float dashSpeed;
    public float dashDuration;
    public float dashCooldown;

    Rigidbody2D rb;
    IEnumerator dashCoroutine;
    bool isDashing;
    bool isLooking = true;
    bool canDash = true;
    
    public Camera cam;
    Vector2 mousePos;

    private bool facingRight = true;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isLooking)
        {
            mousePos = cam.ScreenToWorldPoint(Input.mousePosition);

            Vector2 lookDirection = mousePos - rb.position;
            float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;
            rb.rotation = angle;
        }
        
        horizontal = Input.GetAxisRaw("Horizontal");
        //vertical = Input.GetAxis("Jump");
        vertical = Input.GetAxisRaw("Vertical");

        if (Input.GetMouseButton(0) && canDash == true)
        {
            if (dashCoroutine != null)
            {
                StopCoroutine(dashCoroutine);
            }
            dashCoroutine = Dash();
            StartCoroutine(dashCoroutine);
        }

        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
    }

    private void FixedUpdate()
    {
        rb.AddForce(new Vector2(horizontal * swimSpeed, 0));
        //rb.AddForce(new Vector2(0, vertical), ForceMode2D.Impulse);  JUMP
        rb.AddForce(new Vector2(0, vertical * swimSpeed));

        if (isDashing)
        {
            //rb.velocity = Vector2.zero;
            //rb.AddForce(new Vector2(10, 0), ForceMode2D.Impulse);
            rb.AddRelativeForce(Vector2.right * dashSpeed, ForceMode2D.Force);
        }

        if(facingRight == false && horizontal > 0)
        {
            Flip();
        }

        else if(facingRight == true && horizontal < 0)
        {
            Flip();
        }
    }
    
    IEnumerator Dash()
    {
        Vector2 originalVelocity = rb.velocity;
        isLooking = false;
        isDashing = true;
        canDash = false;
        yield return new WaitForSeconds(dashDuration);
        isDashing = false;
        isLooking = true;
        rb.velocity = originalVelocity;
        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 scaler = transform.localScale;
        scaler.x *= -1;
        transform.localScale = scaler;
    }
}
