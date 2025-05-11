using System.Collections.Generic;
using UnityEngine;

public class Cheat : MonoBehaviour
{
    public bool konami = false;
    public bool hell = false;

    // Konami Mode
    private readonly List<KeyCode> konamiCode = new List<KeyCode> {
        KeyCode.UpArrow, KeyCode.UpArrow,
        KeyCode.DownArrow, KeyCode.DownArrow,
        KeyCode.LeftArrow, KeyCode.RightArrow,
        KeyCode.LeftArrow, KeyCode.RightArrow,
        KeyCode.B, KeyCode.A
    };

    // Hell Mode
    private readonly List<KeyCode> hellModeCode = new List<KeyCode> {
        KeyCode.Alpha6, KeyCode.Alpha6, KeyCode.Alpha6
    };

    private List<KeyCode> inputBuffer = new List<KeyCode>();

    void Update()
    {
        // Vérifie si une touche a été pressée
        if (Input.anyKeyDown && konami == false && hell == false && GetComponent<Player>().Level == 1)
        {
            // Parcourt toutes les touches du code Konami et du code Hell Mode
            foreach (KeyCode key in System.Enum.GetValues(typeof(KeyCode)))
            {
                if (Input.GetKeyDown(key))
                {
                    inputBuffer.Add(key);
                    Debug.Log("Touche : " + key);

                    // Garde la taille du buffer égale à la taille du code
                    if (inputBuffer.Count > Mathf.Max(konamiCode.Count, hellModeCode.Count))
                        inputBuffer.RemoveAt(0);

                    // Vérifie si le buffer correspond au Konami Code ou au Hell Mode Code
                    if (IsKonamiCodeEntered())
                    {
                        Debug.Log("Konami Mode Activé !");
                        TriggerKonamiAction();
                        inputBuffer.Clear(); // Reset
                    }
                    else if (IsHellModeCodeEntered())
                    {
                        Debug.Log("Hell Mode Activé !");
                        TriggerHellMode();
                        inputBuffer.Clear(); // Reset
                    }

                    break;
                }
            }
        }
    }

    private bool IsKonamiCodeEntered()
    {
        if (inputBuffer.Count != konamiCode.Count) return false;

        for (int i = 0; i < konamiCode.Count; i++)
        {
            if (inputBuffer[i] != konamiCode[i])
                return false;
        }
        return true;
    }

    private bool IsHellModeCodeEntered()
    {
      Debug.Log(inputBuffer);
        if (inputBuffer.Count != hellModeCode.Count) return false;

        for (int i = 0; i < hellModeCode.Count; i++)
        {
            if (inputBuffer[i] != hellModeCode[i])
                return false;
        }
        return true;
    }

    private void TriggerKonamiAction()
    {
        GetComponent<Attack>().CloseAttackCoolDown = 0.05f;
        GetComponent<Attack>().LongDistAttackCoolDown = 0.1f;
        GetComponent<Player>().AttackDamage = 99;
        GetComponent<Player>().MaxHealthPoint = 999;
        GetComponent<Player>().CurrentHealthPoint = 999;
        konami = true;
    }

    private void TriggerHellMode()
    {
        GetComponent<Player>().MaxHealthPointCap = 1;
        GetComponent<Player>().MaxHealthPoint = 1;
        GetComponent<Player>().CurrentHealthPoint = 1;
        hell = true;
    }
}
