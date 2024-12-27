using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CellView : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI value_text;
    [SerializeField] RectTransform cell_pos;
    [SerializeField] Image cell_img;
    Cell cell;
    GameObject cell_gameObject;

    // (0,0) смещение дл€ отображени€ на сцене
    int x_zero_shift = -100;
    int y_zero_shift = 75;

    public void Init(Cell cell_, GameObject gameObject)
    {
        cell = cell_;
        cell_gameObject = gameObject;
        value_text.text = cell.value.ToString();
        cell.OnValueChanged += UpdateValue;
        cell.OnPositionChanged += UpdatePosition;
        cell.OnDelete += Delete;

        cell_pos.anchoredPosition = new Vector2(x_zero_shift + cell.x * 50, y_zero_shift - cell.y * 50);

        return;
    }

    private void UpdateValue()
    {
        if (value_text)
        {
            value_text.text = cell.value.ToString();
            // Ѕольше значение - более класна€ клетка
            float coeff = Mathf.Log(cell.value, 2) / 11;
            cell_img.color = new Color(1, 1f - coeff, 1f - coeff);
        }
        return;
    }

    private void UpdatePosition()
    {
        cell_pos.anchoredPosition = new Vector2(x_zero_shift + cell.x * 50, y_zero_shift - cell.y * 50);

        return;
    }

    private void Delete()
    {
        Destroy(cell_gameObject);
        return;
    }
}
