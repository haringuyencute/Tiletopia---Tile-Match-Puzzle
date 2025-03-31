using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GameController : MonoBehaviour
{
    private static GameController instance;
    public static GameController Instance { get => instance; }
    public LevelController levelController;
    public SortControllerRemake SortControllerRemake;
    public int numOfTile;
    private void Awake()
    {
        levelController.Init();
        instance = this;
        numOfTile = FindObjectsOfType<TilebaseController>().Length;
        SortControllerRemake = transform.GetComponentInChildren<SortControllerRemake>();
        levelController = GetComponent<LevelController>();
    }
    private void Start()
    {
    }
    private void Update()
    {
        if(CheckWinCondition() && SortControllerRemake.lsTilebaseClicked.Count == 0)
        {
            Debug.Log("Win");
        }
        if(CheckLoseCondition())
        {
            Debug.Log("Lose");
        }
    }
    public bool CheckWinCondition()
    {
        return numOfTile == 0;
    }
    public bool CheckLoseCondition()
    {
        return SortControllerRemake.lsTilebaseClicked.Count >= 8; // sua lai 
    }
}
