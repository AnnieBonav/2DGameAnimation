using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static event Action Jumped;

    private float direction;
    [SerializeField]
    private float speed = 5f;

    [SerializeField]
    private float jumpHeight = 5f;

    private Rigidbody2D rb;

    private bool isGrounded = false;

    void Awake()
    {
        //Jumped += Jump();
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        direction = Input.GetAxis("Horizontal");
        transform.Translate(transform.right * direction * speed * Time.deltaTime);
        if (Input.GetKeyDown(KeyCode.UpArrow) && isGrounded)
        {
            Jump();
            isGrounded = false;
        }

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.gameObject.tag);
        if(collision.gameObject.tag == "Floor")
        {
            Debug.Log("Is checked");
            isGrounded = true;
        }
    }

    private void Jump()
    {
        rb.AddForce(Vector2.up * jumpHeight, ForceMode2D.Impulse);
    }
}
