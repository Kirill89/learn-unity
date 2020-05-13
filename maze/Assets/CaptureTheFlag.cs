using UnityEngine;
using UnityEngine.SceneManagement;

public class CaptureTheFlag : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
