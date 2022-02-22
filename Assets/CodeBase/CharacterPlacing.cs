using System.Collections.Generic;
using CodeBase.CameraLogic;
using UltEvents;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace CodeBase
{
    public class CharacterPlacing : MonoBehaviour
    {
        [SerializeField] private CharacterSpawnPoint[] _spawnPoints;
        [SerializeField] private CharacterSpawnPoint[] _enemyspawnPoints;

        [SerializeField] private FinishMenu _winMenu;
        [SerializeField] private FinishMenu _loseMenu;
        [SerializeField] private Business _enemyBusiness;
        [SerializeField] private CameraMove _cameraMove;

        [SerializeField] private GameObject _playButton;
        [SerializeField] private GameObject[] _clearOnPlayObjects;

        [SerializeField] private PlayerStats _playerStats;

        public List<Character> EnemyCharacters = new List<Character>();
        public List<Character> PlayerCharacters = new List<Character>();

        private SceneLoader _sceneLoader;

        private int _additionalDamage;
        private int _enemyAdditionalDamage;

        public UltEvent OnWin;
        public UltEvent OnLose;
        public UltEvent OnStartFight;

        [Inject]
        private void Construct(SceneLoader sceneLoader) 
        {
            _sceneLoader = sceneLoader;
        }

        private void Awake()
        {
            _enemyBusiness = _enemyBusiness.CurrentBusiness;

            AddEnemyCharacters();

            CheckCount();
        }

        public CharacterSpawnPoint AddCharacter(Character character)
        {
            var spawnPoint = FindFirstPoint();

            if (spawnPoint == null) return null;

            var initializedCharacter = spawnPoint.Initialize(character, this, true);

            if(initializedCharacter.AdditionalDamage > 0)
            {
                _additionalDamage += initializedCharacter.AdditionalDamage;
            }

            PlayerCharacters.Add(initializedCharacter);

            ChangeScale(initializedCharacter);

            CheckCount();

            return spawnPoint;
        }

        public void RemoveCharacter(Character removingCharacter)
        {
            var character = removingCharacter.gameObject;

            if (removingCharacter.AdditionalDamage > 0)
            {
                _additionalDamage -= removingCharacter.AdditionalDamage;
            }

            PlayerCharacters.Remove(removingCharacter);

            CheckCount();

            Destroy(character);
        }

        public bool CanAddCharacter()
        {
            if (PlayerCharacters.Count < _spawnPoints.Length) return true;

            return false;
        }

        public void AddEnemyCharacters()
        {
            var index = 0;

            foreach (var character in _enemyBusiness.EnemyCharacters)
            {
                var enemyCharacter = _enemyspawnPoints[index].Initialize(character, this, false);

                if (enemyCharacter.AdditionalDamage > 0)
                {
                    _enemyAdditionalDamage += enemyCharacter.AdditionalDamage;
                }

                EnemyCharacters.Add(enemyCharacter);

                index++;
            }
        }

        public void TryFinishGame(bool isPlayerCharacter)
        {
            if (isPlayerCharacter)
            {
                if (PlayerCharacters.Count == 0)
                {
                    _loseMenu.Show(_enemyBusiness.Sprite);
                    OnLose.Invoke();
                }
            }
            else
            {
                if (EnemyCharacters.Count == 0)
                {
                    _playerStats.Businesses.Add(_enemyBusiness);
                    
                    _winMenu.Show(_enemyBusiness.Sprite);
                    OnWin.Invoke();
                }
            }
        }

        public void FinishGame()
        {
            _sceneLoader.LoadScene(_playerStats.CurrentSceneName);
        }

        public void Restart()
        {
            _sceneLoader.LoadScene(SceneManager.GetActiveScene().name);
        }

        public void StartFight()
        {
            //foreach (var gameObject in _clearOnPlayObjects) gameObject.SetActive(false);
            OnStartFight.Invoke();
            
            _cameraMove.enabled = true;

            ApplyAdditionalDamage();

            FindEnemiesForCharacters();

            ActiveCharacters();
        }

        public Character FindFirstCharacter(bool isPlayerCharacter)
        {
            List<Character> characters;
            Character result = null;

            var distance = 0f;

            if (isPlayerCharacter)
                characters = EnemyCharacters;
            else
                characters = PlayerCharacters;

            foreach (var character in characters)
            {
                if (character == null) continue;

                if (result == null)
                {
                    result = character;

                    distance = (character.transform.position - transform.position).magnitude;
                }
                else
                {
                    if (distance > (character.transform.position - result.transform.position).magnitude)
                        result = character;
                }
            }

            return result;
        }

        public void RemoveAtList(bool isPlayerCharacter, Character character)
        {
            if (isPlayerCharacter)
                PlayerCharacters.Remove(character);
            else
                EnemyCharacters.Remove(character);
        }

        public void ActiveCharacters()
        {
            foreach (var character in PlayerCharacters) character.CharacterView.enabled = true;

            foreach (var character in EnemyCharacters) character.CharacterView.enabled = true;
        }

        private void FindEnemiesForCharacters()
        {
            var index = 0;

            foreach (var character in PlayerCharacters)
            {
                if (EnemyCharacters.Count - 1 < index) index--;

                character.CharacterView.SetEnemy(EnemyCharacters[index]);

                index++;
            }

            index = 0;

            foreach (var character in EnemyCharacters)
            {
                if (PlayerCharacters.Count - 1 < index) index--;

                character.CharacterView.SetEnemy(PlayerCharacters[index]);

                index++;
            }
        }

        private void ApplyAdditionalDamage()
        {
            foreach (Character character in PlayerCharacters)
            {
                character.Damage += _additionalDamage;
            }

            foreach (Character character in EnemyCharacters)
            {
                character.Damage += _enemyAdditionalDamage;
            }
        }

        private void ChangeScale(Character character)
        {
            var scale = character.transform.localScale;

            scale.x = -scale.x;

            character.transform.localScale = scale;
        }

        private CharacterSpawnPoint FindFirstPoint()
        {
            foreach (var spawnPoint in _spawnPoints)
                if (spawnPoint.Character == null)
                    return spawnPoint;

            return null;
        }

        private void CheckCount()
        {
            if (PlayerCharacters.Count == 0)
            {
                _playButton.SetActive(false);

                return;
            }

            _playButton.SetActive(true);
        }
    }
}