using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour {

	public void PlayClicked()
    {
        SceneManager.LoadScene(1);
    }
}
