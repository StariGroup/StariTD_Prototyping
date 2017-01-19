using UnityEngine;
using System.Collections;

public class Camera_move : MonoBehaviour {

	public GameObject Main_Camera;
	public struct BoxLimit //deklaracja struktury granic
	{
		public float LeftLimit;
		public float RightLimit;
		public float TopLimit;
		public float BottomLimit;
	}

	public static BoxLimit cameraLimits = new BoxLimit(); //stworzenie obiektu granic dla kamery
	public static BoxLimit mouseScrollLimits = new BoxLimit(); //stworzenie obiektu granic dla myszki
	public static Camera_move Instance; //stworzenie obiektu klasy danego pliku(który jest klasą)

	private float cameraMoveSpeed = 50f; //Jak szybko kamera się przesuwa
	private float mouseBoundary = 30f; //Jak daleko od granic ekranu myszka musi być żeby zacząć przesuwać kamerę

	private float mouseX;//zmienne do rotacji

	void Awake()
	{
		Instance = this;
	}

	void Start()
	{
		cameraLimits.LeftLimit = -100f; //Jak daleko kamera może wyjechać w lewo, prawo...
		cameraLimits.RightLimit = 340f;
		cameraLimits.TopLimit= 170f;
		cameraLimits.BottomLimit = -200f;

		mouseScrollLimits.LeftLimit = mouseBoundary;
		mouseScrollLimits.RightLimit = mouseBoundary;
		mouseScrollLimits.TopLimit = mouseBoundary;
		mouseScrollLimits.BottomLimit = mouseBoundary;
	}

	void LateUpdate()
	{
		HandleMouseRotation ();//Funkcja do rotacji kamery

		if(CheckIfUserCameraInput()) //Czy jest myszka na granicy ekranu albo wciśnięty przycisk ruszający kamerę
		{
			Vector3 cameraDesireMove = GetDesiredTranslation(); //Vector kamery z pozycją na którą przeskoczy w następnym framie
			if(!IsDesiredPositionOverBoundaries(cameraDesireMove)) // Czy ta pozycja jest dopuszczalna
			{
				//Vector3 clampedCameraDesireMove = new Vector3 (Mathf.Clamp(cameraDesireMove.x,cameraLimits.LeftLimit,cameraLimits.RightLimit),cameraDesireMove.y,Mathf.Clamp(cameraDesireMove.z,cameraLimits.BottomLimit,cameraLimits.TopLimit));
				this.transform.Translate(cameraDesireMove, Space.Self); //Przesuń
			}else{
				this.transform.Translate(-40 * cameraDesireMove, Space.Self);
			}
		}

		mouseX = Input.mousePosition.x;
	}

	//Obracanie kamerą
	public void HandleMouseRotation()
	{
		var easeFactor = 10f;
		if (Input.GetMouseButton(2)) 
		{
			//Rotacja horyzontalna
			if (Input.mousePosition.x != mouseX) 
			{
				var cameraRotationY = (Input.mousePosition.x - mouseX) * easeFactor * Time.deltaTime;
				this.transform.Rotate(0, cameraRotationY, 0);
			}
		}
	}

	public bool CheckIfUserCameraInput()
	{
		var keyboardMove = Camera_move.AreCameraKeyboardButtonsPressed();
		var mouseMove = IsMousePositionWithinBoundaries();
		return keyboardMove || mouseMove;

	}

	public Vector3 GetDesiredTranslation()
	{
		var moveSpeed =  cameraMoveSpeed * Time.deltaTime;
		var desiredZ = 0f;
		var desiredX = 0f;


		if(AreCameraKeyboardButtonsPressed())
		{
			if(Input.GetKey(KeyCode.UpArrow))
				desiredZ = moveSpeed;
			if(Input.GetKey(KeyCode.DownArrow))
				desiredZ = moveSpeed*-1;
			if(Input.GetKey(KeyCode.LeftArrow))
				desiredX = moveSpeed*-1;
			if(Input.GetKey(KeyCode.RightArrow))
				desiredX = moveSpeed;
		}
		else
		{
			if(Input.mousePosition.y > (Screen.height-mouseScrollLimits.TopLimit))
				desiredZ = moveSpeed;
			if(Input.mousePosition.y < mouseScrollLimits.BottomLimit)
				desiredZ = moveSpeed*-1;
			if(Input.mousePosition.x < mouseScrollLimits.LeftLimit)
				desiredX = moveSpeed*-1;
			if(Input.mousePosition.x > (Screen.width-mouseScrollLimits.RightLimit))
				desiredX = moveSpeed;
		}

		return new Vector3(desiredX, 0, desiredZ);

	}

	public bool IsDesiredPositionOverBoundaries(Vector3 desiredPosition)
	{
		return  (this.transform.position.x + desiredPosition.x) < cameraLimits.LeftLimit ||
			(this.transform.position.x + desiredPosition.x) > cameraLimits.RightLimit ||
			(this.transform.position.z + desiredPosition.z) > cameraLimits.TopLimit ||
			(this.transform.position.z + desiredPosition.z) < cameraLimits.BottomLimit;
	}

	/*public Vector3 HowMuchIsDesiredPositionOverBoundaries(Vector3 desiredPosition)
	{
		if ((this.transform.position.x + desiredPosition.x) < cameraLimits.LeftLimit) 
		{
			return new Vector3 ((cameraLimits.LeftLimit - this.transform.position.x + desiredPosition.x) * -1, 0, 0);
		} else if ((this.transform.position.x + desiredPosition.x) > cameraLimits.RightLimit) 
		{
			return new Vector3 ((cameraLimits.RightLimit - this.transform.position.x + desiredPosition.x) * -1, 0, 0);
		} else if ((this.transform.position.z + desiredPosition.z) > cameraLimits.TopLimit) 
		{
			return new Vector3 ((0 , 0, cameraLimits.TopLimit - this.transform.position.z + desiredPosition.z) * -1);
		}
		else if((this.transform.position.z + desiredPosition.z) < cameraLimits.BottomLimit)
		{
			return new Vector3 ((0 , 0, cameraLimits.BottomLimit - this.transform.position.z + desiredPosition.z) * -1);
		}
	}*/

	public static bool AreCameraKeyboardButtonsPressed() //kontrolki
	{
		return (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.RightArrow));	
	}

	public static bool IsMousePositionWithinBoundaries()
	{
		//Debug.Log (Input.mousePosition.x + "  " + Screen.width + "  " + mouseScrollLimits.RightLimit);
		//Jak daleko za obszar ekranu może wyjechać myszka żeby przesuwać kamerę
		return(Input.mousePosition.x < mouseScrollLimits.LeftLimit && Input.mousePosition.x > -25)||
			(Input.mousePosition.x > (Screen.width-mouseScrollLimits.RightLimit) && Input.mousePosition.x < (Screen.width + 25)) ||
			(Input.mousePosition.y < mouseScrollLimits.BottomLimit && Input.mousePosition.y > -25)||
			(Input.mousePosition.y > (Screen.height-mouseScrollLimits.TopLimit)  && Input.mousePosition.y < (Screen.height + 25) );
	}
}
