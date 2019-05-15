using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
    class Snake : Sprite
    {
        enum Direction
        {
            Up, Right, Down, Left
        }
        List<Point> snakePoints;
        Direction direction = Direction.Left;


        float timer = 0;
        float timerSpeed = 0.05f;
        public Snake()
        {
            snakePoints = new List<Point>();
            for (int i = 0; i < 3; i++)
            {
                snakePoints.Add(new Point(VALUES.TILE_W / 2 + i, VALUES.TILE_H / 2));
            }
        }

        public override void Update()
        {
            var state = Keyboard.GetState();
            if (state.IsKeyDown(Keys.A) && direction != Direction.Right)
            {
                direction = Direction.Left;
            }
            else if (state.IsKeyDown(Keys.D) && direction != Direction.Left)
            {
                direction = Direction.Right;
            }
            else if (state.IsKeyDown(Keys.W) && direction != Direction.Down)
            {
                direction = Direction.Up;
            }
            else if (state.IsKeyDown(Keys.S) && direction != Direction.Up)
            {
                direction = Direction.Down;
            }

            if (timer >= 1)
            {
                var headPos = snakePoints.First();

                Point newPointPos = headPos;
                switch (direction)
                {
                    case Direction.Up:
                        newPointPos.Y--;
                        break;
                    case Direction.Right:
                        newPointPos.X++;
                        break;
                    case Direction.Down:
                        newPointPos.Y++;
                        break;
                    case Direction.Left:
                        newPointPos.X--;
                        break;
                    default:
                        break;
                }
                snakePoints.Insert(0, newPointPos);
                snakePoints.Remove(snakePoints.Last());
                timer = 0;

                for (int i = 0; i < snakePoints.Count; i++)
                {
                    if (snakePoints[i].X < 0)
                    {
                        snakePoints[i] = new Point(VALUES.TILE_W - 1, snakePoints[i].Y);
                    }
                    else if (snakePoints[i].Y < 0)
                    {
                        snakePoints[i] = new Point(snakePoints[i].X, VALUES.TILE_H-1);
                    }
                    else if (snakePoints[i].X >= VALUES.TILE_W)
                    {
                        snakePoints[i] = new Point(0, snakePoints[i].Y);
                    }
                    else if (snakePoints[i].Y >= VALUES.TILE_H)
                    {
                        snakePoints[i] = new Point(snakePoints[i].X, 0);
                    }
                }

                foreach (var item in snakePoints.Skip(1))
                {
                    if (newPointPos == item)
                    {
                        timerSpeed = 0;
                    }
                }
                
            }

            timer += timerSpeed;

            base.Update();
        }

        public void Grow()
        {

            var headPos = snakePoints.First();

            Point newPointPos = headPos;
            switch (direction)
            {
                case Direction.Up:
                    newPointPos.Y--;
                    break;
                case Direction.Right:
                    newPointPos.X++;
                    break;
                case Direction.Down:
                    newPointPos.Y++;
                    break;
                case Direction.Left:
                    newPointPos.X--;
                    break;
                default:
                    break;
            }
            snakePoints.Insert(0, newPointPos);
        }

        public override void Draw(SpriteBatch sb)
        {
            for (int i = 0; i < snakePoints.Count; i++)
            {
                Point? left = null;
                if (i != 0 )
                {
                    left = snakePoints[i - 1];
                }

                Point current = snakePoints[i];

                Point? right = null;
                if (i != snakePoints.Count-1)
                {
                    right = snakePoints[i + 1];
                }


                Direction dLeft = GetDirection(left, current);
                Direction dRight = GetDirection(right, current);

                int a = 0;
                if (left != null)
                {
                    a = 0b1 << (3-(int)dLeft);
                }

                int b = 0;
                if (right != null)
                {
                    b = 0b1 << (3 - (int)dRight);
                }

                a += b;


            }
            foreach (var item in snakePoints)
            {
                sb.Draw(Assets.snake,
                    sourceRectangle: new Rectangle(0, 0, 64, 64),
                    destinationRectangle: new Rectangle(item.X * VALUES.TILE_SIZE, item.Y * VALUES.TILE_SIZE, VALUES.TILE_SIZE, VALUES.TILE_SIZE));
            }
            base.Draw(sb);
        }

        public bool IsIntersects (Point point)
        {
            foreach (var item in snakePoints)
            {
                if (item==point)
                {
                    return true;
                }
            }

            return false;
        }

        private Direction GetDirection (Point? left, Point current)
        {
            Direction dLeft = Direction;
            if (left != null)
            {
                if (left.Value.X < current.X)
                {
                    dLeft = Direction.Left;
                }
                else if (left.Value.X > current.X)
                {
                    dLeft = Direction.Right;
                }
                else if (left.Value.Y > current.Y)
                {
                    dLeft = Direction.Down;
                }
                else if (left.Value.Y > current.Y)
                {
                    dLeft = Direction.Down;
                }
            }
            return dLeft;
        }
    }
}
