using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Attack : MonoBehaviour
{   

    private List<GameObject> _targetableMonsters = new List<GameObject>();

    Player player;



    void Start(){
        player = GetComponent<Player>();
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("Monster") && !_targetableMonsters.Contains(other.gameObject))
        {
            _targetableMonsters.Add(other.gameObject);
        }
    }


    private void OnTriggerExit2D(Collider2D other) {
        if (other.gameObject.CompareTag("Monster"))
        {
            _targetableMonsters.Remove(other.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0)){
            foreach (GameObject target in _targetableMonsters){
                MonsterStats stats = target.GetComponent<MonsterStats>();
                stats.CurrentHealth -= player.AttackDamage;
            }
        }
    }
}
