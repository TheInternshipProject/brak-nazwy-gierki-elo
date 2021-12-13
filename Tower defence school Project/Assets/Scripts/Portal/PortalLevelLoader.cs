using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class PortalLevelLoader : MonoBehaviour
{

    //public string sceneToLoad;

    public GameObject loadingScreen;
    public Slider slider;
    public TextMeshProUGUI progressText;
    public int sceneIndex;

    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player") && !other.isTrigger)
        {

            //SceneManager.LoadScene(sceneToLoad);

            StartCoroutine(LoadAsyncchronosuly(sceneIndex));

        }

        IEnumerator LoadAsyncchronosuly(int sceneIndex)
        {
            AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);

            loadingScreen.SetActive(true);

            while (!operation.isDone)
            {
                float progress = Mathf.Clamp01(operation.progress);

                slider.value = progress;
                progressText.text = progress * 100f + "%";

                yield return null;
            }
        }

    }

}
