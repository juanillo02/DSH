using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Prueba_Drag_Drop : MonoBehaviour
{   
    Vector3 screenSpace, offset, posInicial;
    SensoresT sensores;
    PuzzleT puzzle;
    bool moviendoLeft, moviendoRight, moviendoUp, moviendoDown;
    UI_T ui;

    void Awake()
    {
        sensores = GetComponentInChildren(typeof(SensoresT)) as SensoresT;
        puzzle = GameObject.Find("Scripts").GetComponent(typeof(PuzzleT)) as PuzzleT;
        ui = GameObject.Find("Scripts").GetComponent(typeof(UI_T)) as UI_T;
        Debug.Log("Awake");
    }

    void OnMouseDown()
    {
        screenSpace = Camera.main.WorldToScreenPoint(transform.position);
        offset = transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenSpace.z));
        posInicial = transform.position;
        Debug.Log("OnMouseDown");

        // isDragging = true;
    }

    void OnMouseDrag()
    {
        Vector3 posicion = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenSpace.z);
        Vector3 curScreenSpace = posicion;
        Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenSpace) + offset;
        Debug.Log("OnMouseDrag");

    
        if(!sensores.ocupadoLeft || !sensores.ocupadoRight)
        {
            curPosition = new Vector3 (curPosition.x, transform.position.y, 0);
            if(!sensores.ocupadoLeft && !moviendoLeft && !moviendoRight)
            {
                moviendoLeft = true;
                Debug.Log("moviendoLeft");
            }
            if(!sensores.ocupadoRight && !moviendoLeft && !moviendoRight)
            {
                moviendoRight = true;
                Debug.Log("moviendoRight");
            }
        }
        else if (!sensores.ocupadoUp || !sensores.ocupadoDown)
        {
            curPosition = new Vector3 (transform.position.x, curPosition.y, 0);
            if(!sensores.ocupadoUp && !moviendoUp && !moviendoDown)
            {
                moviendoUp = true;
                Debug.Log("moviendoUp");
            }
            if(!sensores.ocupadoDown && !moviendoUp && !moviendoDown)
            {
                moviendoDown = true;
                Debug.Log("moviendoDown");
            }
        }
        else
        {
            return;
        }

        if(moviendoLeft)
        {
            if(curPosition.x > posInicial.x)
            {return;}
        }
        if(moviendoRight)
        {
            if(curPosition.x < posInicial.x)
            {return;}
        }
        if(moviendoUp)
        {
            if(curPosition.y < posInicial.y)
            {return;}
        }
        if(moviendoDown)
        {
            if(curPosition.y > posInicial.y)
            {return;}
        }

        if(Vector3.Distance(curPosition, posInicial) > 1f)
        {return;}

        transform.position = curPosition;
    }

    void OnMouseUp()
{
    Debug.Log("OnMouseUp");
    transform.position = new Vector3(Mathf.Round(transform.position.x), Mathf.Round(transform.position.y), 0);
    moviendoLeft = false;
    moviendoRight = false;
    moviendoUp = false;
    moviendoDown = false;

    //  ui.SumarMovimiento();
    puzzle.ComprobarGanador();

}


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
}
