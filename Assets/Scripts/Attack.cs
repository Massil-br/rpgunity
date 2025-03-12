using System.Collections.Generic;
using System.Linq;
using System.Threading;
using UnityEngine;

public class Attack : MonoBehaviour
{   

    public float AttackCoolDown = 3;

    private float _timer;

    private List<GameObject> _targetableMonsters = new List<GameObject>();

    private Player _player;



    void Start(){
        _player = GetComponent<Player>();
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
        if (_timer >= 0 ){
            _timer -= Time.deltaTime;
        }
        if (Input.GetKeyDown(KeyCode.Mouse0) &&  _timer <= 0){

            _player.GetComponent<PlayerAnimationHandler>().PlayFireAttackAnimation();
            _timer = 3;

            foreach (GameObject target in _targetableMonsters){
                
                MonsterStats stats = target.GetComponent<MonsterStats>();
                target.GetComponent<MonsterAnimationHandler>().PlayTakeDamageAnimation();
                stats.CurrentHealth -= _player.AttackDamage;
               
            }
        }
    }
}