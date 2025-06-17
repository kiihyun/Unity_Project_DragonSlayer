using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Energipa : MonoBehaviour
{
    public GameObject skillPrefab;
    public Transform skillPivot;

    private Player player;
    private Vector3 originPivot;

    private void Awake()
    {
        player = GetComponentInParent<Player>();
        originPivot = skillPivot.localPosition;
    }

    public void UseSkill()
    {
        float direction = player.CharacterImage.flipX ? 1f : -1f;
        
        skillPivot.localPosition = new Vector3(originPivot.x * direction, originPivot.y, originPivot.z );

        if (skillPrefab != null)
        {
            GameObject skillInstance = Instantiate(skillPrefab, skillPivot.position, Quaternion.identity);
            skillInstance.GetComponent<Rigidbody2D>().velocity = new Vector2(10f * -direction, 0);
            skillInstance.GetComponent<SpriteRenderer>().flipX = player.CharacterImage.flipX;
        }
    }

}
