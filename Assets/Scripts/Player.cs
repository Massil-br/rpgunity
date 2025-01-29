using System.Xml.XPath;
using UnityEngine;

public class Player : MonoBehaviour
{   
    public bool IsAdmin;
    public int MaxHealthPoint;
    public int CurrentHealthPoint;
    public bool IsAlive;
    public int Level;
    public int Xp;
    public int NextLevelXp;
    public float NextLevelRatio;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {   
        IsAlive = true;
        Level = 1;
    }

    // Update is called once per frame
    void Update()
    {
        CheckHealth();
        CheckLevelUp();
        AdminActions();

        
    }



    private void CheckHealth(){
        if (CurrentHealthPoint <= 0){
            CurrentHealthPoint = 0;
            IsAlive = false;
        }

        if (CurrentHealthPoint >= MaxHealthPoint){
            CurrentHealthPoint = MaxHealthPoint;
        }
    }

    private void CheckLevelUp(){
        if (Xp >= NextLevelXp){
            Xp -=NextLevelXp;
            Level++;
            NextLevelXp = (int)(NextLevelXp * NextLevelRatio);
        }
    }


    // Input.GetKeyDown(KeyCode.)
    //Input.GetKey(KeyCode.LeftControl)
    private void AdminActions(){
        if (!IsAdmin){
            return;
        }

        if (Input.GetKey(KeyCode.LeftControl)&& Input.GetKeyDown(KeyCode.Keypad1) ){
            RemoveHealth();
        }
        if(Input.GetKey(KeyCode.LeftControl)&& Input.GetKeyDown(KeyCode.Keypad2)){
            GainHealth();
        }
        if(Input.GetKey(KeyCode.LeftControl)&& Input.GetKeyDown(KeyCode.Keypad3)){
            GainXp();
        }
        
        if (Input.GetKey(KeyCode.LeftControl)&& Input.GetKeyDown(KeyCode.Keypad0)){
            Revive();
        }


    }


    private void RemoveHealth(){
        CurrentHealthPoint--;
    }

    private void GainHealth(){
        CurrentHealthPoint++;
    }

    private void Revive(){
        CurrentHealthPoint++;
        IsAlive =true;
    }

    private void GainXp(){
        Xp++;
    }





}
