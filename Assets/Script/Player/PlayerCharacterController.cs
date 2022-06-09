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

    float cameraMoveSensivity = 5.0f;

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
            Vector2 mouseDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
            Vector3 characterRotateVector = new Vector3(0, mouseDelta.x, 0);
            Vector3 cameraRotateVector = new Vector3(- mouseDelta.y, 0, 0);

            // Debug.Log($"mouseDelta : {mouseDelta}");
            if (!(Mathf.Approximately(characterRotateVector.magnitude, 0.0f)))
            {
                // Debug.Log($"Rotate! : {characterRotateVector}");
                PossessedCharacter.RotateBy(characterRotateVector * Time.deltaTime * cameraMoveSensivity);
                playerCamera.RotateBy(characterRotateVector * Time.deltaTime * cameraMoveSensivity);
            }

            // camera rotate
            if (!(Mathf.Approximately(cameraRotateVector.magnitude, 0.0f)))
            {
                playerCamera.RotateBy(cameraRotateVector * Time.deltaTime * cameraMoveSensivity);
            }

            // move
            Vector3 forwardVector = playerCamera.transform.forward.normalized;
            Vector3 rightVector = playerCamera.transform.right.normalized;
            Vector3 inputVector = new Vector3(Input.GetAxis("Horizontal"), 0 ,Input.GetAxis("Vertical"));
            if (!(Mathf.Approximately(inputVector.magnitude, 0.0f)))
            {
                Debug.Log($"Forward : {forwardVector} / Right : {rightVector}");
                Vector3 moveVector = (forwardVector * inputVector.z) + (rightVector * inputVector.x);
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
