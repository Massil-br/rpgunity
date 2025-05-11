using System.Collections.Generic;
using UnityEngine;

public class Cheat : MonoBehaviour
{
    public bool konami = false;

    private readonly List<KeyCode> konamiCode = new List<KeyCode> {
        KeyCode.UpArrow, KeyCode.UpArrow,
        KeyCode.DownArrow, KeyCode.DownArrow,
        KeyCode.LeftArrow, KeyCode.RightArrow,
        KeyCode.LeftArrow, KeyCode.RightArrow,
        KeyCode.B, KeyCode.A
    };

    private List<KeyCode> inputBuffer = new List<KeyCode>();

    void Update()
    {
        // Vérifie si une touche a été pressée
        if (Input.anyKeyDown && konami == false && GetComponent<Hell>().hell == false && GetComponent<Player>().Level == 1)
        {
            // Parcourt toutes les touches du code Konami
            foreach (KeyCode key in System.Enum.GetValues(typeof(KeyCode)))
            {
                if (Input.GetKeyDown(key))
                {
                    inputBuffer.Add(key);

                    // Garde la taille du buffer égale à celle du code
                    if (inputBuffer.Count > konamiCode.Count)
                        inputBuffer.RemoveAt(0);

                    // Vérifie si le buffer correspond au code
                    if (IsKonamiCodeEntered())
                    {
                        Debug.Log("Konami Mode Activé !");
                        TriggerKonamiAction();
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

    private void TriggerKonamiAction()
    {
        GetComponent<Attack>().CloseAttackCoolDown = 0.1f;
        GetComponent<Attack>().LongDistAttackCoolDown = 0.1f;
        GetComponent<Player>().AttackDamage = 99;
        GetComponent<Player>().MaxHealthPoint = 999;
        GetComponent<Player>().CurrentHealthPoint = 999;
        konami = true;
    }
}