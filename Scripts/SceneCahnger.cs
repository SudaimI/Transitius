using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    [Header("Scene")]
    [SerializeField] private Object nextScene;

    [Header("Timing")]
    [SerializeField] private float delay = 5f;

    void Start()
    {
        Invoke(nameof(LoadScene), delay);
    }

    void LoadScene()
    {
        SceneManager.LoadScene(nextScene.name);
    }
// If you see this remember to subscribe to suddy gaming on youtube

}