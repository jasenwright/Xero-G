import java.util.Random;
public class proceduralLevel{

	public static void main(String [] args){
		int[][] cave = new int[20][75];
		
		//Create 'cave'
		//3 represents a wall
		//1 represents potential open space
		//0 will represent open space
		for (int i=0;i<cave.length;i++){
			for(int y=0; y<cave[0].length; y++){
				if (i==0 || i==cave.length-1){
					cave[i][y] = 3;
				}
				else if (y == 0 || y == cave[0].length-1){
					cave[i][y] = 3;
				}
				else{
					cave[i][y] = 1;
				}
			}
		}
		
		//Build the cave-----------------------------------------------------------------
		
		for (int z=0; z<5; z++){
		
			int value = 1;
			int upDirection = 0;
			int rightDirection = 0;
			Random r = new Random();
			//Adjust these if you want to start in a different location in the cave
			int xPosition = 9;
			int yPosition = 30;
			
			//Code just stops when it hits a wall
			//Could be run multiple times to create 
			while (value !=3){
				cave[xPosition][yPosition] = 0;
				//This is the exclusive or which states that one of right or upDirection needs to be 0
				//	so that don't move diagonally
				while (!(rightDirection == 0 ^ upDirection==0)){
					rightDirection = r.nextInt(1+2) - 1;
					upDirection = r.nextInt(1+2) - 1;
				}

				//Adjust x and y position in cave
				xPosition = xPosition + upDirection;
				yPosition = yPosition + rightDirection;
				
				//See what value you're on so if it's a wall stop the algorithm
				value = cave[xPosition][yPosition];
				
				//Needed to reset upDirection and rightDirection so will enter the above while loop 
				upDirection = 0;
				rightDirection= 0;
			}
		}	
		//-------------------------------------------------------------------------------------------
		System.out.println();
		System.out.println();
		System.out.println();
		
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