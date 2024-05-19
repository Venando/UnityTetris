using System;
using Tetris.Messages;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Tetris.Ui.Windows
{
    public class GameHudWindow : UiWindowObject
    {
        [SerializeField] private Button _pauseButton;
        [SerializeField] private TMP_Text _scoreText;
        [SerializeField] private string _scoreTextFormat = "Score:\n{0}";

        protected override void Awake()
        {
            base.Awake();
            _pauseButton.onClick.AddListener(OnPauseButtonClick);
            Messenger.Subscribe<ScoreUpdateMessage>(OnScoreUpdateMessage);
        }

        private void OnScoreUpdateMessage(ScoreUpdateMessage scoreUpdateMessage)
        {
            _scoreText.text = string.Format(_scoreTextFormat, scoreUpdateMessage.Score.ToString()); 
        }

        private void OnPauseButtonClick()
        {
            UiManager.Open<PausePanelWindow>();
        }

        private void OnDestroy()
        {
            Messenger.Unsubscribe<ScoreUpdateMessage>(OnScoreUpdateMessage);
        }
    }
}