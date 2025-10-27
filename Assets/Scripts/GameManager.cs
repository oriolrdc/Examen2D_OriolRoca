using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public void Win()
    {
        SceneManager.LoadScene(1);
    }
}
