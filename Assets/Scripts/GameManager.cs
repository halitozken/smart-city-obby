using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] public GameObject escPanel;
    private bool esc = false;

    

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            esc = !esc;
        }

        if(esc )
        {
            escPanel.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            escPanel.SetActive(false);
          
        }
    }

    public void Resume()
    {
        esc = false;
    }

    public void Menu()
    {
        SceneManager.LoadScene("Start Screen");
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Level01");
    }


    public void ExitGame()
    {
        Application.Quit();
    }
}
