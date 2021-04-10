using UnityEngine;
using System.Collections;
using TMPro;
public class XPManager : MonoBehaviour
{
    // How much xp needs from L1 to L2
    [SerializeField] private int BaseLevelThreshold;

    // The multiplier for each next level, eg: L3-L2 = (L2-L1)*2
    [SerializeField] private int LevelThresholdModifier;

    [SerializeField] private TextMeshProUGUI LevelNum;
    [SerializeField] private TextMeshProUGUI XPNum;
    [SerializeField] private UIGradientBar ProgressBar;

    private int totalXP = 0;
    private int currentLevel = 1;
    private int nextLevelThreshold;
    private int newXPForThisLevel = 0;
    private int thisLevelFullXP = 0;

    // Use this for initialization
    void Start()
    {
        thisLevelFullXP = BaseLevelThreshold;
        nextLevelThreshold = BaseLevelThreshold;

        updateUI_Level();
        updateUI_XP();
        updateUI_progressBar();
        //Debug.Log(currentLevel);
        //Debug.Log(thisLevelFullXP);
        //Debug.Log(nextLevelThreshold);
        //Debug.Log("_______________________");
    }

    //// Update is called once per frame
    //void Update()
    //{

    //}

    public void GainXP(int XP)
    {
        totalXP += XP;
        newXPForThisLevel += XP;
        CheckIfLevelUp();
    }

    // update nextLevelThreshold and thisLevelFullXP
    private void GetNextLevelThreshold()
    {
        int lastLevelThreshold = nextLevelThreshold;
        nextLevelThreshold = nextLevelThreshold + (int)Mathf.Pow(LevelThresholdModifier, currentLevel-1) * BaseLevelThreshold;
        thisLevelFullXP = nextLevelThreshold - lastLevelThreshold;
        //Debug.Log(currentLevel);
        //Debug.Log(thisLevelFullXP);
        //Debug.Log(nextLevelThreshold);
        //Debug.Log("_______________________");
    }

    private void CheckIfLevelUp()
    {
        // Level up, update current level and next level threshold
        if (totalXP >= nextLevelThreshold)
        {
            currentLevel++;
            newXPForThisLevel = totalXP - nextLevelThreshold;

            GetNextLevelThreshold();
            updateUI_Level();
        }
        updateUI_XP();
        updateUI_progressBar();

    }

    // update level progress bar
    private void updateUI_progressBar()
    {
        float levelPercent = (float)newXPForThisLevel / thisLevelFullXP;
        // update progress bar with precentage
        ProgressBar.SetValue(levelPercent*100);
    }

    // update level number
    private void updateUI_Level()
    {
        LevelNum.text = "Lv. " + currentLevel.ToString();
    }

    // update xp number
    private void updateUI_XP()
    {
        XPNum.text = "XP: " + newXPForThisLevel.ToString();
    }
}
