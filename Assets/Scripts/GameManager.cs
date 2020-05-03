using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField]
    private float gameTime = 60f;

    [Header("Setup stuff")]
    [SerializeField]
    private EndReceptionistStuff endReceptionistStuff = default;
    [SerializeField]
    private ReceptionistStuff receptionistStuff = default;
    [SerializeField]
    private Text timerText = default;
    [SerializeField]
    private GameObject introScreen = default;
    [SerializeField]
    private GameObject wonScreen = default;
    [SerializeField]
    private GameObject lostScreen = default;
    [SerializeField]
    private GameObject finalScreen = default;
    [SerializeField]
    private PlayableCharacter player = default;
    [SerializeField]
    private Slider sliderSuitcase = default;
    [SerializeField]
    private TextMeshProUGUI scoreText = default;
    [SerializeField]
    private TextMeshProUGUI scoreTimeText = default;
    [SerializeField]
    private TextMeshProUGUI scoreClothesText = default;

    private float timer;
    private bool gameStarted = false;
    private bool isPlaying = false;

    // Start is called before the first frame update
    void Start()
    {
        int randomGood = Random.Range(0, 4);
        endReceptionistStuff.SetGoodReceptionist(randomGood);
        receptionistStuff.SetGoodDoor(randomGood);
        timer = gameTime;
        introScreen.SetActive(true);
        Time.timeScale = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameStarted) {
            if (Input.GetButtonDown("Interact")) {
                StartGame();
            }
            else {
                return;
            }
        }

        if (gameStarted && !isPlaying) {
            if (Input.GetButtonDown("Interact")) {
                if (wonScreen.activeSelf) {
                    wonScreen.SetActive(false);
                    finalScreen.SetActive(true);
                }
                else {
                    Scene scene = SceneManager.GetActiveScene();
                    SceneManager.LoadScene(scene.name);
                }
            }
        }
        
        if (isPlaying) {
            timer -= Time.deltaTime;
            if (timer <= 0) {
                timerText.text = SecondsToText(0);
                Lost();
                return;
            }
            timerText.text = SecondsToText(timer);
            sliderSuitcase.value = player.GetPercentSuitcaseFill();
        }
    }

    public void StartGame() {
        gameStarted = true;
        isPlaying = true;
        introScreen.SetActive(false);
        Time.timeScale = 1;
    }

    public void Lost() {
        isPlaying = false;
        lostScreen.SetActive(true);
        Time.timeScale = 0;
    }

    public void Won() {
        isPlaying = false;
        Time.timeScale = 0;
        int score = (int)(timer * 1000 * player.GetPercentSuitcaseFill());
        int scoreTime = (int)(timer * 1000);
        int scoreLost = scoreTime - score;
        scoreText.text = "Score : " + score;
        scoreTimeText.text = "Time left : +" + scoreTime;
        scoreClothesText.text = "Missing : -" + scoreLost;
        wonScreen.SetActive(true);
    }

    private string SecondsToText(float time) {
        return string.Format("{0}:{1:00}", (int)(time / 60), (int)(time % 60));
    }
}
