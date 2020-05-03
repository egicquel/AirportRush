using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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

    
    
    private float timer;
    private bool isPlaying = false;

    // Start is called before the first frame update
    void Start()
    {
        int randomGood = Random.Range(0, 4);
        endReceptionistStuff.SetGoodReceptionist(randomGood);
        receptionistStuff.SetGoodDoor(randomGood);
        timer = gameTime;
        StartGame();
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlaying) {
            timer -= Time.deltaTime;
            if (timer <= 0) {
                timerText.text = SecondsToText(0);
                isPlaying = false;
                return;
            }
            timerText.text = SecondsToText(timer);
        }
    }

    public void StartGame() {
        isPlaying = true;
    }

    private string SecondsToText(float time) {
        return (int)(time / 60) + ":" + (int)(time % 60);
    }
}
