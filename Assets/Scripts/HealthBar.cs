using System;
using UnityEngine;

/// <summary>
/// Displays a configurable health bar for any object with a Damageable as a parent
/// </summary>
public class HealthBar : MonoBehaviour {

    private MaterialPropertyBlock _matBlock;
    private MeshRenderer _meshRenderer;
    private Camera _mainCamera;
    public float currentHp = 100f;
    [SerializeField] private float _maxHP = 100f;

    private bool _inCooldown = false;

    public delegate void Death();

    public event Death onDie;
    private float _lastHealTime = 0;
    [SerializeField] private float _delayBetweenHeal = 5;
    [SerializeField] private float _healInCooldown = 1;
    
    private void Awake() {
        _meshRenderer = GetComponent<MeshRenderer>();
        _matBlock = new MaterialPropertyBlock();
        // get the damageable parent we're attached to
    }

    private void Start() {
        _mainCamera = Camera.main;
    }

    private void Update() {
        // Only display on partial health
        if (currentHp < _maxHP) {
            _meshRenderer.enabled = true;
            AlignCamera();
            UpdateParams();
        } else if(!_inCooldown && currentHp < _maxHP) {
            _meshRenderer.enabled = false;
        }
        if (_inCooldown && (Time.time - _lastHealTime) >= _delayBetweenHeal)
        {
            _lastHealTime = Time.time;
            if (currentHp + _healInCooldown >= _maxHP)
                _inCooldown = false;
            GetHealth(_healInCooldown);
        }
    }

    private void UpdateParams() {
        _meshRenderer.GetPropertyBlock(_matBlock);
        _matBlock.SetFloat("_Fill", currentHp / _maxHP);
        _meshRenderer.SetPropertyBlock(_matBlock);
    }

    private void AlignCamera() {
        if (_mainCamera != null) {
            var camXform = _mainCamera.transform;
            var forward = transform.position - camXform.position;
            forward.Normalize();
            var up = Vector3.Cross(forward, camXform.right);
            transform.rotation = Quaternion.LookRotation(forward, up);
        }
    }

    public void GetHealth(float count)
    {
        if (currentHp + count >= _maxHP) currentHp = _maxHP;
        else currentHp += count;
    }

    public void SetHealth(float newHp)
    {
        currentHp = newHp;
        if (newHp <= 0)
        {
            Cooldown();
            onDie?.Invoke();
        }
    }

    public void GetDamage(float damage)
    {
        if (currentHp - damage <= 0)
        {
            Cooldown();
            onDie?.Invoke();
        }
        else currentHp -= damage;
    }

    public float GetCurrentHP()
    {
        return currentHp;
    }
    
    public float GetMaxHP()
    {
        return _maxHP;
    }

    public void Cooldown()
    {
        _inCooldown = true;
    }

}