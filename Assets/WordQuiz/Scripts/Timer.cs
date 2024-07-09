using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Bayu
{
    public class Timer : MonoBehaviour
    {
        public static Timer instance;

        [SerializeField] private float timeLimit = 10f;
        [SerializeField] private TMP_Text timerText;
        private float currentTime;
        private bool isRunning = false;

        public float CurrentTime => currentTime;

        public bool IsRunning
        {
            get => isRunning;
            set => isRunning = value;
        }
        

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

        void Update()
        {
            if (isRunning)
            {
                
                currentTime -= Time.deltaTime;
                timerText.text = Mathf.Round(currentTime).ToString();

                if (currentTime <= 0)
                {
                    TimerEnded();
                }
            }
         
            //  Debug.Log("update timer. isRunning" + isRunning + ", currentTime: " + currentTime);
        }

        bool _isRunning;

        private void LateUpdate()
        {
            if(_isRunning == isRunning)
            {
                Debug.Log(isRunning);
                _isRunning = !isRunning;
            }
        }

        public void ResetTimer()
        {
            currentTime = timeLimit;
            isRunning = true;
            Debug.Log("timer direset. isRunning" + isRunning);
        }

        public void StopTimer()
        {
            isRunning = false;
            Debug.Log("timer dihentikan. isRunning" + isRunning);
        }

        
        public void StartTimer()
        {
            currentTime = timeLimit;
            isRunning = true;
            Debug.Log("Timer started. isRunning: " + isRunning);
            
        }

       

        void TimerEnded()
        {
            isRunning = false;
            if (GameManager.instance.GameStatus == GameStatus.Playing)
            {
                GameManager.instance.SetGameStatus(GameStatus.GameOver);
                GameManager.instance.ShowGameOver();
            }
        }
    }
}