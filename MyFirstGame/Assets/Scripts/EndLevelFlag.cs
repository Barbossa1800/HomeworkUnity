using UnityEngine;
using UnityEngine.SceneManagement;

public class EndLevelFlag : MonoBehaviour
{
    [SerializeField] private int _coinsToNextLevel;
    [SerializeField] private int _levelToLoad;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private Sprite _waveOfTheFlag;

    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerController player = other.GetComponent<PlayerController>();
        if (player != null && player.CoinsAmount >= _coinsToNextLevel)
        {
            _spriteRenderer.sprite = _waveOfTheFlag;
            //SceneManager.LoadScene("Level_2"); //по названию 1 способ, не самое топ, из за стринга
            Invoke(nameof(LoadNextScene), 1f);//задержка
            
        }
    }
    private void LoadNextScene()
    {
        SceneManager.LoadScene(_levelToLoad);
    }
    
    
    
}
