using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Snake
{
    class Main : Sprite
    {
        List<Sprite> sprites;
        Point food;
        Random random;
        Snake snake;

        public Main()
        {
            sprites = new List<Sprite>();
            snake = new Snake();
            sprites.Add(snake);
            random = new Random();
            food = new Point(random.Next(VALUES.TILE_W), random.Next(VALUES.TILE_H));
        }

        public override void Update()
        {
            foreach (var item in sprites)
            {
                item.Update();
            }
            
            if (snake.IsIntersects(food))
            {
                food = new Point(random.Next(VALUES.TILE_W), random.Next(VALUES.TILE_H));
                snake.Grow();
            }
            base.Update();
        }

        public override void Draw(SpriteBatch sb)
        {
            foreach (var item in sprites)
            {
                item.Draw(sb);
            }
            sb.FillRectangle(food.X * VALUES.TILE_SIZE, food.Y * VALUES.TILE_SIZE, VALUES.TILE_SIZE, VALUES.TILE_SIZE, Color.Green, 0);
            base.Draw(sb);
        }
    }
}
