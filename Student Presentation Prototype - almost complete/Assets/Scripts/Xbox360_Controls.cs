//------------------------------------------------------------------------------
//daniel watson
//xbxo 360 controller controls
//used to take in Xbox 360 controls 

//Version log: 
//	Mostly rewritten to incorporate multiplayer control set up - all that is left of daniels stuff is what happens with each button press -- for now
//
//------------------------------------------------------------------------------
using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Xbox360_Controls : MonoBehaviour 
{
	string[,] gamepad = new string[9,6] {
											{"p0horizontal", "p0vertical", "p0attack", "p0Y", "p0action", "p0start"},
											{"p1horizontal", "p1vertical", "p1attack", "p1Y", "p1action", "p1start"},
											{"p2horizontal", "p2vertical", "p2attack", "p2Y", "p2action", "p2start"},
											{"p3horizontal", "p3vertical", "p3attack", "p3Y", "p3action", "p3start"},
											{"p4horizontal", "p4vertical", "p4attack", "p4Y", "p4action", "p4start"},
											{"p5horizontal", "p5vertical", "p5attack", "p5Y", "p5action", "p5start"},
											{"p6horizontal", "p6vertical", "p6attack", "p6Y", "p6action", "p6start"},
											{"p7horizontal", "p7vertical", "p7attack", "p7Y", "p7action", "p7start"},
											{"p8horizontal", "p8vertical", "p8attack", "p8Y", "p8action", "p8start"}											
										};
	public int playerController;
	public Animator playerAnimator;

	void Start()
	{
		playerAnimator = GetComponentInChildren<Animator>();

		for(int i = 1; i < 9; i++)
		{
			if(this.gameObject.name == "Player" + i)
			{
				if(StartMenu.controllerNumber != null)
				{
					if(StartMenu.controllerNumber.Count >= i)
					{
						playerController = StartMenu.controllerNumber[i - 1];

						//Debug.Log(this.name + " " + playerController);
						break;
					}
					else
					{
						this.gameObject.SetActive(false);
					}
				}
			}
		}
	}

	void Update()
	{
		UserInputs();
	}
	
	void UserInputs()
	{
		if(playerAnimator)
		{
			//get the current state
			AnimatorStateInfo stateInfo = playerAnimator.GetCurrentAnimatorStateInfo(0);

			if(Input.GetButton(gamepad[playerController, 4]))//gets the A button
			{				
				//Debug.Log("A" + (playerController) + " Button!");
			}

			if(Input.GetButton(gamepad[playerController, 5]))//gets the B button
			{
				//Debug.Log("Start" + (playerController) + " Button!");
			}

			if(Input.GetButton(gamepad[playerController, 2]))//Gets the X button
			{
				//if we're in "Run" mode, respond to input for jump, and set the Jump parameter accordingly. 
				if(stateInfo.nameHash == Animator.StringToHash("Base Layer.Run"))
				{ 
					playerAnimator.SetBool("Attack1", true);
					float time = playerAnimator.GetCurrentAnimatorStateInfo(0).length;
					StartCoroutine(SwingOnce(time/2.0f));
				}
				else if(stateInfo.nameHash == Animator.StringToHash("Base Layer.Idle"))
				{ 
					playerAnimator.SetBool("Attack1", true);
					float time = playerAnimator.GetCurrentAnimatorStateInfo(0).length;
					StartCoroutine(SwingOnce(time/6.0f));
				}

				//Debug.Log("X" + (playerController) + " Button!");
				
			}

			if(Input.GetButton(gamepad[playerController, 3]))//Gets the Y button
			{
				//Debug.Log("Y" + (playerController) + " Button!");
			}
		}
	}

	IEnumerator SwingOnce(float time)
	{
		yield return new WaitForSeconds(time);
		playerAnimator.SetBool("Attack1", false);
	}
}

