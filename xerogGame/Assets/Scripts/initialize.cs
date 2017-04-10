using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class initialize : MonoBehaviour {

    public GameObject tile;
    public GameObject character;
    public GameObject turret;
    public static char[,] cave = new char[25, 125];
    public static int xEnd = 10;
    public static int yEnd = 10;

    // Use this for initialization
    void Start () {

        buildCave();
        fillBorder();
        cellularAutomata();
        fillBorder();
        ArrayList xAndYPositions = new ArrayList();
        xAndYPositions = findCaves();
        connectCaves(xAndYPositions);
        cleanCave();
        initializeCaveAndCharacter(tile, character);
        addTurrets(turret);
        
    }

    public static void addTurrets(GameObject turret) {
        System.Random rand = new System.Random();
        int prob;
        prob = rand.Next(0, 99);
 
        float x = -10.35f;
        float y = 8.4f;
        for (int i = 0; i < cave.GetLength(0); i++) {
            y = y - 2.65f;
            x = -10.35f;
            for (int j = 0; j < cave.GetLength(1); j++) {
                x = x + 2.65f;
                //Put turrets on ground
                if (cave[i, j] != '#' && cave[i+1,j]=='#') {
                    prob = rand.Next(0, 10);
                    if (prob == 5) {
                        Instantiate(turret, new Vector3(x, y - .8f, 0), Quaternion.identity);
                    }
                }
                //Put turrets on roof
                if (cave[i,j] !='#' && cave[i-1, j] == '#') {
                    prob = rand.Next(0, 10);
                    if (prob == 5) {
                        var tur = Instantiate(turret, new Vector3(x, y + .8f, 0), Quaternion.identity);
                        tur.transform.Rotate(0, 0, 180f);
                    }
                }
                //Put turrets on right wall
                if (cave[i, j] != '#' && cave[i, j-1] == '#') {
                    prob = rand.Next(0, 10);
                    if (prob == 5) {
                        var tur = Instantiate(turret, new Vector3(x-.8f, y, 0), Quaternion.identity);
                        tur.transform.Rotate(0, 0, -90f);
                    }
                }
                //Put turrets left wall
                if (cave[i, j] != '#' && cave[i, j+1] == '#') {
                    prob = rand.Next(0, 10);
                    if (prob == 5) {
                        var tur = Instantiate(turret, new Vector3(x + .8f, y , 0), Quaternion.identity);
                        tur.transform.Rotate(0, 0, 90f);
                    }
                }
            }
        }

    }
            

    public static void initializeCaveAndCharacter(GameObject tile, GameObject character)
    {
        bool characterInitialized = false;
        float x = -10.35f;
        float y = 8.4f;
        for (int i = 0; i < cave.GetLength(0); i++)
        {
            y = y - 2.65f;
            x = -10.35f;
            for (int j = 0; j < cave.GetLength(1); j++)
            {
                x = x + 2.65f;
                if (cave[i, j] == '#')
                {
                    Instantiate(tile, new Vector3(x, y, 0), Quaternion.identity);
                }
                if (cave[i,j]!='#' && characterInitialized == false)
                {
                    Instantiate(character, new Vector3(x, y, 0), Quaternion.identity);
                    characterInitialized = true;
                }
            }
        }

    }

    public static void cleanCave()
    {
        for (int i = 0; i < cave.GetLength(0); i++)
        {
            for (int j = 0; j < cave.GetLength(1); j++)
            {
                if (cave[i, j] != '#')
                {
                    cave[i, j] = ' ';
                }
            }
        }
    }

    public static void connectCaves(ArrayList positions)
    {

        for (int i = 0; i < positions.Count; i = i + 2)
        {
            findPath(xEnd, yEnd, (int)positions[i], (int)positions[i + 1]);
        }
    }

    public static ArrayList findCaves()
    {
        int count = 0;
        ArrayList listOfXYPositions = new ArrayList();
        for (int i = 0; i < cave.GetLength(0); i++)
        {
            for (int j = 0; j < cave.GetLength(1); j++)
            {
                if (cave[i, j] == '.')
                {
                    //Lists to keep track of where the 'miner' has gone
                    ArrayList xPositionList = new ArrayList();
                    ArrayList yPositionList = new ArrayList();
                    int xpos = i;
                    int ypos = j;
                    bool foundSpace = true;
                    count++;
                    char countChar = (char)(count + 48);
                    cave[i, j] = countChar;

                    //While a space is available to fill, always keep against the wall and move in a circle towards the middle
                    while (foundSpace)
                    {
                        foundSpace = false;
                        //Add current position to list of positions for backtracking
                        xPositionList.Add(xpos);
                        yPositionList.Add(ypos);

                        //Try and go up because theres a wall to your left
                        if ((cave[xpos, ypos - 1] == '#' || cave[xpos, ypos - 1] == countChar) && cave[xpos - 1, ypos] == '.')
                        {
                            cave[xpos - 1, ypos] = countChar;
                            xpos--;
                            foundSpace = true;
                        }
                        //Try and go right because there's a wall above you
                        else if ((cave[xpos - 1, ypos] == '#' || cave[xpos - 1, ypos] == countChar) && cave[xpos, ypos + 1] == '.')
                        {
                            cave[xpos, ypos + 1] = countChar;
                            ypos++;
                            foundSpace = true;
                        }
                        //Try and go down because there's a wall to your right
                        else if ((cave[xpos, ypos + 1] == '#' || cave[xpos, ypos + 1] == countChar) && cave[xpos + 1, ypos] == '.')
                        {
                            cave[xpos + 1, ypos] = countChar;
                            xpos++;
                            foundSpace = true;
                        }
                        //Try and go left because there's a wall below you
                        else if ((cave[xpos + 1, ypos] == '#' || cave[xpos + 1, ypos] == countChar) && cave[xpos, ypos - 1] == '.')
                        {
                            cave[xpos, ypos - 1] = countChar;
                            ypos--;
                            foundSpace = true;
                        }


                        //Either have gotten to the middle of the cave, or stuck
                        if (foundSpace == false)
                        {
                            //Retrace steps, start at -1 incase only 1 item in list 
                            for (int k = xPositionList.Count - 1; k >= 0; k--)
                            {
                                ////Try and go up because theres a wall to your left
                                if ((cave[(int)xPositionList[k], (int)yPositionList[k] - 1] == '#' || cave[(int)xPositionList[k], (int)yPositionList[k] - 1] == countChar) && cave[(int)xPositionList[k] - 1, (int)yPositionList[k]] == '.')
                                {
                                    xpos = (int)xPositionList[k];
                                    ypos = (int)yPositionList[k];
                                    foundSpace = true;
                                    break;
                                }
                                //Try and go right because there's a wall above you
                                else if ((cave[(int)xPositionList[k] - 1, (int)ypos] == '#' || cave[(int)xPositionList[k] - 1, (int)yPositionList[k]] == countChar) && cave[(int)xPositionList[k], (int)yPositionList[k] + 1] == '.')
                                {
                                    xpos = (int)xPositionList[k];
                                    ypos = (int)yPositionList[k];
                                    foundSpace = true;
                                    break;
                                }
                                //Try and go down because there's a wall to your right
                                else if ((cave[(int)xPositionList[k], (int)ypos + 1] == '#' || cave[(int)xPositionList[k], (int)yPositionList[k] + 1] == countChar) && cave[(int)xPositionList[k] + 1, (int)yPositionList[k]] == '.')
                                {
                                    xpos = (int)xPositionList[k];
                                    ypos = (int)yPositionList[k];
                                    foundSpace = true;
                                    break;
                                }
                                //Try and go left because there's a wall below you
                                else if ((cave[(int)xPositionList[k] + 1, (int)ypos] == '#' || cave[(int)xPositionList[k] + 1, (int)yPositionList[k]] == countChar) && cave[(int)xPositionList[k], (int)yPositionList[k] - 1] == '.')
                                {
                                    xpos = (int)xPositionList[k];
                                    ypos = (int)yPositionList[k];
                                    foundSpace = true;
                                    break;
                                }
                            }
                        }
                    }
                    //This is to make the last position the correct character
                    cave[xpos, ypos] = countChar;
                    listOfXYPositions.Add(xpos);
                    listOfXYPositions.Add(ypos);
                }
            }
        }
        return listOfXYPositions;
    }

    public static void cellularAutomata()
    {
        // Cellular Automata
        int numWalls;
        //Loop z times to have clean cave
        for (int z = 0; z < 5; z++)
        {
            //Then loop through 2d array
            for (int j = 0; j < cave.GetLength(1); j++)
            {
                for (int i = 0; i < cave.GetLength(0); i++)
                {
                    //If at the border of the cave skip
                    if (i == 0 || i == cave.GetLength(0) - 1)
                    {
                        continue;
                    }
                    else if (j == 0 || j == cave.GetLength(1) - 1)
                    {
                        continue;
                    }
                    numWalls = 0;
                    
                    //Check how many walls are around the point of interest
                    numWalls += lookAround(i, j);
                    if (numWalls > 5)
                    {
                        cave[i, j] = '#';
                    }
                    else if (numWalls < 4)
                    {
                        cave[i, j] = '.';
                    }
                }
            }
        }
    }

    // col = x, row = y
    public static int lookAround(int row, int col)
    {
        int walls = 0;

        if (row - 1 >= 0)
        {
            // checks north
            if (cave[row - 1, col] == '#')
            {
                walls++;
            }
            // checks northwest
            if (col - 1 >= 0)
            {
                if (cave[row - 1, col - 1] == '#')
                {
                    walls++;
                }
            }
            // checks northeast
            if (col + 1 < cave.GetLength(1))
            {
                if (cave[row - 1, col + 1] == '#')
                {
                    walls++;
                }
            }
        }

        if (row + 1 < cave.GetLength(0))
        {
            // checks south
            if (cave[row + 1, col] == '#')
            {
                walls++;
            }
            // checks southwest
            if (col - 1 >= 0)
            {
                if (cave[row + 1, col - 1] == '#')
                {
                    walls++;
                }
            }
            // checks southeast
            if (col + 1 < cave.GetLength(1))
            {
                if (cave[row + 1, col + 1] == '#')
                {
                    walls++;
                }
            }
        }

        //checks west
        if (col - 1 >= 0)
        {
            if (cave[row, col - 1] == '#')
            {
                walls++;
            }
        }

        //checks east
        if (col + 1 < cave.GetLength(1))
        {
            if (cave[row, col + 1] == '#')
            {
                walls++;
            }
        }

        return walls;
    }

    public static void buildCave()
    {
        System.Random rand = new System.Random();
        int prob;

        //Create 'cave'
        //# represents a wall
        //. will represent open space
        for (int i = 0; i < cave.GetLength(0); i++)
        {
            for (int y = 0; y < cave.GetLength(1); y++)
            {
                prob = rand.Next(0, 99);
                if (prob < 55)
                {
                    cave[i, y] = '#';
                }
                else
                {
                    cave[i, y] = '.';
                }
            }
        }
    }

    public static void fillBorder()
    {
        for (int i = 0; i < cave.GetLength(0); i++)
        {
            for (int y = 0; y < cave.GetLength(1); y++)
            {
                if (i == 0 || i == cave.GetLength(0) - 1)
                {
                    cave[i, y] = '#';
                }
                else if (y == 0 || y == cave.GetLength(1) - 1)
                {
                    cave[i, y] = '#';
                }
            }
        }
    }

    public static void printCave()
    {
        //Print out what cave looks like after digging
        Console.Write("\r\n");
        for (int i = 0; i < cave.GetLength(0); i++)
        {
            for (int y = 0; y < cave.GetLength(1); y++)
            {
                Console.Write(cave[i, y]);
            }
            Console.Write("\r\n");
        }
    }

    public static void findPath(int xGoal, int yGoal, int xPosition, int yPosition)
    {
        System.Random rand = new System.Random();
        int prob;
        int xDifference = 0;
        int yDifference = 0;
        double chanceOfVerticalMovement;

        while (!(xPosition == xGoal && yPosition == yGoal))
        {
            xDifference = xGoal - xPosition;
            yDifference = yGoal - yPosition;

            chanceOfVerticalMovement = ((double)Math.Abs(xDifference) / (Math.Abs(xDifference) + Math.Abs(yDifference))) * 100;

            prob = rand.Next(0, 99);
            if (((int)chanceOfVerticalMovement != 0) && (((int)chanceOfVerticalMovement) >= prob + 1))
            {
                xPosition = xPosition + (xDifference / Math.Abs(xDifference));
                //xPosition = (int)xPosition;
                cave[xPosition, yPosition] = '.';
            }
            else
            {
                yPosition = yPosition + (yDifference / Math.Abs(yDifference));
                //yPosition = (int)yPosition;
                cave[xPosition, yPosition] = '.';
            }
        }
    }


    // Update is called once per frame
    void Update () {
		
	}
}
