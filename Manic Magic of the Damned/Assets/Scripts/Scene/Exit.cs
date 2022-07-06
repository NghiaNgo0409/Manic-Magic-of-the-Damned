using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Exit : MonoBehaviour
{
    [SerializeField] string sceneName;
    [SerializeField] string scenePassword;
    [SerializeField] Animator anim;
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Player")
        {
            PlayerController.instance.sceneNewPassword = scenePassword;
            StartCoroutine(LoadScene(sceneName));
        }
    }

    IEnumerator LoadScene(string name)
    {
        anim.SetTrigger("Start");
        PlayerController.instance.state = State.Transition;
        yield return new WaitForSeconds(1f);
        PlayerController.instance.state = State.Normal;
        SceneManager.LoadScene(sceneName);
        GameManager.instance.enemies.Clear();
    }   
}
