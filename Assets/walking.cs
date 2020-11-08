using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class walking : MonoBehaviour
{
    Animator animator;
    Rigidbody2D rigidBody;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float vertical = Input.GetAxis("Vertical");
        float horizontal = Input.GetAxis("Horizontal");
        float xdir = Mathf.Sign(horizontal);
        //change x position
        animator.SetBool("up", vertical > 0);
        animator.SetBool("down", vertical < 0);
        animator.SetBool("walking", horizontal != 0f || vertical != 0f);
        Vector2 velocity = ((Vector2.right * horizontal) + (Vector2.up * vertical))*10;
        rigidBody.MovePosition(rigidBody.position + (velocity * Time.deltaTime));
        if (horizontal != 0f) {
            transform.localScale = new Vector3(xdir, 1f, 1f);
        }

        if (Input.GetKeyDown("space")) {
            animator.SetBool("slash", true);
            RaycastHit2D hit = Physics2D.Raycast(rigidBody.position+new Vector2(0.5f, 0f),Vector2.right*transform.localScale.x, .1f);
            if (hit.collider != null) {
                Debug.LogFormat("Hit object {0}", hit.collider);
            }
        } else {
            animator.SetBool("slash", false);
        }
    }
}
