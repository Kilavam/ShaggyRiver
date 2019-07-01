using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Killable : MonoBehaviour
{
    public int MaxHealth;
    private int _health;

    private void Start()
    {
        _health = MaxHealth;
    }

    public void Hit()
    {
        _health--;
        if (_health <= 0)
            Destroy(gameObject);
    }

    private void OnGUI()
    {
        GUILayout.Box($"Health: {_health}/{MaxHealth}");
    }
}
