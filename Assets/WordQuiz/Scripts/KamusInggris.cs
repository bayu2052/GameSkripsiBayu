using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KamusInggris : MonoBehaviour
{
    [SerializeField] private Image imagePanel1; // Panel untuk gambar pertama
    [SerializeField] private Image imagePanel2; // Panel untuk gambar kedua
    [SerializeField] private Button soundButton;
    [SerializeField] private Button upButton;
    [SerializeField] private Button downButton;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip[] audioClips;
    [SerializeField] private Sprite[] imageSprites1; // Gambar pertama
    [SerializeField] private Sprite[] imageSprites2; // Gambar kedua

    private int currentIndex = 0;

    private void Start()
    {
        // Set initial images and sound
        UpdateImageAndSound();

        // Add listeners to buttons
        soundButton.onClick.AddListener(PlaySound);
        upButton.onClick.AddListener(NavigateUp);
        downButton.onClick.AddListener(NavigateDown);

        // Update button visibility
        UpdateButtonVisibility();
    }

    private void PlaySound()
    {
        if (audioClips.Length > 0 && audioSource != null)
        {
            audioSource.clip = audioClips[currentIndex];
            audioSource.Play();
        }
    }

    private void UpdateImageAndSound()
    {
        if (imageSprites1.Length > 0 && imageSprites2.Length > 0)
        {
            imagePanel1.sprite = imageSprites1[currentIndex];
            imagePanel2.sprite = imageSprites2[currentIndex];
        }
    }


    private void NavigateUp()
    {
        if (currentIndex > 0)
        {
            currentIndex--;
            UpdateImageAndSound();
            UpdateButtonVisibility();
        }
    }

    private void NavigateDown()
    {
        if (currentIndex < imageSprites1.Length - 1)
        {
            currentIndex++;
            UpdateImageAndSound();
            UpdateButtonVisibility();
        }
    }

    private void UpdateButtonVisibility()
    {
        // Hide upButton if at the top, otherwise show it
        if (currentIndex == 0)
        {
            upButton.gameObject.SetActive(false);
        }
        else
        {
            upButton.gameObject.SetActive(true);
        }

        // Hide downButton if at bottom, otherwise show it
        if (currentIndex == imageSprites1.Length -1)
        {
            downButton.gameObject.SetActive(false);
        }
        else
        {
            downButton.gameObject.SetActive(true);
        }
    }
}