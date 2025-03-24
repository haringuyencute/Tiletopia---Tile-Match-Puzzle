using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    // Start is called before the first frame update
    public void Init()
    {
        var currentLevel = Resources.Load<GameObject>("Level_" + PlayerPrefs.GetInt("CurrentLevel", 1));
        GameController.Instance.levelData = Instantiate(currentLevel).GetComponent<LevelData>();
        GameController.Instance.levelData.GetComponent<LevelData>();
    }

}
