using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float _speed = 10;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        var direction = new Vector2(
            Input.GetAxis("Horizontal"),
            Input.GetAxis("Vertical"));

        var body = GetComponent<Rigidbody2D>();
        body.velocity += direction * _speed * Time.deltaTime;
    }
}
