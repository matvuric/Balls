using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayButton : MonoBehaviour
{
    public void SceneChanger(int sceneNumber)
    {
        SceneManager.LoadScene(sceneNumber);
    }
}
