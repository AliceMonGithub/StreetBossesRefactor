using CodeBase;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.CodeBase
{
    public class CharacterView : MonoBehaviour
    {
    //    [SerializeField] private Transform _transform;
    //    [SerializeField] private Animator _animator;
    //    [SerializeField] private Image _sliderImage;
    //    [SerializeField] private GameObject _sliderRoot;

    //    [SerializeField] private ParticleSystem _damageParticle;

    //    [Space] [SerializeField] private Character _character;

    //    public Transform Transform;
    //    public Character Enemy;

    //    [HideInInspector] public bool IsPlayerCharacter;
    //    private readonly int AttackHash = Animator.StringToHash("Attack");
    //    private readonly int DieHash = Animator.StringToHash("Die");

    //    private readonly int WalkHash = Animator.StringToHash("Walk");

    //    private bool _canAttack = true;

    //    private CharacterPlacing _characterPlacing;

    //    private CharacterState _characterState;
    //    private bool _die;

    //    private bool _isEnemyNull => Enemy == null;

    //    private void Awake()
    //    {
    //        UpdateSlider();
    //    }

    //    public void Die()
    //    {
    //        if (_die) return;

    //        _die = true;

    //        _sliderRoot.SetActive(false);

    //        _characterPlacing.RemoveAtList(IsPlayerCharacter, _character);

    //        _characterPlacing.TryFinishGame(IsPlayerCharacter);

    //        _animator.SetTrigger(DieHash);

    //        enabled = false;
    //    }

    //    public void AttackEnemy()
    //    {
    //        Enemy.TakeDamage(_character);

    //        if(Enemy?.PunchFX != null)
    //        {
    ////            Enemy.PunchFX.InstantiateFX();
    //        }

    //        _canAttack = true;
    //    }

    //    public void SetServices(CharacterPlacing characterPlacing) => _characterPlacing = characterPlacing;

    //    public void SetEnemy(Character character) => Enemy = character;

    //    public void SetCharacter(Character character) => _character = character;

    //    private void MoveToEnemy()
    //    {
    //        var delta = Enemy.CharacterView.Transform.position - Transform.position;

    //        delta.Normalize();

    //        _transform.position = _transform.position + delta * (_character.MoveSpeed * Time.deltaTime);
    //    }

    //    private void Update()
    //    {
    //        if (_isEnemyNull)
    //        {
    //            Enemy = _characterPlacing.FindFirstCharacter(IsPlayerCharacter);

    //            return;
    //        }

    //        var distance = (Enemy.CharacterView.Transform.position - Transform.position);

    //        if (distance.x > 0)
    //        {
    //            SetScale(false);
    //        }
    //        else
    //        {
    //            SetScale(true);
    //        }

    //        if (distance.magnitude > _character.Distance)
    //        {
    //            _characterState = CharacterState.Move;

    //            _animator.SetBool(WalkHash, true);
    //        }
    //        else
    //        {
    //            _characterState = CharacterState.Attack;

    //            _animator.SetBool(WalkHash, false);
    //        }

    //        if (_characterState == CharacterState.Move)
    //        {
    //            MoveToEnemy();

    //            _animator.SetBool(WalkHash, true);
    //        }
    //        else if (_characterState == CharacterState.Attack && _canAttack)
    //        {
    //            _animator.SetTrigger(AttackHash);

    //            _canAttack = false;
    //        }
    //    }

    //    private void OnEnable()
    //    {
    //        _character.HealthValueChanged += UpdateSlider;
    //        _character.HealthValueChanged += ShowDamageParticles;
    //    }

    //    private void ShowDamageParticles()
    //    {
    //        Instantiate(_damageParticle, _transform.position, Quaternion.identity).Play();
    //    }

    //    private void OnDisable() => _character.HealthValueChanged -= UpdateSlider;

    //    private void UpdateSlider()
    //    {
    //        _sliderImage.fillAmount = _character.Health / (_character.MaxHealth / 100f) / 100f;
    //    }

    //    private void SetScale(bool scaleRight)
    //    {
    //        var scale = _transform.localScale;

    //        if (scaleRight)
    //        {
    //            scale.x = Mathf.Abs(scale.x);

    //            _transform.localScale = scale;
    //        }
    //        else
    //        {
    //            scale.x = -Mathf.Abs(scale.x);

    //            _transform.localScale = scale;
    //        }
    //    }
    }
}