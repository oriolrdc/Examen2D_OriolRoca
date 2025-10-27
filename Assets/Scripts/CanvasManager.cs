using UnityEngine;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour //GUI
{
    /*public static GUI instance;*/
    [SerializeField] private Text _coinCounter;
    private float _coins;
   /* void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        if (instance != this)
        {
            
        }

        GameObject.DontDestroyOnLoad(gameManager);
    }*/
    public void AddCoins()
    {
        _coins++;
        _coinCounter.text = "0" + _coins.ToString();
    }
}
