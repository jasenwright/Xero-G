import java.util.Random;
public class proceduralLevel{

	public static char[][] cave = new char[15][75];

	public static void main(String [] args){
		
		Random rand = new Random();
		int prob;
		int numWalls;
		
		//Create 'cave'
		//# represents a wall
		//. will represent open space
		boolean percentFalse = true;
		double walls=0;
		double total=0;
		

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
			
		fillBorder();
		printCave();
		
		// Cellular Automata
		for (int z = 0; z < 5; z++) {
		
			for(int j=0; j<cave[0].length; j++) {
				for (int i=0;i<cave.length;i++) {
				
					
					if (i==0 || i==cave.length-1){
						continue;
					}
					else if (j == 0 || j == cave[0].length-1){
						continue;
					}
					
					numWalls = 0;
					char currSpace = cave[i][j];
					
					numWalls += lookAround(i, j);
					if (numWalls > 5) {
						cave[i][j] = '#';
					} else if (numWalls < 4) {
						cave[i][j] = '.';
					}
				}
			}
		
		}
		fillBorder();
		printCave();
			
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
}