using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{

    [SerializeField]
    private AudioSource music;

    [SerializeField]
    private TextMeshProUGUI scoreText;

    public void Awake()
    {
        ScoreKeeper.Score = 0;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.M))
        {
            music.volume = music.volume == 0 ? 1 : 0;
        }

        scoreText.text = $"GOOP : {ScoreKeeper.Score}";
    }
}
