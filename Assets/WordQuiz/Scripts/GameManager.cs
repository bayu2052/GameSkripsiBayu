using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Bayu
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager instance;
        private GameStatus gameStatus;
        public GameStatus GameStatus => gameStatus;


        [SerializeField] private GameObject gameOverPanel;
        [SerializeField] private GameObject pausePanel;


        private void Awake()
        {
            if (instance == null)
                instance = this;
            else
            {
                Destroy(instance);
                instance = this;
            }
        }

        public void StartGame()
        {
            SetGameStatus(GameStatus.Playing);
            Timer.instance.StartTimer();
            Debug.Log("Game started.");

        }

        public void ShowGameOver()
        {
            gameOverPanel.SetActive(true);
        }

        public void RestartGame()
        {
            QuizManager.instance.SetQuestionIndex(0);
            gameOverPanel.SetActive(false);
            pausePanel.SetActive(false);
            QuizManager.instance.winPanel.SetActive(false);
            QuizManager.instance.SetQuestion();
           // Timer.instance.ResetTimer();
            Timer.instance.StartTimer();
            Time.timeScale = 1f;
            Debug.Log("Game direstart .");
        }

        public void GoHome()
        {
            
            SceneManager.LoadSceneAsync(0);
        }

        public void SetGameStatus(GameStatus gameStatus)
        {
            this.gameStatus = gameStatus;
            Debug.Log(gameStatus);
        }

        public void PauseGame()
        {
            SetGameStatus(GameStatus.Paused);
            Timer.instance.StopTimer();
            pausePanel.SetActive(true);
            Time.timeScale = 0f;
            Debug.Log("game di-pause");
        }

        public void ResumeGame()
        {
            SetGameStatus(GameStatus.Playing);
            Timer.instance.IsRunning = true;
            pausePanel.SetActive(false);
            Time.timeScale = 1f;
            Debug.Log("game di-resume");
        }

    }
}