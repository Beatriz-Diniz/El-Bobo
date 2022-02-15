using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Fog.Dialogue
{
    [RequireComponent(typeof(AudioSource))]
    public class OptionHandler : MonoBehaviour
    {
        [SerializeField] private DialogueScrollPanel scrollPanel = null;
        [SerializeField] private RectTransform container = null;
        [SerializeField] private RectTransform optionList = null;
        [SerializeField] private GameObject optionPrefab = null;
        [SerializeField] private float inputCooldown = 0.1f;
        [SerializeField] private float activationTime = 0.5f;
        private float timer;
        private AudioSource source;
        [SerializeField] private AudioClip changeOption;
        [SerializeField] private AudioClip selectOption;

        private int currentOption = -1;
        private List<DialogueOption> options = new List<DialogueOption>();

        public bool IsActive { get; private set; }

        void Awake(){
            source = GetComponent<AudioSource>();
            IsActive = false;
            container.gameObject.SetActive(false);
            inputCooldown = Mathf.Max(0f, inputCooldown);
            if(!optionPrefab){
                Debug.Log("No prefab detected");
                Destroy(this);
            }else{
                if(!optionPrefab.GetComponent<DialogueOption>()){
                    Debug.Log("Prefab must have a DialogueOption component");
                    Destroy(this);
                }
            }
        }

        public void CreateOptions(DialogueOptionInfo[] infos){
            if(infos.Length > 0){
                container.gameObject.SetActive(true);
                foreach(DialogueOptionInfo info in infos){
                    GameObject go = Instantiate(optionPrefab, optionList);
                    DialogueOption newOption = go.GetComponentInChildren<DialogueOption>();
                    newOption.Configure(info);
                    newOption.OnSelect += SelectOption;
                    newOption.OnFocus += FocusOption;
                    options.Add(newOption);
                }
                // See Activate method comment
                StartCoroutine(DelayedActivate(0.5f));                
            }else{
                Debug.Log("Passed empty option array to Dialogue Handler");
                SelectOption();
            }
        }

        private IEnumerator DelayedActivate(float delay){
            yield return new WaitForSeconds(delay);

            Activate();
        }

        // This can be called from animation instead of coroutine, for better visual effect
        public void Activate(){
            currentOption = 0;
            options[currentOption].OnFocus?.Invoke();
            IsActive = true;
        }

        private void FocusOption(){
            float normalizedTop = scrollPanel.NormalizedTopPosition(options[currentOption].GetComponent<RectTransform>());
            float normalizedBottom = scrollPanel.NormalizedBottomPosition(options[currentOption].GetComponent<RectTransform>());

            if(scrollPanel.verticalNormalizedPosition < normalizedTop 
            || scrollPanel.viewport.rect.height <= options[currentOption].GetComponent<RectTransform>().rect.height){
                scrollPanel.ScrollToPosition(normalizedTop);
            }else if (scrollPanel.verticalNormalizedPosition > normalizedBottom){
                scrollPanel.ScrollToPosition(normalizedBottom);
            }
        }

        private void SelectOption(){
            IsActive = false;
            timer = 0f;
            source.PlayOneShot(selectOption);
            Dialogue selectedDialogue = (currentOption >= 0)? options[currentOption].NextDialogue : null;
            foreach(RectTransform transform in optionList){
                Destroy(transform.gameObject);
            }
            options.Clear();
            currentOption = -1;
            container.gameObject.SetActive(false);
            if(selectedDialogue){
                DialogueHandler.instance.StartDialogue(selectedDialogue);
            }else{
                DialogueHandler.instance.EndDialogue(true);
            }
        }

        // Update is called once per frame
        void Update()
        {
            if(IsActive){
                if(timer > inputCooldown){
                    if(Input.GetButton("Submit")){
                        options[currentOption].OnSelect?.Invoke();
                    }else{
                        float input = Input.GetAxisRaw("Vertical") * (-1f);
                        if(input != 0){
                            int newOption = Mathf.Clamp(currentOption + ((input > 0)? 1 : -1), 0, options.Count-1);
                            if(newOption != currentOption){
                                options[currentOption].OnExit?.Invoke();
                                currentOption = newOption;
                                options[currentOption].OnFocus?.Invoke();
                            }
                            // If you press up on the first option it tries to scroll further up
                            // For cases when the question and answers are using the same scrollpanel
                            if(input > 0 && currentOption == 0){
                                scrollPanel.ScrollToStart();
                            }
                        }
                    }
                    timer = 0f;
                }
                timer += Time.deltaTime;
            }
        }
    }
}