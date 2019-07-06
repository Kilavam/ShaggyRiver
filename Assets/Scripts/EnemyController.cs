using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float Speed;

    public float DetectionRadius;
    public float DetectionFrequency;
    private float _lastDetection;
    private Vector2 _targetPosition;
    private Rigidbody2D _body;
    


    void Start()
    {
        _body = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (_lastDetection > DetectionFrequency)
        {
            _lastDetection = 0;
            var player = CheckDetection();
            if(player!= null)
            {
                _targetPosition = player.transform.position - transform.position;
            }
            else
            {
                _targetPosition = Vector2.zero;
            }
            
        }
        _lastDetection += Time.deltaTime;

        if (_targetPosition != Vector2.zero)
            _body.velocity = _targetPosition.normalized * Speed;
    }

    private PlayerController CheckDetection()
    {
        // TODO: add a layer for the player to limit the amount of checks needed.
        var hits = Physics2D.OverlapCircleAll(transform.position, DetectionRadius);
        return hits.Where(h => h.GetComponent<PlayerController>() != null).FirstOrDefault()?.GetComponent<PlayerController>();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        if (!Application.isPlaying)
        {
            Gizmos.DrawWireSphere((Vector2)transform.position, DetectionRadius);
        }
        else if (_lastDetection > DetectionFrequency)
        {   
            Gizmos.DrawWireSphere((Vector2)transform.position, DetectionRadius);
        }
    }
}
