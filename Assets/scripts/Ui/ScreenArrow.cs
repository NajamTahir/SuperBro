using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenArrow : MonoBehaviour
{
    [SerializeField]private RectTransform[] options;
    [SerializeField]private AudioClip changeSounds;
    [SerializeField]private AudioClip interactSound;
    private RectTransform rect;
    private int currentPosition;

    private void Awake(){
        rect = GetComponent<RectTransform>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //change the position of the arrow
        if(Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)){
            ChangePosition(-1);
        }
        else if(Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)){
            ChangePosition(1);
        }
        if(Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.E)){
            Intract();
        }
    }
    private void ChangePosition(int _change){
        currentPosition += _change;
        if(_change != 0){
            SoundManager.instance.PlaySound(changeSounds);
        }

        if (currentPosition < 0){
            currentPosition = options.Length - 1;
        }
        else if (currentPosition >= options.Length){
            currentPosition = 0;
        }

        rect.position = new Vector3(rect.position.x, options[currentPosition].position.y, 0);
    }
    private void Intract(){
        SoundManager.instance.PlaySound(interactSound);

        //Access the button component and call the OnClick method
        options[currentPosition].GetComponent<Button>().onClick.Invoke();
    }
}
