using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Fog.Dialogue
{
    /// <summary>
    ///     Creates a scriptable object for an array of dialogue lines, so that it can be saved as a file.
    /// </summary>
    [CreateAssetMenu(fileName = "NewOptionsDialogue", menuName = "DialogueSystem/OptionsDialogue")]
    public class OptionsDialogue: Dialogue
    {
        [SerializeField] private DialogueLine question;
        [SerializeField] private DialogueOptionInfo[] options;

        public override void AfterDialogue(){
            base.AfterDialogue();
            if(Agent.Instance != null){
                Agent.Instance.canInteract = false;
            }
            DialogueHandler.instance.DisplayOptions(question, options);

            DialogueHandler.instance.OnDialogueEnd -= AfterDialogue;
        }
    }
}