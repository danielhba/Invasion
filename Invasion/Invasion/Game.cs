using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Xna.Framework.Media;
using GameFramework;

namespace Invasion
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game : GameHost
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Ship ship;

        int type_shoot = 1;
        int contador_shoot = 0;

        int velocidade_nivel = 1;
        int inimigos_total = 1;
        int inimigos_fazer = 1;
        int inimigos_restantes = 1;
        int contar_fazer = 0;
        int contar_vida = 0;

        public Game()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            // Frame rate is 30 fps by default for Windows Phone.
            TargetElapsedTime = TimeSpan.FromTicks(333333);

            // Extend battery life under lock.
            InactiveSleepTime = TimeSpan.FromSeconds(1);

            graphics.IsFullScreen = true;
            graphics.PreferredBackBufferWidth = 480;
            graphics.PreferredBackBufferHeight = 800;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here

            // Carregando as imagens do jogo
            {
                //Imagem de background
                Textures.Add("back", Content.Load<Texture2D>("back"));

                //Imagens do ufo1
                Textures.Add("ufo1", Content.Load<Texture2D>("Sprites/ufo1"));
                Textures.Add("exp1", Content.Load<Texture2D>("Sprites/exp1"));

                //Imagens do ufo2
                Textures.Add("ufo2", Content.Load<Texture2D>("Sprites/ufo2"));
                Textures.Add("exp2", Content.Load<Texture2D>("Sprites/exp2"));

                //Imagens do ufo3
                Textures.Add("ufo3", Content.Load<Texture2D>("Sprites/ufo3"));
                Textures.Add("exp3", Content.Load<Texture2D>("Sprites/exp3"));

                //Imagens da nave (ship)
                Textures.Add("ship", Content.Load<Texture2D>("Sprites/ship"));
                Textures.Add("shipexp", Content.Load<Texture2D>("Sprites/shipexp"));

                //Imagens do tiro
                Textures.Add("shoot1", Content.Load<Texture2D>("Sprites/shoot1"));
                Textures.Add("shoot2", Content.Load<Texture2D>("Sprites/shoot2"));
                Fonts.Add("vida", Content.Load<SpriteFont>("vida"));
            }

            //Carregando os sons do jogo
            {
                SoundEffects.Add("blaster", Content.Load<SoundEffect>("Sounds/blaster"));
                SoundEffects.Add("blub", Content.Load<SoundEffect>("Sounds/blub"));
                SoundEffects.Add("explosion", Content.Load<SoundEffect>("Sounds/explosion"));
                SoundEffects.Add("gameover", Content.Load<SoundEffect>("Sounds/gameover"));
                SoundEffects.Add("getextra", Content.Load<SoundEffect>("Sounds/getextra"));
                SoundEffects.Add("ship", Content.Load<SoundEffect>("Sounds/ship"));
                SoundEffects.Add("skid", Content.Load<SoundEffect>("Sounds/skid"));
                SoundEffects.Add("tap", Content.Load<SoundEffect>("Sounds/tap"));
                SoundEffects.Add("ufoshoot", Content.Load<SoundEffect>("Sounds/ufoshoot"));
                SoundEffects.Add("weaponchange", Content.Load<SoundEffect>("Sounds/weaponchange"));
            }

            Background back1 = new Background(this, new Vector2(-000, -000), Textures["back"]);
            Background back2 = new Background(this, new Vector2(-000, -800), Textures["back"]);
            GameObjects.Add(back1);
            GameObjects.Add(back2);

            ship = new Ship(this, new Vector2((Window.ClientBounds.Width - Textures["ship"].Width / 5) / 2,
                                                    (Window.ClientBounds.Height - Textures["ship"].Height / 5)),
                                 Textures["ship"], SoundEffects["ship"]);
            GameObjects.Add(ship);

            Ufo ufo = new Ufo(this, new Vector2(GameHelper.RandomNext(Window.ClientBounds.Width - 70), -70),
                                    Textures["ufo1"], velocidade_nivel, 1);
            GameObjects.Add(ufo);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            // TODO: Add your update logic here
            UpdateAll(gameTime);
            if (ship.Vida > 0)
            {             
                TouchCollection tc = TouchPanel.GetState();
                foreach (TouchLocation tl in tc)
                {
                    if (tl.State == TouchLocationState.Pressed)
                    {
                        if (contador_shoot == 100)
                        {
                            contador_shoot = 0;
                            type_shoot++;
                            if (type_shoot == 7)
                            {
                                type_shoot = 1;
                            }
                        }
                        if (type_shoot == 1)
                        {
                            Shoot1 tiro = new Shoot1(this, new Vector2(ship.PositionX + Textures["shoot1"].Width + 7, ship.PositionY),
                                                        Textures["shoot1"], 0);
                            GameObjects.Add(tiro);
                            SoundEffects["blaster"].Play();
                        }
                        if (type_shoot == 2)
                        {
                            Shoot1 tiro1 = new Shoot1(this, new Vector2(ship.PositionX + Textures["shoot1"].Width + 4, ship.PositionY),
                                                        Textures["shoot1"], -1);
                            Shoot1 tiro2 = new Shoot1(this, new Vector2(ship.PositionX + Textures["shoot1"].Width + 10, ship.PositionY),
                                                        Textures["shoot1"], +1);
                            GameObjects.Add(tiro1);
                            GameObjects.Add(tiro2);
                            SoundEffects["blaster"].Play();
                            SoundEffects["blaster"].Play();
                        }
                        if (type_shoot == 3)
                        {
                            Shoot1 tiro1 = new Shoot1(this, new Vector2(ship.PositionX + Textures["shoot1"].Width + 4, ship.PositionY),
                                                        Textures["shoot1"], -1);
                            Shoot1 tiro2 = new Shoot1(this, new Vector2(ship.PositionX + Textures["shoot1"].Width + 7, ship.PositionY),
                                                        Textures["shoot1"], -0);
                            Shoot1 tiro3 = new Shoot1(this, new Vector2(ship.PositionX + Textures["shoot1"].Width + 10, ship.PositionY),
                                                        Textures["shoot1"], +1);
                            GameObjects.Add(tiro1);
                            GameObjects.Add(tiro2);
                            GameObjects.Add(tiro3);
                            SoundEffects["blaster"].Play();
                            SoundEffects["blaster"].Play();
                            SoundEffects["blaster"].Play();
                        }
                        if (type_shoot == 4)
                        {
                            Shoot2 tiro = new Shoot2(this, new Vector2(ship.PositionX + Textures["shoot2"].Width / 11 + 7, ship.PositionY),
                                                        Textures["shoot2"], 0);
                            GameObjects.Add(tiro);
                            SoundEffects["ufoshoot"].Play();
                        }
                        if (type_shoot == 5)
                        {
                            Shoot2 tiro1 = new Shoot2(this, new Vector2(ship.PositionX + Textures["shoot2"].Width / 11 + 4, ship.PositionY),
                                                        Textures["shoot2"], -1);
                            Shoot2 tiro2 = new Shoot2(this, new Vector2(ship.PositionX + Textures["shoot2"].Width / 11 + 10, ship.PositionY),
                                                        Textures["shoot2"], +1);
                            GameObjects.Add(tiro1);
                            GameObjects.Add(tiro2);
                            SoundEffects["ufoshoot"].Play();
                            SoundEffects["ufoshoot"].Play();
                        }
                        if (type_shoot == 6)
                        {
                            Shoot2 tiro1 = new Shoot2(this, new Vector2(ship.PositionX + Textures["shoot2"].Width / 11 + 4, ship.PositionY),
                                                        Textures["shoot2"], -1);
                            Shoot2 tiro2 = new Shoot2(this, new Vector2(ship.PositionX + Textures["shoot2"].Width / 11 + 7, ship.PositionY),
                                                        Textures["shoot2"], -0);
                            Shoot2 tiro3 = new Shoot2(this, new Vector2(ship.PositionX + Textures["shoot2"].Width / 11 + 10, ship.PositionY),
                                                        Textures["shoot2"], +1);
                            GameObjects.Add(tiro1);
                            GameObjects.Add(tiro2);
                            GameObjects.Add(tiro3);
                            SoundEffects["ufoshoot"].Play();
                            SoundEffects["ufoshoot"].Play();
                            SoundEffects["ufoshoot"].Play();
                        }
                    }
                }

                for (int i = 0; i < GameObjects.Count; i++)
                {
                    Object gob1 = GameObjects.ElementAt(i);
                    if (gob1 is Exp)
                    {
                        Exp exp = (Exp)(gob1);
                        if (exp.apagar)
                        {
                            GameObjects.Remove(exp);
                        }
                    }
                    if (gob1 is Ufo)
                    {
                        Ufo ufo = (Ufo)(gob1);
                        if (ufo.PositionY > Window.ClientBounds.Height)
                        {
                            ufo.PositionX = GameHelper.RandomNext(Window.ClientBounds.Width - 70);
                            ufo.PositionY = -70;
                        }
                        else
                        {
                            for (int j = 0; j < GameObjects.Count; j++)
                            {
                                Object gob2 = GameObjects.ElementAt(j);
                                if (gob2 is Shoot1)
                                {
                                    Shoot1 tiro = (Shoot1)(gob2);
                                    Rectangle tiroRect = new Rectangle((int)tiro.PositionX, (int)tiro.PositionY,
                                                                        Textures["shoot1"].Width, Textures["shoot1"].Height);
                                    Rectangle ufoRect = new Rectangle((int)ufo.PositionX, (int)ufo.PositionY, 70, 70);
                                    if (tiroRect.Intersects(ufoRect))
                                    {
                                        if (ufo.Tipo == 1)
                                        {
                                            Exp exp = new Exp(this, new Vector2(ufo.PositionX, ufo.PositionY), Textures["exp1"]);
                                            GameObjects.Add(exp);
                                        }
                                        if (ufo.Tipo == 2)
                                        {
                                            Exp exp = new Exp(this, new Vector2(ufo.PositionX, ufo.PositionY), Textures["exp2"]);
                                            GameObjects.Add(exp);
                                        }
                                        if (ufo.Tipo == 3)
                                        {
                                            Exp exp = new Exp(this, new Vector2(ufo.PositionX, ufo.PositionY), Textures["exp3"]);
                                            GameObjects.Add(exp);
                                        }
                                        SoundEffects["explosion"].Play();
                                        GameObjects.Remove(ufo);
                                        GameObjects.Remove(tiro);
                                        inimigos_restantes--;
                                        contador_shoot++;
                                        if (ship.Vida < 100)
                                        {
                                            ship.Vida += 1;
                                        }
                                    }
                                }
                                if (gob2 is Shoot2)
                                {
                                    Shoot2 tiro = (Shoot2)(gob2);
                                    Rectangle tiroRect = new Rectangle((int)tiro.PositionX, (int)tiro.PositionY,
                                                                        Textures["shoot1"].Width, Textures["shoot1"].Height);
                                    Rectangle ufoRect = new Rectangle((int)ufo.PositionX, (int)ufo.PositionY, 70, 70);
                                    if (tiroRect.Intersects(ufoRect))
                                    {
                                        if (ufo.Tipo == 1)
                                        {
                                            Exp exp = new Exp(this, new Vector2(ufo.PositionX, ufo.PositionY), Textures["exp1"]);
                                            GameObjects.Add(exp);
                                        }
                                        if (ufo.Tipo == 2)
                                        {
                                            Exp exp = new Exp(this, new Vector2(ufo.PositionX, ufo.PositionY), Textures["exp2"]);
                                            GameObjects.Add(exp);
                                        }
                                        if (ufo.Tipo == 3)
                                        {
                                            Exp exp = new Exp(this, new Vector2(ufo.PositionX, ufo.PositionY), Textures["exp3"]);
                                            GameObjects.Add(exp);
                                        }
                                        SoundEffects["explosion"].Play();
                                        GameObjects.Remove(ufo);
                                        GameObjects.Remove(tiro);
                                        inimigos_restantes--;
                                        contador_shoot++;
                                        if (ship.Vida < 100)
                                        {
                                            ship.Vida += 1;
                                        }
                                    }
                                }
                                if (gob2 is Ship)
                                {
                                    Rectangle shipRect = new Rectangle((int)ship.PositionX, (int)ship.PositionY, 70, 70);
                                    Rectangle ufoRect = new Rectangle((int)ufo.PositionX, (int)ufo.PositionY, 70, 70);
                                    if (shipRect.Intersects(ufoRect))
                                    {
                                        if (ufo.Tipo == 1)
                                        {
                                            Exp exp = new Exp(this, new Vector2(ufo.PositionX, ufo.PositionY), Textures["exp1"]);
                                            GameObjects.Add(exp);
                                        }
                                        if (ufo.Tipo == 2)
                                        {
                                            Exp exp = new Exp(this, new Vector2(ufo.PositionX, ufo.PositionY), Textures["exp2"]);
                                            GameObjects.Add(exp);
                                        }
                                        if (ufo.Tipo == 3)
                                        {
                                            Exp exp = new Exp(this, new Vector2(ufo.PositionX, ufo.PositionY), Textures["exp3"]);
                                            GameObjects.Add(exp);
                                        }
                                        SoundEffects["explosion"].Play();
                                        GameObjects.Remove(ufo);
                                        ship.Vida -= 5;
                                        inimigos_restantes--;
                                        contador_shoot++;
                                    }
                                }
                            }
                        }
                    }
                    if (gob1 is Shoot1)
                    {
                        Shoot1 tiro = (Shoot1)(gob1);
                        if (tiro.PositionY < 0 || tiro.PositionX < 0 || tiro.PositionX > Window.ClientBounds.Width)
                        {
                            GameObjects.Remove(tiro);
                        }
                    }
                    if (gob1 is Shoot2)
                    {
                        Shoot2 tiro = (Shoot2)(gob1);
                        if (tiro.PositionY < 0 || tiro.PositionX < 0 || tiro.PositionX > Window.ClientBounds.Width)
                        {
                            GameObjects.Remove(tiro);
                        }
                    }
                }

                if (inimigos_restantes == 0)
                {
                    inimigos_total++;
                    velocidade_nivel++;
                    inimigos_restantes = inimigos_total;
                    inimigos_fazer = 0;
                }
                contar_fazer++;
                if (contar_fazer == 10)
                {
                    contar_fazer = 0;
                    if (inimigos_fazer < inimigos_total)
                    {
                        int tipo = GameHelper.RandomNext(3);
                        if (tipo == 0)
                        {
                            Ufo ufo = new Ufo(this, new Vector2(GameHelper.RandomNext(Window.ClientBounds.Width - 70), -70),
                                Textures["ufo1"], velocidade_nivel, 1);
                            GameObjects.Add(ufo);
                        }
                        if (tipo == 1)
                        {
                            Ufo ufo = new Ufo(this, new Vector2(GameHelper.RandomNext(Window.ClientBounds.Width - 70), -70),
                                Textures["ufo2"], velocidade_nivel, 2);
                            GameObjects.Add(ufo);
                        }
                        if (tipo == 2)
                        {
                            Ufo ufo = new Ufo(this, new Vector2(GameHelper.RandomNext(Window.ClientBounds.Width - 70), -70),
                                Textures["ufo3"], velocidade_nivel, 3);
                            GameObjects.Add(ufo);
                        }
                        inimigos_fazer++;
                    }
                }
                contar_vida++;
                if (contar_vida == 100)
                {
                    contar_vida = 0;
                    ship.Vida -= 5;
                }
                if (velocidade_nivel > 150)
                    velocidade_nivel = 1;
                base.Update(gameTime);
            }
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            // TODO: Add your drawing code here

            spriteBatch.Begin();
            if (ship.Vida > 0)
            {
                DrawSprites(gameTime, spriteBatch);
                spriteBatch.DrawString(Fonts["vida"], "Vida :" + ship.Vida.ToString() + "%", Vector2.Zero, Color.White);
            }
            else
            {
                spriteBatch.DrawString(Fonts["vida"], "Game Over", 
                                        new Vector2(Window.ClientBounds.Width/2 - 50, Window.ClientBounds.Height/2), 
                                        Color.White);

            }
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}