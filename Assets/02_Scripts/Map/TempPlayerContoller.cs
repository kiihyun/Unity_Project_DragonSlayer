using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempPlayercontoller : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private Rigidbody2D _rigidbody;
    
    private float horizontalInput;

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        HandleInput();
        Move();
    }

    private void HandleInput()
    {
        // 좌우 입력 받기 (A/D 키 또는 좌/우 화살표 키)
        horizontalInput = Input.GetAxis("Horizontal");
    }

    private void Move()
    {
        // 좌우 이동 처리
        if (horizontalInput != 0)
        {
            Vector3 moveDirection = new Vector3(horizontalInput, 0, 0);
            transform.Translate(moveDirection * moveSpeed * Time.deltaTime);
        }

        // 점프
        if(Input.GetKeyDown(KeyCode.Space))
        {
            _rigidbody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }
}
