using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D _rb;

    public LayerMask platformLayerMask;
    public Transform gapChecker;

    public bool isJumping, isJumpingOnce;
    public float moveSpeed = 10.0f;
    public float jumpForce = 10.0f;
    public float groundDetectionDistance = 2f;
    public float gapDetectionDistance = 2f;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        AutoJump();
    }
    private void FixedUpdate()
    {
        AutoMove();
    }

    private void AutoMove()
    {
        if (!GameManager.Instance.isStarted) return;

        float mv = 1 * moveSpeed * Time.deltaTime;
        transform.Translate(mv, 0, 0);
    }

    private void AutoJump()
    {
        if (IsOnGround() && IsThereGap())
        {
            GameManager.Instance.PlaySfx("Jump");
            _rb.velocity = new Vector3(_rb.velocity.x, jumpForce);
        }
    }

    private bool IsOnGround()
    {
        return Physics2D.Raycast(transform.position, -Vector2.up, groundDetectionDistance, platformLayerMask);
    }

    private bool IsThereGap()
    {
        return !Physics2D.Raycast(gapChecker.position, -Vector2.up, gapDetectionDistance, platformLayerMask);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, new Vector3(transform.position.x, transform.position.y - groundDetectionDistance, transform.position.z));
        Gizmos.DrawLine(gapChecker.position, new Vector3(gapChecker.position.x, gapChecker.position.y - gapDetectionDistance, gapChecker.position.z));
    }
}