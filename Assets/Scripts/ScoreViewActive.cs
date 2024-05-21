using TMPro;
using UnityEngine;

public class ScoreViewActive : MonoBehaviour
{
    [SerializeField] private CubeFactory _cubeFactory;
    [SerializeField] private BombFactory _bombFactory;
    [SerializeField] private TMP_Text _activeCount;

    private int _bombCount;
    private int _cubeCount;

    private void Update()
    {
        ActiveChanged();
    }

    private void OnEnable()
    {
        _cubeFactory.CubeActiveChanged += OnCubeActiveChanged;
        _bombFactory.BombActiveChanged += OnBombActiveChanged;
    }

    private void OnDisable()
    {
        _cubeFactory.CubeActiveChanged -= OnCubeActiveChanged;
        _bombFactory.BombActiveChanged -= OnBombActiveChanged;
    }


    private void OnCubeActiveChanged(int score)
    {
        _cubeCount = score;
    }

    private void OnBombActiveChanged(int score)
    {
        _bombCount = score;
    }

    private void ActiveChanged()
    {
        _activeCount.text = "Active: " + (_cubeCount + _bombCount).ToString();
    }
}