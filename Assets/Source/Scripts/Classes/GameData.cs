using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class GameData
{
    public Level Level;
    public Player Player;
    public CinemachineVirtualCamera Camera;

    public List<Unit> Units = new List<Unit>();

    public Vector3 MoveDirection;

    public bool IsPlayerTurn;
}