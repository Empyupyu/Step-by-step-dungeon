using Supyrb;
using UnityEngine;

public class PathfindingSystem : GameSystem
{
    public override void OnAwake()
    {
        Signals.Get<EnemyPathfindingSignal>().AddListener(FindNearestNodeOnDirectionToTargetUnit);
    }

    private void FindNearestNodeOnDirectionToTargetUnit(Enemy enemy)
    {
        var distance = Mathf.Infinity;
        Node nearestNode = null;

        for (int i = 0; i < enemy.AvailableNodes.Count; i++)
        {
            var distanceBetweenUnits = Vector3.Distance(enemy.AvailableNodes[i].transform.position, enemy.TargetUnit.transform.position);

            if (distanceBetweenUnits < distance)
            {
                distance = distanceBetweenUnits;
                nearestNode = enemy.AvailableNodes[i];
            }
        }

        enemy.SetTarget(nearestNode);
    }
}