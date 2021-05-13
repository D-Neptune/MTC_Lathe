using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InformationGatherer : MonoBehaviour
{
    [SerializeField] public string scene;
    [SerializeField] private LanguageSceneToggle languageScene;
    [SerializeField] private SceneDisplayToggle sceneDisplayToggle;
    [DrawIf("Discover", true)] [SerializeField] private ComponentHint componentHint;
    [DrawIf("MainMenu", true)] [SerializeField] private LoadSceneScript loadSceneScript;
    [SerializeField] private LanguageSceneSwitcher languageSceneSwitcher;
    [SerializeField] private VideoManager2 videoManager;
    [DrawIf("Discover", true)][SerializeField] private DialogueTrigger trigger;
    [DrawIf("AnimScene", true)] [SerializeField] private TriggerDialogueInterface triggerDialogue;
    [DrawIf("AnimScene", true)] [SerializeField] private InteractionManager interactionManager;
    [SerializeField] private bool AnimScene, Discover, MainMenu;

    private string levelToLoad;
    
    // Start is called before the first frame update
    public void Awake()
    {
        LoadInformation();
    }

    public void SaveInformation()
    {
        if (Discover)
        {
            SaveSystem.SaveData(languageScene.Name, languageScene.Number, languageScene.getLanguage(), scene, sceneDisplayToggle.getTutorial(), componentHint);
        }
        else if (AnimScene)
        {
            SaveSystem.SaveData(languageScene.Name, languageScene.Number, languageScene.getLanguage(), scene, sceneDisplayToggle.getTutorial(), interactionManager.CurrentAnim);
        }
    }

    public void LoadInformation()
    {
        DataHandler data = SaveSystem.LoadData();
        if(data != null)
        {
            if (scene.Equals("LatheMainMenu"))
            {
                bool tmpLang = languageScene.getLanguage();
                languageScene.setName(data.StudentName);
                languageScene.setNumber(data.StudentNumber);
                languageScene.setLanguage(data.Language);
                sceneDisplayToggle.setTutorial(data.IsTutorial);
                levelToLoad = data.savedLevel;
                //Debug.Log(data.StudentName);
                //Debug.Log(data.StudentNumber);
                Debug.Log(data.Language);
                if (data.StudentName.Equals("CEED ADMIN") && data.StudentNumber.Equals("1234567"))
                {
                    //Debug.Log("ADMIN MODE");
                    sceneDisplayToggle.AdminMode = true;
                }
                if (languageScene.getLanguage() && (languageScene.getLanguage() != tmpLang))
                {
                    languageSceneSwitcher.toggleOnStart();
                }
            }
            else if (scene.Equals("Operations Scene"))
            {
                if (sceneDisplayToggle.getTutorial())
                {
                    if (data.SavedAnim != anim.NA)
                    {
                        interactionManager.SetupAnims(data.SavedAnim);
                        videoManager.VideoWatched = true;
                        triggerDialogue.SentenceTrigger = true;
                        //Debug.Log(sceneDisplayToggle.AdminMode);
                    }
                }
            }
            else if(scene.Equals("Discovery Scene"))
            {
                if (sceneDisplayToggle.getTutorial())
                {
                    foreach (SetupOnHover part in componentHint.parts)
                    {
                        if (data.DetailIndexes.Contains(part.DetailIndex))
                        {
                            //Debug.Log("Contains");
                            part.savedPartClicked = true;
                        }
                    }
                    videoManager.VideoWatched = true;
                    trigger.sentenceTrigger = true;
                }
            }

        }
    }

    public void ContinueTraining()
    {
        loadSceneScript.loadlevel(levelToLoad);
    }
}
