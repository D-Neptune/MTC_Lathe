using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HintController : MonoBehaviour
{
    [SerializeField] private List<HintFlash.ObjectTriggerInfo> tool_holding_info;
    [SerializeField] private DialogueOperator manager;
    [SerializeField] private AnimController animController;
    [SerializeField] private Button hint;
    [SerializeField] private Color HintColor;
    [SerializeField] private int NumOfFlashForHint;
    [SerializeField] private float DelayForFlash;
    [SerializeField] private int HintCountdown;
    [SerializeField] private Button proceedBTN;

    private int animIndex = -1;
    private bool[] toolClicked;
    private List<GameObject> currentGameObjects;
    private int sentenceIndex = 0;
    private bool state = false;
    private bool react;
    private int prevVal = 0;
    private int triggerIndex = -1;
    private float timer = -1;
    private int currentInfoSentence = -1;
    private Image currentImage = null;
    private Color color;
    private int currentClickedIndex;

    // Start is called before the first frame update
    void Start()
    {
        toolClicked = new bool[tool_holding_info.Count];
        hint.interactable = false;
        timer = -1;
        animIndex = -1;

    }

    public int AnimIndex
    {
        set => animIndex = value;
        get => animIndex;
    }

    public void Reset()
    {
        sentenceIndex = 0;
        prevVal = 0;
        currentGameObjects = new List<GameObject>();
        currentClickedIndex = 0;
        for (int i = 0; i < toolClicked.Length; i++)
        {
            toolClicked[i] = false;
        }
        StopAllCoroutines();
        IncrementAnim();
    }

    private void IncrementAnim()
    {
        timer = -1;
        triggerIndex = -1;
        currentInfoSentence = -1;
        hint.interactable = false;
        proceedBTN.interactable = true;
        react = false;
        foreach (HintFlash.ObjectTriggerInfo info in tool_holding_info)
        {
            Debug.Log(info.SenteceIndex + " vs " + sentenceIndex);
            if (sentenceIndex == info.SenteceIndex)
            {
                proceedBTN.interactable = false;
                currentGameObjects = info.TriggerObject;
                triggerIndex = info.TriggerIndex;
                currentInfoSentence = info.SenteceIndex;
                state = true;
                timer = HintCountdown;
                break;
            }
        }
        sentenceIndex++;
    }

    public void LateUpdate()
    {
        if (animIndex > -1)
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
            if (state && animController.CurrentAnimationStatus && currentInfoSentence == manager.SentenceIndex)
            {
                timer -= Time.deltaTime;
                if (timer < 0)
                {
                    hint.interactable = true;
                    state = false;
                    timer = -1;
                    react = true;
                }
            }
            bool found = false;
            int i = -1;
            foreach (HintFlash.ObjectTriggerInfo info in tool_holding_info)
            {
                i++;
                if (manager.SentenceIndex == info.SenteceIndex)
                {
                    found = true;
                    break;
                }
            }
            if (!found)
            {
                proceedBTN.interactable = true;
            }
            else
            {
                proceedBTN.interactable = toolClicked[i];
            }

        }

    }

    public void GiveHint()
    {
        if (currentGameObjects.Count != 0)
        {
            foreach (GameObject go in currentGameObjects)
            {
                MeshRenderer meshRenderer = go.GetComponent<MeshRenderer>();
                Image image = go.GetComponent<Image>();
                if (meshRenderer != null)
                {
                    Material[] material = meshRenderer.materials;
                    if (material != null && (animController.Index == triggerIndex))
                    {
                        StartCoroutine(FlashMesh(material));
                    }
                }
                else if (image != null && (animController.Index == triggerIndex))
                {
                    if (tool_holding_info[triggerIndex].Panel)
                    {

                        if (tool_holding_info[triggerIndex].Control != null)
                        {
                            if (tool_holding_info[triggerIndex].PanelControlBool)
                            {
                                tool_holding_info[triggerIndex].Control.SetPanel1(true);

                            }
                            else
                            {
                                tool_holding_info[triggerIndex].Control.SetPanel2(true);
                            }
                        }


                    }
                    if (tool_holding_info[triggerIndex].PanelControl != null)
                    {
                        tool_holding_info[triggerIndex].PanelGO.SetActive(true);
                        tool_holding_info[triggerIndex].PanelControl.TabActive(tool_holding_info[triggerIndex].Tab);

                    }
                    StartCoroutine(FlashImage(image));
                }
            }

        }
    }

    public void StopRoutine()
    {
        StopAllCoroutines();
        if (currentImage != null)
        {
            currentImage.color = color;
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

    public void ProceedState(bool state)
    {
        proceedBTN.interactable = state;
    }

    public void AnimPlayed()
    {
        toolClicked[currentClickedIndex] = true;
        currentClickedIndex++;
    }
}
