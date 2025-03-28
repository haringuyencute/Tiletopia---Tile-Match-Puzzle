using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GameController : MonoBehaviour
{
    private static GameController instance;
    public static GameController Instance { get => instance; }
    public SortController sortController;
    public LevelController levelController;
    public LevelData levelData;
    private void Awake()
    {
        instance = this;    
    }
    private void Start()
    {
        levelController.Init();
        sortController.Init();
    }
}
