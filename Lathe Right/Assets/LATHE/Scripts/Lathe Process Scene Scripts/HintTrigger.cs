using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HintTrigger : MonoBehaviour
{

    [SerializeField] private DialogueOperator manager;
    [SerializeField] private ProcessController animationController
        ;
    [SerializeField] private Button hint;
    [SerializeField] private Color HintColor;
    [SerializeField] private int NumOfFlashForHint;
    [SerializeField] private float DelayForFlash;
    [SerializeField] private int HintCountdown;
    [SerializeField] private Button proceedBTN;
    [SerializeField] private List<HintFlash.ObjectTriggerInfo> drillingInfo, turningInfo, facingInfo;
    private bool[] drillingClicked, turningClicked, facingClicked;

    private int animIndex = -1;
    private List<HintFlash.ObjectTriggerInfo> currentInfo;
    private bool[] currentClickedInfo;
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


    private void Start()
    {
        drillingClicked = new bool[drillingInfo.Count];
        turningClicked = new bool[turningInfo.Count];
        facingClicked = new bool[facingInfo.Count];
        hint.interactable = false;
        timer = -1;
    }
    public void SetAnimIndex()
    {
        animIndex = animationController.Index;

        if (animIndex == 0)
        {
            currentClickedInfo = facingClicked;
            currentInfo = facingInfo;
            Reset();
        }
        else if (animIndex == 1)
        {
            currentClickedInfo = turningClicked;
            currentInfo = turningInfo;
            Reset();
        }
        else if (animIndex == 2)
        {
            currentClickedInfo = drillingClicked;
            currentInfo = drillingInfo;
            Reset();
        }
        else
        {
            animIndex = -1;
        }
    }

    private void Reset()
    {
        sentenceIndex = 0;
        prevVal = 0;
        currentGameObjects = new List<GameObject>();
        currentClickedIndex = 0;
        for (int i = 0; i < currentClickedInfo.Length; i++)
        {
            currentClickedInfo[i] = false;
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
        foreach (HintFlash.ObjectTriggerInfo info in currentInfo)
        {
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
            if (state && animationController.Operation.CurrentAnimationStatus && currentInfoSentence == manager.SentenceIndex)
            {
                timer -= Time.deltaTime;
                if (timer < 0)
                {
                    if((manager.sentenceIndex == manager.indexWait[animationController.Index]) && manager.MachiningWait)
                    {
                        manager.DialogWait();
                    }
                    hint.interactable = true;
                    state = false;
                    timer = -1;
                    react = true;
                }
            }
            bool found = false;
            int i = -1;
            foreach (HintFlash.ObjectTriggerInfo info in currentInfo)
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
                proceedBTN.interactable = currentClickedInfo[i];
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
                    if (material != null && (animationController.Operation.Index == triggerIndex))
                    {
                        StartCoroutine(FlashMesh(material));
                    }
                }
                else if (image != null && (animationController.Operation.Index == triggerIndex))
                {
                    if (currentInfo[triggerIndex].Panel)
                    {

                        if (currentInfo[triggerIndex].Control != null)
                        {
                            if (currentInfo[triggerIndex].PanelControlBool)
                            {
                                currentInfo[triggerIndex].Control.SetPanel1(true);

                            }
                            else
                            {
                                currentInfo[triggerIndex].Control.SetPanel2(true);
                            }
                        }


                    }
                    if (currentInfo[triggerIndex].PanelControl != null)
                    {
                        currentInfo[triggerIndex].PanelGO.SetActive(true);
                        currentInfo[triggerIndex].PanelControl.TabActive(currentInfo[triggerIndex].Tab);

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
        currentClickedInfo[currentClickedIndex] = true;
        currentClickedIndex++;
    }

}
