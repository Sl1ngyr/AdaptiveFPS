using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class QuitGame : MonoBehaviour
    {
        private Button quitButton;

        private void Start()
        {
            quitButton = GetComponent<Button>();
            quitButton.onClick.AddListener(Quit);
        }

        private void OnDestroy()
        {
            quitButton.onClick.RemoveListener(Quit);
        }
        
        private void Quit()
        {
            Debug.Log("Quit Game");
            if (Application.platform == RuntimePlatform.Android)
            {
                AndroidJavaObject activity = new AndroidJavaClass("com.unity3d.player.UnityPlayer").GetStatic<AndroidJavaClass>("currentActivity");
                activity.Call("finish");
            }
            else
            {
                Application.Quit();
            }
        }
    }
}