using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bayu
{
    public class Score : MonoBehaviour
    {
        public static Score instance;

        [SerializeField] private GameObject scoreObject1; // Game object untuk waktu >= 1 dan < 4 detik
        [SerializeField] private GameObject scoreObject2; // Game object untuk waktu >= 4 dan < 7 detik
        [SerializeField] private GameObject scoreObject3; // Game object untuk waktu >= 7 detik

        private void Awake()
        {
            if (instance == null)
                instance = this;
            else
                Destroy(this.gameObject);
        }

        public void UpdateScore(float timeRemaining)
        {
            // Nonaktifkan semua game object terlebih dahulu
            scoreObject1.SetActive(false);
            scoreObject2.SetActive(false);
            scoreObject3.SetActive(false);

            // Aktifkan game object berdasarkan waktu yang tersisa
            if (timeRemaining >= 14f)
            {
                scoreObject3.SetActive(true);
            }
            else if (timeRemaining >= 8f && timeRemaining < 14f)
            {
                scoreObject2.SetActive(true);
            }
            else if (timeRemaining >= 1f && timeRemaining < 8f)
            {
                scoreObject1.SetActive(true);
            }
        }
    }
}