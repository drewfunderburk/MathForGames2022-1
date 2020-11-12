using System;
using System.Collections.Generic;
using System.Text;
using Raylib_cs;
using MathLibrary;

namespace MathForGames3D
{
    class Game
    {

        private Camera3D _camera = new Camera3D();
        private Scene scene = new Scene();

        public static bool GameOver { get; set; } = false;

        #region CORE
        private void Start()
        {
            // Init window
            Raylib.InitWindow(1024, 720, "MathForGames3D");
            Raylib.SetTargetFPS(60);

            // Init camera
            _camera.position = new System.Numerics.Vector3(0.0f, 20.0f, 20.0f); ;
            _camera.target = new System.Numerics.Vector3(0.0f, 0.0f, 0.0f);
            _camera.up = new System.Numerics.Vector3(0.0f, 1.0f, 0.0f);
            _camera.fovy = 45.5f;
            _camera.type = CameraType.CAMERA_PERSPECTIVE;

            // Create Actors
            Actor actor1 = new Actor();
            Actor actor2 = new Actor(0, 0, 5);
            Actor actor3 = new Actor(0, 0, 3);

            scene.AddActor(actor1);
            scene.AddActor(actor2);
            scene.AddActor(actor3);

            actor1.AddChild(actor2);
            actor2.AddChild(actor3);
        }

        private void Update(float deltaTime)
        {
            if (!scene.Started)
                scene.Start();
            scene.Update(deltaTime);
        }

        private void Draw()
        {
            Raylib.BeginDrawing();
            Raylib.BeginMode3D(_camera);

            Raylib.ClearBackground(Color.RAYWHITE);

            Raylib.DrawGrid(20, 1.0f);
            scene.Draw();

            Raylib.EndMode3D();
            Raylib.EndDrawing();
        }

        private void End()
        {
            scene.End();
        }

        public void Run()
        {
            Start();

            while(!GameOver)
            {
                float deltaTime = Raylib.GetFrameTime();
                Update(deltaTime);
                Draw();
            }

            End();
        }
        #endregion
    }
}
