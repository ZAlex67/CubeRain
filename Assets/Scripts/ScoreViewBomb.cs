using TMPro;
using UnityEngine;

public class ScoreViewBomb : MonoBehaviour
{
    [SerializeField] private BombFactory _bombFactory;
    [SerializeField] private TMP_Text _scoreBomb;

    private void OnEnable()
    {
        _bombFactory.BombNumberChanged += OnBombNumberChanged;
    }

    private void OnDisable()
    {
        _bombFactory.BombNumberChanged -= OnBombNumberChanged;
    }

    private void OnBombNumberChanged(int score)
    {
        _scoreBomb.text = "Bomb: " + score.ToString();
    }
}