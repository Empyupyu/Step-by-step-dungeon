using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    [field : SerializeField] public Transform PlayerStartPositionOnLevel { get; private set; }
    [field : SerializeField] public List<Portal> Portals { get; private set; }
    [field : SerializeField] public List<Enemy> Enemy { get; private set; }
}