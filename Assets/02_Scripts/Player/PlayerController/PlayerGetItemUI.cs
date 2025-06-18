using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class ItemGetUI : MonoBehaviour
{
    [Header("UI Components")]
    [SerializeField] private TextMeshProUGUI itemText;
    [SerializeField] private float displayDuration = 2f;

    public void ShowItems(ItemData itemData, Vector3 position)
    {
        itemText.text = itemData.ItemName + "을 획득했습니다.";
        transform.position = position;
        StartCoroutine(HideAfterDelay());
    }

    private IEnumerator HideAfterDelay()
    {
        // 조금씩 올라가면서 
        transform.DOLocalMoveY(transform.localPosition.y + 10, 1f);

        // 텍스트 투명해지게
        itemText.DOFade(0, displayDuration);

        yield return new WaitForSeconds(displayDuration);

        Destroy(gameObject);
    }
}
