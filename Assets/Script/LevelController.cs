using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    public void GenarateLevel()
    {
        var currentLevel = Resources.Load<GameObject>("Level/Level_" + GameController.Instance.currentLevel);
        Instantiate(currentLevel);
        GameController.Instance.numOfTile = FindObjectsOfType<TilebaseController>().Length;
    }

}
