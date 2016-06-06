# SFAS-game-attempted
SFAS incomplete

Search for a star – attempted game

FPS

The aim of the game is to collect the money bags in the maze, there is an AI droid that searching for you, and will fire you when it sees you. 

I defined the enemy AI as a finite state machine, they have different behaviours based on the triggers that cause that behaviour, so like for enemy we have patrol, investigate, alert, we have different conditions that allow a state transition between the different states of alert and patrol. 

public enum State
	{
		Patrol, //= green
		Investigate, // = yellow
		Alert // = red
	}

So here if we are in a state of patrol, if we see a player, then that state is now changed to alert state which would mean fire the player and follow him. 

see = new Job(Seeing(player, sightAngle,sightDistance,sightMaxHeight,0.5f,true	,() => 
{Debug.Log ("Saw you!");state = State.Alert;}),true); 

Here is a use of lambda function, useful when we want to run a section of code when another one frequently finishes. 

I created a randomly generated maze based on the kruskal algorithm. We have random lengths of wall created, they can not form a cycle if they do not then we include this edge other wise discard it.

			int[] repl;

			if (Random.Range(0, 2) >= 1)
			{
				int Z = Random.Range(0, v_wall.GetLength(0));
				int X = Random.Range(1, v_wall.GetLength(1) - 1);
				if (cell[Z,X] == cell[Z,X - 1])
					continue;
				repl = new int[] { cell[Z,X], cell[Z,X - 1] };
				v_wall[Z,X] = false;
			}
			else
			{
				int Z = Random.Range(1, h_wall.GetLength(0) - 1);
				int X = Random.Range(0, h_wall.GetLength(1));
				if (cell[Z,X] == cell[Z - 1,X])
					continue;
				repl = new int[] { cell[Z,X], cell[Z - 1,X] };
				h_wall[Z,X] = false;
			}
			System.Array.Sort(repl);
			replace_ids(repl);
			if (ids_all_zero(repl) == true)
				break;
		}
		create_exits(rand_exits, rand_floor_exits);

![kruskal maze algorthm](https://cloud.githubusercontent.com/assets/15308778/15822761/a3715f4c-2bef-11e6-96c6-8f5e874e50ba.JPG)
