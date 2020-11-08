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
        if (Mathf.Abs(vertical) > Mathf.Abs(horizontal)) {
            animator.SetBool("up", vertical > 0);
            animator.SetBool("down", vertical < 0);
        } else {
            animator.SetBool("up", false);
            animator.SetBool("down", false);
        }
        animator.SetBool("walking", horizontal != 0f || vertical != 0f);
        Vector2 velocity = ((Vector2.right * horizontal) + (Vector2.up * vertical));
        rigidBody.MovePosition(rigidBody.position + (velocity * Time.deltaTime));
        if (horizontal != 0f) {
            transform.localScale = new Vector3(xdir, 1f, 1f);
        }

        if (Input.GetKeyDown("space")) {
            animator.SetBool("slash", true);
            animator.GetBool("up");
            animator.GetBool("down");

            Vector2 direction;
            Vector2 position;
            if (animator.GetBool("up")) {
                direction = Vector2.up;
                position = rigidBody.position + new Vector2(0f, 0.5f);
            } else if (animator.GetBool("down")) {
                direction = Vector2.down;
                position = rigidBody.position + new Vector2(0f, -0.5f);
            } else {
                direction = Vector2.right*transform.localScale.x;
                position = rigidBody.position + (new Vector2(0.5f, 0f) * transform.localScale.x);
            }
            Debug.LogFormat("Direction {0}", direction);
            RaycastHit2D hit = Physics2D.Raycast(position, direction, .1f);
            if (hit.collider != null) {
                Debug.LogFormat("Hit object {0}", hit.collider);
                Destroy(hit.collider.gameObject);
            }
        } else {
            animator.SetBool("slash", false);
        }

        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }
    }
}
