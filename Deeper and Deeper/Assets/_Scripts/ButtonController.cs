using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class ButtonController : MonoBehaviour
{
    public static ButtonController S;
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private Text numberOfMashrooms;

    private bool gamePused = false;

    private void Awake()
    {
        Time.timeScale = 1f;
        if (S == null) S = this;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            if (!gamePused)
            {
                Pause();
                gamePused = true;
            }
            else
            {
                Continue();
            }
    }

    public void Pause()
    {
        Time.timeScale = 0f;
        pausePanel.SetActive(true);
        //pausePanel.GetComponent<Animation>().Play("PanelAppearance");
        numberOfMashrooms.text = PlayerPrefs.GetInt("PickedMushrooms" + SceneManager.GetActiveScene().name).ToString() + " грибочков из 4";
    }
    public void Continue()
    {
        Time.timeScale = 1f;
        gamePused = false;
        //pausePanel.GetComponent<Animation>().Play("PanelFadeOut");
        StartCoroutine(DelayedDeactivatingPanel());
    }

    private IEnumerator DelayedDeactivatingPanel()
    {
        yield return new WaitForSeconds(0.5f);
        pausePanel.SetActive(false);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void GoToScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
    public void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
