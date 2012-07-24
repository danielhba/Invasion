using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameFramework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Devices.Sensors;
using Microsoft.Xna.Framework;

namespace Invasion
{
    public class Ship : SpriteObject
    {
        Game _game;
        SoundEffect ShipSound;
        int SoundContador = 0;

        Rectangle ShipRect;
        int SpriteX;
        int SpriteY;

        int PosicaoX;
        int PosicaoY;

        Accelerometer accelSensor;
        Vector3 accelReading;
        bool accelActive;

        public Ship(Game game, Vector2 PosicaoInicial, Texture2D texture, SoundEffect SoundShip) : base(game, PosicaoInicial, texture)
        {
            _game = game;
            ShipSound = SoundShip;
            ShipSound.Play();

            SpriteX = 0;
            SpriteY = 0;
            PosicaoX = (int)PosicaoInicial.X;
            PosicaoY = (int)PosicaoInicial.Y;
            
            accelReading = new Vector3();
            accelSensor = new Accelerometer();
            accelSensor.ReadingChanged +=
                new EventHandler<AccelerometerReadingEventArgs>(AccelerometerReadingChanged);
            try
            {
                accelSensor.Start();
                accelActive = true;
            }
            catch (AccelerometerFailedException e)
            {
                accelActive = false;

            }
            catch (UnauthorizedAccessException e)
            {
                accelActive = false;
            }
        }

        public override void Draw(Microsoft.Xna.Framework.GameTime gameTime, Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(SpriteTexture, Position, ShipRect, Color.White);
        }

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            SoundContador++;
            if (SoundContador > 68)
            {
                ShipSound.Play();
                SoundContador = 0;
            }
            
            SpriteX++;
            if (SpriteX > 4)
            {
                SpriteX = 0;
                SpriteY++;
                if (SpriteY > 4)
                {
                    SpriteY = 0;
                }
            }

            if (accelActive)
            {

                PositionX += (accelReading.X * 15);
                PositionY -= (accelReading.Y * 15);

                if (PositionX < -SpriteTexture.Width / 10)
                    PositionX = -SpriteTexture.Width / 10;
                if (PositionX > _game.Window.ClientBounds.Width - SpriteTexture.Width / 10)
                    PositionX = _game.Window.ClientBounds.Width - SpriteTexture.Width / 10;
                
                if (PositionY < -SpriteTexture.Height / 10)
                    PositionY = -SpriteTexture.Height / 10;
                if (PositionY > _game.Window.ClientBounds.Height - SpriteTexture.Height / 10)
                    PositionY = _game.Window.ClientBounds.Height - SpriteTexture.Height / 10;             
            }

            ShipRect = new Rectangle(SpriteX * 70, SpriteY * 70, 70, 70);
            base.Update(gameTime);
        }

        public void AccelerometerReadingChanged(object sender, AccelerometerReadingEventArgs e)
        {
            accelReading.X = (float)e.X;
            accelReading.Y = (float)e.Y;
            accelReading.Z = (float)e.Z;
        }
    }
}