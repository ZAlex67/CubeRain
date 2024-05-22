using UnityEngine;

public abstract class ScoreView<T> : MonoBehaviour where T : MonoBehaviour
{
    [SerializeField] protected T Factory;

    protected abstract void OnEnable();

    protected abstract void OnDisable();

    protected abstract void OnNumberChanged(int score);
}