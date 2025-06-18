using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneMonster_Move : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private bool stop = false;
    [SerializeField] private Image _fadeImage;
    [SerializeField] private float _fadeDuration;
    [SerializeField] private SceneManager_Intro1 _faded;
    private Rigidbody2D _rigidbody;
    
    

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (stop)
            return;

        Vector2 velocity = _rigidbody.velocity;
        velocity.x = _moveSpeed;
        _rigidbody.velocity = velocity;
        if(this.transform.position.x < -10)
            this.gameObject.SetActive(false);
    }

    public void ToggleStop()
    { stop = !stop; }

    public void fadeout()
    {
        _faded.FadeOut();
    }
    public void Roar()
    {
        SoundManager.Instance.PlaySFX("Roar");
    }
}
