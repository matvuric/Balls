using TMPro;
using UnityEngine;

public class Brick : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Spawner spawner;
    private TextMeshPro textMP;
    private int _health = 0;
    private readonly Color ColorBeige = new Color(212 / 256f, 211 / 256f, 183 / 256f); // бежевый
    private readonly Color ColorPink = new Color(212 / 256f, 132 / 256f, 159 / 256f); // розовый
    private readonly Color ColorPurple = new Color(172 / 256f, 0 / 256f, 238 / 256f);   // фиолетовый
    private readonly Color ColorCyan = new Color(127 / 256f, 123 / 256f, 238 / 256f); // голубой

    private void Awake()
    {
        spawner = FindObjectOfType<Spawner>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        textMP = GetComponentInChildren<TextMeshPro>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        UpdateHealth(1);
    }

    internal void UpdateHealth(int damage)
    {
        _health -= damage;

        if (_health > 0)
        {
            UpdateColor();
        }
        else
        {
            spawner.ClearListOfBricks(this);
            Destroy(gameObject);
        }
    }

    internal void SetHealthValue(int hp)
    {
        _health = hp;
        UpdateColor();
    }

    private void UpdateColor()
    {
        textMP.SetText(_health.ToString());

        if (_health < 10)
        {
            spriteRenderer.color = Color.Lerp(ColorBeige, ColorPink, _health % 10 / 10f);
        }
        else if (_health < 20)
        {
            spriteRenderer.color = Color.Lerp(ColorPink, ColorPurple, _health % 10 / 10f);
        }
        else if (_health < 30)
        {
            spriteRenderer.color = Color.Lerp(ColorPurple, ColorCyan, _health % 10 / 10f);
        }
        else
        {
            spriteRenderer.color = ColorCyan;
        }
    }
}
