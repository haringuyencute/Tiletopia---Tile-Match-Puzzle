using DG.Tweening;
using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class TilebaseController : MonoBehaviour
{
    public int id;
    public List<TilebaseController> lsTilesLowerFloor;
    public SpriteRenderer spriteRenderer;
    public List<TilebaseController> lsTileHigher;
    [SerializeField] private bool isChangeColor;
    public BoxCollider2D boxCollider2D;
    public Rigidbody2D rb;
    private void Update()
    {
        ResetColor();
    }
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
       
        rb = GetComponent<Rigidbody2D>();
        boxCollider2D = GetComponent<BoxCollider2D>();
    }
    public void HandleMoveLocal(Transform parentTransform, Action action)
    {
        this.transform.parent = parentTransform;
        this.transform.DOLocalMove(Vector3.zero, 0.5f).OnComplete(delegate { action?.Invoke();}).SetEase(Ease.InQuad);
    }
    private void OnMouseDown()
    {
        if (lsTileHigher.Count > 0 || GameController.Instance.CheckLoseCondition())
        {
            return;
        }
        boxCollider2D.enabled = false;
        GameController.Instance.SortControllerRemake.HandleOnMouseDown(this);
        
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        int currentLayerIndex = gameObject.layer;
        int triggerLayerIndex = collision.gameObject.layer;
        if ((currentLayerIndex - triggerLayerIndex) == -1)
        {
            TilebaseController tile = collision.gameObject.GetComponent<TilebaseController>();
            if (tile != null) lsTileHigher.Add(tile);
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
            this.spriteRenderer.color = Color.Lerp(Color.gray, Color.white, 2f);
           
        }
    }
}
