using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// This script contains player context and database
public class PlayerCharacterController : MonoBehaviour
{ 
    [SerializeField] Character PossessedCharacter;
    [SerializeField] PlayerCamera playerCamera;
    [SerializeField] BaseMovement baseMovement;

    GameObject CharacterPrefab;

    InputManager inputManager;
    // Start is called before the first frame update
    void Start()
    {
        baseMovement = PossessedCharacter.GetComponent<BaseMovement>();
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

            // rotate
            Vector2 mouseDelta = inputManager.GetMouseDelta();
            Vector3 characterRotateVector = new Vector3(mouseDelta.x, 0, 0);
            Vector3 cameraRotateVector = new Vector3(0, mouseDelta.y, 0);

            if (!(Mathf.Approximately(characterRotateVector.magnitude, 0.0f)))
            {
                PossessedCharacter.RotateBy(characterRotateVector * Time.deltaTime);
            }

            // camera rotate
            if (!(Mathf.Approximately(cameraRotateVector.magnitude, 0.0f)))
            {
                // cameraViewPoint.RotateBy(cameraRotateVector * Time.deltaTime);
            }

            // move
            Vector3 moveVector = new Vector3(Input.GetAxis("Horizontal"), 0 ,Input.GetAxis("Vertical"));
            if (!(Mathf.Approximately(moveVector.magnitude, 0.0f)))
            {
                PossessedCharacter.MoveBy(moveVector * Time.deltaTime);
            }
        }
        else 
        {
            Debug.Log("No Character to Controll!");
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
