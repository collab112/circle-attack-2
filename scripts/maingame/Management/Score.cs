using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{

    public int score;
    public float time;
    bool timerOn;
    public float extraScore;

    public Text winOrLose;
    public Text timer;
    public Text scoreText;
    public Text timeBonus;
    public Text cashLifeBonus;
    public Text totalScore;

    public AudioSource musicSource;
    public AudioSource endMusicSource;
    public AudioClip winClip;
    public AudioClip loseClip;

    void Awake() {

        timerOn = true;
        score = 0;

        musicSource = gameObject.GetComponent<AudioSource>();
        endMusicSource = (GameObject.Find("AudioSourceEnd")).GetComponent<AudioSource>();

    }

    public void updateScore(int update) {

        score += update;

    }

    void Update() {

        if ( timerOn ) {

            time += Time.deltaTime; // start a timer

        }
    }

    public void getFinalScore(bool won) {

        int newScore;

        // This method is only called when the game is over (lose or win) and a different soundtrack plays
        // for either scenario, so the ingame music is stopped
        musicSource.Stop();

        if ( won ) {

            // On win, play winning music, calculate score bonuses and total score and display them on text
            endMusicSource.PlayOneShot(winClip, 1.0f);

            extraScore = gameObject.GetComponent<Life>().health + gameObject.GetComponent<Cash>().cash;
            newScore = (int)(Mathf.Pow(1.01f, extraScore)) + (int)(210000/time) + score;

            winOrLose.text = "WIN";
            timer.text = "TIME: " + ((int)time).ToString() + " SECONDS";

            scoreText.text = "SCORE: " + score.ToString();
            timeBonus.text = "TIME BONUS: " + ((int)(210000/time)).ToString();
            cashLifeBonus.text = "LIFE + CASH BONUS: " + (Mathf.Pow(1.01f, extraScore)).ToString();
            totalScore.text = "TOTAL SCORE: " + newScore.ToString();

        } else {

            // On lose, play losing music. No score bonuses are awarded when losing
            endMusicSource.PlayOneShot(loseClip, 1.0f);

            winOrLose.text = "LOSE";
            newScore = score;

            timer.text = "TIME: " + ((int)time).ToString() + " SECONDS";

            scoreText.text = "SCORE: " + score.ToString();
            timeBonus.text = "TIME BONUS: 0";
            cashLifeBonus.text = "LIFE + CASH BONUS: 0";
            totalScore.text = "TOTAL SCORE: " + score.ToString();

        }

        timerOn = false;

    }

}