//using UnityEngine;
//using UnityEngine.SceneManagement;

//public class CompleteLevel : MonoBehaviour{


//    public SceneFader sceneFader;

//    public string menuSceneName = "MainMenu";

//    public string nextLevel = "Level02";
//    public int levelToUnlock = 2;

//    public void Continue()
//    {
//        if (levelToUnlock > PlayerPrefs.GetInt("levelReached", 1))
//        {
//            PlayerPrefs.SetInt("levelReached", levelToUnlock);
//        }

//        sceneFader.FadeTo(nextLevel);
//    }
//    public void Menu()
//    {
//        sceneFader.FadeTo(menuSceneName);
        
//    }
//}
