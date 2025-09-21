using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(PlayerStat))]
public class PlayerStatsEditor : Editor
{
    public override void OnInspectorGUI()
    {
        // 기본 인스펙터 그리기
        DrawDefaultInspector();

        // 줄 바꿈
        EditorGUILayout.Space();

        // 버튼 추가
        if (GUILayout.Button("레벨업"))
        {
            PlayerStat stats = (PlayerStat)target;
            stats.LevelUP();
        }
    }
}
