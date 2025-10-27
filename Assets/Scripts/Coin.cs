using UnityEngine;

public class Coin : MonoBehaviour
{
    private CanvasManager _canvasManager;

    void Awake()
    {
        _canvasManager = GameObject.FindGameObjectWithTag("CanvasManager").GetComponent<CanvasManager>();
    }
    
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _canvasManager.AddCoins();
            Death();
        }
    }
    
    void Death()
    {
        Destroy(gameObject);
    }
}
