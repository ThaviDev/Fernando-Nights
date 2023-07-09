using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecurityGuard : MonoBehaviour
{
    [Serializable]
    public class AIChoice
    {
        public string actionName;
        public int priority;
    }

    public List<AIChoice> choices = new List<AIChoice>();

    private int charactersOnLeft;
    private int charactersOnRight;
    private bool[] phantomIsInOffice = new bool[3];
    private float[] type2Bar = new float[2];

    private float stunTime;
    private float openDoorsTime;
    void Start()
    {
        
    }
    void Update()
    {
        
    }

    public void MakeDesicion()
    {
        choices.Sort((x, y) => y.priority.CompareTo(x.priority));

        if (choices.Count > 0)
        {
            AIChoice highestPriorityChoice = choices[0];
            ExecuteAction(highestPriorityChoice);
        }
    }

    private void ExecuteAction(AIChoice choice)
    {

    }

    public void DetectedSomething(int ID)
    {
        switch (ID)
        {
            case 1: print("Alguien esta a mi izquierda: Prioridad 5");
                break;
            case 2: print("Alguien esta a mi derecha: Prioridad 5");
                break;
            case 3: print("Barra Foxy esta 50% de gastado: Prioridad 2");
                break;
            case 4: print("Barra Puppet esta 50% de gastado: Prioridad 2");
                break;
            case 5: print("Barra Foxy esta 25% de gastado: Prioridad 4");
                break;
            case 6: print("Barra Puppet esta 25% de gastado Prioridad 4");
                break;
            case 7: print("Shadow Fredy Aparecio: Prioridad 1");
                break;
            case 8: print("Shadow Bonnie Aparecio: Prioridad 1");
                break;
            case 9: print("Golden Freddy Aparecio: Prioridad 1");
                break;
            case 10: print("No hay nadie a mi derecha, debo abrir la puerda para no gastar energia: Prioridad 3");
                break;
            case 11: print("No hay nadie a mi izquierda, debo abrir la puerda para no gastar energia: Prioridad 3");
                break;
        }
    }
}
