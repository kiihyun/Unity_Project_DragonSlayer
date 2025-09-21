using UnityEngine;

public class BossDieState : IBossState
{
    private BossEnemy _boss;
    private bool _hasDied;

    public BossDieState(BossEnemy boss)
    {
        _boss = boss;
    }

    public void Enter()
    {
        if (_hasDied) return;


        // �ִϸ��̼� Ʈ����
        Animator animator = _boss.GetComponent<Animator>();
        if (animator != null)
        {
            animator.SetTrigger("Die");
        }

        // ����Ʈ ��� (��: ����)
        if (_boss.BossData.bossPrefab != null)
        {
            // ���� �� �� ����Ʈ ������ ���� ���� ���� ����
        }

        // ��� �� ������ ���� (�ʿ� �� Rigidbody2D, NavMesh �� ó��)
        Rigidbody2D rb = _boss.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = Vector2.zero;
            rb.isKinematic = true;
        }

        // ���� ���� (��: ����ġ, ������ ��)
        GiveRewards();

        // Ŭ���� ó�� (UI Ȱ��ȭ ��)
        //StageClearUI.Instance.ShowClearPanel();

        _boss.OnBossDie?.Invoke();

        _hasDied = true;
    }

    public void Execute()
    {
        // �ƹ� �͵� ���� ����
    }

    public void Exit()
    {
        // �� ���¿��� ���� Exit �� ��
    }

    private void GiveRewards()
    {

        // ��: GameManager.Instance.AddExp(300);
        // ��: Instantiate(dropItem, _boss.transform.position, Quaternion.identity);
    }
}