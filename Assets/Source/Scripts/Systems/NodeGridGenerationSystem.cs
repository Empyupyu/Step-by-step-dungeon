using UnityEngine;

public class NodeGridGenerationSystem : GameSystem
{
    private int gridLayer = 6;
    private int nodeLayer = 7;


    private int layerAsLayerMask;

    public override void OnAwake()
    {
        layerAsLayerMask = (1 << gridLayer);
        game.NodeLayerMask = (1 << nodeLayer);

        game.NodeGrid = new Node[(int)congfig.GridSize.y,(int)congfig.GridSize.x];

        Generation();
    }

    private void Generation()
    {
        for (int z = 0; z < congfig.GridSize.y; z++)
        {
            for (int x = 0; x < congfig.GridSize.x; x++)
            {
                var nextPositionNode = GetNextPosition(x, z);

                if (!CheckIsAllowableForCherateNode(nextPositionNode))
                {
                    game.NodeGrid[z, x] = null;
                    continue;
                }

                game.NodeGrid[z, x] = CreateNode(nextPositionNode);
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

    private Node CreateNode(Vector3 nextPositionNode)
    {
        var node = Instantiate(congfig.Node);
        node.transform.position = nextPositionNode;
        node.transform.parent = game.Level.PlayerStartPositionOnLevel;
        return node;
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
