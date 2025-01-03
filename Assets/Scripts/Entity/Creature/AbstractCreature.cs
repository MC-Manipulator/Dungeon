using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Node
{
    public Vector2 Position { get; set; }
    public Node Parent { get; set; }
    public float G { get; set; } // 从起点到当前节点的代价
    public float H { get; set; } // 从当前节点到终点的启发式代价
    public float F { get { return G + H; } } // 总代价

    public Node(Vector2 position, Node parent, float g, float h)
    {
        Position = position;
        Parent = parent;
        G = g;
        H = h;
    }
}

public abstract class AbstractCreature : AbstractEntity
{
    public new EntityType entityType = EntityType.Creature;
    public CreatureType creatureType;   // 生物类型
    public float movableDistance;       // 可移动距离
    public float currentSan;            // 当前san值，影响理智
    public float maxSan;                // 最大san值
    public float res;                   // 抵抗力，影响受到伤害时san值的减少程度

    protected Vector2 Move(Vector2 target)
    {
        List<Vector2> path = FindPath(transform.position, target); // 寻找路径
        float totalCost = 0; // 总代价

        foreach (Vector2 step in path)
        {
            float stepCost = Vector2.Distance(transform.position, step); // 计算每一步的代价
            if (totalCost + stepCost <= movableDistance)
            {
                totalCost += stepCost; // 累加代价
                transform.position = step; // 移动到下一步
            }
            else
            {
                // 移动到耗尽movableDistance后的位置
                float remainingDistance = movableDistance - totalCost; // 计算剩余的可移动距离
                Vector2 direction = (step - (Vector2)transform.position).normalized; // 计算方向
                transform.position += (Vector3)(direction * remainingDistance); // 移动到剩余距离的位置
                break;
            }
        }
        return transform.position; // 返回最终位置
    }

    private List<Vector2> FindPath(Vector2 start, Vector2 target)
    {
        List<Node> openList = new List<Node>(); // 开放列表
        HashSet<Node> closedList = new HashSet<Node>(); // 关闭列表

        Node startNode = new Node(start, null, 0, Vector2.Distance(start, target));
        openList.Add(startNode);

        while (openList.Count > 0)
        {
            Node currentNode = openList[0];
            // 找到F值最小的节点
            foreach (Node node in openList)
            {
                if (node.F < currentNode.F || (node.F == currentNode.F && node.H < currentNode.H))
                {
                    currentNode = node;
                }
            }

            openList.Remove(currentNode);
            closedList.Add(currentNode);

            // 如果找到目标节点，返回路径
            if (currentNode.Position == target)
            {
                return RetracePath(startNode, currentNode);
            }

            // 遍历当前节点的邻居节点
            foreach (Vector2 neighbor in GetNeighbors(currentNode.Position))
            {
                // 如果邻居是墙或者已经在关闭列表中，跳过
                if (GetNearbyBlock.GetTypeInPos(neighbor) == "Wall" || closedList.Contains(new Node(neighbor, null, 0, 0)))
                {
                    continue;
                }

                float newMovementCostToNeighbor = currentNode.G + Vector2.Distance(currentNode.Position, neighbor);
                Node neighborNode = new Node(neighbor, currentNode, newMovementCostToNeighbor, Vector2.Distance(neighbor, target));

                // 如果新的路径到邻居节点的代价更小，或者邻居节点不在开放列表中
                if (newMovementCostToNeighbor < neighborNode.G || !openList.Contains(neighborNode))
                {
                    neighborNode.G = newMovementCostToNeighbor;
                    neighborNode.Parent = currentNode;

                    if (!openList.Contains(neighborNode))
                    {
                        openList.Add(neighborNode);
                    }
                }
            }
        }

        return new List<Vector2>(); // 如果没有找路径，返回空路径
    }

    private List<Vector2> RetracePath(Node startNode, Node endNode)
    {
        List<Vector2> path = new List<Vector2>();
        Node currentNode = endNode;

        // 从终点回溯到起点
        while (currentNode != startNode)
        {
            path.Add(currentNode.Position);
            currentNode = currentNode.Parent;
        }
        path.Reverse(); // 反转路径
        return path;
    }

    private List<Vector2> GetNeighbors(Vector2 position)
    {
        // 获取邻居节点
        List<Vector2> neighbors = new List<Vector2>
        {
            position + Vector2.up,
            position + Vector2.down,
            position + Vector2.left,
            position + Vector2.right,
            position + new Vector2(-1, 1),
            position + new Vector2(1, 1),
            position + new Vector2(-1, -1),
            position + new Vector2(1, -1)
        };

        return neighbors;
    }

    protected override float Damage()
    {
        return attack;
    }

    protected override void Die()
    {
        throw new System.NotImplementedException();
    }

    public override void Attack(AbstractEntity target)
    {
        // 计算伤害值
        float damageValue = Damage();
        if (damageValue > 0)
        {
            float hurt = target.Hurt(damageValue);
            currentSan += hurt;
            currentSan = Mathf.Clamp(currentSan, 0, maxSan);
        }
    }

    public override float Hurt(float damage)
    {
        float hurt = damage * defence;
        if (hurt < 0)
            hurt = 0;
        currentHealth -= hurt;
        currentSan -= hurt * res;
        currentSan = Mathf.Clamp(currentSan, 0, maxSan);
        if (currentHealth <= 0)
            Die();
        return hurt;
    }
}

public enum CreatureType
{
    Monster,    // 怪物
    Explorer,   // 探险者
    Netural     // 中立
}