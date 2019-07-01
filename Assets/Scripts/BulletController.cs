using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public Vector2 Direction;
    public float LifeTime;
    private float _age;
    private Rigidbody2D _body;

    void Start()
    {
        _body = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (LifeTime != 0 && _age >= LifeTime)
        {
            Destroy(gameObject);
        }
        else
        {
            _age += Time.deltaTime;
            _body.velocity = Direction;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        var killable = other.GetComponent<Killable>();
        if (killable!= null)
        {
            killable.Hit();
            Destroy(gameObject);
        }
        Debug.Log("enter");
    }
}
