using TMPro;
using UnityEngine;

public class ScoreViewCube : ScoreView<CubeFactory>
{
    [SerializeField] private TMP_Text _scoreCube;

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
        _scoreCube.text = "Cube: " + score.ToString();
    }    
}