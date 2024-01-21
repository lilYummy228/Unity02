using UnityEngine;

public class DamageManagement : MonoBehaviour
{
    [SerializeField] private Material _blinkMaterial;

    private SpriteRenderer _spriteRenderer;
    private Material _defaultMaterial;
    private float _blinkTime = 0.2f;

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _defaultMaterial = _spriteRenderer.material;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.TryGetComponent(out Enemy enemy))
        {
            _spriteRenderer.material = _blinkMaterial;

            Invoke(nameof(ResetMaterial), _blinkTime);
        }
    }

    private void ResetMaterial()
    {
        _spriteRenderer.material = _defaultMaterial;
    }
}
