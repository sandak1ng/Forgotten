using UnityEngine;
using System.Collections;

public class menu : MonoBehaviour
{

	public void StartGame ()
	{
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
