using System.Collections;
using UnityEngine;
using System.Diagnostics;

namespace Assets.CodeBase.Blockchain
{
    public class OpenBrowserBehavior : MonoBehaviour
    {
        public void OpenBrowser(string url)
        {
            Application.OpenURL(url);
        }
    }
}