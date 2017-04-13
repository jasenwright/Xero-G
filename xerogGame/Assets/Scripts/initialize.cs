using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class initialize : MonoBehaviour {

    public GameObject tile;
    public GameObject character;
    public GameObject turret;
    public GameObject healthpack;
    public GameObject caveWall;
    public GameObject npc;
    public GameObject kamTurret;
    public static char[,] cave = new char[25, 125];
    public static int xEnd = 10;
    public static int yEnd = 10;
    public float[] xyOfCharacter;
    public GameObject door;
    public int level;

    // Use this for initialization
    void Start () {

        //Get the level which the player is on
        try {     
            level = PlayerPrefs.GetInt("level");
        }
        catch { 
            level = 0;
            PlayerPrefs.SetInt("level", 0);
        }

        if (level > 5) {
            level = 4;
            PlayerPrefs.SetInt("level", 4);
        }
        
        buildCave();
        fillBorder();
        cellularAutomata();
        fillBorder();
        ArrayList xAndYPositions = new ArrayList();
        xAndYPositions = findCaves();
        connectCaves(xAndYPositions);
        cleanCave();
        initializeCave(tile, caveWall);
        xyOfCharacter = initializeCharacter(character, door);
        addTurretsAndHealthpacks(turret, healthpack, xyOfCharacter, npc, level, kamTurret);
        outerMapInit(caveWall);

    }

    //This puts bricks around the map so the player never sees empty space
    public static void outerMapInit(GameObject caveWall) {
        float x = -10.35f;
        float y = 8.4f;
        for (int i = 0; i < cave.GetLength(0); i++) {
            y = y - 2.65f;
            x = -10.35f;
            for (int j = 0; j < cave.GetLength(1); j++) {
                x = x + 2.65f;
                //Put bricks to the left
                if (j == 0) {
                    float z = x;
                    for (int r=10; r>0; r--) {
                        Instantiate(caveWall, new Vector3(z-2.65f, y, 0), Quaternion.identity);
                        z = z - 2.65f;
                    }
                }
                //Put bricks to the right
                if (j == cave.GetLength(1) - 1){
                    float z = x;
                    for (int r = 10; r > 0; r--) {
                        Instantiate(caveWall, new Vector3(z + 2.65f, y, 0), Quaternion.identity);
                        z = z + 2.65f;
                    }
                }
                //Put bricks to the bottom
                if (i == cave.GetLength(0) - 1) {
                    float z = y;
                    for (int r = 10; r > 0; r--) {

                        //Bottom left corner
                        if (j == 0) {
                            float q = x;
                            for (int a = 10; a > 0; a--) {
                                Instantiate(caveWall, new Vector3(q - 2.65f, z, 0), Quaternion.identity);
                                q = q - 2.65f;

                            }
                        }
                        //Top right corner
                        if (j == cave.GetLength(1) - 1) {
                            float q = x;
                            for (int a = 10; a > 0; a--) {
                                Instantiate(caveWall, new Vector3(q + 2.65f, z, 0), Quaternion.identity);
                                q = q + 2.65f;
                            }
                        }

                        //This is the actual instantiate for the bottom
                        Instantiate(caveWall, new Vector3(x, z - 2.65f, 0), Quaternion.identity);
                        z = z - 2.65f;
                    }
                }
            }
        }

     }

    public static void addTurretsAndHealthpacks(GameObject turret, GameObject healthpacks, float[] characterSpot, GameObject npc, int level, GameObject kamTurret) {
        System.Random rand = new System.Random();
        int prob;

        enemyCounter eCounter = GameObject.Find("enemyCounter").GetComponent<enemyCounter>();
        float x = -10.35f;
        float y = 8.4f;

        for (int i = 0; i < cave.GetLength(0); i++) {
            y = y - 2.65f;
            x = -10.35f;
            for (int j = 0; j < cave.GetLength(1); j++) {
                x = x + 2.65f;

                //This makes sure no turrets spawn right next to the character
                if (x >= characterSpot[0] + 20 || x <= characterSpot[0] -20 || y>= characterSpot[1]+10 || y<=characterSpot[1]-10) {

                                    
                    //Instantiate NPC, odds of there being an NPC in space increase as player progresses
                    if (cave[i,j] != '#') {
                        prob = rand.Next(0, 130-(level*5));
                        if (prob == 0) {
                            Instantiate(npc, new Vector3(x, y, 0), Quaternion.identity);
                            eCounter.numberOfEnemies = eCounter.numberOfEnemies + 1;
                        }
                        if (prob == 1) {
                            Instantiate(kamTurret, new Vector3(x, y, 0), Quaternion.identity);
                            eCounter.numberOfEnemies = eCounter.numberOfEnemies + 1;
                        }
                    }
                   

                    //Put turrets on ground - odds of turrets being placed increases as player progresses
                    if (cave[i, j] != '#' && cave[i + 1, j] == '#') {
                        try {
                            prob = rand.Next(0, 15 - (level * 3));
                        }
                        catch {prob = 0;}
                        if (prob == 0) {
                            Instantiate(turret, new Vector3(x, y - .8f, 0), Quaternion.identity);
                            eCounter.numberOfEnemies = eCounter.numberOfEnemies + 1;
                        }

                        //Add healthpacks on ground
                        //Odds of there being a healthpack decreases as player progresses
                        try {
                            prob = rand.Next(0, 10 + (level * 5));
                        }
                        catch { prob = 0; }
                        if (prob == 0) {
                            Instantiate(healthpacks, new Vector3(x, y - .8f, 0), Quaternion.identity);
                        }
                    }
                    
                    
                    //Put turrets on roof
                    if (cave[i, j] != '#' && cave[i - 1, j] == '#') {
                        try {
                            prob = rand.Next(0, 15 - (level * 3));
                        }
                        catch { prob = 0; }
                        if (prob == 0) {
                            var tur = Instantiate(turret, new Vector3(x, y + .8f, 0), Quaternion.identity);
                            tur.transform.Rotate(0, 0, 180f);
                            eCounter.numberOfEnemies = eCounter.numberOfEnemies + 1;
                        }
                    }
                    //Put turrets on right wall
                    if (cave[i, j] != '#' && cave[i, j - 1] == '#') {
                        try {
                            prob = rand.Next(0, 15 - (level * 3));
                        }
                        catch { prob = 0; }
                        if (prob == 0) {
                            var tur = Instantiate(turret, new Vector3(x - .8f, y, 0), Quaternion.identity);
                            tur.transform.Rotate(0, 0, -90f);
                            eCounter.numberOfEnemies = eCounter.numberOfEnemies + 1;
                        }
                    }
                    //Put turrets left wall
                    if (cave[i, j] != '#' && cave[i, j + 1] == '#') {
                        try {
                            prob = rand.Next(0, 15 - (level * 3));
                        }catch { prob = 0; }
                        if (prob == 0) {
                            var tur = Instantiate(turret, new Vector3(x + .8f, y, 0), Quaternion.identity);
                            tur.transform.Rotate(0, 0, 90f);
                            eCounter.numberOfEnemies = eCounter.numberOfEnemies + 1;
                        }
                    }
                                         

                }
            }
        }

    }
            

    public static void initializeCave(GameObject tile, GameObject caveWall){
        //bool characterInitialized = false;
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

                else {
                    Instantiate(caveWall, new Vector3(x, y, 0), Quaternion.identity);
                }
                
            }
        }

    }

    public static float[] initializeCharacter(GameObject character, GameObject door) {
        float[] characterSpot = new float[2];
        bool placedCharacter = false;
        float x = -10.35f;
        float y = 8.4f;
        for (int i = 0; i < cave.GetLength(0); i++) {
            y = y - 2.65f;
            x = -10.35f;
            for (int j = 0; j < cave.GetLength(1); j++) {
                x = x + 2.65f;
                if (cave[i, j] != '#' && cave[i+1,j]=='#' && placedCharacter==false) {
                    Instantiate(character, new Vector3(x, y, 0), Quaternion.identity);
                    Instantiate(door, new Vector3(x, y, 0), Quaternion.identity);
                    characterSpot[0] = x;
                    characterSpot[1] = y;
                    placedCharacter = true;
                }
            }
        }
        return characterSpot;
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

    public static ArrayList findCaves(){
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
        //Returns a list of the center point of each cavern to connect the caves
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
                //Random Number
                prob = rand.Next(0, 99);
                //55% chance as number is between 0 and 99
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

    //Puts a # along the border of the 2D array
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
