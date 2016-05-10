﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework;

namespace BreakingTheFourth
{
    class Level4
    {
        List<Terrain> pieces = new List<Terrain>();
        int playerY;
        Song bgMusic;
        Color bgColor = Color.Orange;
        //properties
        public Song BgMusic
        {
            get { return bgMusic; }
            set { bgMusic = value; }
        }
        public Color BgColor
        {
            get { return bgColor; }
        }
        public int PlayerY
        {
            get { return playerY; }
        }
        // Next Screen method
        public List<Terrain> NextScreen(int screen, Bullet bullet)
        {
            bullet.Bullets = 500;
            switch(screen)
            {
                case 1:
                    // clear the list
                    pieces.Clear();
                    // add new terrain pieces
                    pieces.Add(new Terrain(0, 0, 30, 500));//left wall
                    pieces.Add(new Terrain(0, 450, 300, 40));//left floor
                    pieces.Add(new Terrain(300, 200, 40, 300));//right wall connected to left floor
                    pieces.Add(new Terrain(200, 250, 100, 40));//right spiked floor
                    pieces.Add(new DeathObject(200, 230, 100, 20, "none"));//said spikes
                    pieces.Add(new Terrain(30, 250, 100, 40));//left spiked floor
                    pieces.Add(new DeathObject(30, 230, 100, 20,"none"));//said spikes
                    pieces.Add(new Terrain(0, 0, 300, 10));//left ceiling
                    pieces.Add(new SpecialTerrain(40, 130, 70, 30, 700, 40, Movement.Horizontal));//moving platform
                    pieces.Add(new Terrain(550, 0, 50, 250));//giant spiked wall of death in the middle, hard to miss
                    pieces.Add(new DeathObject(500, 0, 50, 250, "left"));//left facing spikes
                    pieces.Add(new DeathObject(600, 0, 50, 250, "right"));//right facing spikes
                    pieces.Add(new Terrain(530, 450, 100, 40));//platform under death wall of death
                    pieces.Add(new Terrain(710, 450, 150, 40));//right floor
                    playerY = 370;
                    break;
                case 2:
                    //Clear the list
                    pieces.Clear();
                    //add new terrain pieces
                    pieces.Add(new Terrain(0, 450, 150, 40));//left floor
                    pieces.Add(new SpecialTerrain(700, 450, 100, 30, 700, 200,Movement.Horizontal));//Bottom moving platform
                    pieces.Add(new SpecialTerrain(700, 200, 50, 30, 700, 200, Movement.Horizontal));//Bottom moving platform of top pair
                    pieces.Add(new SpecialTerrain(700, 50, 100, 30, 700, 200, Movement.Horizontal));//top moving platform of top pair
                    pieces.Add(new Terrain(250, 250, 30, 250));//first death wall
                    pieces.Add(new DeathObject(220, 250, 30, 250, "left"));//left facing spikes
                    pieces.Add(new DeathObject(280, 250, 30, 250, "right"));//right facing spikes
                    pieces.Add(new Terrain(425, 0, 30, 250));//second death wall
                    pieces.Add(new DeathObject(395, 0, 30, 250, "left"));//left facing spikes
                    pieces.Add(new DeathObject(455, 0, 30, 250, "right"));//right facing spikes
                    pieces.Add(new Terrain(600, 250, 30, 250));//third death wall
                    pieces.Add(new DeathObject(570, 250, 30, 250, "left"));//left facing spikes
                    pieces.Add(new DeathObject(630, 250, 30, 250, "right"));//right facing spikes
                    pieces.Add(new Terrain(710, 450, 150, 40));//right floor
                    playerY = 370;
                    break;
                case 3:
                    // clear the list
                    pieces.Clear();
                    //add new terrain pieces
                    pieces.Add(new Terrain(0, 450, 150, 40));//left floor
                    pieces.Add(new SpecialTerrain(160, 350, 40, 40, 100, 450, Movement.Vertical));//lift next to start
                    pieces.Add(new Terrain(220, 100, 300, 40));//spiked platform at the top
                    pieces.Add(new DeathObject(220, 70, 250, 30, "none"));//said spikes
                    pieces.Add(new Terrain(480, 0, 200, 20));//ceiling after spikes
                    pieces.Add(new Terrain(550, 100, 30, 300));//left spiked wall
                    pieces.Add(new DeathObject(580, 120, 20, 280, "right"));//spikes on left wall
                    pieces.Add(new Terrain(730, 100, 30, 250));//right spiked wall
                    pieces.Add(new DeathObject(710, 120, 20, 230, "left"));//spikes on right wall
                    pieces.Add(new SpecialTerrain(550, 180, 30, 30, 730, 550, Movement.Horizontal));//first moving platform
                    pieces.Add(new SpecialTerrain(730, 240, 30, 30, 730, 550, Movement.Horizontal));//second moving platform
                    pieces.Add(new SpecialTerrain(550, 300, 30, 30, 730, 550, Movement.Horizontal));//third moving platform
                    pieces.Add(new SpecialTerrain(730, 360, 30, 30, 730, 550, Movement.Horizontal));//fourth moving platform
                    pieces.Add(new SpecialTerrain(550, 450, 30, 30, 730, 550, Movement.Horizontal));//fifth moving platform
                    pieces.Add(new Terrain(710, 450, 150, 40));//right floor
                    playerY = 370;
                    break;
                case 4:
                    // clear the list
                    pieces.Clear();
                    // add new pieces
                    pieces.Add(new Terrain(0, 450, 150, 40));//left floor
                    pieces.Add(new DeathObject(0, 0, 800, 50, "down"));//ceiling spikes of imminent demise
                    //Moving blocks from left to right
                    pieces.Add(new SpecialTerrain(150, 250, 50, 50, 50, 450, Movement.Vertical));
                    pieces.Add(new SpecialTerrain(250, 170, 50, 50, 50, 450, Movement.Vertical));
                    pieces.Add(new SpecialTerrain(350, 290, 50, 50, 50, 450, Movement.Vertical));
                    pieces.Add(new SpecialTerrain(450, 50, 50, 50, 50, 450, Movement.Vertical));
                    pieces.Add(new SpecialTerrain(550, 350, 50, 50, 50, 450, Movement.Vertical));
                    pieces.Add(new SpecialTerrain(650, 90, 50, 50, 50, 450, Movement.Vertical));
                    pieces.Add(new DeathObject(700, 420, 10, 100,"none"));//spikes to stop dirty speedrunners
                    pieces.Add(new Terrain(710, 450, 150, 40));//right floor
                    break;
                case 5:
                    //clear the list
                    pieces.Clear();
                    //add new pieces
                    break;
            }
            return pieces;
        }
    }
}
