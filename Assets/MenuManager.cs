using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuManager : MonoBehaviour
{

    public void EasyLevel()
    {
        GameManager.difficulty = 1;
        SceneManager.LoadScene("Puzzle");
    }

    public void MediumLevel()
    {
        GameManager.difficulty = 2;
        SceneManager.LoadScene("Puzzle");
    }

    public void HardLevel()
    {
        GameManager.difficulty = 3;
        SceneManager.LoadScene("Puzzle");
    }

    public void Quit()
    {
        Application.Quit();
    }
}
