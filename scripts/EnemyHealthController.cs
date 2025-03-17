using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using UnityEngine;

public class EnemyHealthController : MonoBehaviour
{
    public static EnemyHealthController Instance { get; set; }
    [SerializeField] private int maxHealth = 2;
    public int CurrentHealth { get; set; }
    [SerializeField] private int sfxToPlay = 4;
    //[SerializeField] private GameObject OnesToDie;
    // Start is called before the first frame update
    void Start()
    {
        CurrentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void UpdateHealth(int update)
    {
        CurrentHealth += update;
        if(CurrentHealth<=0)
        {
            CurrentHealth = 0;
            GetComponent<EnemyController>().myAnimator.SetTrigger("Dead");
            EnemyController.CurrentAIState = EnemyController.AIState.IS_DEAD;
            AudioController.Instance.PlaySFX(4);
            //OnesToDie.SetActive(false);
        }
    }
}
