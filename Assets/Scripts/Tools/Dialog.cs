using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Tools {
    public class Dialog : MonoBehaviour {
        [SerializeField] private GameObject dialogPanel;
        [SerializeField] private TMP_Text messageText;
        [SerializeField] private Button okButton;
        public static Dialog Instance { get; private set; }

        private void Awake() {
            Reflection.ValidateClassFields(this);

            if (Instance == null)
            {
                Instance = this;
                dialogPanel.SetActive(false);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public void ShowDialog(string message, Action onOk = null) {
            messageText.text = message;
            dialogPanel.SetActive(true);

            okButton.onClick.RemoveAllListeners();
            okButton.onClick.AddListener(() => {
                onOk?.Invoke();
                HideDialog();
            });
        }

        private void HideDialog() {
            dialogPanel.SetActive(false);
        }
    }
}