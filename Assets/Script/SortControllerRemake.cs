using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SortControllerRemake : MonoBehaviour
{
    [SerializeField] public List<Transform> lsSlotSort;
    [SerializeField] public List<TilebaseController> lsTilebaseClicked;
    public ParticleSystem matchedParticle;
    public void AddLsSlotSort()
    {
        foreach (Transform children in transform)
        {
            if (children.name == "Particle System") continue;
            lsSlotSort.Add(children);
        }
    }

    private void Awake()
    {
        this.AddLsSlotSort();
    }

    public void HandleOnMouseDown(TilebaseController tileBaseParam)
    {
        int validPosSort = GetValidPosToSort(tileBaseParam);
        if (validPosSort != -1)
        {
            if (validPosSort < lsTilebaseClicked.Count)
            {
                RearrangeSlotSort(validPosSort);
            }

            MoveTileToTarget(tileBaseParam, lsSlotSort[validPosSort].position);
            GameController.Instance.numOfTile--;
            lsTilebaseClicked.Insert(validPosSort, tileBaseParam);
            StartCoroutine(HandleTileMatch(tileBaseParam));
        }
    }

    protected int GetValidPosToSort(TilebaseController tileBaseParam)
    {
        int count = 0;
        int tilePos = 0;
        for (int i = 0; i < lsTilebaseClicked.Count; i++)
        {
            if (tileBaseParam.id == lsTilebaseClicked[i].id)
            {
                count++;
                tilePos = i;
            }
        }
        if (count != 0) return tilePos + 1;
        return lsTilebaseClicked.Count;
    }

    public virtual void MoveTileToTarget(TilebaseController tile, Vector3 targetPos)
    {
        tile.gameObject.transform.DOMove(targetPos, 0.5f);
    }

    protected void RearrangeSlotSort(int index)
    {
        if (index == 0) return;
        for (int i = lsTilebaseClicked.Count; i > index; i--)
        {
            MoveTileToTarget(lsTilebaseClicked[i - 1], lsSlotSort[i].position);
        }
    }

    private IEnumerator HandleTileMatch(TilebaseController tile)
    {
        if (lsTilebaseClicked.Count < 3) yield break;

        int count = 0;
        List<TilebaseController> matchedTiles = new List<TilebaseController>();

        for (int i = 0; i < lsTilebaseClicked.Count; i++)
        {
            if (tile.id == lsTilebaseClicked[i].id)
            {
                count++;
                matchedTiles.Add(lsTilebaseClicked[i]);
                if (count == 3)
                {
                    Vector3 particlePos = matchedTiles[1].transform.position;
                    yield return new WaitForSeconds(0.3f);

                    foreach (var t in matchedTiles)
                    {
                        if (t != null)
                        {
                            t.transform.DOScale(Vector3.zero, 0.1f).SetDelay(0.2f);
                        }
                    }
                    yield return new WaitForSeconds(0.5f);

                    if (matchedParticle != null)
                    {
                        matchedParticle.transform.position = particlePos;
                        matchedParticle.Play();
                    }

                    yield return new WaitForSeconds(0.1f);

                    foreach (var t in matchedTiles)
                    {
                        if (t != null)
                        {
                            t.transform.DOKill();
                            Destroy(t.gameObject);
                        }
                    }

                    lsTilebaseClicked.RemoveAll(t => matchedTiles.Contains(t));

                    FillTile();
                    yield break;
                }
            }
        }
    }

    protected void FillTile()
    {
        for (int i = 0; i < lsTilebaseClicked.Count; i++)
        {
            if (lsTilebaseClicked[i] != null)
            {
                lsTilebaseClicked[i].transform.DOMove(lsSlotSort[i].position, 0.15f);
            }
        }
    }
}
