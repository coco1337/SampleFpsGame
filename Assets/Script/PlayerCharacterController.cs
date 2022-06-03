using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

// This script contains player context and database
public class PlayerCharacterController : MonoBehaviour, InputSys.IFirstPersonPlayerActions
{
    [SerializeField]
    Character PossessedCharacter;

    [SerializeField]
    BaseMovement baseMovement;

    GameObject CharacterPrefab;

    InputSys inputSys;
    Vector3 InputMoveVector;
    // Start is called before the first frame update
    void Start()
    {
        baseMovement = PossessedCharacter.GetComponent<BaseMovement>();
    }

    void OnEnable() 
    {
        if (inputSys == null) 
        {
            inputSys = new InputSys();
            inputSys.FirstPersonPlayer.SetCallbacks(instance: this);
            inputSys.FirstPersonPlayer.Enable();
        }
    }

    void OnDisable() 
    {
        inputSys.FirstPersonPlayer.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        if (PossessedCharacter != null)
        {
            if (!(Mathf.Approximately(InputMoveVector.magnitude, 0.0f)))
            {
                Debug.Log($"{InputMoveVector}");
                PossessedCharacter.MoveBy(new Vector3(InputMoveVector.x, 0, InputMoveVector.y).normalized * Time.deltaTime);
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

    public void OnMove(InputAction.CallbackContext context)
    {
        InputMoveVector = context.ReadValue<Vector2>();
        Debug.Log("Input!");
    }

    public void OnNewaction(InputAction.CallbackContext context)
    {
        // some codes
    }
}
