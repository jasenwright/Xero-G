import java.util.Random;
public class Automata{

	public static char[][] cave = new char[15][75];

	public static void main(String [] args){
		
		buildCave();		
		fillBorder();
		printCave();
		cellularAutomata();
		fillBorder();
		findCaves();
		printCave();
			
	}
	
	//TO DO!-----------------------------------
	public static void findCaves(){
		for (int i=0; i< cave.length; i++){
			for (int j=0; j< cave[0].length; j++){
				if (cave[i][j] == '.'){
					int xpos = i;
					int ypos = j;
					while(cave[xpos]ypos]!='1'){
						if (cave[xpos+1][ypos] =='.'){
							
				}
			}
		}		
	}
	//-------------------------------------------------
	public static void cellularAutomata(){
		// Cellular Automata
		int numWalls;
		//Loop z times to have clean cave
		for (int z = 0; z < 5; z++) {
			//Then loop through 2d array
			for(int j=0; j<cave[0].length; j++) {
				for (int i=0;i<cave.length;i++) {
					//If at the border of the cave skip
					if (i==0 || i==cave.length-1){
						continue;
					}
					else if (j == 0 || j == cave[0].length-1){
						continue;
					}
					numWalls = 0;
					char currSpace = cave[i][j];
					//Check how many walls are around the point of interest
					numWalls += lookAround(i, j);
					if (numWalls > 5) {
						cave[i][j] = '#';
					} else if (numWalls < 4) {
						cave[i][j] = '.';
					}
				}
			}
		}
	}
	
	// col = x, row = y
	public static int lookAround(int row, int col) {
		int walls = 0;
		
		if (row - 1 >= 0 ) {
			// checks north
			if (cave[row - 1][col] == '#') {
				walls++;
			}
			// checks northwest
			if (col-1 >= 0){
				if (cave[row-1][col-1] == '#'){
					walls++;
				}
			}
			// checks northeast
			if (col + 1 < cave[0].length) {
				if (cave[row-1][col+1] == '#'){
					walls++;
				}
			}
		}
		
		if (row + 1 < cave.length ) {
			// checks south
			if (cave[row + 1][col] == '#') {
				walls++;
			}
			// checks southwest
			if (col-1 >= 0){
				if (cave[row+1][col-1] == '#'){
					walls++;
				}
			}
			// checks southeast
			if (col + 1 < cave[0].length) {
				if (cave[row+1][col+1] == '#'){
					walls++;
				}
			}
		}
		
		//checks west
		if (col - 1 >= 0) {
			if (cave[row][col-1] == '#') {
				walls++;
			}
		}
		
		//checks east
		if (col + 1 < cave[0].length) {
			if (cave[row][col+1] == '#') {
				walls++;
			}
		}
		
		return walls;
	}
	
	public static void buildCave(){
		Random rand = new Random();
		int prob;
		
		//Create 'cave'
		//# represents a wall
		//. will represent open space
		for (int i=0;i<cave.length;i++) {
			for(int y=0; y<cave[0].length; y++) {
				prob = rand.nextInt(99);				
				if (prob < 55) {
					cave[i][y] = '#';
				} else {
					cave[i][y] = '.';
				}
			}
		}
	}	
	
	public static void fillBorder() {
		for (int i=0;i<cave.length;i++){
			for(int y=0; y<cave[0].length; y++){
				if (i==0 || i==cave.length-1){
					cave[i][y] = '#';
				}
				else if (y == 0 || y == cave[0].length-1){
					cave[i][y] = '#';
				}
			}
		}
	}
	
	public static void printCave() {
		//Print out what cave looks like after digging
		System.out.println();
		for (int i=0;i<cave.length;i++){
			for(int y=0; y<cave[0].length; y++){
				System.out.print(cave[i][y]);
			}
			System.out.println();
		}
	}
	
	public static void findPath(int xGoal, int yGoal, int xPosition, int yPosition){
		Random rand = new Random();
		int prob;
		int xDifference = 0;
		int yDifference = 0;
		double chanceOfVerticalMovement;
				
		while( !(xPosition==xGoal && yPosition==yGoal)){
			xDifference = xGoal - xPosition;
			yDifference = yGoal - yPosition;
							
			chanceOfVerticalMovement = ((double)Math.abs(xDifference) / (Math.abs(xDifference) + Math.abs(yDifference))) * 100;
			
			prob = rand.nextInt(99);
			if (((int)chanceOfVerticalMovement!=0) && (((int)chanceOfVerticalMovement) >= prob+1)){
				xPosition = xPosition + (xDifference / Math.abs(xDifference));
				xPosition = (int)xPosition;
				cave[xPosition][yPosition] = '.';
			}
			else{
				yPosition = yPosition + (yDifference / Math.abs(yDifference));
				yPosition = (int)yPosition;
				cave[xPosition][yPosition] = '.';
			}
		}	
	}	
}