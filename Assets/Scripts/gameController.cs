using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class gameController : MonoBehaviour {
	public int pontos;
	public int vidas;
	public SpriteRenderer UIlife;
	public Sprite[] lifeSprites;
	public ninja character;
	public int looselvl=0;
	public int winlvl=0;
	public int limite=30;
	TextMesh pointsTXT;
	// Use this for initialization
	void Start () {
		pontos=0;
		pointsTXT=GameObject.Find("points").GetComponent<TextMesh>();
		UIlife.sprite=lifeSprites[vidas];
	}
	

	public void looseLife () {
		vidas--;
		if(vidas<1)
		{
			character.NinjaDeath();
			UIlife.sprite=lifeSprites[0];
		}else
		{
			character.NinjaHurt();
			UIlife.sprite=lifeSprites[vidas];
		}
	}
	public void LooseGame()
	{
        SceneManager.LoadScene(looselvl);
	}
	public void AddPoints()
	{
		pontos++;
		pointsTXT.text=pontos.ToString()+" killed";
		if(pontos>=limite)
		{
			StartCoroutine(winning());
		}
	}
	IEnumerator winning()
	{
		Animator fd=GameObject.Find("fade").GetComponent<Animator>();
		fd.Play("fadeOUT");
		yield return new WaitForSeconds(0.8f);
        SceneManager.LoadScene(winlvl);
	}
}
