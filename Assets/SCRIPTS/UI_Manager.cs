using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UI_Manager : MonoBehaviour
{
    [SerializeField]
    TMP_Text scoreText;

    [SerializeField]
    GameObject[] healthBars;

    [SerializeField]
    GameObject panelLost;

    [SerializeField]
    GameObject panelWin;

    [SerializeField]
    GameObject wavePanel;

    [SerializeField]
    TMP_Text waveText;

    [SerializeField]
    GameObject menuPanel;

    [SerializeField]
    Button healthButton;

    [SerializeField]
    Button shieldButton;

    [SerializeField]
    Button powerUpButton;

    [SerializeField]
    GameObject panelHealthObstacle;

    [SerializeField]
    GameObject panelPowerUpObstacle;

    [SerializeField]
    internal GameObject soldier;

    [SerializeField]
    Button pauseButton;

    [SerializeField]
    GameObject image;

    [SerializeField]
    GameObject playButtonObject;

    float timeCurrent;

    static internal bool sceneBool = false;


    static internal bool soldierFreeze = false;

    static internal bool soldier_health_flag = false;

    static internal bool zombieOneAttack = false;

    static internal bool shieldFlag = false;

    ZombieGenerator zombieGenerator;

    [SerializeField]
    Button playButton;

    [SerializeField]
    TMP_Text playText;

    [SerializeField]
    Button quitButton;

    [SerializeField]
    GameObject quitButtonObject;



    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 0;
        zombieGenerator = ZombieGenerator.Instance;

        playButton.onClick.AddListener(playButtonTapped);
        quitButton.onClick.AddListener(quitButtonTapped);

        healthButton.onClick.AddListener(healthButtonTapped);
        shieldButton.onClick.AddListener(shieldButtonTapped);
        powerUpButton.onClick.AddListener(powerUpButtonTapped);

        pauseButton.onClick.AddListener(pauseButtonTapped);
    }

    // Update is called once per frame
    void Update()
    {
        if (sceneBool)
        {
            Time.timeScale = 1;
            sceneBool = false;
        }
        scoreText.text = "Score: " + Soldier_Controller.score.ToString();

        if (soldier_health_flag && Soldier_Controller.health_soldier >= 0)
        {
            for (int i = 0; i < Soldier_Controller.health_soldier; i++)
            {
                healthBars[i].SetActive(true);
            }
            for (int i = Soldier_Controller.health_soldier; i < 5; i++)
            {
                healthBars[i].SetActive(false);
            }
            soldier_health_flag = false;
        }

        if (Soldier_Controller.health_soldier == 0)
        {
            soldier.GetComponent<CharacterController>().enabled = false;
            soldier.GetComponent<CapsuleCollider>().enabled = true;
            soldier.transform.position = new Vector3(soldier.transform.position.x, 0.05f, soldier.transform.position.z);
            soldier.GetComponent<Animator>().SetBool("Death", true);
            soldierFreeze = true;
            Invoke("youLost", 3f);
        }

        if (zombieGenerator.gameOver)
        {
            soldierFreeze = true;
            Invoke("youWin", 1.5f);
        }

        if (zombieGenerator.waveChanged)
        {
            soldier.GetComponent<Animator>().speed = 0;
            soldierFreeze = true;
            wavePanel.SetActive(true);
            waveText.text = zombieGenerator.wave.ToString().Split('e', 4)[0].ToUpper() + "E " + zombieGenerator.wave.ToString().Split('e', 4)[1];
            Invoke("removeWavePanel", 1.5f);
            zombieGenerator.waveChanged = false;
        }

        if (Soldier_Controller.score == 6)
        {
            panelHealthObstacle.SetActive(false);
        }

        if (Soldier_Controller.score == 8)
        {
            panelPowerUpObstacle.SetActive(false);
        }
    }

    void removeWavePanel()
    {
        wavePanel.SetActive(false);
        if (zombieGenerator.wave != ZombieGenerator.Wave.wave1)
        {
            Invoke("menuPanelCreator", 0.5f);
        }

        if (zombieGenerator.wave == ZombieGenerator.Wave.wave1)
        {
            soldierFreeze = false;
            soldier.GetComponent<Animator>().speed = 1;
        }
    }

    void menuPanelCreator()
    {
        menuPanel.SetActive(true);
    }

    void healthButtonTapped()
    {
        if (Soldier_Controller.score == 6)
        {
            Soldier_Controller.health_soldier = 5;
            soldier_health_flag = true;
            Invoke("menuPanelDestroyer", 1f);
        }
    }

    void shieldButtonTapped()
    {
        shieldFlag = true;
        Invoke("menuPanelDestroyer", 1f);
    }

    void powerUpButtonTapped()
    {
        if (Soldier_Controller.score == 8)
        {
            zombieOneAttack = true;
            Invoke("menuPanelDestroyer", 1f);
        }
    }

    void menuPanelDestroyer()
    {
        menuPanel.SetActive(false);
        soldierFreeze = false;
        soldier.GetComponent<Animator>().speed = 1;
    }

    void youLost()
    {
        panelLost.SetActive(true);
    }

    void youWin()
    {
        panelWin.SetActive(true);
    }

    void pauseButtonTapped()
    {
        Time.timeScale = 0;
        image.SetActive(true);
        playButtonObject.SetActive(true);
        quitButtonObject.SetActive(true);
    }

    void playButtonTapped()
    {
        image.SetActive(false);
        playButtonObject.SetActive(false);
        quitButtonObject.SetActive(false);
        Invoke("resumeTextCreator", 1f);
        sceneBool = true;
    }

    void resumeTextCreator()
    {
        playText.text = "resume";
    }

    void quitButtonTapped()
    {
        Application.Quit();
    }
}
