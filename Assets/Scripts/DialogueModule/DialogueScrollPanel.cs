using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Fog.Dialogue{
    // Editor class is in ScrollPanel.cs file, tried to put it here, didn't work
    [RequireComponent(typeof(Mask)), RequireComponent(typeof(Image)), RequireComponent(typeof(RectTransform))]
    public class DialogueScrollPanel : ScrollRect
    {
        public bool smoothScrolling;
        public float scrollSpeed;
        [SerializeField] private GameObject scrollUpIndicator = null;
        [SerializeField] private GameObject scrollDownIndicator = null;
        [SerializeField] private GameObject skipIndicator = null;

        // To do: Change this so it also works with horizontal scrolling

        protected void Reset(){
            smoothScrolling = false;
            scrollSpeed = 10f;
            content = null;
            horizontal = false;
            vertical = true;
            movementType = MovementType.Clamped;
            inertia = false;
            scrollSensitivity = 1;
            viewport = null;
            horizontalScrollbar = null;
            horizontalScrollbarSpacing = 1f;
            horizontalScrollbarVisibility = ScrollbarVisibility.AutoHideAndExpandViewport;
            verticalScrollbar = null;
            verticalScrollbarSpacing = 1f;
            verticalScrollbarVisibility = ScrollbarVisibility.AutoHideAndExpandViewport;
            onValueChanged = null;
        }

        protected void Start(){
            base.Start();
        }

        public float NormalizedTopPosition(RectTransform rect){
            float contentHeight = content.rect.height;
            float viewportHeight = viewport.rect.height;

            Vector3[] corners = new Vector3[4];
            content.GetWorldCorners(corners);
            float contentBottom = corners[0].y;

            rect.GetWorldCorners(corners);
            float rectTop = corners[1].y;
            float distance = rectTop - contentBottom;

            return Mathf.Clamp((distance - viewportHeight) / (contentHeight - viewportHeight), 0f, 1f);
        }

        public float NormalizedBottomPosition(RectTransform rect){
            float contentHeight = content.rect.height;
            float viewportHeight = viewport.rect.height;
            
            Vector3[] corners = new Vector3[4];
            content.GetWorldCorners(corners);
            float contentBottom = corners[0].y;

            rect.GetWorldCorners(corners);
            float rectBottom = corners[0].y;
            float distance = rectBottom - contentBottom;

            return Mathf.Clamp((distance) / (contentHeight - viewportHeight), 0f, 1f);
        }

        public void Scroll(float axis){
            RectTransform rectTransform = transform as RectTransform;
            verticalNormalizedPosition = Mathf.Clamp(verticalNormalizedPosition + axis * scrollSpeed * (rectTransform.rect.height / content.rect.height), 0f, 1f);
        }

        public void JumpToEnd(){
            if(smoothScrolling){
                StopAllCoroutines();
            }
            Canvas.ForceUpdateCanvases();
            verticalNormalizedPosition = 0f;
        }

        public void JumpToStart(){
            if(smoothScrolling){
                StopAllCoroutines();
            }
            Canvas.ForceUpdateCanvases();
            verticalNormalizedPosition = 1f;
        }

        public void JumpToPosition(float targetNormalPosition){
            if(smoothScrolling){
                StopAllCoroutines();
            }
            Canvas.ForceUpdateCanvases();
            verticalNormalizedPosition = Mathf.Clamp(targetNormalPosition, 0f, 1f);
        }

        public void ScrollToEnd(){
            if(smoothScrolling){
                StopAllCoroutines();
                StartCoroutine(ScrollingDown());
            }else
                JumpToEnd();
        }

        public void ScrollToStart(){
            if(smoothScrolling){
                StopAllCoroutines();
                StartCoroutine(ScrollingUp());
            }else
                JumpToStart();
        }

        public void ScrollToPosition(float targetNormalPosition){
            targetNormalPosition = Mathf.Clamp(targetNormalPosition, 0f, 1f);
            if(smoothScrolling){
                if(targetNormalPosition < verticalNormalizedPosition - Mathf.Epsilon){
                    StopAllCoroutines();
                    StartCoroutine(ScrollingDown(targetNormalPosition));
                }else if(targetNormalPosition > verticalNormalizedPosition + Mathf.Epsilon){
                    StopAllCoroutines();
                    StartCoroutine(ScrollingUp(targetNormalPosition));
                }
            }else{
                JumpToPosition(targetNormalPosition);
            }
        }

        private IEnumerator ScrollingUp(float targetPosition = 1f){
            yield return new WaitForEndOfFrame();
            while(verticalNormalizedPosition < (targetPosition - Mathf.Epsilon)){
                // Canvas.ForceUpdateCanvases();
                verticalNormalizedPosition += (Time.deltaTime * scrollSpeed * 10)/(content.rect.height);
                yield return new WaitForEndOfFrame();
            }
            verticalNormalizedPosition = 1f;
            velocity = Vector2.zero;
        }

        private IEnumerator ScrollingDown(float targetPosition = 0f){
            yield return new WaitForEndOfFrame();
            while(verticalNormalizedPosition > (targetPosition + Mathf.Epsilon)){
                // Canvas.ForceUpdateCanvases();
                verticalNormalizedPosition -= (Time.deltaTime * scrollSpeed * 10)/(content.rect.height);
                yield return new WaitForEndOfFrame();
            }
            verticalNormalizedPosition = 0f;
            velocity = Vector2.zero;
        }

        protected override void OnEnable(){
            base.OnEnable();
            if(skipIndicator != null){
                skipIndicator.SetActive(true);
            }
            if(scrollUpIndicator != null){
                scrollUpIndicator.SetActive(false);
            }
            if(scrollDownIndicator != null){
                scrollDownIndicator.SetActive(false);
            }
        }

        protected override void OnDisable(){
            if(skipIndicator != null){
                skipIndicator.SetActive(false);
            }
            if(scrollUpIndicator != null){
                scrollUpIndicator.SetActive(false);
            }
            if(scrollDownIndicator != null){
                scrollDownIndicator.SetActive(false);
            }
            base.OnDisable();
        }

        protected override void LateUpdate(){
            base.LateUpdate();
            if(scrollUpIndicator != null){
                scrollUpIndicator.SetActive((verticalNormalizedPosition < (1f - float.Epsilon)) && (content.rect.height - viewport.rect.height > float.Epsilon));
            }
            if(scrollDownIndicator != null){
                scrollDownIndicator.SetActive((verticalNormalizedPosition > float.Epsilon) && (content.rect.height - viewport.rect.height > float.Epsilon));
            }
        }
    }
}
