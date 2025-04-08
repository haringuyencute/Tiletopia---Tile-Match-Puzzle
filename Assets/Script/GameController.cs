using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Unity.VisualScripting;

public class GameController : MonoBehaviour
{
    private static GameController instance;
    public static GameController Instance { get => instance; }
    public LevelController levelController;
    public SortControllerRemake SortControllerRemake;
    public AudioManager audioManager;
    public UIManager UIManager;
    public int numOfTile;
    public int currentLevel;
    public GameScene gameScene;
    private void Awake()
    {
        instance = this;
        currentLevel = PlayerPrefs.GetInt("CurrentLevel",1);
        levelController.GenarateLevel();
        gameScene.Init();
    }
    private void Reset()
    {
        SortControllerRemake = transform.GetComponentInChildren<SortControllerRemake>();
        levelController = FindAnyObjectByType<LevelController>();
        //soundManager = FindAnyObjectByType<SoundManager>();
        //UIManager = FindAnyObjectByType<UIManager>();

    }
  
    public bool CheckLoseCondition()
    {
        return SortControllerRemake.lsTilebaseClicked.Count >= 8; // sua lai 
    }
}
