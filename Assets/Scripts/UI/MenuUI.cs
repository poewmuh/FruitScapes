using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FruitScapes.Extensions;
using UnityEngine.SceneManagement;
using FruitScapes.Audio;

namespace FruitScapes.UI
{
    public class MenuUI : MonoBehaviour
    {
        [SerializeField] private List<MenuDictionary> panels;

        private void Start()
        {
            AudioManager.Instance.PlayMenuTrack();
        }

        public void PlayAction()
        {
            AudioManager.Instance.ButtonSound();
            SceneManager.LoadScene("Game");
        }

        public void AboutAction()
        {
            AudioManager.Instance.ButtonSound();
            SetPanel(MenuState.About);
        }

        public void BackToMenuAction()
        {
            AudioManager.Instance.ButtonSound();
            SetPanel(MenuState.Menu);
        }

        public void ExitGameAction()
        {
            AudioManager.Instance.ButtonSound();
            Application.Quit();
        }

        private void SetPanel(MenuState type)
        {
            foreach (var panel in panels)
            {
                if (panel.state != type)
                {
                    panel.panelObj.SetActive(false);
                }
                else panel.panelObj.SetActive(true);
            }
        }
    }
}
