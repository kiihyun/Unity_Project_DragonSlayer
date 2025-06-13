using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTestPlayer : MonoBehaviour
{
    public static BossTestPlayer Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    public void TakeDamage(float amount)
    {
        Debug.Log($"플레이어가 {amount}의 데미지를 입음!");
        // 체력 감소 처리
    }
}
