using System;
using System.Security.Cryptography.X509Certificates;
using Codecool.Quest.Models;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Codecool.Quest
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class CodecoolQuestGame : Game
    {
        public static CodecoolQuestGame GameSingleton;

        public SpriteBatch SpriteBatch;

        private GameMap _map;
        private TimeSpan _lastMoveTime;

        public const double MoveInterval = 0.1;

        public Cell Cells;

        public CodecoolQuestGame()
        {
            GameSingleton = this;

            using var graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            Window.AllowUserResizing = true;
            IsMouseVisible = true;

            graphics.PreferredBackBufferWidth = 1280;
            graphics.PreferredBackBufferHeight = 720;
            graphics.ApplyChanges();

            _lastMoveTime = TimeSpan.Zero;
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            SpriteBatch = new SpriteBatch(GraphicsDevice);

            GUI.Load();
            Tiles.Load();

            _map = MapLoader.LoadMap();
        }


        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            var keyboardState = Keyboard.GetState();


            if (keyboardState.IsKeyDown(Keys.Escape))
            {
                // Exit the game
                Exit();
                return;
            }

            var deltaTime = gameTime.TotalGameTime - _lastMoveTime;

            if (deltaTime.TotalSeconds < MoveInterval)
                return;

            if (keyboardState.IsKeyDown(Keys.Left))
            {
                // Move left
                var neighborCell = _map.Player.Cell.GetNeighbor(-1, 0);
                if (neighborCell.CellTakenByActor() && neighborCell.CellTakenByItem())
                {
                    _map.Player.MovePlayer(MoveDirection.Left);
                }
                else
                {
                    var cellFreed = _map.Player.ActorFight(neighborCell);
                    var cellCollected = _map.Player.ActorCollectItem(neighborCell);
                    if (cellFreed)
                    {
                        _map.Player.MovePlayer(MoveDirection.Left);
                    }
                    else if (cellCollected)
                    {
                        _map.Player.MovePlayer(MoveDirection.Left);
                    }
                }
                _lastMoveTime = gameTime.TotalGameTime;
            }
            else if (keyboardState.IsKeyDown(Keys.Right))
            {
                // Move right
                var neighborCell = _map.Player.Cell.GetNeighbor(1, 0);
                if (neighborCell.CellTakenByActor() && neighborCell.CellTakenByItem())
                {
                    _map.Player.MovePlayer(MoveDirection.Right);
                }
                else
                {
                    var cellFreed = _map.Player.ActorFight(neighborCell);
                    var cellCollected = _map.Player.ActorCollectItem(neighborCell);
                    if (cellFreed)
                    {
                        _map.Player.MovePlayer(MoveDirection.Right);
                    }
                    else if (cellCollected)
                    {
                        _map.Player.MovePlayer(MoveDirection.Right);
                    }
                }
                _lastMoveTime = gameTime.TotalGameTime;
            }
            else if (keyboardState.IsKeyDown(Keys.Up))
            {
                // Move up
                var neighborCell = _map.Player.Cell.GetNeighbor(0, -1);
                if (neighborCell.CellTakenByActor() && neighborCell.CellTakenByItem())
                {
                    _map.Player.MovePlayer(MoveDirection.Up);
                }
                else
                {
                    var cellFreed = _map.Player.ActorFight(neighborCell);
                    var cellCollected = _map.Player.ActorCollectItem(neighborCell);
                    if (cellFreed)
                    {
                        _map.Player.MovePlayer(MoveDirection.Up);
                    }
                    else if (cellCollected)
                    {
                        _map.Player.MovePlayer(MoveDirection.Up);
                    }
                }
                _lastMoveTime = gameTime.TotalGameTime;
            }
            else if (keyboardState.IsKeyDown(Keys.Down))
            {
                // Move down
                var neighborCell = _map.Player.Cell.GetNeighbor(0, 1);
                if (neighborCell.CellTakenByActor() && neighborCell.CellTakenByItem())
                {
                    _map.Player.MovePlayer(MoveDirection.Down);
                }
                else
                {
                    var cellFreed = _map.Player.ActorFight(neighborCell);
                    var cellCollected = _map.Player.ActorCollectItem(neighborCell);
                    if (cellFreed)
                    {
                        _map.Player.MovePlayer(MoveDirection.Down);
                    }
                    else if (cellCollected)
                    {
                        _map.Player.MovePlayer(MoveDirection.Down);
                    }
                }
                _lastMoveTime = gameTime.TotalGameTime;
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            SpriteBatch.Begin(SpriteSortMode.Deferred, BlendState.Opaque, SamplerState.PointClamp);

            for (var x = 0; x < _map.Width; x++)
            {
                for (var y = 0; y < _map.Height; y++)
                {
                    var cell = _map.GetCell(x, y);

                    //tutaj dziad rysuje 
                    if (cell.Actor != null)
                    {
                        Tiles.DrawTile(SpriteBatch, cell.Actor, x, y);
                    }
                    else if (cell.Items != null)
                    {
                        Tiles.DrawTile(SpriteBatch, cell.Items, x, y);
                    }
                    else
                    {
                        Tiles.DrawTile(SpriteBatch, cell, x, y);
                    }

                }
            }

            //tu button
            GUI.Text(new Vector2(890, 50 + 25), "Inventory".ToUpperInvariant(), Color.WhiteSmoke);
            //GUI.MousesButton(new Rectangle(870, 30, 60, 30), "Pick up");
            ShowCollectedItems(_map, 900, 125, 25);

            SpriteBatch.End();

            base.Draw(gameTime);
        }

        private static void ShowCollectedItems(GameMap map, int width, int height, int spaceBetweenWords)
        {
            var player = map.Player;

            if (player.HasSword)
            {
                GUI.Text(new Vector2(width, height), "sword".ToUpperInvariant(), Color.Gray);
            }

            if (player.HasKey)
            {
                GUI.Text(new Vector2(width, height + spaceBetweenWords), "Key".ToUpperInvariant(), Color.Yellow);
            }


        }
    }
}
