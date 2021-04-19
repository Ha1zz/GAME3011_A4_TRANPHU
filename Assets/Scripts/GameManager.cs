using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("Panel")]
    [SerializeField] GameObject winPanel;
    [SerializeField] GameObject losePanel;
    [SerializeField] GameObject selectPanel;
    [SerializeField] TMP_Text timerText;
    [SerializeField] TMP_Text playerLevelText;
    [SerializeField] TMP_Text playerLevelWinText;


    public int timer = 0;

    public int levelDifficulty = 0;
    public int timerLimit = 0;
    public static int playerLevel = 0;
    public static bool isSelected = false;

    public static int difficulty = 0;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            //DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this);
        }
        timer = 0;
        if (selectPanel) selectPanel.SetActive(true);
        if(losePanel) losePanel.SetActive(false);
        if(winPanel) winPanel.SetActive(false);
    }


    // Start is called before the first frame update
    void Start()
    {
        isSelected = true;
        timer = timerLimit + playerLevel;
        playerLevelText.text = playerLevel.ToString();
        switch(difficulty)
        {
            case 2:
                MediumLevel();
                break;
            case 3:
                HardLevel();
                break;
            default:
                EasyLevel();
                break;
        }
        StartCoroutine("CountDown");
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("isSelected" + isSelected);
        if (isSelected)
        {
            if (timer <= 0)
            {
                losePanel.SetActive(true);
            }
        }
    }

    public void EasyLevel()
    {
        levelDifficulty = 2;
        timerLimit = 20;
        timer = timerLimit + playerLevel;
        if (playerLevelText) playerLevelText.text = playerLevel.ToString();
    }

    public void MediumLevel()
    {
        levelDifficulty = 3;
        timerLimit = 15;
        timer = timerLimit + playerLevel;
        playerLevelText.text = playerLevel.ToString();
    }

    public void HardLevel()
    {
        levelDifficulty = 4;
        timerLimit = 10;
        timer = timerLimit + playerLevel;
        playerLevelText.text = playerLevel.ToString();
    }

    IEnumerator CountDown()
    {
        if (timerText) timerText.text = timer.ToString();
        yield return new WaitForSeconds(1.0f);
        timer--;
        StartCoroutine("CountDown");
    }


    public void WinCondition()
    {
        winPanel.SetActive(true);
        isSelected = false;
        playerLevel++;
        playerLevelWinText.text = playerLevel.ToString();
    }

    public void LoseCondition()
    {
        losePanel.SetActive(true);
        isSelected = false;
    }


    public void Replay()
    {
        SceneManager.LoadScene("Menu");
    }

    public void Quit()
    {
        Application.Quit();
    }

}
