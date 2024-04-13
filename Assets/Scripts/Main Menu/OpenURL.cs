using UnityEngine;

public class OpenURL : MonoBehaviour
{
    public void TryUrl(string url)
    {
        Application.OpenURL(url);
    }
}