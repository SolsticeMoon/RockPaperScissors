using System;
using System.IO;

class Score
//create and keep score incrementally at the end of each game
{
  private static int player1Score =0;
  private static int player2Score =0;

  public static int GetScore(int playerNum)
  {
    if (playerNum == 1)
    {
      return player1Score;
    }
      
    else
    {
      return player2Score;
    }
  }

  public static int IncreaseScore(int playerNum)
  {
    if (playerNum == 1)
    {
      player1Score++;
      return player1Score;
    }
    else 
    {
      player2Score++;
      return player2Score;
    }
  }
}

class Program 
{
  public static string[] moves = {"rock", "paper", "scissors"};
  public static int currentGame = 1;
  public static int maxNumOfGames =6;
  public static string savedFile = "gamelog.txt";
//name save file for the rest of program
  public static void Main (string[] args) 
  {
    string playAgain = "y";
     
      GameTitle("Shadow Wizard Money Game");
      string player1Name = InputName(1);
      string player2Name = InputName(2);

      while(playAgain == "y" && currentGame < maxNumOfGames)//loop starts here so players don't input names each round
      {
        Console.Clear();

//shows scores from the save file at top of screen
        if (File.Exists(savedFile) == true)
        {
          Console.WriteLine("\n\n$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$");
          Console.WriteLine("Previous Game Scores;");
          ReadFromFile();
          Console.WriteLine("$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$\n\n");
        }
        
      Console.WriteLine($"\nWelcome {player1Name} and {player2Name}! You are now within the Shadow Arena.\n");
      Console.WriteLine("Game Number: " + currentGame);
      
      int player1Move = InputMove(player1Name);
      int player2Move = InputMove(player2Name);

      GameResult(player1Name, player2Name, player1Move, player2Move);
      if (currentGame < 5)//This prevents the player being asked if they want to play again at the end of their final game
      {
      playAgain = PlayAgain();
      }
      currentGame++;
      Console.Clear();
      
    } 
    DisplayScores(player1Name, player2Name);
  }

//Display the game title with decorations
  public static void GameTitle(string programName)
  {
    Console.WriteLine("$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$\n\n"+
      $"Welcome to {programName}!\n\n"+
      "$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$\n\n");
    Console.WriteLine("In the Shadow Arena, whoever has the most wins out of 5 games is the winner, the loser is shadow cursed for eternity");
  }

//I'm using string interpolation for number and playerName because it was easier to get working the way I wanted.  
  public static string InputName(int number)
//players enter their names
  {
    Console.WriteLine($"\nPlayer {number}, what is your name? Press ENTER when you're ready to continue");
    string playerName = Console.ReadLine();
    return playerName;
  }


  public static int InputMove(string playerName)
//players pick spell, ints are adjusted so spells are printed with correct number next to it
  {
    int spellNumber = 1;
    Console.WriteLine($"{playerName} It's your turn to cast a spell");
    foreach (string spell in moves)
    {
      Console.WriteLine(Convert.ToString(spellNumber) + ". " + spell);
      spellNumber++;
    }
    Console.WriteLine("Enter '1' for Rock '2' for Paper or '3' for Scissors and press ENTER to confirm");
    int move = Convert.ToInt32(Console.ReadLine()) -1;

    return move;
  }

  public static string PlayAgain()
//players can quit or continue between rounds
  {
    string playAgain;
    Console.WriteLine ("Thank you for playing Shadow Wizard Money Game");
    Console.WriteLine ("Press 'y' and ENTER if you want to play again");
    playAgain = Console.ReadLine();
    
    return playAgain;
  }

  public static void DisplayScores(string player1Name, string player2Name)
//displays final score at the end of 5 rounds and delcares the winner based on best of 5 
  {
    Console.WriteLine("$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$");
    Console.WriteLine(player1Name + "'s Score: ");
    Console.WriteLine(Score.GetScore(1));
    Console.WriteLine(player2Name + "'s Score: ");
    Console.WriteLine(Score.GetScore(2));
    Console.WriteLine("$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$");

    if (Score.GetScore(1) == Score.GetScore(2))
    {
      Console.WriteLine("The contest is a tie! Both players are shadow cursed for eternity...");
    }
    else if (Score.GetScore(1) > Score.GetScore(2))
    {
       Console.WriteLine(player1Name + " wins the Shadow Wizard Money Game!");
       Console.WriteLine(player2Name + " is shadow cursed for eternity...");
    }
    else
    {
       Console.WriteLine(player2Name + " wins the Shadow Wizard Money Game!");
       Console.WriteLine(player1Name + " is shadow cursed for eternity...");
    }

    WriteToFile("The Final Scores of the last game played:\n" + player1Name + "'s Score: " + Score.GetScore(1) + "\n" +player2Name + "'s Score: " + Score.GetScore(2));
  }

  public static void GameResult(string player1Name, string player2Name, int player1Move, int player2Move)
//game logic to figure out who wins the round and adds 1 to the score of the winner
  {
// Combine the moves into a single string
    string resultCombined = moves[player1Move] + moves[player2Move];
    
// Check the combined moves and display the winner or draw
    if (resultCombined == "rockscissors" || resultCombined == "scissorspaper" || resultCombined == "paperrock")
    {
      Console.WriteLine (player1Name + " WINS!");
      Score.IncreaseScore(1);
    }
    else if (resultCombined == "rockpaper" || resultCombined == "scissorsrock" || resultCombined == "paperscissors")
    {
      Console.WriteLine (player2Name + " WINS!");
      Score.IncreaseScore(2);
    }
    else if (resultCombined == "rockrock" || resultCombined == "scissorsscissors" || resultCombined == "paperpaper")
    {
      Console.WriteLine ("Draw");
//my way of catching incorrect input no longer works but it's not needed for assignment requirements
    }
  }

  public static void WriteToFile(string Log)
//saves to the savefile for future access
  {
    string writeText = Log;
    File.WriteAllText(savedFile, writeText);
  }

  public static void ReadFromFile()
//Reads from saved file
  {
    string[] lines = File.ReadAllLines(savedFile);
    foreach (string line in lines)
    {
      Console.WriteLine(line);
    }
  }
}
