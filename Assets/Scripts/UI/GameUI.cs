using FruitScapes.Extensions;
using FruitScapes.MapController;
using UnityEngine;
using UnityEngine.SceneManagement;
using FruitScapes.Audio;
using UnityEngine.UI;

namespace FruitScapes.UI
{
    public class GameUI : MonoBehaviour
    {
        [SerializeField] private Sprite soundOnSprite,soundOffSprite;
        [SerializeField] private Sprite gameWinSprite, gameLoseSprite;
        [SerializeField] private GameObject pauseButton;
        [Header ("Windiws")]
        [SerializeField] private GameObject pauseWindow;
        [SerializeField] private GameObject gameoverWindow;
        [Header ("Scripts")]
        [SerializeField] private ObjectMover mover;
        [SerializeField] private Hint.HintManager hint;
        [Header("Images")]
        [SerializeField] private Image soundImage;
        [SerializeField] private Image gameoverImage;

        private void Start()
        {
            AudioManager.Instance.PlayLevelTrack();
        }

        public void PauseAction()
        {
            AudioManager.Instance.ButtonSound();
            pauseButton.SetActive(false);
            pauseWindow.SetActive(true);
            mover.gameState = GameState.Wait;
            hint.StopTimer();
            if (AudioManager.Instance.CheckMusicOn() == false)
                soundImage.sprite = soundOffSprite;
        }

        public void ContinueAction()
        {
            AudioManager.Instance.ButtonSound();
            pauseButton.SetActive(true);
            pauseWindow.SetActive(false);
            mover.gameState = GameState.Move;
            hint.ContinueTimer();
        }

        public void RestartAction()
        {
            AudioManager.Instance.ButtonSound();
            SceneManager.LoadScene("Game");
        }

        public void ExitAction()
        {
            AudioManager.Instance.ButtonSound();
            SceneManager.LoadScene("Menu");
        }

        public void SoundAction()
        {
            AudioManager.Instance.SoundOnOff(soundImage, soundOnSprite, soundOffSprite);
        }


        public void EndGame(bool itsWin)
        {
            gameoverWindow.SetActive(true);
            pauseButton.SetActive(false);
            mover.gameState = GameState.Wait;
            hint.StopTimer();
            if (itsWin)
            {
                gameoverImage.sprite = gameWinSprite;
            }
            else
                gameoverImage.sprite = gameLoseSprite;
        }
    }
}
