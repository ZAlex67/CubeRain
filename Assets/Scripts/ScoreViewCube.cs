using TMPro;
using UnityEngine;

public class ScoreViewCube : MonoBehaviour
{
    [SerializeField] private CubeFactory _cubeFactory;
    [SerializeField] private TMP_Text _scoreCube;

    private void OnEnable()
    {
        _cubeFactory.CubeNumberChanged += OnCubeNumberChanged;
    }

    private void OnDisable()
    {
        _cubeFactory.CubeNumberChanged -= OnCubeNumberChanged;
    }

    private void OnCubeNumberChanged(int score)
    {
        _scoreCube.text = "Cube: " + score.ToString();
    }    
}