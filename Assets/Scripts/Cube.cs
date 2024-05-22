using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Renderer))]
[RequireComponent(typeof(Rigidbody))]
public class Cube : MonoBehaviour
{
    [SerializeField] private CubeFactory _cubeFactory;
    [SerializeField] private BombFactory _bombFactory;

    private Renderer _renderer;
    private Color[] _colors = new Color[] { Color.green, Color.black, Color.blue, Color.red };
    private bool _isWhite = true;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Ground ground) && _isWhite)
        {
            SetColor(_colors[Random.Range(0, _colors.Length)]);
            StartCoroutine(LifeTime());
            _isWhite = false;
        }
    }

    public void SetCubeFactory(CubeFactory cubeFactory)
    {
        _cubeFactory = cubeFactory;
    }

    public void SetBombFactory(BombFactory bombFactory)
    {
        _bombFactory = bombFactory;
    }

    private void SetColor(Color color)
    {
        _renderer.material.color = color;
    }

    private IEnumerator LifeTime()
    {
        float minTime = 2f;
        float maxTime = 5f;

        WaitForSeconds waitRelease = new WaitForSeconds(Random.Range(minTime, maxTime));

        yield return waitRelease;

        if (isActiveAndEnabled)
        {
            _cubeFactory.ReleaseObject(this);
            SetColor(Color.white);
            _isWhite = true;
            _bombFactory.SetPosition(this);
            _bombFactory.GetPrefab();
        }
    }
}