using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ConfigData (1)", menuName = "Datas/Config")]
public sealed class ConfigData : ScriptableObject
{
      [field : SerializeField] public int StartLevelIndex { get; private set; }
      [field : SerializeField, Header("Player Settings")] public Player Player { get; private set; }
      [field : SerializeField] public float DelayTransitionToPlayerTurn { get; private set; }
      [field : SerializeField] public float MoveTimeToTargetNode { get; private set; }
      [field : SerializeField] public float TimeToLookAt { get; private set; }
      [field : SerializeField] public int HealthOnStart { get; private set; }
      [field : SerializeField] public int DamageOnStart { get; private set; }
      [field : SerializeField] public int AvailableRadius { get; private set; }

      [field : SerializeField, Header("Node Settings")] public Node Node { get; private set; }
      [field : SerializeField] public Color HightlightColor { get; private set; }
      [field : SerializeField] public Color DefaultColor { get; private set; }
      [field : SerializeField] public Vector2 GridSize { get; private set; }
      [field : SerializeField] public Vector2 OffsetNode { get; private set; }
      [field : SerializeField] public float HighlightingDuration { get; private set; }
      [field : SerializeField] public float DefaultColorDuration { get; private set; }
      [field : SerializeField, Header("Level Settings")] public List<ChestData> ChestDatas { get; private set; }
      [field : SerializeField, Header("Debug Settings")] public bool IsDebug { get; private set; }
      [field : SerializeField] public int EnemyHealthOnDebugMode { get; private set; }
}
