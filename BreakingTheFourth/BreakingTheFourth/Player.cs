﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//updated namespaces
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace BreakingTheFourth
{
    //Contributors:
    //Kat Weis - basically everything so far
    // Matt Lienhard - helped with Offset method

    
    class Player
    {
        //Here is the code for the player
        //Will need to store a texture and a rectangle
        private Texture2D playerTexture;
        private Rectangle position;
        //also needs field to detect falling
        private bool isFalling;
        private bool isJumping; //variable for determining if player jumped recently
        private int startingY; //variable for Y before jumping
        int screenCounter = 1;
        Level1 level1 = new Level1();
        
        public enum PlayerState
        {
            faceLeft,
            faceRight,
            walkLeft,
            walkRight 
        }
        private PlayerState pState;

        // FileIO object
        FileIO movement = new FileIO();
        //Since we don't have collectibles, we probably won't need a GameObject class
        //Make a constructor that takes 4 parameters, the x, the y, the width and the height.
        public Player(int x, int y, int width, int height)
        {
            position = new Rectangle(x, y, width, height);
            startingY = y;
            isJumping = false;
            isFalling = false;
            pState = PlayerState.faceRight;
        }
        //make properties for the texture and the position & X , Y coords
        public Texture2D PlayerTexture
        {
            get { return playerTexture; }
            set { playerTexture = value; }
        }
        public Rectangle Position
        {
            get { return position; }
        }
        public int X
        {
            get { return position.X; }
            set { position.X = value; }
        }
        public int Y
        {
            get { return position.Y; }
            set { position.Y = value; }
        }
        //properties for isFalling
        public bool IsFalling
        {
            get { return isFalling; }
            set { isFalling = value; }
        }
        public PlayerState PState
        {
            get { return pState; }
        }
        //property for isJumping
        public bool IsJumping
        {
            get { return isJumping; }
            set { isJumping = value; }
        }
        //property for startingY
        public int StartingY
        {
            get { return startingY; }
            set { startingY = value; }
        }
        public void Update(KeyboardState kbState, KeyboardState previousKbState, List<Terrain> terrain)
        {
            // determining movement and player orientation
            switch (pState)
            {
                case PlayerState.faceRight:
                    if(kbState.IsKeyDown(Keys.D))
                    {
                        pState = PlayerState.walkRight;                 
                    }
                    if(kbState.IsKeyDown(Keys.A))
                    {
                        pState = PlayerState.faceLeft;
                    }
                    break;

                case PlayerState.faceLeft:
                    if(kbState.IsKeyDown(Keys.A))
                    {
                        pState = PlayerState.walkLeft;                       
                    }
                    if(kbState.IsKeyDown(Keys.D))
                    {
                        pState = PlayerState.faceRight;
                    }
                    break;

                case PlayerState.walkRight:
                    if(kbState.IsKeyDown(Keys.D))
                    {
                        X += movement.PlayerSpeed;
                    }
                    if (kbState.IsKeyUp(Keys.D))
                    {
                        pState = PlayerState.faceRight;                       
                    }
                    if(kbState.IsKeyDown(Keys.A))
                    {
                        pState = PlayerState.faceLeft;
                    }
                    break;

                case PlayerState.walkLeft:
                    if(kbState.IsKeyDown(Keys.A))
                    {
                        X -= movement.PlayerSpeed;
                    }
                    if (kbState.IsKeyUp(Keys.A))
                    {
                        pState = PlayerState.faceLeft;                        
                    }
                    if(kbState.IsKeyDown(Keys.D))
                    {
                        pState = PlayerState.faceRight;
                    }
                    break;

            }
            bool collided = false;
            //collision detection
            for (int i = 0; i < terrain.Count; i++)
            {
                if (terrain[i].CollisionDetected(position) == true )/////special terrain is causing issue still when going down
                {
                    //stops no clip issues
                    Offset(terrain, kbState, i);
                    //halts jumping after colliding
                    isJumping = false;
                    collided = true;
                }
                //checks if player is standing on terrain & counts that as colliding
                if(!(position.Left > terrain[i].Position.Right) && !(position.Right < terrain[i].Position.Left))
                {
                    if (position.Bottom == terrain[i].Position.Top)
                    {
                        collided = true;
                    }
                }
                
            }
            if(isJumping ==false && collided ==false)
            {
                isFalling = true;
            }
            if (isFalling == true)
            {
                //go down-gravity
                Gravity();
                isJumping = false; //stops player from jumping while falling to slow descend
            }
            if (isJumping == true)
            {
                position.Y -= 4;
                if (position.Y <= (startingY - (.5 * position.Height)))
                {
                    isFalling = true;
                }
            }
            //jump start
            if (kbState.IsKeyDown(Keys.Space) && previousKbState.IsKeyUp(Keys.Space))
            {
                //jump logic
                //go up
                position.Y -= 4;
                isJumping = true;
            }

            
        }
        public void Gravity()
        {
            position.Y += movement.Gravity;
        }
        
        // draw the character when they're facing left
        public void DrawPlayerStanding(SpriteEffects effect, SpriteBatch sb)
        {
            sb.Draw(playerTexture, position, null, Color.White, 0, Vector2.Zero, effect, 0);
        }

        /// <summary>
        /// draws the player to the screen
        /// </summary>
        /// <param name="spriteBatch"></param>
        public void Draw(SpriteBatch spriteBatch)
        {
            //spriteBatch.Draw(playerTexture, position, Color.White);
            
            // draw the character based on their orientation
            switch (pState)
            {
                case PlayerState.faceRight:
                    DrawPlayerStanding(SpriteEffects.None, spriteBatch);
                    break;

                case PlayerState.faceLeft:
                    DrawPlayerStanding(SpriteEffects.FlipHorizontally, spriteBatch);
                    break;

                case PlayerState.walkRight:
                    // will be a DrawWalking merhod here when we have animation
                    DrawPlayerStanding(SpriteEffects.None, spriteBatch);
                    break;

                case PlayerState.walkLeft:
                    // will be a DrawWalking merhod here when we have animation
                    DrawPlayerStanding(SpriteEffects.FlipHorizontally, spriteBatch);
                    break;
            }
        }
        //We'll probably need math calculations for velocity and stuff
        //Need method for checking collisions with walls and other objects - in terrain
        //Need to decide whether to make player move around level or level move around player

        public void Offset(List<Terrain> terrain, KeyboardState kbState, int i)
        {
            if (position.Bottom > terrain[i].Position.Top + movement.Gravity && isJumping == false)
            {
                if (position.Right > terrain[i].Position.Left && kbState.IsKeyDown(Keys.D))
                {
                    position.X = terrain[i].Position.Left - position.Width;
                    //position.X -= movement.PlayerSpeed;
                }
                if (position.Left < terrain[i].Position.Right && kbState.IsKeyDown(Keys.A))
                {
                    position.X = terrain[i].Position.Right;
                    //position.X += movement.PlayerSpeed;
                }
            }
            if(position.Bottom <= terrain[i].Position.Top + movement.Gravity && position.Bottom > terrain[i].Position.Top)//sets player on top of terrain if fell
            {
                position.Y -= position.Bottom - terrain[i].Position.Top;
                isFalling = false;
                startingY = position.Y;
            }
            
            if(startingY > terrain[i].Position.Bottom && IsJumping == true) // starts below the object & jumps
            {
                if (position.Top < terrain[i].Position.Bottom && IsJumping == true && position.Top > terrain[i].Position.Top)
                {
                    position.Y += terrain[i].Position.Bottom - position.Top;
                }
            }
            else if(startingY - position.Height < terrain[i].Position.Bottom && IsJumping == true) // jumps and hits an object from the side
            {
                if (position.Right > terrain[i].Position.Left && kbState.IsKeyDown(Keys.D))
                {
                    position.X = terrain[i].Position.Left - position.Width;
                    //position.X -= movement.PlayerSpeed;
                }
                if (position.Left < terrain[i].Position.Right && kbState.IsKeyDown(Keys.A))
                {
                    position.X = terrain[i].Position.Right;
                    //position.X += movement.PlayerSpeed;
                }
            }


            
        }//end of offset method
    }
}
