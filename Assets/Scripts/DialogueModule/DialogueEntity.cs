using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Fog.Dialogue{
    public abstract class DialogueEntity : ScriptableObject
    {
        public abstract Color DialogueColor { get; }
        public abstract string DialogueName { get; }
        public abstract Sprite DialoguePortrait { get; }
    }
}
