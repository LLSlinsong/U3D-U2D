using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameController : MonoBehaviour
{
    public static GameController Instance { get;  set; }
    [SerializeField] private GameObject Chair;
    public Vector3 respawnPosition = Vector3.zero;
    [SerializeField] private float respawnTime = 2.0f;

    public int currentWealth = 0;

    [SerializeField] private int levelEndMusice;
    [SerializeField] private string levelToLoad;
    void Awake()
    {
        Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        respawnPosition = PlayerController.Instance.transform.position;
        UpdateWealth(0);
        UIController.Instance.pauseScreen.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            PauseGame();
        }
    }
    public void Respawn()
    {
       
        StartCoroutine(RespawnCoroutine());
    }
    IEnumerator RespawnCoroutine()
    {
        AudioController.Instance.PlaySFX(1);
        PlayerController.Instance.gameObject.SetActive(false);
        //CameraController.Instance.TheCinemachinBrain.enabled = false;
        UIController.Instance.Fading = UIController.FadeState.TO_DARK;
        yield return new WaitForSeconds(respawnTime);
        HealthController.Instance.ResetHealth();
        UIController.Instance.Fading = UIController.FadeState.FROM_DARK;
        PlayerController.Instance.transform.position = respawnPosition;
        Chair.transform.position = respawnPosition;
        PlayerController.Instance.GetComponentInChildren<Animator>().SetBool("Grounded", true);
        yield return new WaitForEndOfFrame();
        PlayerController.Instance.gameObject.SetActive(true);
        //CameraController.Instance.TheCinemachinBrain.enabled = true;
    }
    public void UpdateWealth(int update)
    {
        currentWealth += update;
        UIController.Instance.wealthText.text = currentWealth.ToString();
    }
    public void PauseGame()
    {
        if(UIController.Instance.pauseScreen.activeInHierarchy)
        {
            UIController.Instance.pauseScreen.SetActive(false);
            Time.timeScale = 1.0f;
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            UIController.Instance.pauseScreen.SetActive(true);
            Time.timeScale = 0.0f;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }
    public IEnumerator LevelEndCoroutine()
    {
        yield return new WaitForSeconds(2.0f);
        SceneManager.LoadScene(levelToLoad);
    }
}
