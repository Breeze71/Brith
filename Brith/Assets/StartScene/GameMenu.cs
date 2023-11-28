using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMenu : MonoBehaviour
{
    [SerializeField] private string TechtreeScene;
    [SerializeField] private string FirstLv;
    [SerializeField] private string OptionScene;
    public void OnContinnueGame()
    {
        //todo
        SceneManager.LoadScene(TechtreeScene);
    }
    public void OnStartGame()
    {
        //todo
        SceneManager.LoadScene(FirstLv);
    }
    public void OnOption()
    {
        SceneManager.LoadScene(OptionScene);
    }
    public void OnExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
    }
}
