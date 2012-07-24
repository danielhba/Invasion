using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Input.Touch;

namespace GameFramework
{
    public class GameHost : Microsoft.Xna.Framework.Game
    {
        GameObjectBase[] _objectArray;
        int _score;
        TextObject _placar;

        public Dictionary<string, Texture2D> Textures { get; set; }
        public Dictionary<string, SpriteFont> Fonts { get; set; }
        public List<GameObjectBase> GameObjects { get; set; }
        public Dictionary<string, SoundEffect> SoundEffects { get; set; }

        public TextObject Placar { get; set; }
        public int Score
        {
            get
            {
                return _score;
            }
            set
            {
                _score = value;
                if (Placar != null)
                    Placar.Text = "Placar: " + _score.ToString();
            }
        }

        public GameHost()
        {
            Textures = new Dictionary<string, Texture2D>();
            Fonts = new Dictionary<string, SpriteFont>();
            GameObjects = new List<GameObjectBase>();
            SoundEffects = new Dictionary<string, SoundEffect>();

            Score = 0;
        }

        public void UpdateAll(GameTime gameTime)
        {
            int i;
            int objectCount;

            if (_objectArray == null)
                _objectArray = new GameObjectBase[(int)MathHelper.Max(20, GameObjects.Count * 1.2f)];
            else if (GameObjects.Count > _objectArray.Length)
            {
                _objectArray = new GameObjectBase[(int)(GameObjects.Count * 1.2f)];
            }

            objectCount = GameObjects.Count;

            // Transfer the object references into the array
            for (i = 0; i < _objectArray.Length; i++)
            {
                if (i < objectCount)
                    _objectArray[i] = GameObjects[i];
                else
                    _objectArray[i] = null;
            }

            // Loop for each element within the array
            for (i = 0; i < objectCount; i++)
            {
                // Update the object at this array position
                _objectArray[i].Update(gameTime);
            }
        }

        public void DrawSprites(GameTime gameTime, SpriteBatch spriteBatch, Texture2D restrictToTexture)
        {
            GameObjectBase obj;

            int objectCount = _objectArray.Length;

            for (int i = 0; i < objectCount; i++)
            {
                obj = _objectArray[i];

                if (obj is SpriteObject && !(obj is TextObject))
                {
                    if (restrictToTexture == null || ((SpriteObject)obj).SpriteTexture == restrictToTexture)
                    {
                        System.Diagnostics.Debug.WriteLine((obj as SpriteObject).Position.ToString());

                        ((SpriteObject)obj).Draw(gameTime, spriteBatch);
                    }
                }
            }
        }

        public void DrawSprites(GameTime gameTime, SpriteBatch spriteBatch)
        {
            DrawSprites(gameTime, spriteBatch, null);
        }

        public void DrawText(GameTime gameTime, SpriteBatch spriteBatch)
        {
            GameObjectBase obj;
            int objectCount = _objectArray.Length;

            for (int i = 0; i < objectCount; i++)
            {
                obj = _objectArray[i];

                if (obj is TextObject)
                {
                    (obj as TextObject).Draw(gameTime, spriteBatch);
                }
            }
        }


    }
}
