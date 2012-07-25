using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using GameFramework;

namespace Invasion
{
    public class Ufo : SpriteObject
    {
        Game _game;
        
        int VelocidadeInicial;
        int Velocidade;
        
        Rectangle UfoRect;
        int SpriteX;
        int SpriteY;

        public int Tipo;
        
        public Ufo(Game game, Vector2 PosicaoInicial, Texture2D texture, int velocidade, int tipo): base(game, PosicaoInicial, texture)
        {
            _game = game;
            VelocidadeInicial = velocidade;
            Velocidade = VelocidadeInicial;
            SpriteX = 0;
            SpriteY = 0;
            Tipo = tipo;
        }

        public override void Draw(Microsoft.Xna.Framework.GameTime gameTime, Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(SpriteTexture, Position, UfoRect, Color.White);
        }

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            
            PositionY += Velocidade;

            SpriteX++;
            if (SpriteX > 4)
            {
                SpriteX = 0;
                SpriteY++;
                if (SpriteY > 9)
                {
                    SpriteY = 0;
                }
            }

            UfoRect = new Rectangle(SpriteX * 70, SpriteY * 70, 70, 70);
            base.Update(gameTime);
        }
    }
}