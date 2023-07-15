using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecurityGuard : MonoBehaviour
{
    [Serializable]
    public class AIChoice
    {
        public int ID;
        public int priority;
        public int executeTime;
    }

    public List<AIChoice> choices = new List<AIChoice>();

    private int charactersOnLeft;
    private int charactersOnRight;
    public bool[] phantomIsInOffice = new bool[3]; // Quiza podamos llamar al tipo 3 de animatronico una vez que llegue a su segunda posicion
    public Character[] phantomChar = new Character[3];
    [SerializeField] private FloatSCOB[] type2Bar = new FloatSCOB[2]; // (Probablemente seria mejor utilizar un SCOB aqui)
    public FloatSCOB batery;

    public float stunTime;
    public float openRightDoorTime;
    public float openLeftDoorTime;

    private bool isBussy;
    private float executeTimeOG;
    private float presentExecuteTime;

    AIChoice highestPriorityChoice;

    [SerializeField]
    OfficeDoor doorRight;
    [SerializeField]
    OfficeDoor doorLeft;
    [SerializeField]
    TriggerComp zoneRight;
    [SerializeField]
    TriggerComp zoneLeft;

    bool puppet25P;
    bool puppet50P;
    bool foxy25P;
    bool foxy50P;

    void Start()
    {
        zoneRight.charAmount.Value = 0;
        zoneLeft.charAmount.Value = 0;

        puppet25P = false;
        puppet50P = false;
        foxy25P = false;
        foxy50P = false;
    }
    void Update()
    {

        if (stunTime <= 0) {
            DetectionFunction();
        }
        workTimeDelay();
    }

    void DetectionFunction()
    {
        if (openRightDoorTime <= 0) {
            RightDoor();
        }
        if (openLeftDoorTime <= 0) {
            LeftDoor();
        }
        if (phantomIsInOffice[0] == true) {
            DetectedSomething(7); // Shadow Freddy Aparece
            phantomIsInOffice[0] = false;
        }
        if (phantomIsInOffice[1] == true)
        {
            DetectedSomething(8); // Shadow Bonnie Aparece
            phantomIsInOffice[1] = false;
        }
        if (phantomIsInOffice[2] == true)
        {
            DetectedSomething(9); // Golden Freddy Aparece
            phantomIsInOffice[2] = false;
        }
        if (!foxy25P && !foxy50P)
        {
            CheckFoxyBar();
        }
        if (!puppet25P && !puppet50P)
        {
            CheckPuppetBar();
        }
    }

    void RightDoor()
    {
        if (charactersOnRight != 0 && zoneRight.charAmount.Value == 0)
        {
            DetectedSomething(10); // Abrir la puerta derecha
        }
        else if (charactersOnRight > zoneRight.charAmount.Value || charactersOnRight < zoneRight.charAmount.Value)
        {
            DetectedSomething(1); // Cerrar la puerta derecha
        }
        // Igualar los valores al final
        charactersOnRight = zoneRight.charAmount.Value;
    }

    void LeftDoor()
    {
        if (charactersOnLeft != 0 && zoneLeft.charAmount.Value == 0)
        {
            DetectedSomething(11); // Abrir la puerta izquierda
        }
        else if (charactersOnLeft > zoneLeft.charAmount.Value || charactersOnLeft < zoneLeft.charAmount.Value)
        {
            DetectedSomething(2); // Cerrar la puerta izquierda
        }
        // Igualar los valores al final
        charactersOnLeft = zoneLeft.charAmount.Value;
    }
    void CheckPuppetBar()
    {
        if (type2Bar[0].Value < 25 && puppet25P == false)
        {
            puppet25P = true;
        } else if (type2Bar[0].Value < 50 && puppet50P == false)
        {
            puppet50P = true;
        }
        if (puppet25P)
        {
            //puppet25P = false;
            // Eliminar la desicion de subir la barra en 50, basicamente la reemplaza
            choices.RemoveAll(AIChoice => AIChoice.ID == 4);
            DetectedSomething(6);
        }
        if (puppet50P)
        {
            //puppet50P = false;
            DetectedSomething(4);
        }
    }
    void CheckFoxyBar()
    {
        if (type2Bar[1].Value < 25 && foxy25P == false)
        {
            foxy25P = true;
            DetectedSomething(3);
        }
        else if (type2Bar[1].Value < 50 && foxy50P == false)
        {
            foxy50P = true;
            // Eliminar la desicion de subir la barra en 50, basicamente la reemplaza
            choices.RemoveAll(AIChoice => AIChoice.ID == 3);
            DetectedSomething(5);
        }
    }

    void workTimeDelay()
    {
        // Si terminaste de hacer la tarea decide hacer otra
        if (!isBussy)
        {
            MakeDesicion();
        } else if (isBussy)
        {
            // Se esta ejecutando entonces que se gaste el tiempo de ejecucion para accionarlo
            presentExecuteTime = presentExecuteTime - Time.deltaTime;
            if (presentExecuteTime <= 0)
            {
                // Funcion de ejecucion de accion
                ExecuteAction(highestPriorityChoice);
            }
        }
    }
    public void MakeDesicion()
    {
        choices.Sort((x, y) => y.priority.CompareTo(x.priority));

        if (choices.Count > 0)
        {
            // Igualar la accion prioritaria
            highestPriorityChoice = choices[0];
            // El tiempo de ejecucion es igual al de la decision
            executeTimeOG = highestPriorityChoice.executeTime;
            // El tiempo de ejecucion presente es el inicial
            presentExecuteTime = executeTimeOG;
            // Ya se tomo la desicion ahora se va a estar ocupando haciendola
            isBussy = true;
        }
    }

    private void ExecuteAction(AIChoice choice)
    {
        switch (choice.ID)
        {
            case 1: print("Alguien esta a mi izquierda: Hora de hacerlo");
                doorRight.isClosedOrOpen = true;
                break;
            case 2: print("Alguien esta a mi derecha: Hora de hacerlo");
                doorLeft.isClosedOrOpen = true;
                break;
            case 3: print("Barra Foxy esta 50% de gastado: Hora de hacerlo");
                type2Bar[1].Value += 50;
                foxy50P = true;
                break;
            case 4: print("Barra Puppet esta 50% de gastado: Hora de hacerlo");
                type2Bar[0].Value += 50;
                puppet50P = true;
                break;
            case 5: print("Barra Foxy esta 25% de gastado: Hora de hacerlo");
                type2Bar[1].Value += 75;
                foxy25P = true;
                break;
            case 6: print("Barra Puppet esta 25% de gastado: Hora de hacerlo");
                type2Bar[0].Value += 75;
                puppet25P = true;
                break;
            case 7: print("Shadow Freddy Aparecio: Hora de hacerlo");
                phantomChar[0].presentPosition = 0;
                break;
            case 8: print("Shadow Bonnie Aparecio: Hora de hacerlo");
                phantomChar[1].presentPosition = 0;
                break;
            case 9: print("Golden Freddy Aparecio: Hora de hacerlo");
                phantomChar[2].presentPosition = 0;
                break;
            case 10: print("No hay nadie a mi derecha, debo abrir la puerda para no gastar energia: Hora de hacerlo");
                doorRight.isClosedOrOpen = false;
                break;
            case 11: print("No hay nadie a mi izquierda, debo abrir la puerda para no gastar energia: Hora de hacerlo");
                doorLeft.isClosedOrOpen = false;
                break;
        }
        choices.Remove(choice);
        isBussy = false;
    }

    public void DetectedSomething(int ID)
    {
        switch (ID)
        {
            case 1: print("Alguien esta a mi izquierda: Prioridad 5: Tiempo 1");
                AIChoice closeDoorRight = new AIChoice();
                closeDoorRight.ID = ID;
                closeDoorRight.priority = 5;
                closeDoorRight.executeTime = 1;
                choices.Add(closeDoorRight);
                break;
            case 2: print("Alguien esta a mi derecha: Prioridad 5: Tiempo 1");
                AIChoice closedDoorLeft = new AIChoice();
                closedDoorLeft.ID = ID;
                closedDoorLeft.priority = 5;
                closedDoorLeft.executeTime = 1;
                choices.Add(closedDoorLeft);
                break;
            case 3: print("Barra Foxy esta 50% de gastado: Prioridad 2: Tiempo 3");
                AIChoice foxy50 = new AIChoice();
                foxy50.ID = ID;
                foxy50.priority = 2;
                foxy50.executeTime = 3;
                choices.Add(foxy50);
                break;
            case 4: print("Barra Puppet esta 50% de gastado: Prioridad 2: Tiempo 3");
                AIChoice puppet50 = new AIChoice();
                puppet50.ID = ID;
                puppet50.priority = 2;
                puppet50.executeTime = 3;
                choices.Add(puppet50);
                break;
            case 5: print("Barra Foxy esta 25% de gastado: Prioridad 4: Tiempo 6");
                AIChoice foxy25 = new AIChoice();
                foxy25.ID = ID;
                foxy25.priority = 4;
                foxy25.executeTime = 6;
                choices.Add(foxy25);
                break;
            case 6: print("Barra Puppet esta 25% de gastado Prioridad 4: Tiempo 6");
                AIChoice puppet25 = new AIChoice();
                puppet25.ID = ID;
                puppet25.priority = 4;
                puppet25.executeTime = 6;
                choices.Add(puppet25);
                break;
            case 7: print("Shadow Fredy Aparecio: Prioridad 1: Tiempo 5");
                AIChoice shadowFreddy = new AIChoice();
                shadowFreddy.ID = ID;
                shadowFreddy.priority = 1;
                shadowFreddy.executeTime = 5;
                choices.Add(shadowFreddy);
                break;
            case 8: print("Shadow Bonnie Aparecio: Prioridad 1: Tiempo 5");
                AIChoice shadowBonnie = new AIChoice();
                shadowBonnie.ID = ID;
                shadowBonnie.priority = 1;
                shadowBonnie.executeTime = 5;
                choices.Add(shadowBonnie);
                break;
            case 9: print("Golden Freddy Aparecio: Prioridad 1: Tiempo 5");
                AIChoice goldenFreddy = new AIChoice();
                goldenFreddy.ID = ID;
                goldenFreddy.priority = 1;
                goldenFreddy.executeTime = 5;
                choices.Add(goldenFreddy);
                break;
            case 10: print("No hay nadie a mi derecha, debo abrir la puerda para no gastar energia: Prioridad 3: Tiempo 1");
                AIChoice openDoorRight = new AIChoice();
                openDoorRight.ID = ID;
                openDoorRight.priority = 3;
                openDoorRight.executeTime = 1;
                choices.Add(openDoorRight);
                break;
            case 11: print("No hay nadie a mi izquierda, debo abrir la puerda para no gastar energia: Prioridad 3: Tiempo 1");
                AIChoice openDoorLeft = new AIChoice();
                openDoorLeft.ID = ID;
                openDoorLeft.priority = 3;
                openDoorLeft.executeTime = 1;
                choices.Add(openDoorLeft);
                break;
        }
    }
}
