using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameScene : MonoBehaviour
{
    public Button btnPause;
    [Header("-----------PopUpPause-------------")]
    public GameObject popUpPause;
    public Button btnClose;
    public Button btnHome;
    public Button btnRestart;
    public Button btnSpeaker;
    public Image imgbtnSpeaker;
    public Sprite spriteOnSpeaker;
    public Sprite spriteOffSpeaker;
    public bool isSpeakerOn = true;

    [Header("-------------PopUpWin-----------")]
    public GameObject popUpWin;
    public Button btnHomeWin;
    public Button btnResetWin;
    public Button btnNextLevel;

    [Header("-------------PopUpLose-----------")]
    public GameObject popUpLose;
    public Button btnHomeLose;
    public Button btnResetLose;

    public float checkLoseTime;

    public void Init()
    {
        // PopUp Pause
        btnPause.onClick.AddListener(HandleClickBtnPause);
        btnClose.onClick.AddListener(HandleClickBtnClose);
        btnHome.onClick.AddListener(HandleClickHome);
        btnRestart.onClick.AddListener(HandleClickRestart);
        // popUp Win
        btnNextLevel.onClick.AddListener(HandleClickBtnNextLevel);
        btnHomeWin.onClick.AddListener(HandleClickHome);
        btnResetWin.onClick.AddListener(HandleClickRestart);
        // pop Up Lose
        btnHomeLose.onClick.AddListener(HandleClickHome);
        btnResetLose.onClick.AddListener(HandleClickRestart);
        GameController.Instance.audioManager.Init();
    }
    private void Update()
    {
        if (CheckWinCondition())
        {
            //popUpWin.SetActive(true);
            StartCoroutine(WaitForWin());
        }
        if(checkLose())
        {
            popUpLose.SetActive(true);
        }
       // StartCoroutine(WaitForCheckLose());
        
    }
    #region Handle Pause Game
    public void HandleClickBtnPause()
    {
        popUpPause.SetActive(true);
        TilebaseController.isPause = true;
        
    }

    public void HandleClickBtnClose()
    {
        popUpPause.SetActive(false);

        TilebaseController.isPause = false;
        
    }
    public void HandleClickHome()
    {
        SceneManager.LoadScene("Home");
        
    }

    public void HandleClickRestart()
    {
        SceneManager.LoadScene("GamePlay");
        TilebaseController.isPause = false;
        
    }
    #endregion

    #region Handle Win Game
    public bool CheckWinCondition()
    {
        return GameController.Instance.numOfTile == 0;
    }
    
    public void CheckSameTile(int id)
    {
        foreach (var item in GameController.Instance.SortControllerRemake.lsTilebaseClicked)
        {
            
        }    
    }    
    public IEnumerator WaitForWin()
    {
        yield return new WaitForSeconds(0.5f);
        popUpWin.SetActive(true);
    }    

    public void HandleClickBtnNextLevel()
    {
        GameController.Instance.currentLevel = PlayerPrefs.GetInt("CurrentLevel",1);
        Debug.Log(GameController.Instance.currentLevel);
        GameController.Instance.currentLevel++;
        PlayerPrefs.SetInt("CurrentLevel",GameController.Instance.currentLevel);
        PlayerPrefs.Save();
        Debug.Log(GameController.Instance.currentLevel);
        SceneManager.LoadScene("GamePlay");
    }
    #endregion

    //public IEnumerator WaitForCheckLose()
    //{
    //    yield return new WaitForSeconds(1f);
    //    if (checkLose() && GameController.Instance.SortControllerRemake.Check3Tile == false)
    //    {
    //        popUpLose.SetActive(true);
    //    }
    //}

    
    public bool checkLose()
    {
        // return GameController.Instance.SortControllerRemake.lsTilebaseClicked.Count >= 8;
        if(GameController.Instance.SortControllerRemake.lsTilebaseClicked.Count >= 8 && GameController.Instance.SortControllerRemake.Check3Tile == false)
        {
            checkLoseTime += Time.deltaTime;
            if(checkLoseTime > 1)
            {
                checkLoseTime = 0;
                return true;
            }
        }
        return false;
    }
}
