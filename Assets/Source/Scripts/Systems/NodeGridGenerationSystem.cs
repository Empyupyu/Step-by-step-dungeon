using UnityEngine;

public class NodeGridGenerationSystem : GameSystem
{
    [SerializeField] private int layer = 6;

    private int layerAsLayerMask;

    public override void OnAwake()
    {
        layerAsLayerMask = (1 << layer);

        Generation();
    }

    private void Generation()
    {
        for (int z = 0; z < congfig.GridSize.y; z++)
        {
            for (int x = 0; x < congfig.GridSize.x; x++)
            {
                var nextPositionNode = GetNextPosition(x, z);

                if (!CheckIsAllowableForCherateNode(nextPositionNode)) continue;

                CreateNode(nextPositionNode);
            }
        }
    }

    private Vector3 GetNextPosition(int x, int z)
    {
        var nextPosition = new Vector3(-(x * congfig.OffsetNode.x + congfig.OffsetNode.x / 2), 0, z * congfig.OffsetNode.y);

        if (z % 2 == 0)
        {
            nextPosition = new Vector3(-(x * congfig.OffsetNode.x), 0, z * congfig.OffsetNode.y);
        }

        nextPosition += game.Level.PlayerStartPositionOnLevel.position;

        return nextPosition;
    }

    private void CreateNode(Vector3 nextPositionNode)
    {
        var node = Instantiate(congfig.Node);
        node.transform.position = nextPositionNode;
        node.transform.parent = game.Level.PlayerStartPositionOnLevel;
    }

    private bool CheckIsAllowableForCherateNode(Vector3 nextPositionNode)
    {
        var objects = Physics.OverlapSphere(nextPositionNode, .55f, layerAsLayerMask);

        for (int i = 0; i < objects.Length; i++)
        {
            if (!objects[i].TryGetComponent<Floor>(out var floor))
            {
                return false;
            }
        }

        if (objects.Length == 0) return false;
        else return true;
    }
}
