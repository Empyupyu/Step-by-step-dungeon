using UnityEngine;

[CreateAssetMenu(fileName = "ConfigData (1)", menuName = "Datas/Config")]
public sealed class ConfigData : ScriptableObject
{
      [field : SerializeField] public int StartLevelIndex { get; private set; }
}
