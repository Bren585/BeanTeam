using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Image heart_prefab;
    [SerializeField] private float margin;
    
    private Player player;
    private int hp;
    bool hide;

    Image[] hearts;
    int heart_count;
    
    void Start()
    {
        hide = false;
        player = FindFirstObjectByType<Player>();
        hp = 0;
        if (player != null) Init();
    }
    void Init()
    {
        heart_count = player.GetMaxHp();
        hearts = new Image[heart_count];
        RectTransform parent = GetComponent<RectTransform>();
        Vector2 pos = new Vector2(0, -40);
        for (int i = 0; i < heart_count; i++)
        {
            pos.x = heart_prefab.rectTransform.rect.width / 2 + margin + (margin + heart_prefab.rectTransform.rect.width) * i;
            hearts[i] = Instantiate(heart_prefab, parent);
            hearts[i].GetComponent<RectTransform>().anchoredPosition = pos;

        }
    }
    void Update()
    {
        if (hide) return;
        if (player == null)
        {
            player = FindFirstObjectByType<Player>();
            if (player != null) Init();
            else return;
        }

        if (player.GetHp() != hp)
        {
            hp = player.GetHp();
            for (int i = 0; i < heart_count; i++)
            {
                Color c = hearts[i].color; 
                c.a = (i + 1 <= hp) ? 1 : 0;
                hearts[i].color = c;
            }
        }
    }

    public void Hide() 
    { 
        hide = true; 
        foreach (Image heart in hearts) { Color c = heart.color; c.a = 0; heart.color = c; }
    }

    public void Show()
    {
        hide = false;
        foreach (Image heart in hearts) { Color c = heart.color; c.a = 1; heart.color = c; }
    }
}
