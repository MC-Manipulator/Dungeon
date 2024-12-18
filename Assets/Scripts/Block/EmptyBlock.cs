using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EmptyBlock : AbstractBlock
{
    public LineRenderer lineRenderer;

    public SpriteRenderer spriteRenderer;

    public GameObject blockInIt;
    public GameObject blockReadyToPlace;

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

        spriteRenderer.color = new Color(1, 0.16f, 0.16f, 0.5f);
    }

    public void TurnGreen()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        

        spriteRenderer.color = new Color(0.16f, 1, 0.16f, 0.5f);
    }

    public void Disappear()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        spriteRenderer.color = new Color(0, 0, 0, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Wall" || collision.tag == "Floor")
            Refresh(collision);
        /*
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
        }*/
    }

    public void ActiveTrigger()
    {
        Collider2D[] collisions = Physics2D.OverlapBoxAll(this.transform.position, this.GetComponent<BoxCollider2D>().size, 0);

        foreach (Collider2D collision in collisions)
        {
            if (collision.tag == "Wall" || collision.tag == "Floor")
                Refresh(collision);
        }
    }

    public override void Refresh()
    {
    }

    public void Refresh(Collider2D collision)
    {
        if (!DungeonManager.instance.isBuilding)
        {
            if (collision.gameObject == blockInIt)
            {
                return;
            }
            if (blockInIt == null && collision.gameObject != blockReadyToPlace && (collision.gameObject.tag == "Wall" || collision.gameObject.tag == "Floor"))
            {
                blockInIt = collision.gameObject;
            }
            if (blockInIt != null)
            {
                Disappear();
                if (collision.gameObject != blockReadyToPlace && collision.gameObject.name == "Wall" && blockInIt != collision.gameObject)
                {
                    Destroy(collision.gameObject);
                    return;
                }
                if (blockInIt.name == "Wall" && collision.gameObject != blockReadyToPlace && collision.gameObject.name != "Wall")
                {
                    Destroy(blockInIt);
                    blockInIt = collision.gameObject;
                    return;
                }
                if (blockReadyToPlace != null)
                {

                    if (blockInIt.name == "Wall")
                    {
                        Destroy(blockInIt);
                        blockInIt = blockReadyToPlace;
                        blockReadyToPlace = null;
                        return;
                    }
                    if (blockInIt.tag == "Floor" && blockReadyToPlace.tag == "Wall")
                    {
                        Destroy(blockInIt);
                        blockInIt = blockReadyToPlace;
                        blockReadyToPlace = null;
                        return;
                    }
                    if (blockInIt.tag == "Floor" && blockReadyToPlace.tag == "Floor")
                    {
                        Destroy(blockInIt);
                        blockInIt = blockReadyToPlace;
                        blockReadyToPlace = null;
                        return;
                    }
                    if (blockInIt.tag == "Wall" && blockReadyToPlace.tag == "Floor")
                    {
                        Destroy(blockReadyToPlace);
                        blockReadyToPlace = null;
                        return;
                    }
                }

            }
        }
        else
        {
            if (blockInIt != null)
            {
                if (blockInIt != collision.gameObject && blockReadyToPlace != collision.gameObject && (collision.gameObject.tag == "Wall" || collision.gameObject.tag == "Floor"))
                {
                    blockReadyToPlace = collision.gameObject;
                }
                if (blockInIt.name == "Wall" && blockReadyToPlace != null)
                {
                    this.TurnGreen();
                    return;
                }
                if (blockInIt.tag == "Floor" && blockReadyToPlace != blockInIt && blockReadyToPlace != null)
                {
                    this.TurnGreen();
                    return;
                }
                if (blockInIt.tag == "Wall" && blockInIt.name != "Wall" && collision.gameObject != blockInIt)
                {
                    this.TurnRed();
                    if (!DungeonManager.instance.conflictBlockList.Contains(this.gameObject))
                        DungeonManager.instance.conflictBlockList.Add(gameObject);
                    return;
                }
            }
        }



        /*
        if (!blockInIt.Contains(collision.gameObject) && (collision.tag == "Wall" || collision.tag == "Floor"))
        {
            blockInIt.Add(collision.gameObject);
        }

        if (blockInIt.Count > 1 && !DungeonManager.instance.isBuilding)
        {
            for (int i = 0; i < blockInIt.Count; i++)
            {
                if (blockInIt[i] != null && blockInIt[i].name == ("Wall"))
                {
                    GameObject temp = blockInIt[i];
                    blockInIt.Remove(temp);
                    Destroy(temp);
                    return;
                }
            }
        }

        if (blockInIt.Count > 1 && DungeonManager.instance.isBuilding)
        {
            for (int i = 0; i < blockInIt.Count; i++)
            {
                if (blockInIt[i] != null && blockInIt[i].name == "Wall")
                {
                    this.TurnGreen();
                    return;
                }

                if (blockInIt[i] != null && blockInIt[i].tag == "Floor" && collision.gameObject != blockInIt[i])
                {
                    this.TurnGreen();
                    return;
                }

                if (blockInIt[i] != null && blockInIt[i].tag == "Wall" && blockInIt[i].name != "Wall" && collision.gameObject != blockInIt[i])
                {
                    this.TurnRed();
                    DungeonManager.instance.conflictBlockList.Add(gameObject);
                    return;
                }
            }
        }*/
    }

    public void RefreshBlock()
    {
        AbstractBlock b = (AbstractBlock)FetchComponent.GetSpecificComponent<AbstractBlock>(blockInIt);
        if (b != null)
        {
            b.Refresh();

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == blockReadyToPlace)
        {
            blockReadyToPlace = null;
            if (DungeonManager.instance.conflictBlockList.Contains(this.gameObject))
            {
                DungeonManager.instance.conflictBlockList.Remove(this.gameObject);
            }
            Disappear();
        }

        if (blockInIt == null && blockReadyToPlace == null)
        {
#if UNITY_EDITOR
            if (!UnityEditor.EditorApplication.isPlayingOrWillChangePlaymode)
                return;
#endif
            GameObject wall = DungeonManager.instance.GetBlockByName("Wall");

            GameObject newWall = Instantiate(wall, new Vector3(transform.position.x, transform.position.y, 0), new Quaternion(0, 0, 0, 0));

            newWall.transform.SetParent(transform.parent);
        }

        if (blockInIt == null && blockReadyToPlace != null)
        {
            blockInIt = blockReadyToPlace;
            blockReadyToPlace = null;
        }

        /*
        if (blockInIt.Contains(collision.gameObject))
        {
            blockInIt.Remove(collision.gameObject);
        }

        if (DungeonManager.instance.isBuilding && collision.gameObject.name != "Wall" && blockInIt.Count == 1)
        {
            if (DungeonManager.instance.conflictBlockList.Contains(this.gameObject))
            {
                DungeonManager.instance.conflictBlockList.Remove(this.gameObject);
            }
            Disappear();
        }
        if (!DungeonManager.instance.isBuilding)
        {
            Disappear();
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
        }*/
    }

}
