using TMPro;
using UnityEngine;

public class ScoreViewBomb : ScoreView<BombFactory>
{
    [SerializeField] private TMP_Text _scoreBomb;

    protected override void OnEnable()
    {
        Factory.NumberChanged += OnNumberChanged;
    }

    protected override void OnDisable()
    {
        Factory.NumberChanged -= OnNumberChanged;
    }

    protected override void OnNumberChanged(int score)
    {
        _scoreBomb.text = "Bomb: " + score.ToString();
    }
}