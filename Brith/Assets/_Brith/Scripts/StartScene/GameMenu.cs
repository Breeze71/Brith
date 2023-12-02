using UnityEngine;
using UnityEngine.SceneManagement;
using V.Tool.SaveLoadSystem;

public class GameMenu : MonoBehaviour
{
    [SerializeField] private string TechtreeScene;
    [SerializeField] private string FirstLv;
    [SerializeField] private string OptionScene;
    [SerializeField]private string Startscene;

    public void OnContinnueGame()
    {
        //todo
        SceneManager.LoadScene(TechtreeScene);
    }
    public void OnStartGame()
    {
        //todo
        SceneManager.LoadScene(FirstLv);

        DataPersistenceManager.Instance.StartNewGame();
        Debug.Log("New");
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
    public void OptionbackToStart()
    {
        SceneManager.LoadScene(Startscene);
    }
    public void Guidacne()
    {
        SceneManager.LoadScene("Guidance");
    }
    public void ExitGame()
    {
        Application.Quit();
    }
}
