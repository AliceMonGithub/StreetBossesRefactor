using UnityEngine;

namespace CodeBase
{
    public class StreetCharactersFactory : MonoBehaviour
    {
        [SerializeField] private PlayerStats _playerStats;
        [SerializeField] private MovePoint[] _spawnPoints;
        private void Awake()
        {
            CreateCharacters();
        }

        private void CreateCharacters()
        {
            foreach (Character character in _playerStats.Characters)
            {
                MovePoint movePoint = GetRandomPoint();
                
                Instantiate(character.CharacterWalk, movePoint.Transform.position, Quaternion.identity).Initialize(movePoint.Points);
            }
        }

        private MovePoint GetRandomPoint() => _spawnPoints[Random.Range(0, _spawnPoints.Length)];
    }
}