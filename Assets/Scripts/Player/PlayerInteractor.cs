using UnityEngine;

public class PlayerInteractor : MonoBehaviour
{
public enum Poweup
{
    Speed,
    Heal,
    Damage
}
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out IInteractable interacted))
        {
            interacted.Collect();
        }
        

        if (collision.TryGetComponent(out Poweup poweup))
        {
            if(poweup == Poweup.Speed)
            {
                //speed
            }
            else if(poweup == Poweup.Heal)
            {
                //Heal
            }
        }
        
        
        
    }
}
