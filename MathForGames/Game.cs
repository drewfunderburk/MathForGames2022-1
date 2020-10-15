using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using MathLibrary;
using Raylib_cs;


namespace MathForGames
{
    class Game
    {
        private static bool _gameOver = false;
        private Scene _scene;
        private Camera2D _camera;
        public static ConsoleColor DefaultColor { get; set; } = ConsoleColor.White;

        //Static function used to set game over without an instance of game.
        public static void SetGameOver(bool value)
        {
            _gameOver = value;
        }

        public static ConsoleKey GetNextKey()
        {
            //If the user hasn't pressed a key return
            if(!Console.KeyAvailable)
            {
                return 0;
            }
            //Return the key that was pressed
            return Console.ReadKey(true).Key;
        }

        //Called when the game begins. Use this for initialization.
        public void Start()
        {
            Console.CursorVisible = false;
            _scene = new Scene();
            _camera = new Camera2D();
            Actor actor = new Actor(0,0,Color.GREEN,'■');
            actor.Velocity.X = 1;
            Player player = new Player(0, 1,Color.RED ,'@' );
            _scene.AddActor(player);
            _scene.AddActor(actor);
            Raylib.InitWindow(800, 500, "Math For Games");
            
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
            Texture2D samus = Raylib.LoadTexture("samus.png");
            Rectangle frameRec = new Rectangle(0, 0, samus.width / 4, samus.height / 2);
            while(!_gameOver)
            {
                Raylib.BeginMode2D(_camera);
                Update();
                Raylib.ClearBackground(Color.WHITE);
                Raylib.BeginDrawing();
                Draw();
                Raylib.EndDrawing();
                while (Console.KeyAvailable)
                    Console.ReadKey(true);
                Thread.Sleep(150);
                Raylib.EndMode2D();
            }

            End();
        }
    }
}
