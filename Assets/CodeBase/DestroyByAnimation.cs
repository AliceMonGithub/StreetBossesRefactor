using System.Collections;
using UnityEngine;

namespace Assets
{
    public class DestroyByAnimation : MonoBehaviour
    {
        private void Destroy()
        {
            Destroy(gameObject);
        }
    }
}