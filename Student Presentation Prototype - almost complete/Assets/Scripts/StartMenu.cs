using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StartMenu : MonoBehaviour 
{
	public GUIText playerName;
	GUIText[] playerReady = new GUIText[8];
	public static List<int> controllerNumber;
	bool[] buttonPressed = new bool[8];
	Rect startButton = new Rect(Screen.width/2.5f, Screen.height/3, Screen.width/5, Screen.height/10);
	Rect exitButton = new Rect(Screen.width/2.5f, Screen.height/3, Screen.width/5, Screen.height/10);
	public StartMenuAudio startMenuAudio;
	float guiAlpha = 0;

	void Start()
	{
		startButton.position = new Vector3(10, Screen.height/50 * 45, 0);
		exitButton.position = new Vector3(Screen.width - exitButton.width - 10, Screen.height/50 * 45, 0);
		controllerNumber = new List<int>();
		for(int i = 0; i < buttonPressed.Length; i++)
		{
			buttonPressed[i] = false;
		}
	}

	void Update()
	{
		if(!buttonPressed[0])
		{
			if(Input.GetButton("p1action"))
			{
				controllerNumber.Add(1);
				buttonPressed[0] = true;
				ReadyText();
			}
		}

		if(!buttonPressed[1])
		{
			if(Input.GetButton("p2action"))
			{
				controllerNumber.Add(2);
				buttonPressed[1] = true;
				ReadyText();
			}
		}

		if(!buttonPressed[2])
		{
			if(Input.GetButton("p3action"))
			{
				controllerNumber.Add(3);
				buttonPressed[2] = true;
				ReadyText();
			}
		}

		if(!buttonPressed[3])
		{
			if(Input.GetButton("p4action"))
			{
				controllerNumber.Add(4);
				buttonPressed[3] = true;
				ReadyText();
			}
		}

		if(!buttonPressed[4])
		{
			if(Input.GetButton("p5action"))
			{
				controllerNumber.Add(5);
				buttonPressed[4] = true;
				ReadyText();
			}
		}

		if(!buttonPressed[5])
		{
			if(Input.GetButton("p6action"))
			{
				controllerNumber.Add(6);
				buttonPressed[5] = true;
				ReadyText();
			}
		}

		if(!buttonPressed[6])
		{
			if(Input.GetButton("p7action"))
			{
				controllerNumber.Add(7);
				buttonPressed[6] = true;
				ReadyText();
			}
		}

		if(!buttonPressed[7])
		{
			if(Input.GetButton("p8action"))
			{
				controllerNumber.Add(8);
				buttonPressed[7] = true;
				ReadyText();
			}
		}
	}

	void OnGUI()
	{
		GUI.color = new Vector4(1, 1, 1, guiAlpha);

		if(startMenuAudio.musicPlayed)
		{
			GUIFade(0, 1);
		}

		if(GUI.Button (startButton, "Start Game"))
		{
			Application.LoadLevel(1);
		}

		if(GUI.Button (exitButton, "Exit Game"))
		{
			Application.Quit();
		}
	}

	void GUIFade(float start, float end)
	{
		for (float i = 0; i < 1.0f; i += 0.1f*Time.deltaTime)
		{
			guiAlpha = Mathf.Lerp(start, end, i);
		}

		guiAlpha = end;
	}

	void ReadyText()
	{
		for(int i = 1; i < controllerNumber.Count + 1; i++)
		{
			//FOR GUI TEXT POSITION - COORDS ARE IN VIEWPORT SPACE - (0,0) IS BOTTOM LEFT, (1,1) IS TOP RIGHT
			playerReady[i] = Instantiate(playerName, new Vector3(0, 1.0f - i*1.5f/100.0f, 0), Quaternion.identity) as GUIText;
			playerReady[i].text = "Player " + i + " Ready";
		}
	}
}