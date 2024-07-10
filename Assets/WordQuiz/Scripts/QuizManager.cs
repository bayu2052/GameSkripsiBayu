
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Bayu
{
    public class QuizManager : MonoBehaviour
    {
        public static QuizManager instance;

        [SerializeField] public GameObject gameComplete;
        [SerializeField] public GameObject winPanel;  // Ubah menjadi public
        [SerializeField] public QuizDataScriptable questionDataScriptable;
        [SerializeField] public Image questionImage;
        [SerializeField] public WordData[] answerWordList;
        [SerializeField] public WordData[] optionsWordList;
        [SerializeField] public AudioClip[] audioWordList;
        [SerializeField] private GameManager gameManager;

        private AudioSource audioSource;
        private char[] wordsArray = new char[12];
        private List<int> selectedWordsIndex;
        public int currentAnswerIndex = 0, currentQuestionIndex = 0;  // Ubah menjadi public
        private bool correctAnswer = true;
        private string answerWord;

        private void Awake()
        {
            if (instance == null)
                instance = this;
            else
                Destroy(this.gameObject);
        }

        void Start()
        {
            Invoke("blabla", 1f);
            
        }

        void blabla()
        {
            selectedWordsIndex = new List<int>();
            SetQuestion();
            audioSource = GetComponent<AudioSource>();
        }

        public void SetQuestionIndex(int index)  // Tambahkan metode ini
        {
            currentQuestionIndex = index;
        }
        
        
        public void SetQuestion()  
        {
            answerWord = questionDataScriptable.questions[currentQuestionIndex].answer;
            questionImage.sprite = questionDataScriptable.questions[currentQuestionIndex].questionImage;

            ResetQuestion();

            selectedWordsIndex.Clear();
            Array.Clear(wordsArray, 0, wordsArray.Length);
            for (int i = 0; i < answerWord.Length; i++)
            {
                wordsArray[i] = char.ToUpper(answerWord[i]);
            }
            for (int j = answerWord.Length; j < wordsArray.Length; j++)
            {
                wordsArray[j] = (char)UnityEngine.Random.Range(65, 90);
            }

            wordsArray = ShuffleList.ShuffleListItems<char>(wordsArray.ToList()).ToArray();
            for (int k = 0; k < optionsWordList.Length; k++)
            {
                optionsWordList[k].SetWord(wordsArray[k]);
            }
            GameManager.instance.StartGame();
        }


        public void ResetQuestion()
        {
            for (int i = 0; i < answerWordList.Length; i++)
            {
                answerWordList[i].gameObject.SetActive(true);
                answerWordList[i].SetWord('_');
            }
            for (int i = answerWord.Length; i < answerWordList.Length; i++)
            {
                answerWordList[i].gameObject.SetActive(false);
            }
            for (int i = 0; i < optionsWordList.Length; i++)
            {
                optionsWordList[i].gameObject.SetActive(true);
            }

            currentAnswerIndex = 0;
        }

        public void SelectedOption(WordData value)
        {
            if (gameManager.GameStatus != GameStatus.Playing || currentAnswerIndex >= answerWord.Length) return;
            selectedWordsIndex.Add(value.transform.GetSiblingIndex());
            value.gameObject.SetActive(false);
            answerWordList[currentAnswerIndex].SetWord(value.wordValue);
            currentAnswerIndex++;
            if (currentAnswerIndex == answerWord.Length)
            {
                correctAnswer = true;
                for (int i = 0; i < answerWord.Length; i++)
                {
                    if (char.ToUpper(answerWord[i]) != char.ToUpper(answerWordList[i].wordValue))
                    {
                        correctAnswer = false;
                        break;
                    }
                }
                if (correctAnswer)
                {
                    gameManager.SetGameStatus(GameStatus.Next);
                    PlayClip(currentQuestionIndex);
                    Invoke(nameof(ShowWinPanel), 1f);
                }
            }
        }

        public void PlayClip(int index)
        {
            if (index >= 0 && index < audioWordList.Length)
            {
                audioSource.clip = audioWordList[index];
                audioSource.Play();
            }
            else
            {
                Debug.Log("Index out of range" + currentAnswerIndex);
            }
        }

        public void ShowWinPanel()
        {
            if (gameManager.GameStatus != GameStatus.GameOver)
            {
                gameManager.SetGameStatus(GameStatus.Next);
                winPanel.SetActive(true);
                Timer.instance.StopTimer();
                Score.instance.UpdateScore(Timer.instance.CurrentTime);
            }
        }

        public void HideWinPanel()
        {
            winPanel.SetActive(false);
            audioSource.clip = null;
            CallNextWord();
        }

        public void CallNextWord()
        {
            if (gameManager.GameStatus == GameStatus.GameOver) return;
            currentQuestionIndex++;
            if (currentQuestionIndex < questionDataScriptable.questions.Count)
            {
                SetQuestion();
            }
            else
            {
                Debug.Log("Game Complete");
                gameComplete.SetActive(true);
            }
        }

        public void ResetLastWord()
        {
            if (selectedWordsIndex.Count > 0)
            {
                int index = selectedWordsIndex[selectedWordsIndex.Count - 1];
                optionsWordList[index].gameObject.SetActive(true);
                selectedWordsIndex.RemoveAt(selectedWordsIndex.Count - 1);

                currentAnswerIndex--;
                answerWordList[currentAnswerIndex].SetWord('_');
            }
        }
    }

    [System.Serializable]
    public class QuestionData
    {
        public Sprite questionImage;
        public string answer;
    }

    public enum GameStatus
    {
        Next,
        Playing,
        GameOver,
        Paused
    }
}

