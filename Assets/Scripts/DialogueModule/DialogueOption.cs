using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

namespace Fog.Dialogue
{
    [System.Serializable]
    public struct DialogueOptionInfo{
        [TextArea] public string text;
        public Dialogue nextDialogue;
    }

    [RequireComponent(typeof(RectTransform))]
    public class DialogueOption : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI textField;
        [SerializeField] private Image focusIndicator;
        public Dialogue NextDialogue { get; private set; }

        public UnityAction OnSelect = null;
        public UnityAction OnFocus = null;
        public UnityAction OnExit = null;

        void Awake(){
            if(focusIndicator){
                focusIndicator.enabled = false;
                OnFocus += ToggleFocus;
                OnExit += ToggleFocus;
            }
        }

        public void Configure(DialogueOptionInfo info){
            textField.text = info.text;
            NextDialogue = info.nextDialogue;
        }

        private void ToggleFocus(){
            if(focusIndicator){
                focusIndicator.enabled = (!focusIndicator.enabled);
            }
        }
    }
}