using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class WarpPoint : MonoBehaviour, IInteract
{
    [SerializeField] private Player _player;
    public string InteractText { get; set; } = "Warp";
    public string GetInteractText()
    {
        return InteractText;
    }
    
    private bool _isInteractable = false;
    public bool IsInteractable { get => _isInteractable; set => _isInteractable = value; }

    [SerializeField] private float _moveDistance = 1f;
    [SerializeField] private float _duration = 1f;

    public void Start()
    {
        // 임시
        _player = FindObjectOfType<Player>();

    {
        // 현재 위치에서 Y축으로 moveDistance만큼 위로 이동, 그리고 다시 아래로 반복
        transform.DOLocalMoveY(transform.localPosition.y + _moveDistance, _duration)
            .SetLoops(-1, LoopType.Yoyo)
            .SetEase(Ease.InOutSine);
        }
    }
    
    public void Interact()
    {
        // 워프 화면 창 띄우기


        if(IsInteractable && _player != null)
        {
            _player.gameObject.transform.position = new Vector3(transform.position.x, transform.position.y + 1, _player.gameObject.transform.position.z);
        }
    }
}
