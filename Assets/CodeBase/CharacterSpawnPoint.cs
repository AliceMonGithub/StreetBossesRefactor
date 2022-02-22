using UnityEngine;

namespace CodeBase
{
    public class CharacterSpawnPoint : MonoBehaviour
    {
        [SerializeField] private Transform _transform;

        public Character Character;

        public Character Initialize(Character character, CharacterPlacing characterPlacing, bool isPlayerCharacter)
        {
            if (Character != null) Clear();

            Character = Instantiate(character, _transform.position, Quaternion.identity, _transform);

            Character.CharacterView.SetServices(characterPlacing);
            Character.CharacterView.SetCharacter(Character);

            Character.CharacterView.IsPlayerCharacter = isPlayerCharacter;

            return Character;
        }

        private void Clear()
        {
            Destroy(Character.gameObject);
        }
    }
}