using UnityEngine;

public class PlayerSpawnerSystem : GameSystem
{
    public override void OnAwake()
    {
        Spawn();
    }

    private void Spawn()
    {
        game.Player = Instantiate(congfig.Player, game.Level.PlayerStartPositionOnLevel.position, Quaternion.identity);

        Initialize();
    }

    private void Initialize()
    {
        game.Player.SetMoveTimeToTargetNode(congfig.MoveTimeToTargetNode);
        game.Player.SetTimeToLookAt(congfig.TimeToLookAt);
        game.Player.SetHealth(congfig.HealthOnStart);
        game.Player.SetDamage(congfig.DamageOnStart);
    }
}