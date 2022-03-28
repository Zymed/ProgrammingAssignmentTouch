using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.UI;

public class TestingInputSystem : MonoBehaviour
{

    //I have received help from my class mates: Daniel Lerche & Magnus Bergstedt

    [SerializeField]
    private Button button;
    private PlayerInputActions playerInputActions;
    private bool isMeasuring = false;
    private Save save;
    [SerializeField]
    public TextMeshProUGUI text;
    private void Awake()
    {
        save = new Save();

        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();
        playerInputActions.Player.Jump.performed += Press;
    }

    public void Press(InputAction.CallbackContext context)
    {
        Debug.Log(context);
        if (context.performed)
        {
            Debug.Log("Pressed! " + context.phase);
        }
        
    }

    private void Update()
    {
        text.text = "Acc " + Input.acceleration;

        if (isMeasuring)
        {
            save.data.Add(Input.acceleration);
        }
        if(save.data.Count > 0 && !isMeasuring)
        {
            save.WriteCSV();
            save.data.Clear();
        }

        //New input system would use the following code to read, but Unity cannot detect the phone.
        //Vector3 tispis = playerInputActions.Player.Accelerator.ReadValue<Vector3>();
        //Debug.Log(tispis);
    }

    private void Start()
    {
        button.onClick.AddListener(StartSaving);
    }

    private void StartSaving()
    {
        if(isMeasuring != true)
        {
            isMeasuring = true;
            Debug.Log("saving");
        }
        else
        {
            isMeasuring = false;
            Debug.Log("cancelling");
        }
    }
}
