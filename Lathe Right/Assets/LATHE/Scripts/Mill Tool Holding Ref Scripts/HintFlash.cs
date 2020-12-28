using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public class HintFlash : MonoBehaviour
{
    [SerializeField] private List<ObjectTriggerInfo> triggerInfos;
    [SerializeField] private AnimationController controller;
    [SerializeField] private DialogueManager manager;
    [SerializeField] private Button hint;
    [SerializeField] private Color HintColor;
    [SerializeField] private int NumOfFlashForHint;
    [SerializeField] private float DelayForFlash;
    [SerializeField] private int HintCountdown;

    private List<GameObject> current = null;
    private int sentenceIndex = 0;
    private Boolean state = false;
    private Boolean react;
    private int prevVal = 0;
    private int triggerIndex = -1;
    private float timer = -1;
    private int currentInfoSentence = -1;
    private Image currentImage = null;
    private Color color;
    // Start is called before the first frame update
    void Start()
    {
        hint.interactable = false;
        timer = -1;
        IncrementAnim();
    }

    public void StopRoutine()
    {
        StopAllCoroutines();
        if (currentImage != null)
        {
            currentImage.color = color;
        }
    }

    public void IncrementAnim()
    {
        current = new List<GameObject>();
        triggerIndex = -1;
        currentInfoSentence = -1;
        hint.interactable = false;
        state = false;
        react = false;
        timer = -1;
        foreach (ObjectTriggerInfo triggerInfo in triggerInfos)
        {
            if (sentenceIndex == triggerInfo.SenteceIndex)
            {
                current = triggerInfo.TriggerObject;
                triggerIndex = triggerInfo.TriggerIndex;
                currentInfoSentence = triggerInfo.SenteceIndex;
                state = true;
                timer = HintCountdown;
                break;
            }
        }
        sentenceIndex++;
    }

    public void LateUpdate()
    {
        if (manager.SentenceIndex > prevVal)
        {
            prevVal = manager.SentenceIndex;
            IncrementAnim();
        }
        if (currentInfoSentence != manager.SentenceIndex)
        {
            hint.interactable = false;
        }
        else
        {
            hint.interactable = react;
        }
        if (state && controller.CurrentAnimationStatus && currentInfoSentence == manager.SentenceIndex)
        {
            timer -= Time.deltaTime;
            if (timer < 0)
            {
                hint.interactable = true;
                react = true;
                timer = -1;
            }
        }

    }

    public void Reset()
    {
        sentenceIndex = 0;
        prevVal = 0;
        current = new List<GameObject>();
        StopAllCoroutines();
        IncrementAnim();
    }

    public void GiveHint()
    {
        if (current.Count != 0)
        {
            foreach (GameObject go in current)
            {
                Material[] material = go.GetComponent<MeshRenderer>().materials;
                Image image = go.GetComponent<Image>();
                if (material != null && (controller.Index == triggerIndex))
                {
                    StartCoroutine(FlashMesh(material));
                }
                else if (image != null && (controller.Index == triggerIndex))
                {
                    StartCoroutine(FlashImage(image));
                }
            }

        }
    }

    public IEnumerator FlashImage(Image image)
    {
        Color basicColor = image.color;
        color = basicColor;
        currentImage = image;
        for (int i = 0; i < NumOfFlashForHint; i++)
        {
            image.color = HintColor;
            yield return new WaitForSecondsRealtime(DelayForFlash);
            image.color = basicColor;
            yield return new WaitForSecondsRealtime(DelayForFlash);
        }
    }

    public IEnumerator FlashMesh(Material[] materials)
    {
        List<Color> basicColors = new List<Color>();
        foreach (Material material in materials)
        {
            basicColors.Add(material.color);
        }

        for (int i = 0; i < NumOfFlashForHint; i++)
        {
            foreach (Material material in materials)
            {
                material.color = HintColor;
            }
            yield return new WaitForSecondsRealtime(DelayForFlash);
            int index = 0;
            foreach (Color color in basicColors)
            {
                materials[index].color = color;
                index++;
            }
            yield return new WaitForSecondsRealtime(DelayForFlash);
        }
    }
    public Boolean Hint
    {
        get => react;
    }

    [System.Serializable]
    public struct ObjectTriggerInfo
    {
        [SerializeField] private int sentenceIndex;
        [SerializeField] private int triggerIndex;
        [SerializeField] private List<GameObject> triggerObject;
        [SerializeField] private bool panel;
        [SerializeField] private GameObject panelGO;
        [SerializeField] private bool controlPanel;
        [SerializeField] private ControlPanel panelControl;
        [SerializeField] private bool panelControlBool;
        [SerializeField] private bool tab;
        [SerializeField] private PanelControl control;

        private Boolean clicked;

        public List<GameObject> TriggerObject
        {
            get => triggerObject;
        }

        public int SenteceIndex
        {
            get => sentenceIndex;
        }

        public int TriggerIndex
        {
            get => triggerIndex;
        }

        public Boolean Clicked
        {
            get => clicked;
            set => clicked = value;
        }

        public GameObject PanelGO
        {
            get => panelGO;
        }

        public bool Tab
        {
            get => tab;
        }

        public bool Panel
        {
            get => panel;
        }

        public bool ControlPanel
        {
            get => controlPanel;
        }
        
        public bool PanelControlBool
        {
            get => panelControlBool;
        }

        public ControlPanel PanelControl
        {
            get => panelControl;
        }

        public PanelControl Control
        {
            get => control;
        }


    }

}



