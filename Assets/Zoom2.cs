using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zoom2 : MonoBehaviour
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
    


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        

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

        if (isLooking)
        {
            Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;

            difference.Normalize();

            float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;

            transform.rotation = Quaternion.Euler(0f, 0f, rotationZ);

            if (rotationZ < -90 || rotationZ > 90)
            {
                if (rb.transform.eulerAngles.y == 0)
                {
                    transform.localRotation = Quaternion.Euler(180, 0, -rotationZ);
                }

                else if (rb.transform.eulerAngles.y == 180)
                {
                    transform.localRotation = Quaternion.Euler(180, 180, -rotationZ);
                }
            }
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

    
}
