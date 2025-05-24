using System.Diagnostics;
using System.Collections.Concurrent;

//var t1 = new Thread(() =>
//{
//    while(true)
//    {
//        Thread.Sleep(500);
//        Console.WriteLine("Tgread 1");
//    }

//});
//t1.Start();

//var t2 = new Thread(() =>
//{
//    while(true)
//    {
//        Thread.Sleep(250);
//        Console.WriteLine("Tgread 2");
//    }

//});
//t2.Start();

//var t3 = new Thread(() =>
//{
//    while (true)
//    {
//        var key = Console.ReadKey(true);
//        Console.WriteLine($"Key pressed: {key.KeyChar}");
//    }

//});
//t3.Start();




int xPos = 20;
int yPos = 20;

int nextXPos = 20;
int nextYPos = 20;

Console.CursorVisible = false;
Console.SetCursorPosition(xPos, yPos);
Console.Write("X");
Console.Title = "Simple Game Exmple";

var bulletXPos = xPos;
var bulletYPos = yPos;
var bulletNextXPos = xPos;
var bulletNextYPos = yPos;
var bulletSpeed = 200;
var bulletExists = false;

var pause = new object();

    var playerThread = new Thread(() =>
{
    Thread? bulletThread = null;

    while (true)
    {
        var key = Console.ReadKey(true);

        switch (key.Key)
        {
            case ConsoleKey.Spacebar:
                lock(pause)
                {
                    Console.SetCursorPosition(0, 0);
                    Console.WriteLine("Threads paused. Press any key to resume.");
                    Console.ReadKey(true);
                    Console.SetCursorPosition(0, 0);
                    Console.WriteLine("                                        ");
                }
                break;
            case ConsoleKey.UpArrow:
                nextYPos--;
                break;
            case ConsoleKey.DownArrow:
                nextYPos++;
                break;
            case ConsoleKey.LeftArrow:
                nextXPos--;
                break;
            case ConsoleKey.RightArrow:
                nextXPos++;
                break; 
            case ConsoleKey.Enter:
                //fire
                if (bulletThread == null)
                {
                    bulletExists = true;
                    bulletXPos = xPos;
                    bulletYPos = yPos - 1;
                    bulletNextXPos = xPos;
                    bulletNextYPos = yPos - 1;

                    bulletThread = new Thread(() =>
                    {
                        while (bulletYPos > 1)
                        {
                            bulletNextYPos--;
                            Thread.Sleep(bulletSpeed);
                        }
                        bulletExists = false;
                        bulletThread = null;
                        Console.SetCursorPosition(bulletXPos, bulletYPos);
                        Console.Write(" ");
                    });
                    bulletThread.Start();
                }



                break;
            default:
                break;
        }

    }
});
playerThread.Start();

var enemyXPos = 0;
var enemyYPos = 0;
var enemyNextXPos = 0;
var enemyNextYPos = 0;
var enemySpeed = 300;

var tEnemy = new Thread(() =>
{
    while (true)
    {
        if (enemyXPos < xPos)
        {
            enemyNextXPos++;
        }
        else if (enemyXPos > xPos)
        {
            enemyNextXPos--;
        }

        if (enemyYPos < yPos)
        {
            enemyNextYPos++;
        }
        else if (enemyYPos > yPos)
        {
            enemyNextYPos--;
        }

        Thread.Sleep(enemySpeed);
    }
});
tEnemy.Start();

    while (true)
    {

        if (xPos != nextXPos || yPos != nextYPos)
        {
            lock (pause) { }

            Console.SetCursorPosition(xPos, yPos);
            Console.Write(" ");
            xPos = nextXPos;
            yPos = nextYPos;
            Console.SetCursorPosition(xPos, yPos);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("X");

        }

        if (enemyXPos != enemyNextXPos || enemyYPos != enemyNextYPos)
        {
            lock (pause) { }
            Console.SetCursorPosition(enemyXPos, enemyYPos);
            Console.Write(" ");
            enemyXPos = enemyNextXPos;
            enemyYPos = enemyNextYPos;
            Console.SetCursorPosition(enemyXPos, enemyYPos);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("O");

        }

        if (bulletExists)
        {
            if (bulletYPos != bulletNextYPos)
            {
                lock (pause) { }
                Console.SetCursorPosition(bulletXPos, bulletYPos);
                Console.Write(" ");
                bulletYPos = bulletNextYPos;
                Console.SetCursorPosition(bulletXPos, bulletYPos);
                Console.Write("I");

            }
        }

        if (xPos == enemyXPos && yPos == enemyYPos)
        {
            Console.SetCursorPosition(0, 0);
            Console.WriteLine("Game Over");
            break;
        }

        if (enemyXPos == bulletXPos && enemyYPos == bulletYPos)
        {
            enemyXPos = 0;
            enemyYPos = 0;
            enemyNextYPos = 0;
            enemyNextXPos = 0;
        }

        Thread.Sleep(10);
        lock (pause) { }
    }





