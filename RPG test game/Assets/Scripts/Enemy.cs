using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour
{
    public event UnityAction OnTakingDamage;

    private float _maxHealth = 100f;
    public float CurrentHealth;
    private Animator _animator;

    public Vector3 StartPosition;

    private void Start()
    {
        CurrentHealth = _maxHealth;
        _animator = GetComponent<Animator>();
        StartPosition = transform.position;
        
    }

    public void TakeDamage(float damage)
    {
        CurrentHealth -= damage;
        OnTakingDamage?.Invoke();
        if (CurrentHealth <= 0)
            EnemyDie();
    }



    private void EnemyDie()
    {
        _animator.SetBool("isDead", true);
        gameObject.GetComponent<BoxCollider>().enabled = false;
        gameObject.GetComponent<EnemyAI>().enabled = false;
        this.enabled = false;
        StartCoroutine(DestroyEnemy());
    }

    private IEnumerator DestroyEnemy()
    {
        yield return new WaitForSeconds(3f);
        Destroy(gameObject);
        int random = Random.Range(0, 4);
        Instantiate(FoodManager.Instance.Food[random], transform.position, Quaternion.identity);
    }

   

    private void Update()
    {
        
    }
}
