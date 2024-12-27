using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameField : MonoBehaviour
{
    System.Random rd = new System.Random();

    [SerializeField] Canvas canvas;

    Cell[,] cells;
    [SerializeField] GameObject cell_prefab;
    [SerializeField] int n = 4; // –азмер пол€

    void Awake()
    {
        // ”станавливаем Time.fixedDeltaTime на 1/60, чтобы физика обновл€лась 60 раз в секунду
        Time.fixedDeltaTime = 1f / 60f;
    }

    void Start()
    {
        cells = new Cell[n, n]; // ѕоле размера n x n, пустые клетки - null
        CreateCell();
    }

    public (int, int) GetEmptyPosition()
    {
        List<(int, int)> empty_pos = new List<(int, int)> (n);
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n; j++)
            {
                if (cells[i, j] == null)
                {
                    empty_pos.Add((j, i));
                }
            }
        }

        (int x, int y) empt_cords = empty_pos[rd.Next(empty_pos.Count)];

        return empt_cords;
    }

    public void CreateCell()
    {
        (int x, int y) coords = GetEmptyPosition(); //  —лучайна€ свободна€ клетка
        int rand_val = 2;
        if (rd.Next(0, 10) == 0) rand_val = 4;
        cells[coords.y, coords.x] = new Cell(coords, rand_val);

        GameObject cell = Instantiate(cell_prefab, canvas.transform);
        CellView cell_view = cell.GetComponent<CellView>();
        cell_view.Init(cells[coords.y, coords.x], cell);

        return;
    }

    public void Left()
    {
        for (int i = 0; i < n; i++)
        {
            int k = 0; // сколько надо прокатитьс€ клетке до столкновени€ с другой
            if (cells[i, 0] == null) k++;

            for (int j = 1; j < n; j++)
            {
                if (cells[i, j] == null)
                {
                    k++;
                    continue;
                }
                else
                {
                    // ѕодвинули на максимум влево
                    cells[i, j].Move(j - k, i);
                    cells[i, j - k] = cells[i, j];
                    if (k != 0) cells[i, j] = null;
                    if (j - k == 0) continue;
                    // —ревниваем с клеткой слева
                    if (cells[i, j - k].value == cells[i, j - k - 1].value)
                    {
                        cells[i, j - k - 1].Double();
                        cells[i, j - k].Delete();
                        cells[i, j - k] = null;
                        k++; // ќсвободилось место
                    }
                }
            }
        }
        CreateCell();
        return;
    }

    public void Right()
    {
        for (int i = 0; i < n; i++)
        {
            int k = 0; // сколько надо прокатитьс€ клетке до столкновени€ с другой
            if (cells[i, n - 1] == null) k++;

            for (int j = n - 2; j >= 0; j--)
            {
                if (cells[i, j] == null)
                {
                    k++;
                    continue;
                }
                else
                {
                    // ѕодвинули на максимум вправо
                    cells[i, j].Move(j + k, i);
                    cells[i, j + k] = cells[i, j];
                    if (k != 0) cells[i, j] = null;
                    if (j + k == n - 1) continue;
                    if (cells[i, j + k].value == cells[i, j + k + 1].value)
                    {
                        cells[i, j + k + 1].Double();
                        cells[i, j + k].Delete();
                        cells[i, j + k] = null;
                        k++; // ќсвободилось место
                    }
                }
            }
        }
        CreateCell();
        return;
    }

    public void Up()
    {
        for (int j = 0; j < n; j++)
        {
            int k = 0; // сколько надо прокатитьс€ клетке до столкновени€ с другой
            if (cells[0, j] == null) k++;

            for (int i = 1; i < n; i++)
            {
                if (cells[i, j] == null)
                {
                    k++;
                    continue;
                }
                else
                {
                    // ѕодвинули на максимум влево
                    cells[i, j].Move(j, i - k);
                    cells[i - k, j] = cells[i, j];
                    if (k != 0) cells[i, j] = null;
                    if (i - k == 0) continue;
                    // —ревниваем с клеткой слева
                    if (cells[i - k, j].value == cells[i - k - 1, j].value)
                    {
                        cells[i - k - 1, j].Double();
                        cells[i - k, j].Delete();
                        cells[i - k, j] = null;
                        k++; // ќсвободилось место
                    }
                }
            }
        }
        CreateCell();
        return;
    }

    public void Down()
    {
        for (int j = 0; j < n; j++)
        {
            int k = 0; // сколько надо прокатитьс€ клетке до столкновени€ с другой
            if (cells[n - 1, j] == null) k++;

            for (int i = n - 2; i >= 0; i--)
            {
                if (cells[i, j] == null)
                {
                    k++;
                    continue;
                }
                else
                {
                    // ѕодвинули на максимум вправо
                    cells[i, j].Move(j, i + k);
                    cells[i + k, j] = cells[i, j];
                    if (k != 0) cells[i, j] = null;
                    if (i + k == n - 1) continue;
                    if (cells[i + k, j].value == cells[i + k + 1, j].value)
                    {
                        cells[i + k + 1, j].Double();
                        cells[i + k, j].Delete();
                        cells[i + k, j] = null;
                        k++; // ќсвободилось место
                    }
                }
            }
        }
        CreateCell();
        return;
    }

}
