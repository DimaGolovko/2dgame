using Collectibles.Common.Classes;
using UnityEngine;

public class ItemDestroyer : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Item")) Destroy(other.GetComponent<Item>().gameObject);
    }
}
