using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[Serializable()]
public struct SolutionUIElements
{
    [SerializeField] Image bG;
    public Image BG { get { return bG; } }

    [SerializeField] Image quizTableBG;
    public Image QuizTableBG { get { return quizTableBG; } }

    [Header("SolutionUI Screen Options")]

    [SerializeField] Color default_BG_Color;
    public Color Default_BG_Color { get { return default_BG_Color; } }

    [SerializeField] Color default_QuizTableBG_Color;
    public Color Default_QuizTableBG_Color { get { return default_QuizTableBG_Color; } }

    [SerializeField] Color solution_BG_Color;
    public Color Solution_BG_Color { get { return solution_BG_Color; } }

    [SerializeField] Color solution_QuizTableBG_Color;
    public Color Solution_QuizTableBG_Color { get { return solution_QuizTableBG_Color; } }

    [SerializeField] TextMeshProUGUI solution_QInfo;
    public TextMeshProUGUI Solution_QInfo { get { return solution_QInfo; } }

    [SerializeField] TextMeshProUGUI solution_Info;
    public TextMeshProUGUI Solution_Info { get { return solution_Info; } }

}

[Serializable()]
public struct UIManagerParameters
{
    [Header("Answers Options")]
    [SerializeField] float margins;
    public float Margins { get { return margins; } }

    [Header("Resolution Screen Options")]
    [SerializeField] Color correctBGColor;
    public Color CorrectBGColor { get { return correctBGColor; } }
    [SerializeField] Color incorrectBGColor;
    public Color IncorrectBGColor { get { return incorrectBGColor; } }
    [SerializeField] Color finalBGColor;
    public Color FinalBGColor { get { return finalBGColor; } }
}

[Serializable()]
public struct UIElements
{
    [SerializeField] RectTransform answersContentArea;
    public RectTransform AnswersContentArea { get { return answersContentArea; } }

    [SerializeField] TextMeshProUGUI questionInfoTextObject;
    public TextMeshProUGUI QuestionInfoTextObject { get { return questionInfoTextObject; } }

    [SerializeField] TextMeshProUGUI scoreText;
    public TextMeshProUGUI ScoreText { get { return scoreText; } }

    [Space]

    [SerializeField] Animator resolutionScreenAnimator;
    public Animator ResolutionScreenAnimator { get { return resolutionScreenAnimator; } }

    [SerializeField] Image resolutionBG;
    public Image ResolutionBG { get { return resolutionBG; } }

    [SerializeField] TextMeshProUGUI resolutionStateInfoText;
    public TextMeshProUGUI ResolutionStateInfoText { get { return resolutionStateInfoText; } }

    [SerializeField] TextMeshProUGUI resolutionScoreText;
    public TextMeshProUGUI ResolutionScoreText { get { return resolutionScoreText; } }

    [Space]

    [SerializeField] TextMeshProUGUI highScoreText;
    public TextMeshProUGUI HighScoreText { get { return highScoreText; } }

    [SerializeField] CanvasGroup mainCanvasGroup;
    public CanvasGroup MainCanvasGroup { get { return mainCanvasGroup; } }

    [SerializeField] RectTransform finishUIElements;
    public RectTransform FinishUIElements { get { return finishUIElements; } }

    [Space] // Mine

    [SerializeField] RectTransform solutionUIElements;
    public RectTransform SolutionUIElements { get { return solutionUIElements; } }

    [SerializeField] TextMeshProUGUI solutionQInfo;
    public TextMeshProUGUI SolutionQInfo { get { return solutionQInfo; } }

    [SerializeField] RectTransform solutionsContentArea;
    public RectTransform SolutionsContentArea { get { return solutionsContentArea; } }
}

public class UIManager : MonoBehaviour {

    #region Variables
    public enum         ResolutionScreenType   { Correct, Incorrect, Finish }

    [Header("References")]
    [SerializeField]    GameEvents             events                       = null;

    [Header("UI Elements (Prefabs)")]
    [SerializeField]    AnswerData             answerPrefab                 = null;

    [SerializeField]    UIElements             uIElements                   = new UIElements();

    [Space]
    [SerializeField]    UIManagerParameters    parameters                   = new UIManagerParameters();

    private             List<AnswerData>       currentAnswers               = new List<AnswerData>();
    private             int                    resStateParaHash             = 0;

    private             IEnumerator            IE_DisplayTimedResolution    = null;
    private int QuestionCounter = 0;
    private bool solutionAccepted = false;
    
    [Space]
    [SerializeField] Button continueButton;

    [Space]
    [SerializeField]    SolutionUIElements     solutionUIElements                   = new SolutionUIElements();


    #endregion

    #region Default Unity methods

    /// Function that is called when the object becomes enabled and active
    void OnEnable()
    {
        events.UpdateQuestionUI         += UpdateQuestionUI;
        events.DisplayResolutionScreen  += DisplayResolution;
        events.ScoreUpdated             += UpdateScoreUI;
        events.ShowSolution             += ShowSolutionUI;
        events.UpdateSolutionUI         += UpdateSolutionUI;
    }

    /// Function that is called when the behaviour becomes disabled
    void OnDisable()
    {
        events.UpdateQuestionUI         -= UpdateQuestionUI;
        events.DisplayResolutionScreen  -= DisplayResolution;
        events.ScoreUpdated             -= UpdateScoreUI;
        events.ShowSolution             -= ShowSolutionUI;
        events.UpdateSolutionUI         -= UpdateSolutionUI;
    }

    /// Function that is called when the script instance is being loaded.
    void Start()
    {
        UpdateScoreUI();
        resStateParaHash = Animator.StringToHash("ScreenState");
    }

    #endregion

    /// Function that is used to update new question UI information.
    void UpdateQuestionUI(Question question)
    {
        solutionUIElements.Solution_QInfo.enabled = false;
        solutionUIElements.Solution_QInfo.gameObject.SetActive(false);

        solutionUIElements.Solution_Info.enabled = false;
        solutionUIElements.Solution_Info.gameObject.SetActive(false);


        uIElements.QuestionInfoTextObject.text = question.Info;
        CreateAnswers(question);
        QuestionCounter++;
    }

    public void UpdateSolutionUI(Question question)
    {
        //uIElements.QuestionInfoTextObject.text = "Click 'Continue' after reviewing the solution to proceed to the next question.";
        uIElements.QuestionInfoTextObject.text = "";


        solutionUIElements.Solution_QInfo.enabled = true;
        solutionUIElements.Solution_QInfo.gameObject.SetActive(true);
        solutionUIElements.Solution_QInfo.text = "Question: " + question.Info;

        solutionUIElements.Solution_Info.enabled = true;
        solutionUIElements.Solution_Info.gameObject.SetActive(true);
        solutionUIElements.Solution_Info.text = "Solution: ";
    }

    void ShowSolutionUI(int index, bool b) // mine
    {
        currentAnswers[index].ShowSolution(b);
    }

    /// Function that is used to display resolution screen.
    void DisplayResolution(ResolutionScreenType type, int score)
    {
        UpdateResUI(type, score);
        uIElements.ResolutionScreenAnimator.SetInteger(resStateParaHash, 2); // transition from hidden resolution screen to PopUp (i.e. ScreenState > 1)
        uIElements.MainCanvasGroup.blocksRaycasts = false; // Blocks the main canvas elements from being interacted with while ResolutionDisplay 

        //if ((type != ResolutionScreenType.Finish) && (type != ResolutionScreenType.Solution) && (type != ResolutionScreenType.Incorrect))
        if (type != ResolutionScreenType.Finish) // If type not Finish and not Solution resolution screen, fade out resolution screen in certain amount of seconds
        {
            if (IE_DisplayTimedResolution != null)
            {
                StopCoroutine(IE_DisplayTimedResolution);
            }
            IE_DisplayTimedResolution = DisplayTimedResolution();
            StartCoroutine(IE_DisplayTimedResolution);
        }
    }


    IEnumerator DisplayTimedResolution()
    {
        yield return new WaitForSeconds(GameUtility.ResolutionDelayTime);
        uIElements.ResolutionScreenAnimator.SetInteger(resStateParaHash, 1); // 1 is fade out animation
        uIElements.MainCanvasGroup.blocksRaycasts = true;   
    }

    /// Function that is used to display resolution UI information.
    void UpdateResUI(ResolutionScreenType type, int score)
    {
        var highscore = PlayerPrefs.GetInt(GameUtility.SavePrefKey);

        switch (type)
        {
            case ResolutionScreenType.Correct:
                uIElements.ResolutionBG.color = parameters.CorrectBGColor;
                uIElements.ResolutionStateInfoText.text = "Correct";
                uIElements.ResolutionScoreText.text = "+" + score;
                break;
            case ResolutionScreenType.Incorrect:
                uIElements.ResolutionBG.color = parameters.IncorrectBGColor;
                uIElements.ResolutionStateInfoText.text = "Incorrect";
                uIElements.ResolutionScoreText.text = "X";
                break;
            case ResolutionScreenType.Finish:
                uIElements.ResolutionBG.color = parameters.FinalBGColor;
                uIElements.ResolutionStateInfoText.text = "Final Score";

                StartCoroutine(CalculateScore());
                uIElements.FinishUIElements.gameObject.SetActive(true);
                uIElements.HighScoreText.gameObject.SetActive(true);
                uIElements.HighScoreText.text = ((highscore > events.StartupHighscore) ? "<color=yellow>new </color>" : string.Empty) + "Highscore: " + highscore;
                break;
        }
    }

    // IEnumerator SolutionScreen()
    // {
    //     var waitForButton = new WaitForUIButtons(continueButton);
    //     yield return waitForButton.Reset();
    //     if (waitForButton.PressedButton == continueButton)
    //     {
    //         if (IE_DisplayTimedResolution != null)
    //         {
    //             StopCoroutine(IE_DisplayTimedResolution);
    //         }
    //         IE_DisplayTimedResolution = DisplayTimedResolution();
    //         StartCoroutine(IE_DisplayTimedResolution);
    //     }     
    // }

    /// Function that is used to calculate and display the score.
    IEnumerator CalculateScore()
    {
        var scoreValue = 0;
        while (scoreValue < events.CurrentFinalScore)
        {
            scoreValue++;
            uIElements.ResolutionScoreText.text = scoreValue.ToString() + "/" + QuestionCounter.ToString();

            yield return null;
        }
        QuestionCounter = 0;
    }

    /// Function that is used to create new question answers.
    void CreateAnswers(Question question)
    {
        EraseAnswers();

        float offset = 0 - parameters.Margins;
        for (int i = 0; i < question.Answers.Length; i++)
        {
            AnswerData newAnswer = (AnswerData)Instantiate(answerPrefab, uIElements.AnswersContentArea);
            newAnswer.UpdateData(question.Answers[i].Info, i);

            newAnswer.Rect.anchoredPosition = new Vector2(0, offset);

            offset -= (newAnswer.Rect.sizeDelta.y + parameters.Margins);
            uIElements.AnswersContentArea.sizeDelta = new Vector2(uIElements.AnswersContentArea.sizeDelta.x, offset * -1);

            currentAnswers.Add(newAnswer);
        }
    }

    /// Function that is used to erase current created answers.
    void EraseAnswers()
    {
        foreach (var answer in currentAnswers)
        {
            Destroy(answer.gameObject);
        }
        currentAnswers.Clear();
    }

    /// Function that is used to update score text UI.
    void UpdateScoreUI()
    {
        uIElements.ScoreText.text = "Score: " + events.CurrentFinalScore + "/" + QuestionCounter.ToString();
    }
}