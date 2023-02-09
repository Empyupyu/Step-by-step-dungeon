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
    }
}