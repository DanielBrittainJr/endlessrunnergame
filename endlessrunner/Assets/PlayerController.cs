using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public bool alive = true;
    [Tooltip("Maximum slope the character can jump on")] [Range(5f, 60f)]
    public float slopeLimit = 45f;

    public float moveSpeed = 5f;
    public float turnSpeed = 300;
    public bool allowJump = false;
    public float jumpSpeed = 4f;
    
    public bool IsGrounded { get; private set; }
    public float ForwardInput { get; set; }
    public float TurnInput { get; set; }
    public bool JumpInput { get; set; }

    new private Rigidbody rigidbody;

    private CapsuleCollider capsuleCollider;

    private Animator animator;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        capsuleCollider = GetComponent<CapsuleCollider>();
        animator = GetComponentInChildren<Animator>();
    }

    private void FixedUpdate()
    {
        if (!alive) return; 
        Vector3 forwardMove = transform.forward * moveSpeed * Time.fixedDeltaTime;
        rigidbody.MovePosition(rigidbody.position + forwardMove);
        CheckGrounded();
        ProcessActions();
        moveSpeed = moveSpeed + .005f;
    }

    private void Update()
    {
        if (transform.position.y < -5)
        {
            Die();
        }
    }

    private void CheckGrounded()
    {
        IsGrounded = false;
        float capsuleHeight = Mathf.Max(capsuleCollider.radius * 2f, capsuleCollider.height);
        Vector3 capsuleBottom = transform.TransformPoint(capsuleCollider.center - Vector3.up * capsuleHeight / 2f);
        float radius = transform.TransformVector(capsuleCollider.radius, 0f, 0f).magnitude;
        Ray ray = new Ray(capsuleBottom + transform.up * .01f, -transform.up);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, radius * 5f))
        {
            float normalAngle = Vector3.Angle(hit.normal, transform.up);
            if (normalAngle < slopeLimit)
            {
                float maxDist = radius / Mathf.Cos(Mathf.Deg2Rad * normalAngle) - radius + .02f;
                if (hit.distance < maxDist)
                    IsGrounded = true;
            }
        }
    }

    private void ProcessActions()
    {
        if (TurnInput != 0f)
        {
            float angle = Mathf.Clamp(TurnInput, -1f, 1f) * turnSpeed;
            transform.Rotate(Vector3.up, Time.fixedDeltaTime * angle);
        }

        if (IsGrounded)
        {
            rigidbody.velocity = Vector3.zero;

            if (JumpInput && allowJump)
            {
                rigidbody.velocity += Vector3.up * jumpSpeed;
                //play jump animation
                animator.SetBool("IsJumping", true);
            }
            else
            {
                animator.SetBool("IsJumping", false);
            }


        }
    }

    public void Die()
    {
        alive = false;
        
        //Invoke("Restart",.5f);
        Restart();
    }

    void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
