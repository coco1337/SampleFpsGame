using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// This script contains player context and database
public class PlayerCharacterController : MonoBehaviour
{ 
    [SerializeField] Character PossessedCharacter;
    [SerializeField] PlayerCamera playerCamera;

    GameObject CharacterPrefab;

    [SerializeField] float rotateSensivity = 5.0f;

    // Start is called before the first frame update
    void Start()
    {
        if (playerCamera == null) 
        {
            playerCamera = FindObjectOfType<PlayerCamera>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (PossessedCharacter != null)
        {
            // 캐릭터의 움직임은 rotate -> move 순서로 진행

            // character rotate
            Vector2 mouseDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
            Vector3 characterRotateVector = new Vector3(0, mouseDelta.x, 0);
            Vector3 cameraRotateVector = new Vector3(- mouseDelta.y, 0, 0);

            if (!(Mathf.Approximately(characterRotateVector.magnitude, 0.0f)))
            {
                PossessedCharacter.Rotate(characterRotateVector * Time.deltaTime * rotateSensivity);
            }

            // camera rotate
            if (!(Mathf.Approximately(cameraRotateVector.magnitude, 0.0f)))
            {
                playerCamera.Rotate(cameraRotateVector * Time.deltaTime * rotateSensivity);
            }

            // move
            Vector3 inputVector = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            if (!(Mathf.Approximately(inputVector.magnitude, 0.0f)))
            {
                PossessedCharacter.Move(inputVector);
            }

            // jump
            if (Input.GetKeyDown(KeyCode.Space)) 
            {
                PossessedCharacter.Jump();
            }
        }
        else 
        {
            D.Log("No Character to Controll!");
        }

    }

    public void PossessBy(Character ByPossessCharacter) 
    {
        PossessedCharacter = ByPossessCharacter;
        PossessedCharacter.CharacterController = GetComponent<BaseCharacterController>();
    }

    public void Release() 
    {
        PossessedCharacter = null;
    }
}
