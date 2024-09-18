using UnityEngine;
using UnityEngine.Events;

public class DamageAdapter : MonoBehaviour, IDamagable
{
	public UnityEvent<int> OnDamaged;

	public void TakeDamage(int damage)
	{
		OnDamaged?.Invoke(damage);
	}
}
