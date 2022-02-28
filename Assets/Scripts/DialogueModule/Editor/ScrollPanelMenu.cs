using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

namespace Fog.Dialogue{

    // This shouldn't be here, it wasn't working in the DialogueScrollPanel.cs file so it's here for reasons unknown
    [CustomEditor(typeof(DialogueScrollPanel))]
    public class DialogueScrollPanelEditor : UnityEditor.UI.ScrollRectEditor{
        [SerializeField] private bool wasVerticalLast = true;

        public override void OnInspectorGUI(){
            EditorGUILayout.LabelField("Custom Scroll Rect Fields", EditorStyles.boldLabel);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("smoothScrolling"), new GUIContent("Smooth Scrolling"));
            SerializedProperty prop = serializedObject.FindProperty("smoothScrolling");
            if(prop != null && prop.propertyType == SerializedPropertyType.Boolean){
                if(prop.boolValue){
                    EditorGUILayout.PropertyField(serializedObject.FindProperty("scrollSpeed"), new GUIContent("Scroll Speed"));
                }
            }
            EditorGUILayout.PropertyField(serializedObject.FindProperty("scrollUpIndicator"), new GUIContent("Scroll Up Indicator"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("scrollDownIndicator"), new GUIContent("Scroll Down Indicator"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("skipIndicator"), new GUIContent("Skip Indicator"));
            // Trying to restrict to only one type of scrolling
            // Cant find serialized property of parent class, so doesnt work
            // SerializedProperty verticalProp = serializedObject.FindProperty("vertical");
            // SerializedProperty horizontalProp = serializedObject.FindProperty("horizontal");
            // if(verticalProp != null && horizontalProp != null){
            //     if(verticalProp.boolValue && horizontalProp.boolValue){
            //         if(wasVerticalLast){
            //             wasVerticalLast = false;
            //             verticalProp.boolValue = false;
            //         }else{
            //             wasVerticalLast = true;
            //             horizontalProp.boolValue = false;
            //         }
            //     }
            // }
            serializedObject.ApplyModifiedProperties();
            EditorGUILayout.Separator();
            EditorGUILayout.LabelField("Regular Scroll Rect Fields", EditorStyles.boldLabel);
            base.OnInspectorGUI();
        }
    }

    public class ScrollPanelMenu : MonoBehaviour
    {
        [MenuItem("GameObject/UI/FOG.Dialogue - ScrollPanel", false, 49)]
        static void CreateScrollPanel(MenuCommand menuCommand){
            // Create a custom game object
            GameObject panelObj = new GameObject("Dialogue Scroll Panel");
            // Register the creation in the undo system
            Undo.RegisterCreatedObjectUndo(panelObj, "Create " + panelObj.name);
            // Cria um Canvas se ele nao existir
            Canvas canvas = FindObjectOfType<Canvas>();
            if(!canvas){
                GameObject canvasObj = new GameObject("Canvas", typeof(CanvasScaler));
                Undo.RegisterCreatedObjectUndo(canvasObj, "Create " + canvasObj.name);
                canvas = canvasObj.GetComponent<Canvas>();
                CanvasScaler canvasScaler = canvasObj.GetComponent<CanvasScaler>();
                canvasScaler.uiScaleMode = CanvasScaler.ScaleMode.ConstantPixelSize;
                canvasScaler.scaleFactor = 1f;
                canvasScaler.referencePixelsPerUnit = 100f;
                canvas.renderMode = RenderMode.ScreenSpaceCamera;
                canvas.worldCamera = FindObjectOfType<Camera>();
                canvas.pixelPerfect = true;
                canvas.sortingOrder = 10;
                canvas.targetDisplay = 0;
                canvas.planeDistance = 100f;
                canvas.additionalShaderChannels = AdditionalCanvasShaderChannels.Normal | AdditionalCanvasShaderChannels.Tangent | AdditionalCanvasShaderChannels.TexCoord1;
            }
            // Adiciona o panel como filho do canvas e seta propriedades iniciais
            panelObj.transform.SetParent(canvas.transform);
            panelObj.AddComponent<DialogueScrollPanel>();
            panelObj.GetComponent<CanvasRenderer>().cullTransparentMesh = false;
            panelObj.GetComponent<Mask>().showMaskGraphic = true;
            Image img = panelObj.GetComponent<Image>();
            img.transform.localPosition = new Vector3(img.transform.localPosition.x, img.transform.localPosition.y, 0f);
            img.sprite = AssetDatabase.GetBuiltinExtraResource<Sprite>("UI/Skin/Background.psd");
            img.color = Color.white;
            img.material = null;
            img.raycastTarget = true;
            img.type = Image.Type.Sliced;
            img.fillCenter = true;
            int count = 0;
            Debug.Log("count = " + (count++) + ":  posZ = " + img.transform.localPosition.z);
            img.rectTransform.pivot = new Vector2(0.5f, 0f);
            Debug.Log("count = " + (count++) + ":  posZ = " + img.transform.localPosition.z);
            img.rectTransform.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Right, 0f, 0);
            Debug.Log("count = " + (count++) + ":  posZ = " + img.transform.localPosition.z);
            img.rectTransform.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, 0f, 0);
            Debug.Log("count = " + (count++) + ":  posZ = " + img.transform.localPosition.z);
            img.rectTransform.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Bottom, 0f, 150f);
            Debug.Log("count = " + (count++) + ":  posZ = " + img.transform.localPosition.z);
            img.rectTransform.anchorMin = new Vector2(0f, 0f);
            img.rectTransform.anchorMax = new Vector2(1f, 0f);
            Debug.Log("count = " + (count++) + ":  posZ = " + img.transform.localPosition.z);
            //img.rectTransform.position = new Vector3(img.rectTransform.position.x, 0f, img.rectTransform.position.z);
            img.rectTransform.localScale = new Vector3(1f, 1f, 1f);
            // Adiciona o objeto de viewport como filho do panel
            GameObject viewObj = new GameObject("Viewport");
            Undo.RegisterCreatedObjectUndo(viewObj, "Create " + viewObj.name);
            viewObj.transform.SetParent(panelObj.transform);
            viewObj.AddComponent<Image>();
            viewObj.AddComponent<Mask>();
            img = viewObj.GetComponent<Image>();
            img.transform.localPosition = new Vector3(img.transform.localPosition.x, img.transform.localPosition.y, 0f);
            img.sprite = AssetDatabase.GetBuiltinExtraResource<Sprite>("UI/Skin/Background.psd");
            img.color = Color.white;
            img.material = null;
            img.raycastTarget = true;
            img.type = Image.Type.Sliced;
            img.fillCenter = true;
            viewObj.GetComponent<Mask>().showMaskGraphic = false;
            viewObj.GetComponent<CanvasRenderer>().cullTransparentMesh = false;
            panelObj.GetComponent<DialogueScrollPanel>().viewport = img.rectTransform;
            img.rectTransform.pivot = new Vector2(0f, 0f);
            img.rectTransform.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, 0f, 0f);
            img.rectTransform.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Right, 0f, 0f);
            img.rectTransform.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Bottom, 0f, 0f);
            img.rectTransform.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Top, 0f, 0f);
            img.rectTransform.anchorMin = new Vector2(0f, 0f);
            img.rectTransform.anchorMax = new Vector2(1f, 1f);
            img.rectTransform.sizeDelta = new Vector2(0f, 0f);
            //img.rectTransform.position = new Vector3(0f, 0f, 0f);
            img.rectTransform.localScale = new Vector3(1f, 1f, 1f);
            // Adiciona o objeto de content como filho do viewport
            // O content por padrao contem um TextMeshProGUI
            GameObject contentObj = new GameObject("Content");
            Undo.RegisterCreatedObjectUndo(contentObj, "Create " + contentObj.name);
            contentObj.transform.SetParent(viewObj.transform);
            contentObj.AddComponent<ContentSizeFitter>();
            contentObj.AddComponent<TMPro.TextMeshProUGUI>();
            contentObj.GetComponent<ContentSizeFitter>().horizontalFit = ContentSizeFitter.FitMode.Unconstrained;
            contentObj.GetComponent<ContentSizeFitter>().verticalFit = ContentSizeFitter.FitMode.PreferredSize;
            TMPro.TextMeshProUGUI textMesh = contentObj.GetComponent<TMPro.TextMeshProUGUI>();
            textMesh.transform.localPosition = new Vector3(img.transform.localPosition.x, img.transform.localPosition.y, 0f);
            // textMesh.font = TMPro.TMP_FontAsset.defaultFontAsset;
            textMesh.fontSize = 21;
            textMesh.color = Color.black;
            textMesh.alignment = TMPro.TextAlignmentOptions.TopJustified;
            textMesh.wordWrappingRatios = 0f;
            textMesh.overflowMode = TMPro.TextOverflowModes.ScrollRect;
            textMesh.margin = new Vector4(3, 20, 3, 1);
            panelObj.GetComponent<DialogueScrollPanel>().content = textMesh.rectTransform;
            textMesh.rectTransform.pivot = new Vector2(0f, 1f);
            textMesh.rectTransform.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, 0f, 0f);
            textMesh.rectTransform.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Right, 0f, 0f);
            textMesh.rectTransform.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Bottom, 0f, 0f);
            textMesh.rectTransform.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Top, 0f, 0f);
            textMesh.rectTransform.anchorMin = new Vector2(0f, 1f);
            textMesh.rectTransform.anchorMax = new Vector2(1f, 1f);
            textMesh.rectTransform.sizeDelta = new Vector2(0f, 0f);
            textMesh.rectTransform.localScale = new Vector3(1f, 1f, 1f);
            // Configura o DialogueHandler da cena (cria se necessario)
            DialogueHandler handler = FindObjectOfType<DialogueHandler>();
            if(!handler){
                GameObject go = new GameObject("Dialogue Handler", typeof(DialogueHandler));
                Undo.RegisterCreatedObjectUndo(go, "Create " + go.name);
                handler = go.GetComponent<DialogueHandler>();
            }
            handler.dialogueText = textMesh;
            handler.useTitles = true;
            handler.titleText = textMesh;
            handler.dialogueBox = panelObj.GetComponent<DialogueScrollPanel>();
            handler.useTypingEffect = true;
            handler.fillInBeforeSkip = true;
            handler.isSingleton = true;

            // Select newly created object
            panelObj.SetActive(false);
            Selection.activeObject = panelObj;
        }
    }
}
