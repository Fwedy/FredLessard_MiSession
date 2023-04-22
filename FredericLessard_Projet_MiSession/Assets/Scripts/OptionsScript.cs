using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class OptionsScript : MonoBehaviour
{
    [SerializeField] private Slider VolumeSlider;
    [SerializeField] private TextMeshProUGUI roundTXT;
    [SerializeField] private LevelData mainMenu;

    [SerializeField] private GameManager gameManager;
    [SerializeField] private PlayerMovement playerMovement;
    // Start is called before the first frame update
    void Start()
    {
        if (!PlayerPrefs.HasKey("mainVolume"))
        {
            PlayerPrefs.SetFloat("mainVolume", 1);
            LoadPP();
        }

        else
        {
            LoadPP();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gameObject.activeInHierarchy)
                UnPause();
        }
    }

    public void VolumeChanged()
    {
        AudioListener.volume = VolumeSlider.value;
        SavePP();
    }

    private void LoadPP()
    {
        VolumeSlider.value = PlayerPrefs.GetFloat("mainVolume");
    }

    private void SavePP()
    {
        PlayerPrefs.SetFloat("mainVolume", VolumeSlider.value);
    }
    
    private void UnPause()
    {
        roundTXT.text = gameManager.roundNumber.ToString();
        Time.timeScale = 1;
        playerMovement.PauseGame();

        gameObject.SetActive(false);
    }
   

    private void OnEnable()
    {
        Time.timeScale = 0;
        roundTXT.text = "Round: " + gameManager.roundNumber;
        playerMovement.PauseGame();
    }

    public void OnResumeBTNClick()
    {
        UnPause();
    }

    public void OnMainMenuBTNClick()
    {
        PersistentData.Serialize(gameManager.coins,"",null);

        SceneManager.LoadScene(mainMenu.sceneName);
    }

}
