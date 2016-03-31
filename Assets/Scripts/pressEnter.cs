using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class pressEnter : MonoBehaviour {

	public int nivel=1;
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.Return))
		{
            SceneManager.LoadScene(nivel);
		}
	}
}
