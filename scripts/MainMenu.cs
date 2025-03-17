using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private string levelSelect;
    [SerializeField] private string firstLevel;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void NewGame()
    {
        SceneManager.LoadScene(firstLevel);
    }
    public void ContinueGame()
    {
        SceneManager.LoadScene(levelSelect);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
