using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
namespace Bayu
{
    public class MenuManager : MonoBehaviour
    {
        public static MenuManager instance;

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

        public void GoToFamilyGame()
        {
            LoadScene(1, (onComplete) =>
            {
                GameManager.instance?.StartGame();
                Debug.Log(GameManager.instance);
            });
        }

        public void GoToFruitGame()
        {
            LoadScene(2, (onComplete) =>
            {
                GameManager.instance?.StartGame();
                Debug.Log(GameManager.instance);
            });
        }

        public void GoToFamilyCamus()
        {
            LoadScene(3);
        }

        public void GoToFruitCamus()
        {
            LoadScene(4);
        }

        public void LoadScene(int index, Action <AsyncOperation> onComplete = null)
        {
            var handler = SceneManager.LoadSceneAsync(index);
            handler.completed += onComplete;
        }
        public void Quit()
        {
                Application.Quit();
        }
    }
}