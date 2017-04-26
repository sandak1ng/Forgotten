using UnityEngine;
using System.Collections;

public class menu : MonoBehaviour
{
	public GameObject PlayerObject;

	public void StartGame ()
	{
		PlayerPrefs.SetInt ("CurrentScore", 0);
		PlayerPrefs.SetFloat ("playerX", 11);
		PlayerPrefs.SetFloat ("playerY", 95);
		PlayerPrefs.SetFloat ("playerZ", -63);

		Application.LoadLevel ("forgotten");
	}

	public void ExitGame ()
	{
		Application.Quit ();          
	}
		
	public void DisableBoolInAnimator(Animator anim) {
		anim.SetBool ("isDisplayed", false);
	}

	public void EnableBoolInAnimator(Animator anim) {
		anim.SetBool ("isDisplayed", true);
	}
}
