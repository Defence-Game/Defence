using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_HPBar : UI_Base
{
    enum GameObjects
    {
        HPBar
    }

    public override void Init()
    {
        Bind<GameObject>(typeof(GameObjects));
    }

    private void Update()
    {
        Transform parent = transform.parent;
        transform.position = parent.position + new Vector3(0,1.1f,0)* (parent.GetComponent<Collider2D>().bounds.size.y);
        transform.rotation = Camera.main.transform.rotation;
        CreatureController cc = gameObject.GetComponentInParent<CreatureController>();
        float ratio = cc._hp / (float)cc._maxHp;
        SetHpRatio(ratio);
    }

    public void SetHpRatio(float ratio)
    {
        GetObject((int)GameObjects.HPBar).GetComponent<Slider>().value= ratio;
    }
}
