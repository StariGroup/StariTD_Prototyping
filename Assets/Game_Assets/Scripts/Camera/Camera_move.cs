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
	private float mouseBoundary = 10f; //Jak daleko od granic ekranu myszka musi być żeby zacząć przesuwać kamerę

	private float mouseX;//zmienna do rotacji
	private float impactOffset = -0.000001f;

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
		//HandleMouseRotation ();//Funkcja do rotacji kamery

		if(CheckIfUserCameraInput()) //Czy jest myszka na granicy ekranu albo wciśnięty przycisk ruszający kamerę
		{
			Vector3 cameraDesireMove = GetDesiredTranslation(); //Vector kamery z pozycją na którą przeskoczy w następnym framie
			//Stara wersja
			/*if(!IsDesiredPositionOverBoundaries(cameraDesireMove)) // Czy ta pozycja jest dopuszczalna
			{
				this.transform.Translate(cameraDesireMove, Space.Self); //Przesuń
			}else{
				this.transform.Translate(-40 * cameraDesireMove, Space.Self);
			}*/

			//Nowa wersja
			//Sprawdza czy każdy warunek przesuwania jest spełniony
			if(CanIMoveDown(cameraDesireMove) && CanIMoveUp(cameraDesireMove) && CanIMoveRight(cameraDesireMove) && CanIMoveLeft(cameraDesireMove) )
			{
				this.transform.Translate(cameraDesireMove, Space.Self); //Przesuń
			}else if(CanIMoveRight(cameraDesireMove) == false && CanIMoveUp(cameraDesireMove) == true && CanIMoveDown(cameraDesireMove) == true)
			{
				this.transform.Translate(0,0,cameraDesireMove.z, Space.Self);
			}else if(CanIMoveLeft(cameraDesireMove) == false && CanIMoveUp(cameraDesireMove) == true && CanIMoveDown(cameraDesireMove) == true)
			{
				this.transform.Translate(0,0,cameraDesireMove.z, Space.Self);
			}else if(CanIMoveUp(cameraDesireMove) == false && CanIMoveLeft(cameraDesireMove) == true && CanIMoveRight(cameraDesireMove) == true)
			{
				this.transform.Translate(cameraDesireMove.x,0,0, Space.Self);
			}else if(CanIMoveDown(cameraDesireMove) == false && CanIMoveLeft(cameraDesireMove) == true && CanIMoveRight(cameraDesireMove) == true)
			{
				this.transform.Translate(cameraDesireMove.x,0,0, Space.Self);
			}else{ //Jeżeli jesteś w rogu
				this.transform.Translate(impactOffset * cameraDesireMove, Space.Self);
			}
		}

		mouseX = Input.mousePosition.x;
		if(Input.GetMouseButtonUp(2))
		{
			this.transform.Rotate(0, 0, 0);
		}
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

	#region HelperFunctions

	public bool IsDesiredPositionOverBoundaries(Vector3 desiredPosition)
	{
		return  (this.transform.position.x + desiredPosition.x) < cameraLimits.LeftLimit ||
			(this.transform.position.x + desiredPosition.x) > cameraLimits.RightLimit ||
			(this.transform.position.z + desiredPosition.z) > cameraLimits.TopLimit ||
			(this.transform.position.z + desiredPosition.z) < cameraLimits.BottomLimit;
	}

	public bool CanIMoveLeft(Vector3 desiredPosition)
	{
		if ((this.transform.position.x + desiredPosition.x) > cameraLimits.LeftLimit) 
		{
			return true;
		}else{
			return false;
		}
	}

	public bool CanIMoveRight(Vector3 desiredPosition)
	{
		if ((this.transform.position.x + desiredPosition.x) < cameraLimits.RightLimit) 
		{
			return true;
		}else{
			return false;
		}
	}

	public bool CanIMoveUp(Vector3 desiredPosition)
	{
		if ((this.transform.position.z + desiredPosition.z) < cameraLimits.TopLimit) 
		{
			return true;
		}else{
			return false;
		}
	}

	public bool CanIMoveDown(Vector3 desiredPosition)
	{
		if ((this.transform.position.z + desiredPosition.z) > cameraLimits.BottomLimit) 
		{
			return true;
		}else{
			return false;
		}
	}

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
	#endregion
}
