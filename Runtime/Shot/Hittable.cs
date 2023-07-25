using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Sirenix.OdinInspector;

public class Hittable : MonoBehaviour
{
    [SerializeField] Vector2Int HpRange = new Vector2Int(3,4);
    [SerializeField][ReadOnly] int CurrentHp;
    [SerializeField][ReadOnly] bool IsDie;

    public GameObject DieRoot;


    public UnityEvent OnDieEvent;

    public UnityEvent OnHitEvent;

  
    private void OnEnable()
    {
        CurrentHp = UnityEngine.Random.Range(HpRange.x, HpRange.y);
    }

    [Button("Harm")]
    public void Harm()
    {
        if (!IsDie)
        {
            Debug.Log("虫子受伤");
            CurrentHp -= 1;
            if (CurrentHp > 0 )
            {
                OnHitEvent?.Invoke();
            }

            if (CurrentHp <= 0)
            {
                Die();
            }
        }
    }

    [Button("Die")]
    public void Die()
    {

        if (DieRoot != null)
        {
            transform.SetParent(DieRoot.transform);
        }

        IsDie = true;
    }
}
