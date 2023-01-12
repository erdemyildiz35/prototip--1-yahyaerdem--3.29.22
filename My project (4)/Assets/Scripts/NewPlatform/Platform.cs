using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    // Oyuncu taraf?ndan de?i?tirilebilir bir h?z de?i?keni
    public float speed = 1;

    // Platformun hareket yönünü tutan de?i?ken
    private Vector2 direction = Vector2.down;

    void Update()
    {
        // Platformu belirlenen h?zda hareket ettirin
        transform.Translate(direction * speed * Time.deltaTime);

        // Oyuncunun platformun üzerinde olup olmad???n? kontrol edin
        if (IsPlayerOnTop())
        {
            // E?er oyuncu platformun üzerindeyse, platformun engel olmas?n? sa?lay?n
            gameObject.layer = LayerMask.NameToLayer("Platform");
        }
        else
        {
            // E?er oyuncu platformun alt?nda ya da yan?nda ise, platformun engel olmamas?n? sa?lay?n
            gameObject.layer = LayerMask.NameToLayer("Default");
        }
    }

    // Oyuncunun platformun üzerinde olup olmad???n? kontrol eden fonksiyon
    bool IsPlayerOnTop()
    {
        // Oyuncuyu temsil eden "Player" isimli GameObject'i bulun
        GameObject player = GameObject.Find("Player");

        // Oyuncunun platformun üzerinde olup olmad???n? kontrol edin
        return player.transform.position.y > transform.position.y;
    }
}