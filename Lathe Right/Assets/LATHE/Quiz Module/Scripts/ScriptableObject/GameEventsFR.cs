using UnityEngine;

[CreateAssetMenu(fileName = "GameEventsFR", menuName = "Quiz/new GameEventsFR")]
public class GameEventsFR : ScriptableObject {

    public delegate void    UpdateQuestionUICallback            (QuestionFR question);
    public                  UpdateQuestionUICallback            UpdateQuestionUI                = null;

    public delegate void    UpdateSolutionUICallback            (QuestionFR question);
    public                  UpdateSolutionUICallback            UpdateSolutionUI                = null;

    public delegate void    ShowSolutionCallback                (int index, bool b);
    public                  ShowSolutionCallback                ShowSolution                    = null;

    public delegate void    UpdateQuestionAnswerCallback        (AnswerDataFR pickedAnswer);
    public                  UpdateQuestionAnswerCallback        UpdateQuestionAnswer            = null;

    public delegate void    DisplayResolutionScreenCallback     (UIManagerFR.ResolutionScreenType type, int score);
    public                  DisplayResolutionScreenCallback     DisplayResolutionScreen         = null;

    public delegate void    ScoreUpdatedCallback();
    public                  ScoreUpdatedCallback                ScoreUpdated                    = null;

    [HideInInspector]
    public                  int                                 CurrentFinalScore               = 0;
    [HideInInspector]
    public                  int                                 StartupHighscore                = 0;
}