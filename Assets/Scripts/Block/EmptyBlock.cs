using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EmptyBlock : AbstractBlock
{
    public LineRenderer lineRenderer;

    public SpriteRenderer spriteRenderer;

    public List<GameObject> blockInIt;

    public void ShowLine()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.enabled = true;
        lineRenderer.positionCount = 6;
        lineRenderer.startWidth = 0.01f;
        lineRenderer.endWidth = 0.01f;
        lineRenderer.SetPosition(0, new Vector3(this.transform.position.x - 0.5f, this.transform.position.y - 0.5f, 0));
        lineRenderer.SetPosition(1, new Vector3(this.transform.position.x - 0.5f, this.transform.position.y + 0.5f, 0));
        lineRenderer.SetPosition(2, new Vector3(this.transform.position.x + 0.5f, this.transform.position.y + 0.5f, 0));
        lineRenderer.SetPosition(3, new Vector3(this.transform.position.x + 0.5f, this.transform.position.y - 0.5f, 0));
        lineRenderer.SetPosition(4, new Vector3(this.transform.position.x - 0.5f, this.transform.position.y - 0.5f, 0));
        lineRenderer.SetPosition(5, new Vector3(this.transform.position.x - 0.5f, this.transform.position.y - 0.4f, 0));
    }

    public void HideLine()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.enabled = false;
    }

    public void TurnRed()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        spriteRenderer.color = new Color(1, 0.16f, 0.16f, 0.2f);
    }

    public void TurnGreen()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        

        spriteRenderer.color = new Color(0.16f, 1, 0.16f, 0.2f);
    }

    public void Disappear()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        spriteRenderer.color = new Color(0, 0, 0, 0);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("1");

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!blockInIt.Contains(collision.gameObject) && (collision.tag == "Wall" || collision.tag == "Floor") && !DungeonManager.instance.isBuilding)
        {
            blockInIt.Add(collision.gameObject);
        }
        if (blockInIt.Count > 1)
        {
            for (int i = 0;i < blockInIt.Count;i++)
            {
                if (blockInIt[i] != null && blockInIt[i].name == ("Wall(Clone)"))
                {
                    Destroy(blockInIt[i]);
                }
            }
        }
    }

    public void Refresh()
    {
        Collider2D[] collisions = Physics2D.OverlapBoxAll(this.transform.position, new Vector2(1, 1), 0);

        foreach (Collider2D collision in collisions)
        {

            if (!blockInIt.Contains(collision.gameObject) && (collision.tag == "Wall" || collision.tag == "Floor") && !DungeonManager.instance.isBuilding)
            {
                blockInIt.Add(collision.gameObject);
            }
            if (blockInIt.Count > 1)
            {
                for (int i = 0; i < blockInIt.Count; i++)
                {
                    if (blockInIt[i] != null && blockInIt[i].name == ("Wall(Clone)"))
                    {
                        Destroy(blockInIt[i]);
                    }
                }
            }

        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (blockInIt.Contains(collision.gameObject))
        {
            blockInIt.Remove(collision.gameObject);
        }

        
        if (blockInIt.Count == 0)
        {
#if UNITY_EDITOR
            if (!UnityEditor.EditorApplication.isPlayingOrWillChangePlaymode)
                return;
#endif
            GameObject wall = DungeonManager.instance.GetBlockByName("Wall");

            GameObject newWall = Instantiate(wall, new Vector3(transform.position.x, transform.position.y, 0), new Quaternion(0, 0, 0, 0));

            newWall.transform.SetParent(transform.parent);
        }
    }

}
