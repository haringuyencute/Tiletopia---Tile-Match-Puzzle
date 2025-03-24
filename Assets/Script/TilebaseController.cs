using DG.Tweening;
using System;
using System.Collections.Generic;
using UnityEngine;


public class TilebaseController : MonoBehaviour
{
    public int id;
    public List<TilebaseController> lsTilesLowerFloor;
    public SpriteRenderer spriteRenderer;
    public List<TilebaseController> lsTileHigher;
    string layerName;
    [SerializeField] private bool isChangeColor;




    public void HandleMoveLocal(Transform parentTransform, Action action)
    {
        this.transform.parent = parentTransform;
        this.transform.DOLocalMove(Vector3.zero, 0.5f).OnComplete(delegate { action?.Invoke(); });
    }
    private void OnMouseDown()
    {
        if (lsTileHigher.Count > 0)
        {
            return;
        }
        GameController.Instance.sortController.HandleParent(this);
        PlayerPrefs.SetString("lastFloor", layerName);
        PlayerPrefs.Save();
        this.gameObject.layer = LayerMask.NameToLayer("floor1");
    }
    private void Update()
    {
        ResetColor();
        if (Input.GetMouseButtonDown(1))
        {
            layerName = PlayerPrefs.GetString("lastFloor", "");
            this.gameObject.layer = LayerMask.NameToLayer(layerName);
        }
    }
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        layerName = LayerMask.LayerToName(this.gameObject.layer);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Tile")
        {
            int currentLayerIndex = gameObject.layer;
            int triggerLayerIndex = collision.gameObject.layer;
            if ((currentLayerIndex - triggerLayerIndex) == -1)
            {
                TilebaseController tile = collision.gameObject.GetComponent<TilebaseController>();
                if (tile != null) lsTileHigher.Add(tile);
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        for (int i = 0; i < lsTileHigher.Count; i++)
        {
            if (lsTileHigher[i].gameObject.name == collision.gameObject.name)
            {
                lsTileHigher.RemoveAt(i);
                break;
            }
        }
    }
    private void ResetColor()
    {
        if (lsTileHigher.Count > 0)
        {
            this.spriteRenderer.color = Color.gray;
        }
        if (lsTileHigher.Count == 0)
        {
            this.spriteRenderer.color = Color.white;
        }
    }
}
