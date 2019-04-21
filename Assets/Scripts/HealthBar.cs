using System;
using UnityEngine;

/// <summary>
/// Displays a configurable health bar for any object with a Damageable as a parent
/// </summary>
public class HealthBar : MonoBehaviour {

    protected MaterialPropertyBlock _matBlock;
    protected MeshRenderer _meshRenderer;
    private Camera _mainCamera;
    public float currentHp = 100f;
    [SerializeField] protected float _maxHP = 100f;

    protected bool _inCooldown = false;

    public delegate void Death();

    public event Death onDie;
    protected float _lastHealTime = 0;
    [SerializeField] protected float _delayBetweenHeal = 5;
    [SerializeField] protected float _healInCooldown = 1;

    public Transform Player;

    private void Awake() {
        _meshRenderer = GetComponent<MeshRenderer>();
        _matBlock = new MaterialPropertyBlock();
        // get the damageable parent we're attached to

        // Player = GameObject.FindGameObjectWithTag("Player").transform;
        Player = Camera.main.transform;
    }

    public virtual void Start() {
        _mainCamera = Camera.main;
    }

    public virtual void Update() {
        // Only display on partial health
        if (currentHp < _maxHP) {
            _meshRenderer.enabled = true;
            AlignCamera();
            UpdateParams();
        } else if(!_inCooldown && currentHp < _maxHP) {
            _meshRenderer.enabled = false;
        }

        //Look at player
        transform.LookAt(Player);
    }

    public virtual void UpdateParams() {
        _meshRenderer.GetPropertyBlock(_matBlock);
        _matBlock.SetFloat("_Fill", currentHp / _maxHP);
        _meshRenderer.SetPropertyBlock(_matBlock);
    }

    public virtual void AlignCamera() {
        if (_mainCamera != null) {
            var camXform = _mainCamera.transform;
            var forward = transform.position - camXform.position;
            forward.Normalize();
            var up = Vector3.Cross(forward, camXform.right);
            transform.rotation = Quaternion.LookRotation(forward, up);
        }
    }

    public virtual void GetHealth(float count)
    {
        if (currentHp + count >= _maxHP) currentHp = _maxHP;
        else currentHp += count;
    }

    public virtual void SetHealth(float newHp)
    {
        currentHp = newHp;
        if (newHp <= 0)
        {
            Cooldown();
            CallOnDieEvent();
        }
    }

    public virtual void GetDamage(float damage)
    {
        if (currentHp - damage <= 0)
        {
            CallOnDieEvent();
        }
        else currentHp -= damage;
    }

    public void CallOnDieEvent()
    {
        onDie?.Invoke();
    }

    public virtual float GetCurrentHP()
    {
        return currentHp;
    }
    
    public virtual float GetMaxHP()
    {
        return _maxHP;
    }

    public void Cooldown()
    {
        _inCooldown = true;
    }

}