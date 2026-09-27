using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Pong1
{
    class Program
    {
        static void Main(string[] args)
        {
            const int fieldLength = 50, fieldWidth = 15;
            const char fieldTile = '-';
            string line = string.Concat(Enumerable.Repeat(fieldTile, fieldLength));


            const int RacketLength = 4;
            const char RacketTile = '|';

            int leftRacketHeight = 0;
            int rightRacketHeight = 0;

            int ballX = fieldLength / 2;
            int ballY = fieldWidth / 2;
            const char ballTile = 'O';

            bool isBallGoingDown = true;
            bool isBallGoingRight = true;

            int leftPlayerPoints = 0;
            int rightPlayerPoints = 0;

            int scoreboardX = fieldLength / 2 - 2;
            int scoreboardY = fieldWidth + 3;


            while (true) 
            {
                Console.SetCursorPosition(0, 0);
                Console.WriteLine(line);

                Console.SetCursorPosition(0, fieldWidth);
                Console.WriteLine(line);

                for (int i = 0; i < RacketLength; i++) 
                {
                    Console.SetCursorPosition(0, i + 1 + leftRacketHeight);
                    Console.WriteLine(RacketTile);
                    Console.SetCursorPosition(fieldLength - 1, i + 1 + rightRacketHeight);
                    Console.WriteLine(RacketTile);
                }

                while (!Console.KeyAvailable)
                {
                    Console.SetCursorPosition(ballX, ballY);
                    Console.WriteLine(ballTile);
                    Thread.Sleep(50);

                    Console.SetCursorPosition(ballX, ballY);
                    Console.WriteLine(' ');
                    if (isBallGoingDown)
                    {
                        ballY++;
                    }
                    else
                    {
                        ballY--;
                    }
                    if (isBallGoingRight)
                    {
                        ballX++;
                    }
                    else
                    {
                        ballX--;
                    }

                    if(ballY == 1 || ballY == fieldWidth - 1) 
                    {
                        isBallGoingDown = !isBallGoingDown;
                    }

                    if (ballX == 1)
                    {
                        if (ballY >= leftRacketHeight && ballY <= leftRacketHeight + RacketLength)
                        {
                            isBallGoingRight = !isBallGoingRight;
                        }
                        else
                        {
                            rightPlayerPoints++;
                            ballY = fieldWidth / 2;
                            ballX = fieldLength / 2;
                                
                            Console.SetCursorPosition(scoreboardX, scoreboardY);
                            Console.WriteLine($"{leftPlayerPoints} | {rightPlayerPoints}");

                            if (rightPlayerPoints == 5)
                            {
                                goto IfFinished;
                            }
                        }
                    }
                    if (ballX == fieldLength - 2)
                    {
                        if (ballY >= rightRacketHeight + 1 && ballY <= rightRacketHeight + RacketLength)
                        {
                            isBallGoingRight = !isBallGoingRight;
                        }
                        else
                        {
                            leftPlayerPoints++;
                            ballY = fieldWidth / 2;
                            ballX = fieldLength / 2;
                            
                            Console.SetCursorPosition(scoreboardX, scoreboardY);
                            Console.WriteLine($"{leftPlayerPoints} | {rightPlayerPoints}");
                            if (leftPlayerPoints == 5)
                            {
                                goto IfFinished;
                            }
                        }
                    }
                }

                switch (Console.ReadKey().Key)
                { 
                    case ConsoleKey.UpArrow:
                        if(rightRacketHeight > 0)
                        {
                            rightRacketHeight--;
                        }
                        break;
                    case ConsoleKey.DownArrow:
                        if (rightRacketHeight < fieldWidth - RacketLength - 1)
                        {
                            rightRacketHeight++;
                        }
                        break;
                    case ConsoleKey.W:
                        if (leftRacketHeight > 0)
                        {
                            leftRacketHeight--;
                        }
                        break;
                    case ConsoleKey.S:
                        if (leftRacketHeight < fieldWidth - RacketLength - 1)
                        {
                            leftRacketHeight++;
                        }
                        break;
                }
                for (int i = 1; i < fieldWidth; i++)
                {
                    Console.SetCursorPosition(0, i);
                    Console.WriteLine(" ");
                    Console.SetCursorPosition(fieldLength - 1, i);
                    Console.WriteLine(" ");
                }
            }
        IfFinished:;
            Console.Clear();
            Console.SetCursorPosition(fieldLength / 2, 2);
            if (rightPlayerPoints == 5)
            {
                Console.WriteLine("The Right Player Has Won!");
            }
            else { Console.WriteLine("The Left Player Has Won!"); }
        }
    }
}
