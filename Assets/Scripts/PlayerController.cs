using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D _body;

    public float Speed;

    [Header("Jump")]
    public float JumpForce;
    public float JumpFallMultiplier;
    public float JumpRaiseMultiplier;

    [Header("Colision Detection")]
    public Vector2 BottomOffset;
    public Vector2 RightOffset;
    public Vector2 LeftOffset;
    public float CollisionRadius;

    [Header("Shooting")]
    public BulletController BulletPrefab;
    public float BulletVelocity;
    public float FireRate;

    private bool _onGround;
    private bool _onWall;
    private int _wallLayer = 8;
    private float _fireDelay;

    // Start is called before the first frame update
    void Start()
    {
        _wallLayer = LayerMask.GetMask("Wall");
        _body = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckCollision();
        Walk();
        Jump();
        Fire();

        var renderer = GetComponent<SpriteRenderer>();
        renderer.flipX = _body.velocity.x < -1;
        renderer.flipY = _body.velocity.y > 1;
    }

    private void Fire()
    {
        _fireDelay += Time.deltaTime;
        if (_fireDelay > FireRate && InputManager.GetFire())
        {
            _fireDelay = 0;
            BulletController bullet = Instantiate(BulletPrefab, transform.position, Quaternion.Euler(0, 0, Random.value * 360), transform);

            var direction = InputManager.GetDirection();
            direction.Normalize();
            if (direction.sqrMagnitude == 0) direction = Vector2.right;
            bullet.Direction = direction * BulletVelocity;
        }
    }

    private void Walk()
    {
        var direction = InputManager.GetDirection();

        _body.velocity = new Vector2(
            direction.x * Speed,
            _body.velocity.y);
    }

    private void Jump()
    {
        if (_onGround && InputManager.GetJump())
        {
            _body.velocity = new Vector2(_body.velocity.x, 0);
            _body.velocity += Vector2.up * JumpForce;
        }

        if (_body.velocity.y < 0)
        {
            _body.velocity += Physics2D.gravity * JumpFallMultiplier * Time.deltaTime;
        }
        else if (_body.velocity.y > 0 && !InputManager.GetJump())
        {
            _body.velocity += Physics2D.gravity * JumpRaiseMultiplier * Time.deltaTime;
        }
    }


    private void CheckCollision()
    {
        _onGround = CheckCollision(BottomOffset);
        _onWall = CheckCollision(RightOffset) || CheckCollision(LeftOffset);
    }

    private bool CheckCollision(Vector2 offset)
    {
        return Physics2D.OverlapCircle((Vector2)transform.position + offset, CollisionRadius, _wallLayer);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawWireSphere((Vector2)transform.position + BottomOffset, CollisionRadius);
        Gizmos.DrawWireSphere((Vector2)transform.position + RightOffset, CollisionRadius);
        Gizmos.DrawWireSphere((Vector2)transform.position + LeftOffset, CollisionRadius);
    }
}
