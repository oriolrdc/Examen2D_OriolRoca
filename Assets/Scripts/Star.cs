using UnityEngine;

public class Star : MonoBehaviour
{
    private GameManager _gameManager;

    void Awake()
    {
        _gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }
    
    public void Death()
    {

        _gameManager.Win();
        Destroy(gameObject);
    }
}
