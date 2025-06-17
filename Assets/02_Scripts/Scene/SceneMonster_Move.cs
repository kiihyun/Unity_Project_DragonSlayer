using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneMonster_Move : MonoBehaviour
{
    private Rigidbody2D _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        _rigidbody.velocity += Vector2.left*0.5f;
    }
}
