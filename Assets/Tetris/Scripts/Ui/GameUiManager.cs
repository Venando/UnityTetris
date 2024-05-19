using System;
using Tetris.Ui.Windows;
using UnityEngine;

namespace Tetris.Ui
{
    public class GameUiManager : MonoBehaviour
    {
        [SerializeField] private UiWindowObject _activeWindow;
    
        private static GameUiManager Instance { get; set; }
    
        private UiWindowObject[] _windows;
        
        private void Awake()
        {
            Instance = this;
            InitiateWindowsArray();
            ResetToDefault();
        }

        private void InitiateWindowsArray()
        {
            _windows = GetComponentsInChildren<UiWindowObject>(true);
        }

        public void DisableAll()
        {
            foreach (UiWindowObject window in _windows)
            {
                window.gameObject.SetActive(false);
            }
        }
        
        public T Open<T>() where T : UiWindowObject
        {
            DisableAll();
            T window = (T)Array.Find(_windows, w => w.GetType() == typeof(T));
            window.gameObject.SetActive(true);
            return window;
        }

        public static T OpenWindow<T>() where T : UiWindowObject
        {
            return Instance.Open<T>();
        }

        public void ResetToDefault()
        {
            DisableAll();
            _activeWindow.gameObject.SetActive(true);
        }
    }
}
