using System.Collections;
using System.Collections.Generic;
using DG.Tweening.Core.Easing;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class SkillButtonUI : MonoBehaviour
{
    public static SkillButtonUI Instance;
    [Header("Skills")]
    [Header("Undo")]
    [SerializeField] private Button undoSkillButton;
    [SerializeField] private Button undoSkillADSButton;
    [SerializeField] private TextMeshProUGUI undoSkillAmount;
    [SerializeField] private TextMeshProUGUI adUndoSKill;

    [Header("Hint")]
    [SerializeField] private Button hintSkillButton;
    [SerializeField] private Button hintSkillADSButton;
    [SerializeField] private TextMeshProUGUI hintSkillAmount;
    [SerializeField] private TextMeshProUGUI adHintSKill;

    [Header("Shuffle")]
    [SerializeField] private Button shuffleSkillButton;
    [SerializeField] private Button shuffleSkillADSButton;
    [SerializeField] private TextMeshProUGUI shuffleSkillAmount;
    [SerializeField] private TextMeshProUGUI adShuffleSKill;

    public int _undoSKillAmount;
    public int _hintSkillAmount;
    public int _shuffleSkillAmount;

    private void Awake()
    {
        if (Instance != null) Destroy(Instance);
        else Instance = this;
        LoadAmountSkill();
        UpdateSkill();
    }
    public void Start()
    {
        RewardedAdsUndoButton.onRewardADUndoRewarded += ADUndo;
        RewardedAdsHintButton.onRewardADHintRewarded += ADHint;
        RewardedAdsShuffleButton.onRewardADShuffleRewarded += ADShuffle;
    }
    public void OnDestroy()
    {
        RewardedAdsUndoButton.onRewardADUndoRewarded -= ADUndo;
        RewardedAdsHintButton.onRewardADHintRewarded -= ADHint;
        RewardedAdsShuffleButton.onRewardADShuffleRewarded -= ADShuffle;
    }
    public void Undo()
    {
        SkillManager.instance.UndoTile();
        UpdateSkill();
    }
    public void Hint()
    {
        SkillManager.instance.HintTile();
        UpdateSkill();
    }
    public void Shuffle()
    {
        SkillManager.instance.ShuffleTile();
        _shuffleSkillAmount--;
        UpdateSkill();
    }
    public void ADUndo()
    {
        _undoSKillAmount++;
        UpdateSkill();
    }public void ADHint()
    {
        _hintSkillAmount++;
        UpdateSkill();
    }public void ADShuffle()
    {
        _shuffleSkillAmount++;
        UpdateSkill();
    }
    private void UpdateSkill()
    {
        undoSkillAmount.text = _undoSKillAmount.ToString();
        hintSkillAmount.text = _hintSkillAmount.ToString();
        shuffleSkillAmount.text = _shuffleSkillAmount.ToString();

        if (_undoSKillAmount <= 0)
        {
            undoSkillAmount.gameObject.SetActive(false);
            adUndoSKill.gameObject.SetActive(true);
            undoSkillButton.interactable = false;
            undoSkillADSButton.enabled = true;
        }
        else
        {
            undoSkillAmount.gameObject.SetActive(true);
            adUndoSKill.gameObject.SetActive(false);
            undoSkillButton.interactable = true;
            undoSkillADSButton.enabled = false;
        }

        if (_hintSkillAmount <= 0)
        {
            hintSkillAmount.gameObject.SetActive(false);
            adHintSKill.gameObject.SetActive(true);
            hintSkillButton.interactable = false;
            hintSkillADSButton.enabled = true;
        }
        else
        {
            hintSkillAmount.gameObject.SetActive(true);
            adHintSKill.gameObject.SetActive(false);
            hintSkillButton.interactable = true;
            hintSkillADSButton.enabled = false;
        }

        if (_shuffleSkillAmount <= 0)
        {
            shuffleSkillAmount.gameObject.SetActive(false);
            adShuffleSKill.gameObject.SetActive(true);
            shuffleSkillButton.interactable = false;
            shuffleSkillADSButton.enabled = true;
        }
        else
        {
            shuffleSkillAmount.gameObject.SetActive(true);
            adShuffleSKill.gameObject.SetActive(false);
            shuffleSkillButton.interactable = true;
            shuffleSkillADSButton.enabled = false;
        }

        SaveAmountSkill();
    }
    private void LoadAmountSkill()
    {
        _undoSKillAmount = PlayerPrefs.GetInt("Amount Undo Skill", 3);
        _hintSkillAmount = PlayerPrefs.GetInt("Amount Hint Skill", 3);
        _shuffleSkillAmount = PlayerPrefs.GetInt("Amount Shuffle Skill", 3);
    }
    private void SaveAmountSkill()
    {
        PlayerPrefs.SetInt("Amount Undo Skill", _undoSKillAmount);
        PlayerPrefs.SetInt("Amount Hint Skill", _hintSkillAmount);
        PlayerPrefs.SetInt("Amount Shuffle Skill", _shuffleSkillAmount);
    }
}
