using UnityEngine;
using UnityEngine.UI;

public class LevelSelector : MonoBehaviour
{

    public SceneFader sceneFader;
    //pour stocker les buttons
    public Button[] levelButtons;

    private void Start()
    {
        //ça nous permette de recupere le dernier niveau enregistre
        int levelReached = PlayerPrefs.GetInt("levelReached", 1);

        for (int i = 0; i < levelButtons.Length; i++)
        {
            if (i + 1 > levelReached)
            {
                levelButtons[i].interactable = false;
            }

        }
        //pour d'effacer levelReached
        //PlayerPrefs.DeleteKey("levelReached");
    }

    public void Select(string levelName)
    {
        sceneFader.FadeTo(levelName);
    }
}
