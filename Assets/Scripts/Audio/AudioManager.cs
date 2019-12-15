using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


namespace FruitScapes.Audio
{
    public class AudioManager : MonoBehaviour
    {
        [SerializeField]
        private AudioClip gameOST, mainTheme, swipe, explosion, lost, win, shuffle, buttonSound, knock;
        private AudioSource musicPlayer;

        public static AudioManager Instance = null;

        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else if (Instance != this)
                Destroy(gameObject);
            DontDestroyOnLoad(gameObject);
            musicPlayer = GetComponent<AudioSource>();
        }

        private void Start()
        {
            if (SceneManager.GetActiveScene().name == "Game")
                PlayLevelTrack();
            else
                PlayMenuTrack();
        }

        public bool CheckMusicOn()
        {
            if (musicPlayer.isPlaying)
                return true;
            else
                return false;
        }

        public void PlayMenuTrack()
        {
            if (soundOn)
            {
                musicPlayer.Stop();
                musicPlayer.clip = mainTheme;
                musicPlayer.Play();
            }
        }

        public void PlayKnock()
        {
            if (soundOn)
            {
                musicPlayer.PlayOneShot(knock, 0.3f);
            }
        }

        public void PlayLevelTrack()
        {
            if (soundOn)
            {
                musicPlayer.Stop();
                musicPlayer.clip = gameOST;
                musicPlayer.Play();
            }
        }

        public void PlayExplosion()
        {
            if (soundOn)
                musicPlayer.PlayOneShot(explosion, 0.6f);
        }

        public void SwipeSound()
        {
            if (soundOn)
                musicPlayer.PlayOneShot(swipe);
        }

        public void ShuffleSound()
        {
            if (soundOn)
                musicPlayer.PlayOneShot(shuffle);
        }

        public void PlayWinSound()
        {
            if (soundOn)
                musicPlayer.PlayOneShot(win);
        }
        public void PlayLoseSound()
        {
            if (soundOn)
                musicPlayer.PlayOneShot(lost);
        }

        public void ButtonSound()
        {
            if (soundOn)
                musicPlayer.PlayOneShot(buttonSound);
        }
        private bool soundOn = true;

        public void SoundOnOff(Image buttonImage, Sprite on, Sprite off)
        {
            if (soundOn)
            {
                buttonImage.sprite = off;
                musicPlayer.Pause();
                soundOn = false;
            }
            else
            {
                buttonImage.sprite = on;
                musicPlayer.Play();
                soundOn = true;
            }
        }
    }
}

