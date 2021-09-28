using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Fog.Dialogue{
    [CreateAssetMenu(fileName = "NewOptionsDialogue", menuName = "DialogueSystem/Speaker")]
    public class Speaker : DialogueEntity
    {
        [SerializeField] private Color dialogueColor;
        [SerializeField] private string dialogueName;
        [SerializeField] private Sprite dialoguePortrait;

        public override Color DialogueColor => dialogueColor;
        public override string DialogueName => dialogueName;
        public override Sprite DialoguePortrait => dialoguePortrait;
    }
}