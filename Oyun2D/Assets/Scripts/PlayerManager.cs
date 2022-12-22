using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
    public float health, bulletSpeed;

    bool dead = false;

    Transform muzzle;

    public Transform bullet, floatingText, bloodParticle;

    public Slider slider;

    bool mouseIsNotOverUI;

    // Start is called before the first frame update
    void Start()
    {
        //
        muzzle = transform.GetChild(1);
        slider.maxValue = health;
        slider.value = health;

    }

    // Update is called once per frame
    void Update()
    {
        mouseIsNotOverUI = EventSystem.current.currentSelectedGameObject == null;

        //mouse un sol tusuna basiliyorsa ve ui elemanina tiklanmiyorsa ates ac
        if (Input.GetMouseButtonDown(0) && mouseIsNotOverUI)
        {
            ShootBullet();
        }

    }

    //Hasar alma metodu
    public void GetDamage(float damage)
    {
        //hasar aldiginkadi 10 sayisi gosterir
        Instantiate(floatingText, transform.position, Quaternion.identity).GetComponent<TextMesh>().text = damage.ToString();

        if ((health - damage) >=0)
        {
            health -= damage;
        }
        else
        {
            health = 0;
        }
        slider.value = health;
        AmIDead();
    }

    //Olup olmedigini kotrol eden metot
    void AmIDead()
    {
        if (health <=0)
        {
            //cani bittiyse kan sicrama ve kaybolma 
            Destroy(Instantiate(bloodParticle, transform.position, Quaternion.identity), 3f);
            DataManager.Instance.LoseProcess();
            dead = true;
            Destroy(gameObject);
        }
    }

    //Kursunu firlatan metot
    void ShootBullet()
    {
        Transform tempBullet;
        tempBullet = Instantiate(bullet, muzzle.position, Quaternion.identity);
        tempBullet.GetComponent<Rigidbody2D>().AddForce(muzzle.forward * bulletSpeed);
        DataManager.Instance.ShotBullet++;
    }

    


}
