using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Onclick_Intro : MonoBehaviour
{
    private Animator _animator;
    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }
    public void OnClicked()
    {
        SoundManager.Instance.PlaySFX("GameStart");
        _animator.SetTrigger("Click");
    }
}
