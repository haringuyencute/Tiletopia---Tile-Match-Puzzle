using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    public void Init()
    {
        var currentLevel = Resources.Load<GameObject>("Level/Level_" + PlayerPrefs.GetInt("CurrentLevel", 1));
        Instantiate(currentLevel);
        //GameController.Instance.levelData.GetComponent<LevelData>();
        //GameController.Instance.levelData.current = GameController.Instance.levelData.getLevel(PlayerPrefs.GetInt("CurrentLevel",1));
    }

}
