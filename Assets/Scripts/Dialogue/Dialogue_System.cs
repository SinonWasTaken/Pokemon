using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Dialogue_System : MonoBehaviour
{
    public static Dialogue_System Instance;

    private Dialogue_Lines line;
    
    [SerializeField] private string currentLine;
    
    [SerializeField] private char[] letters;

    [SerializeField] private string outputLine;

    [SerializeField] private float current_Speed;

    [SerializeField] private float fast_speed;
    [SerializeField] private float normal_speed;
    [SerializeField] private float slow_speed;

    [SerializeField] private bool dialogue_active;

    [SerializeField] private int index = 0;

    [SerializeField] private Text text;

    private Player_Controls _controls;
    
    private void Awake()
    {
        _controls = new Player_Controls();
        _controls.Controls.Interact.performed += InteractOnPerformed;
        
        if (Instance != null && Instance != this)
        {
            Destroy(Instance.gameObject);
        }

        Instance = this;

        current_Speed = slow_speed;

        get_next_line();
    }

    private void OnEnable()
    {
        _controls.Enable();
    }

    private void OnDisable()
    {
        _controls.Disable();
    }

    private void InteractOnPerformed(InputAction.CallbackContext obj)
    {
        Update_Dialogue();
    }

    public void Start_Dialogue(Dialogue_Lines line)
    {
        line.StartDialogue();
        this.line = line;
        dialogue_active = true;   
        gameObject.SetActive(true);
        currentLine = line.getLine();

        if (currentLine.Length > 0)
        {
            index = -1;
            get_next_line();
        }
    }
    
    private void Update_Dialogue()
    {
        if (outputLine.ToCharArray().Length == letters.Length)
        {
            get_next_line();
        }
    }

    private IEnumerator do_lines()
    {
        for (int i = 0; i < letters.Length; i++)
        {
            outputLine += letters[i];

            text.text = outputLine;
            
            yield return new WaitForSeconds(current_Speed);
        }
    }

    private void get_next_line()
    {
        index++;
        
        if (index < currentLine.Length)
        {
            text.text = "";
            currentLine = line.getLine();
            letters = currentLine.ToCharArray();

            outputLine = "";
            
            StartCoroutine(do_lines());
        }
        else
        {
            dialogue_active = false;
            gameObject.SetActive(false);
        }
    }
    
    public bool DialogueActive => dialogue_active;
}
