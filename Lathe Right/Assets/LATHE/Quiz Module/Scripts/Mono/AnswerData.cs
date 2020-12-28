using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AnswerData : MonoBehaviour {

    #region Variables

    [Header("UI Elements")]
    [SerializeField]    TextMeshProUGUI infoTextObject      = null;
    [SerializeField]    Image           toggle              = null;
    [SerializeField]    Image           checkmark           = null;
    [SerializeField]    Image           xmark               = null;

    [Header("Textures")]
    [SerializeField]    Sprite          uncheckedToggle     = null;
    [SerializeField]    Sprite          checkedToggle       = null;
    [SerializeField]    Sprite          checkmarkSprite     = null;
    


    [Header("References")]
    [SerializeField]    GameEvents      events              = null;

    private             RectTransform   _rect               = null;
    public              RectTransform   Rect
    {
        get
        {
            if (_rect == null)
            {
                _rect = GetComponent<RectTransform>() ?? gameObject.AddComponent<RectTransform>();
            }
            return _rect;
        }
    }

    private             int             _answerIndex        = -1;
    public              int             AnswerIndex         { get { return _answerIndex; } }

    private             bool            Checked             = false;

    #endregion

    // Function that is called to update the answer data.
    public void UpdateData (string info, int index)
    {
        infoTextObject.text = info;
        _answerIndex = index;
    }

    // Function that is called to reset values back to default.
    public void Reset ()
    {
        Checked = false;
        UpdateUI();
    }

    // Function that is called to switch the state.
    public void SwitchState ()
    {
        Checked = !Checked;
        UpdateUI();

        if (events.UpdateQuestionAnswer != null)
        {
            events.UpdateQuestionAnswer(this);
        }
    }

    // Function that is called to update UI.
    void UpdateUI ()
    {
        if (toggle == null) return;

        toggle.sprite = (Checked) ? checkedToggle : uncheckedToggle; 
    }

    public void ShowSolution (bool b) // My function
    {
        //Debug.Log(b);
        if(b==true)
        {
            checkmark.enabled = true;
            checkmark.gameObject.SetActive(true);
            xmark.enabled = false;
            xmark.gameObject.SetActive(false);
        }
        else
        {
            checkmark.enabled = false;
            checkmark.gameObject.SetActive(false);
            xmark.enabled = true;
            xmark.gameObject.SetActive(true);
        }
        

    }
}