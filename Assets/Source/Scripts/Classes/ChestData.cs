using UnityEngine;

[CreateAssetMenu(fileName = "ChestData (1)", menuName = "Datas/ChestData")]
public class ChestData : ScriptableObject
{
    [field: SerializeField, TextArea(3, 5)] public string InfoTextAfterInteract;
    [field: SerializeField] public string Reward;
}