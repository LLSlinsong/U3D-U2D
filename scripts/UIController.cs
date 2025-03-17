using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public static UIController Instance { get; private set; }

    private Image fadeScreen;

    public enum FadeState { IDLE, FROM_DARK, TO_DARK };
    public FadeState Fading { get; set; }
    [SerializeField] private float fadeTime = 2.0f;

    public Text healthText;
    public Slider healthSlider;

    public Text wealthText;

    public GameObject pauseScreen;
    public GameObject optionsScreen;

    public Slider musixSlider;
    public Slider sfxSlider;

    [SerializeField] private string mainMenu;
    [SerializeField] private string levelSelect;
    // Start is called before the first frame update
    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        fadeScreen = Instance.transform.Find("FadeScreen").GetComponent<Image>();
        Fading = FadeState.FROM_DARK;
        optionsScreen.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        switch(Fading)
        {
            case FadeState.FROM_DARK:
                fadeScreen.color = new Color(fadeScreen.color.r, fadeScreen.color.g, fadeScreen.color.b, Mathf.MoveTowards(fadeScreen.color.a, 0.0f, fadeTime * Time.deltaTime));
                if (fadeScreen.color.a.Equals(0.0f))
                {
                    Fading = FadeState.IDLE;
                }
                break;
            case FadeState.TO_DARK:
                fadeScreen.color = new Color(fadeScreen.color.r, fadeScreen.color.g, fadeScreen.color.b, Mathf.MoveTowards(fadeScreen.color.a, 1.0f, fadeTime * Time.deltaTime));
                if(fadeScreen.color.a.Equals(1.0f))
                {
                    Fading = FadeState.IDLE;
                }
                break;
        }
    }
    public void Resume()
    {
        GameController.Instance.PauseGame();
    }
    public void OpenOptions()
    {
        optionsScreen.SetActive(true);
    }
    public void CloseOptions()
    {
        optionsScreen.SetActive(false);
    }
    public void LevelSelect()
    {
        SceneManager.LoadScene(levelSelect);
        Time.timeScale = 1.0f;

    }
    public void MainMenu()
    {
        SceneManager.LoadScene(mainMenu);
        Time.timeScale = 1.0f;
    }

    public void SetMusicLevel()
    {
        AudioController.Instance.SetMusicLevel();
    }
    public void SetSFXLevel()
    {
        AudioController.Instance.SetSFXLevel();
    }

}
