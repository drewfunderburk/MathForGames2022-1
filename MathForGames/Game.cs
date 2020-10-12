using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;

namespace MathForGames
{
    class Game
    {
        private static bool _gameOver = false;
        private Scene _scene;

        //Static function used to set game over without an instance of game.
        public static void SetGameOver(bool value)
        {
            _gameOver = value;
        }

        //Return wether or not the specified console key is pressed
        public static bool CheckKey(ConsoleKey key)
        {
            
            if (Console.KeyAvailable)
            {
                if (Console.ReadKey(true).Key == key)
                {
                    return true;
                }
            }

            return false;
        }

        //Called when the game begins. Use this for initialization.
        public void Start()
        {
            _scene = new Scene();
            Actor actor = new Actor();
            _scene.AddActor(actor);
        }



        //Called every frame.
        public void Update()
        {
            _scene.Update();
        }

        //Used to display objects and other info on the screen.
        public void Draw()
        {
            Console.Clear();
            _scene.Draw();
        }


        //Called when the game ends.
        public void End()
        {

        }


        //Handles all of the main game logic including the main game loop.
        public void Run()
        {
            Start();

            while(!_gameOver)
            {
                
                Update();
                Draw();
                
                Thread.Sleep(50);
            }

            End();
        }
    }
}
