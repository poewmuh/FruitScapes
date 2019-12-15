using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace FruitScapes.Task
{
    public class Task : MonoBehaviour
    {
        private Image _sourceImage;
        private Text _sourceText;
        private int _curCount;
        private int _maxCount;

        public int Id { get; set; }
        public bool iCompliete => _sourceText.text == "Compliete";

        private void Awake()
        {
            _sourceImage = GetComponentInChildren<Image>();
            _sourceText = GetComponentInChildren<Text>();
        }

        public void SetSprite(Sprite sprite)
        {
            _sourceImage.sprite = sprite;
        }

        public void Init(int maxCount)
        {
            _curCount = 0;
            _maxCount = maxCount;

            _sourceText.text = _curCount.ToString() + '/' + _maxCount.ToString();
            
        }

        public void UpCount()
        {
            if (_curCount > _maxCount)
                return;
            if (_curCount == _maxCount)
            {
                _sourceText.text = "Compliete";
            }
            else
            {
                _curCount++;
                _sourceText.text = _curCount.ToString() + '/' + _maxCount.ToString();
            }
        }
    }
}
