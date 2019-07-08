using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Frogger
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class FroggerGame : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        //Screen Dimensions
        int screenWidth;
        int screenHeight;

        //Picutres and boxes for frog idle positions
        Texture2D frogIdle;
        Texture2D frogIdleDown;
        Rectangle idleSourceBox;

        Texture2D frogIdleRight;
        Texture2D frogIdleLeft;
        Rectangle idleSideSourceBox;

        //Textures for frog jump positions
        Texture2D frogJump;
        Texture2D frogJumpDown;

        Texture2D frogJumpLeft;
        Texture2D frogJumpRight;

        //Background texture and box
        Texture2D background;
        Rectangle backgroundBox;

        //Keyboard states
        KeyboardState kb;
        KeyboardState prevKb;

        //Timers for animating jumps
        int jumpInterval;
        int jumpTimer;

        //Jumping bools for animatingjumps
        bool isJumpingUp;
        bool isJumpingDown;
        bool isJumpingRight;
        bool isJumpingLeft;

        //Variables to makes different positions visible or invisible
        int upVisible;
        int downVisible;
        int rightVisible;
        int leftVisible;

        //Car textures
        Texture2D carOne;
        Texture2D carTwo;
        Texture2D carThree;
        Texture2D carFour;
        Texture2D carFive;

        //Car boxes
        Rectangle carOneSourceBox;
        Rectangle carOneSourceBoxTwo;
        Rectangle carOneSourceBoxThree;

        Rectangle carTwoSourceBox;
        Rectangle carTwoSourceBoxTwo;
        Rectangle carTwoSourceBoxThree;

        Rectangle carThreeSourceBox;
        Rectangle carThreeSourceBoxTwo;
        Rectangle carThreeSourceBoxThree;

        Rectangle carFourSourceBox;
        Rectangle carFourSourceBoxTwo;
        Rectangle carFourSourceBoxThree;

        Rectangle carFiveSourceBox;
        Rectangle carFiveSourceBoxTwo;

        //Space between car
        int carSpace;

        //Arrays of ints of different car speeds;
        int[] carSpeeds;

        //Array of cars boxes
        Rectangle[] carsArray;

        //Interval for switching speeds while assigning car speeds
        int speedInterval;

        //Array of ints of X coordinates for cars to respawn at
        int[] respawnCarsX;

        //Death animation variables
        Texture2D deathSpriteSheet;
        Rectangle deathAnimation;
        Rectangle deathSourceBox;

        int deathFrameNum = 0;
        int deathNumFrames = 4;
        int deathRows = 1;
        int deathColumns = 4;
        int deathFrameWidth = 0;
        int deathFrameHeight = 0;
        int deathRow;
        int deathColumn;
        int deathRepeatCount = 0;
        int deathRepeatLimit = 17;

        //Variables for drawing death animation
        int death;

        //Timers for death animation
        int deathAnimationTimer;
        int deathAnimationInterval;

        //Number of lives
        int lives;

        //Variable for allowing or disallowing movement
        int movement;

        //Log textures and boxes
        Texture2D smallLog;
        Rectangle smallLogSourceBox;
        Rectangle smallLogSourceBoxTwo;
        Rectangle smallLogSourceBoxThree;


        Texture2D mediumLog;
        Rectangle mediumLogSourceBox;
        Rectangle mediumLogSourceBoxTwo;
        Rectangle mediumLogSourceBoxThree;
        Rectangle mediumLogSourceBoxFour;

        Texture2D bigLog;
        Rectangle bigLogSourceBox;
        Rectangle bigLogSourceBoxTwo;

        //Array of log boxes
        Rectangle[] logsArray;

        //Space between logs
        int logSpace;

        //Array of ints of different log speeds
        int[] logSpeeds;

        //Variable for witching between log speeds
        int logSpeedInterval;

        //Turtle boxes
        Texture2D turtle;
        Rectangle turtleBox;
        Rectangle turtleBoxTwo;
        Rectangle turtleBoxThree;
        Rectangle turtleBoxFour;
        Rectangle turtleBoxFive;
        Rectangle turtleBoxSix;

        Rectangle bobbingTurtleBoxOne;
        Rectangle bobbingTurtleBoxTwo;
        Rectangle bobbingTurtleBoxThree;


        Rectangle turtleTwoBox;
        Rectangle turtleTwoBoxTwo;
        Rectangle turtleTwoBoxThree;
        Rectangle turtleTwoBoxFour;

        Rectangle bobbingTurtleTwoBoxOne;
        Rectangle bobbingTurtleTwoBoxTwo;

        //turtle speed
        int turtleSpeed;
        //arrays of turtles
        Rectangle[] turtlesArray;
        Rectangle[] bobbingTurtlesArray;
        //space between turtles
        int turtleSpace;
        //turtle animation variables
        Texture2D turtleSpriteSheet;
        Rectangle bobbingTurtleSourceBox;

        int turtleFrameNum = 0;
        int turtleNumFrames = 6;
        int turtleRows = 1;
        int turtleColumns = 6;
        int turtleFrameWidth = 0;
        int turtleFrameHeight = 0;
        int turtleRow;
        int turtleColumn;
        int turtleRepeatCount = 0;
        int turtleRepeatLimit = 21;

        //lilipad boxes
        Rectangle padOne;
        Rectangle padTwo;
        Rectangle padThree;
        Rectangle padFour;
        Rectangle padFive;
        //lilipad visiblities
        int padOneVisible;
        int padTwoVisible;
        int padThreeVisible;
        int padFourVisible;
        int padFiveVisible;
        //array of pad boxes
        Rectangle[] padsArray;
        //array of pad visiblities
        int[] padsVisibleArray;
        //index of pad that was landed on
        int landIndex;
        //bool to determine if pad was landed on
        bool padCollision;

        //game state variables
        const int mainMenu = 0;
        const int playGame = 1;
        const int winLose = 3;
        const int highScores = 2;
        int gameState;

        //life boxes
        Rectangle lifeOne;
        Rectangle lifeTwo;
        Rectangle lifeThree;
        //life visibilities
        int lifeOneVisible;
        int lifeTwoVisible;
        int lifeThreeVisible;
        //array of live visiblities
        int[] livesArray;

        //score variables
        int score;
        int curScore;

        //score location
        Vector2 scorePos;
        //font
        SpriteFont font;
        //main timing variables
        int mainTimer;
        int timeLimit;
        //time bar rectangles
        Texture2D timeBar;
        Rectangle timeBox;
        // sounds and music
        SoundEffect splashSound;
        SoundEffectInstance splashSoundInstance;

        SoundEffect winSound;
        SoundEffectInstance winSoundInstance;

        SoundEffect loseSound;
        SoundEffectInstance loseSoundInstance;

        SoundEffect squashSound;
        SoundEffectInstance squashSoundInstance;

        SoundEffect padLandSound;
        SoundEffectInstance padLandSoundInstance;

        SoundEffect hopSound;

        SoundEffect timeOutSound;
        SoundEffectInstance timeOutSoundInstance;

        Song music;
        //mouse states and location
        Point mouseLocation;
        MouseState mouse;
        //logos and icons
        Texture2D froggerLogo;
        Rectangle logoBox;

        Texture2D scoresIcon;
        Rectangle scoresIconBox;

        Texture2D quitIcon;
        Rectangle quitIconBox;

        Texture2D playGameIcon;
        Rectangle playGameIconBox;

        Texture2D menuIcon;
        Rectangle menuIconBox;

        Vector2 winLoseLoc;
        SpriteFont winLoseFont;

        Vector2 enterNameLoc;
        //name variables and arrays
        string name;
        string[] namesArray;
        int[] scoresArray;
        //score locations
        Vector2 scoreOne;
        Vector2 scoreTwo;
        Vector2 scoreThree;
        Vector2 scoreFour;
        Vector2 scoreFive;
        Vector2 scoreSix;
        Vector2 scoreSeven;
        Vector2 scoreEight;
        Vector2 scoreNine;
        Vector2 scoreTen;
        //score name locations
        Vector2 scoreOneName;
        Vector2 scoreTwoName;
        Vector2 scoreThreeName;
        Vector2 scoreFourName;
        Vector2 scoreFiveName;
        Vector2 scoreSixName;
        Vector2 scoreSevenName;
        Vector2 scoreEightName;
        Vector2 scoreNineName;
        Vector2 scoreTenName;
        //array of locations
        Vector2[] scoreLocArray;
        Vector2[] namesLocArray;
        Vector2 highScoresLoc;
        int nameSort;
        public FroggerGame()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {


            base.Initialize();
            IsMouseVisible = true;
            this.graphics.PreferredBackBufferHeight = screenHeight;
            this.graphics.PreferredBackBufferWidth = screenWidth;
            graphics.ApplyChanges();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            screenWidth = 516;
            screenHeight = 544;


            score = 0;
            curScore = 0;
            frogIdle = Content.Load<Texture2D>("Images/Sprites/frogIdle");
            frogIdleDown = Content.Load<Texture2D>("Images/Sprites/frogIdleDown");
            idleSourceBox = new Rectangle(242, 470, frogIdle.Width, frogIdle.Height);

            frogIdleRight = Content.Load<Texture2D>("Images/Sprites/frogIdleRight");
            frogIdleLeft = Content.Load<Texture2D>("Images/Sprites/frogIdleLeft");
            idleSideSourceBox = new Rectangle(242, 470, frogIdleRight.Width, frogIdleRight.Height);


            frogJump = Content.Load<Texture2D>("Images/Sprites/frogJump");
            frogJumpDown = Content.Load<Texture2D>("Images/Sprites/frogJumpDown");

            frogJumpRight = Content.Load<Texture2D>("Images/Sprites/frogJumpRight");
            frogJumpLeft = Content.Load<Texture2D>("Images/Sprites/frogJumpLeft");

            background = Content.Load<Texture2D>("Images/Background/froggerBackground");
            backgroundBox = new Rectangle(0, 0, background.Width, background.Height);

            upVisible = 1;
            downVisible = 0;
            rightVisible = 0;
            leftVisible = 0;

            carOne = Content.Load<Texture2D>("Images/Sprites/CarOne");
            carTwo = Content.Load<Texture2D>("Images/Sprites/CarTwo");
            carThree = Content.Load<Texture2D>("Images/Sprites/CarThree");
            carFour = Content.Load<Texture2D>("Images/Sprites/CarFour");
            carFive = Content.Load<Texture2D>("Images/Sprites/CarFive");

            carSpace = 175;

            carOneSourceBox = new Rectangle(490, 435, carOne.Width, carOne.Height);
            carOneSourceBoxTwo = new Rectangle(490 + carSpace, 435, carOne.Width, carOne.Height);
            carOneSourceBoxThree = new Rectangle(490 + (carSpace * 2 + carOne.Width), 435, carOne.Width, carOne.Height);

            carTwoSourceBox = new Rectangle(0, 399, carTwo.Width, carTwo.Height);
            carTwoSourceBoxTwo = new Rectangle(0 - carSpace, 399, carTwo.Width, carTwo.Height);
            carTwoSourceBoxThree = new Rectangle(0 - (carSpace * 2 + carTwo.Width), 399, carTwo.Width, carTwo.Height);

            carThreeSourceBox = new Rectangle(485, 363, carThree.Width, carThree.Height);
            carThreeSourceBoxTwo = new Rectangle(485 + carSpace, 363, carThree.Width, carThree.Height);
            carThreeSourceBoxThree = new Rectangle(485 + (carSpace * 2 + carThree.Width), 363, carThree.Width, carThree.Height);

            carFourSourceBox = new Rectangle(0, 325, carFour.Width, carFour.Height);
            carFourSourceBoxTwo = new Rectangle(0 - carSpace, 325, carFour.Width, carFour.Height);
            carFourSourceBoxThree = new Rectangle(0 - (carSpace * 2 + carFour.Width), 325, carFour.Width, carFour.Height);

            carFiveSourceBox = new Rectangle(468, 289, carFive.Width, carFive.Height);
            carFiveSourceBoxTwo = new Rectangle(468 + carSpace, 289, carFive.Width, carFive.Height);

            carSpeeds = new int[] { -1, 1, -2, 1, -4 };
            carsArray = new Rectangle[] {carOneSourceBox, carOneSourceBoxTwo, carOneSourceBoxThree,
                                         carTwoSourceBox, carTwoSourceBoxTwo, carTwoSourceBoxThree,
                                         carThreeSourceBox, carThreeSourceBoxTwo, carThreeSourceBoxThree,
                                         carFourSourceBox, carFourSourceBoxTwo, carFourSourceBoxThree,
                                         carFiveSourceBox, carFiveSourceBoxTwo};
            jumpInterval = 175;
            speedInterval = 0;
            respawnCarsX = new int[] { 505, -20 };

            deathSpriteSheet = Content.Load<Texture2D>("Images/Sprites/deathSpritesheet");
            deathFrameWidth = deathSpriteSheet.Width / deathColumns;
            deathFrameHeight = deathSpriteSheet.Height / deathRows;
            deathAnimation = new Rectangle(0, 0, deathFrameWidth, deathFrameHeight);
            deathSourceBox = new Rectangle(0, 0, deathFrameWidth, deathFrameHeight);

            lives = 3;

            deathAnimationInterval = 1000;

            movement = 1;
            logSpace = 160;

            smallLog = Content.Load<Texture2D>("Images/Sprites/smallLog");
            mediumLog = Content.Load<Texture2D>("Images/Sprites/mediumLog");
            bigLog = Content.Load<Texture2D>("Images/Sprites/bigLog");

            smallLogSourceBox = new Rectangle(0, 173, smallLog.Width, smallLog.Height);
            smallLogSourceBoxTwo = new Rectangle(0 - (logSpace + smallLog.Width), 173, smallLog.Width, smallLog.Height);
            smallLogSourceBoxThree = new Rectangle(smallLogSourceBoxTwo.X - logSpace, 173, smallLog.Width, smallLog.Height);

            mediumLogSourceBox = new Rectangle(0, 63, mediumLog.Width, mediumLog.Height);
            mediumLogSourceBoxTwo = new Rectangle(mediumLogSourceBox.X - logSpace, 63, mediumLog.Width, mediumLog.Height);
            mediumLogSourceBoxThree = new Rectangle(mediumLogSourceBoxTwo.X - logSpace, 63, mediumLog.Width, mediumLog.Height);
            mediumLogSourceBoxFour = new Rectangle(mediumLogSourceBoxThree.X - logSpace, 63, mediumLog.Width, mediumLog.Height);

            bigLogSourceBox = new Rectangle(0, 137, bigLog.Width, bigLog.Height);
            bigLogSourceBoxTwo = new Rectangle(0 - (logSpace + bigLog.Width), 137, bigLog.Width, bigLog.Height);


            logsArray = new Rectangle[] {smallLogSourceBox, smallLogSourceBoxTwo, smallLogSourceBoxThree,
                                         mediumLogSourceBox, mediumLogSourceBoxTwo,
                                         mediumLogSourceBoxThree, mediumLogSourceBoxFour,
                                         bigLogSourceBox, bigLogSourceBoxTwo};

            logSpeeds = new int[] { 2, 3, 4 };
            logSpeedInterval = 0;
            turtleSpace = 75;

            turtle = Content.Load<Texture2D>("Images/Sprites/turtle");
            turtleSpriteSheet = Content.Load<Texture2D>("Images/Sprites/turtleSpritesheet");
            turtleFrameWidth = turtleSpriteSheet.Width / turtleColumns;
            turtleFrameHeight = turtleSpriteSheet.Height / turtleRows;

            turtleBox = new Rectangle(504, 211, turtle.Width, turtle.Height);
            turtleBoxTwo = new Rectangle(turtleBox.Right, 211, turtle.Width, turtle.Height);
            turtleBoxThree = new Rectangle(turtleBoxTwo.Right, 211, turtle.Width, turtle.Height);

            turtleBoxFour = new Rectangle(turtleBoxThree.Right + turtleSpace, 211, turtle.Width, turtle.Height);
            turtleBoxFive = new Rectangle(turtleBoxFour.Right, 211, turtle.Width, turtle.Height);
            turtleBoxSix = new Rectangle(turtleBoxFive.Right, 211, turtle.Width, turtle.Height);

            bobbingTurtleBoxOne = new Rectangle(turtleBoxSix.Right + turtleSpace, 211, turtleFrameWidth, turtleFrameHeight);
            bobbingTurtleBoxTwo = new Rectangle(bobbingTurtleBoxOne.Right, 211, turtleFrameWidth, turtleFrameHeight);
            bobbingTurtleBoxThree = new Rectangle(bobbingTurtleBoxTwo.Right, 211, turtleFrameWidth, turtleFrameHeight);

            turtleTwoBox = new Rectangle(504, 99, turtle.Width, turtle.Height);
            turtleTwoBoxTwo = new Rectangle(turtleTwoBox.Right, 99, turtle.Width, turtle.Height);

            turtleTwoBoxThree = new Rectangle(turtleTwoBoxTwo.Right + turtleSpace, 99, turtle.Width, turtle.Height);
            turtleTwoBoxFour = new Rectangle(turtleTwoBoxThree.Right, 99, turtle.Width, turtle.Height);

            bobbingTurtleTwoBoxOne = new Rectangle(turtleTwoBoxFour.Right + turtleSpace, 99, turtleFrameWidth, turtleFrameHeight);
            bobbingTurtleTwoBoxTwo = new Rectangle(bobbingTurtleTwoBoxOne.Right, 99, turtleFrameWidth, turtleFrameHeight);

            bobbingTurtleSourceBox = new Rectangle(0, 0, turtleFrameWidth, turtleFrameHeight);

            turtlesArray = new Rectangle[] {turtleBox, turtleBoxTwo, turtleBoxThree,
                                            turtleBoxFour, turtleBoxFive, turtleBoxSix,                                            
                                            turtleTwoBox, turtleTwoBoxTwo, turtleTwoBoxThree,
                                            turtleTwoBoxFour};

            bobbingTurtlesArray = new Rectangle[] {bobbingTurtleBoxOne, bobbingTurtleBoxTwo, bobbingTurtleBoxThree,
                                                    bobbingTurtleTwoBoxOne, bobbingTurtleTwoBoxTwo};

            turtleSpeed = 2;

            padOne = new Rectangle(21, 23, frogIdleDown.Width, frogIdleDown.Height);
            padTwo = new Rectangle(132, 23, frogIdleDown.Width, frogIdleDown.Height);
            padThree = new Rectangle(242, 23, frogIdleDown.Width, frogIdleDown.Height);
            padFour = new Rectangle(353, 23, frogIdleDown.Width, frogIdleDown.Height);
            padFive = new Rectangle(463, 23, frogIdleDown.Width, frogIdleDown.Height);

            padOneVisible = 0;
            padTwoVisible = 0;
            padThreeVisible = 0;
            padFourVisible = 0;
            padFiveVisible = 0;

            padsArray = new Rectangle[] { padOne, padTwo, padThree, padFour, padFive};
            padsVisibleArray = new int[] { padOneVisible, padTwoVisible, padThreeVisible, padFourVisible, padFiveVisible };

            landIndex = 0;

            lifeOne = new Rectangle(0, 516, frogIdle.Width, frogIdle.Height);
            lifeTwo = new Rectangle(35, 516, frogIdle.Width, frogIdle.Height);
            lifeThree = new Rectangle(70, 516, frogIdle.Width, frogIdle.Height);

            lifeOneVisible = 1;
            lifeTwoVisible = 1;
            lifeThreeVisible = 1;

            livesArray = new int[] { lifeOneVisible, lifeTwoVisible, lifeThreeVisible };
            padCollision = false;

            scorePos = new Vector2(100, 516);
            font = Content.Load<SpriteFont>("Fonts/font");

            timeBar = Content.Load<Texture2D>("Images/Sprites/timer");
            timeBox = new Rectangle(250, 505, 250, timeBar.Height);
            timeLimit = 25000;

            music = Content.Load<Song>("Audio/Music/music");
            MediaPlayer.Play(music);
            MediaPlayer.IsRepeating = true;

            hopSound = Content.Load<SoundEffect>("Audio/SoundEffects/hopSound");

            loseSound = Content.Load<SoundEffect>("Audio/SoundEffects/loseSound");
            loseSoundInstance = loseSound.CreateInstance();

            padLandSound = Content.Load<SoundEffect>("Audio/SoundEffects/padLandSound");
            padLandSoundInstance = padLandSound.CreateInstance();

            splashSound = Content.Load<SoundEffect>("Audio/SoundEffects/splashSound");
            splashSoundInstance = splashSound.CreateInstance();

            squashSound = Content.Load<SoundEffect>("Audio/SoundEffects/squashSound");
            squashSoundInstance = squashSound.CreateInstance();

            timeOutSound = Content.Load<SoundEffect>("Audio/SoundEffects/timeOutSound");
            timeOutSoundInstance = timeOutSound.CreateInstance();

            winSound = Content.Load<SoundEffect>("Audio/SoundEffects/winSound");
            winSoundInstance = winSound.CreateInstance();

            froggerLogo = Content.Load<Texture2D>("Images/Sprites/logo");
            logoBox = new Rectangle(170, 0, 180, 150);

            playGameIcon = Content.Load<Texture2D>("Images/Sprites/playGame");
            playGameIconBox = new Rectangle(30, 165, 130, 130);

            scoresIcon = Content.Load<Texture2D>("Images/Sprites/scoresIcon");
            scoresIconBox = new Rectangle(190, 165, 130, 130);

            quitIcon = Content.Load<Texture2D>("Images/Sprites/quitIcon");
            quitIconBox = new Rectangle(350, 165, 130, 130);

            menuIcon = Content.Load<Texture2D>("Images/Sprites/menuIcon");
            menuIconBox = new Rectangle(190, 165, 130, 130);

            winLoseLoc = new Vector2(100, 100);
            winLoseFont = Content.Load<SpriteFont>("Fonts/winLoseFont");

            enterNameLoc = new Vector2(230, 100);
            gameState = mainMenu;

            name = "";
            namesArray = new string[] {"AAA", "AAA" , "AAA" , "AAA" , "AAA", "AAA" , "AAA" , "AAA" , "AAA" , "AAA"};
            scoresArray = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            scoreOne = new Vector2(0, 0);
            scoreTwo = new Vector2(0, 30);
            scoreThree = new Vector2(0, 60);
            scoreFour = new Vector2(0, 90);
            scoreFive = new Vector2(0, 120);
            scoreSix = new Vector2(0, 150);
            scoreSeven = new Vector2(0, 180);
            scoreEight = new Vector2(0, 210);
            scoreNine = new Vector2(0, 240);
            scoreTen = new Vector2(0, 270);

            scoreOneName = new Vector2(100, 0);
            scoreTwoName = new Vector2(100, 30);
            scoreThreeName = new Vector2(100, 60);
            scoreFourName = new Vector2(0100, 90);
            scoreFiveName = new Vector2(100, 120);
            scoreSixName = new Vector2(100, 150);
            scoreSevenName = new Vector2(100, 180);
            scoreEightName = new Vector2(100, 210);
            scoreNineName = new Vector2(100, 240);
            scoreTenName = new Vector2(100, 270);

            scoreLocArray = new Vector2[] {scoreOne , scoreTwo, scoreThree , scoreFour , scoreFive , scoreSix , scoreSeven,
                                           scoreEight, scoreNine, scoreTen};

            namesLocArray = new Vector2[] {scoreOneName , scoreTwoName, scoreThreeName , scoreFourName , scoreFiveName , scoreSixName , scoreSevenName,
                                           scoreEightName, scoreNineName, scoreTenName};
            highScoresLoc = new Vector2(100, 100);

            
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {

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
            //getting kb and mouse states
            kb = Keyboard.GetState();
            mouse = Mouse.GetState();

            mouseLocation = new Point(mouse.X, mouse.Y);


            if(gameState == mainMenu)
            {

                if(playGameIconBox.Contains(mouseLocation) && mouse.LeftButton == ButtonState.Pressed)
                {
                    //setting game back to start point
                    score = 0;
                    curScore = 0;
                    mainTimer = 0;
                    lives = 3;
                    livesArray[0] = 1;
                    livesArray[1] = 1;
                    livesArray[2] = 1;
                    idleSourceBox.X = 242;
                    idleSourceBox.Y = 470;
                    idleSideSourceBox.X = 240;
                    idleSideSourceBox.Y = 470;
                    upVisible = 1;
                    deathAnimationTimer = 0;
                    movement = 1;
                    death = 0;
                    for (int j = 0; j < padsVisibleArray.Length; j++)
                    {
                        padsVisibleArray[j] = 0;
                    }
                    gameState = playGame;
                }
                else if (scoresIconBox.Contains(mouseLocation) && mouse.LeftButton == ButtonState.Pressed)
                {                    
                    gameState = highScores;
                }
                else if (quitIconBox.Contains(mouseLocation) && mouse.LeftButton == ButtonState.Pressed)
                {
                    this.Exit();
                }
            }

            if(gameState == highScores)
            {
                if (menuIconBox.Contains(mouseLocation) && mouse.LeftButton == ButtonState.Pressed)
                {
                    gameState = mainMenu;
                }
            }

            
            if (gameState == playGame)
            {

                mainTimer += gameTime.ElapsedGameTime.Milliseconds;
                //time bar shrinking
                timeBox.Width = 250 - (mainTimer / 100);

                if (mainTimer >= timeLimit)
                {
                    //time deatg
                    timeOutSoundInstance.Play();
                    death = 1;
                }
                if (curScore > score)
                {
                    score = curScore;
                }
                
                if (movement == 1)
                {
                    //movement
                    if (prevKb.IsKeyUp(Keys.Up) && kb.IsKeyDown(Keys.Up) && idleSourceBox.Y > 50)
                    {
                        hopSound.Play();
                        curScore += 10;
                        isJumpingUp = true;
                        idleSourceBox.Y -= 37;
                        idleSideSourceBox.Y = idleSourceBox.Y;
                    }
                    if (isJumpingUp == true)
                    {
                        upVisible = 1;
                        downVisible = 0;
                        rightVisible = 0;
                        leftVisible = 0;
                        jumpTimer += gameTime.ElapsedGameTime.Milliseconds;
                    }
                    if (jumpTimer >= jumpInterval)
                    {
                        upVisible = 1;
                        downVisible = 0;
                        rightVisible = 0;
                        leftVisible = 0;
                        isJumpingUp = false;
                        jumpTimer = 0;

                    }


                    if (prevKb.IsKeyUp(Keys.Down) && kb.IsKeyDown(Keys.Down) && idleSourceBox.Y != 470)
                    {
                        hopSound.Play();
                        curScore -= 10;
                        isJumpingDown = true;
                        idleSourceBox.Y += 37;
                        idleSideSourceBox.Y = idleSourceBox.Y;
                    }
                    if (isJumpingDown == true)
                    {
                        upVisible = 0;
                        downVisible = 1;
                        rightVisible = 0;
                        leftVisible = 0;
                        jumpTimer += gameTime.ElapsedGameTime.Milliseconds;
                    }
                    if (jumpTimer >= jumpInterval)
                    {
                        upVisible = 0;
                        downVisible = 1;
                        rightVisible = 0;
                        leftVisible = 0;
                        isJumpingDown = false;
                        jumpTimer = 0;
                    }


                    if (prevKb.IsKeyUp(Keys.Right) && kb.IsKeyDown(Keys.Right) && idleSideSourceBox.X < 450)
                    {
                        hopSound.Play();
                        isJumpingRight = true;
                        idleSideSourceBox.X += 37;
                        idleSourceBox.X = idleSideSourceBox.X;
                    }
                    if (isJumpingRight == true)
                    {
                        upVisible = 0;
                        downVisible = 0;
                        rightVisible = 1;
                        leftVisible = 0;
                        jumpTimer += gameTime.ElapsedGameTime.Milliseconds;
                    }
                    if (jumpTimer >= jumpInterval)
                    {
                        upVisible = 0;
                        downVisible = 0;
                        rightVisible = 1;
                        leftVisible = 0;
                        isJumpingRight = false;
                        jumpTimer = 0;
                    }

                    if (prevKb.IsKeyUp(Keys.Left) && kb.IsKeyDown(Keys.Left) && idleSideSourceBox.X > 30)
                    {
                        hopSound.Play();
                        isJumpingLeft = true;
                        idleSideSourceBox.X -= 37;
                        idleSourceBox.X = idleSideSourceBox.X;
                    }
                    if (isJumpingLeft == true)
                    {
                        upVisible = 0;
                        downVisible = 0;
                        rightVisible = 0;
                        leftVisible = 1;
                        jumpTimer += gameTime.ElapsedGameTime.Milliseconds;
                    }
                    if (jumpTimer >= jumpInterval)
                    {
                        upVisible = 0;
                        downVisible = 0;
                        rightVisible = 0;
                        leftVisible = 1;
                        isJumpingLeft = false;
                        jumpTimer = 0;
                    }
                }



                //moving cars and repsawning them
                carsArray = MoveCars(carsArray, carSpeeds);
                carsArray = RespawnCars(carsArray, respawnCarsX);
                //cars collision check
                if ((CollisionCheck(idleSourceBox, carsArray) == true) & (upVisible == 1 || downVisible == 1))
                {
                    squashSoundInstance.Play();
                    death = 1;
                }

                if ((CollisionCheck(idleSideSourceBox, carsArray) == true) & (leftVisible == 1 || rightVisible == 1))
                {
                    squashSoundInstance.Play();
                    death = 1;
                }

                //moving and respawning logs
                logsArray = MoveLogs(logsArray, logSpeeds);
                logsArray = RespawnLogs(logsArray);
                //logs collision check
                if ((CollisionCheck(idleSourceBox, logsArray) == true ||
                    (CollisionCheck(idleSideSourceBox, logsArray) == true) && (upVisible == 1 || downVisible == 1 ||
                   rightVisible == 1 || leftVisible == 1)))
                {
                    if (movement == 1)
                    {
                        idleSourceBox.X += LogMove(idleSourceBox, logsArray);
                        idleSideSourceBox.X += LogMove(idleSourceBox, logsArray);
                    }

                }

                if ((CollisionCheck(idleSourceBox, turtlesArray) == true ||
                   CollisionCheck(idleSideSourceBox, turtlesArray) == true) && (upVisible == 1 || downVisible == 1 ||
                   rightVisible == 1 || leftVisible == 1))
                {
                    if (movement == 1)
                    {
                        idleSourceBox.X -= turtleSpeed;
                        idleSideSourceBox.X -= turtleSpeed;
                    }

                }

                if ((CollisionCheck(idleSourceBox, bobbingTurtlesArray) == true ||
                  CollisionCheck(idleSideSourceBox, bobbingTurtlesArray) == true) && (upVisible == 1 || downVisible == 1 ||
                  rightVisible == 1 || leftVisible == 1) && turtleFrameNum <= 4)
                {
                    if (movement == 1)
                    {
                        idleSourceBox.X -= turtleSpeed;
                        idleSideSourceBox.X -= turtleSpeed;
                    }

                }


                turtlesArray = MoveTurtles(turtlesArray, turtleSpeed);
                turtlesArray = RespawnTurtles(turtlesArray);

                bobbingTurtlesArray = MoveTurtles(bobbingTurtlesArray, turtleSpeed);
                bobbingTurtlesArray = RespawnTurtles(bobbingTurtlesArray);
                //pads collision check
                if (CollisionCheck(idleSourceBox, padsArray))
                {
                    if (PadLanding(idleSourceBox, padsArray, padsVisibleArray, landIndex) > -1)
                    {
                        padLandSoundInstance.Play();
                        landIndex = PadLanding(idleSourceBox, padsArray, padsVisibleArray, landIndex);
                        padCollision = true;
                        mainTimer = 0;
                        curScore += 100 + (timeBox.Width);
                        padsVisibleArray[landIndex] = 1;
                        idleSourceBox.X = 242;
                        idleSourceBox.Y = 470;
                        idleSideSourceBox.X = 240;
                        idleSideSourceBox.Y = 470;
                        upVisible = 1;

                    }

                    padCollision = false;

                }
                //determing if frog drowned
                if (WaterLogDeath(idleSourceBox) == true && WaterLogDeath(idleSideSourceBox) == true &&
                    WaterTurtleDeath(idleSideSourceBox) == true && WaterTurtleDeath(idleSourceBox) == true &&
                    WaterBobbingTurtleDeath(idleSourceBox) == true && WaterBobbingTurtleDeath(idleSideSourceBox) == true)
                {
                    splashSoundInstance.Play();
                    death = 1;
                }
                if (OutOfBounds(idleSourceBox) == true)
                {
                    splashSoundInstance.Play();
                    death = 1;
                }

                if (CheckIfWin(padsVisibleArray) == true)
                {
                    winSoundInstance.Play();
                    gameState = winLose;
                }

                TurtleAnimation();
                

                
            }
            EnterName();
            prevKb = kb;
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();

            spriteBatch.Draw(background, backgroundBox, Color.White);

            if (gameState == mainMenu)
            {
                spriteBatch.Draw(froggerLogo, logoBox, Color.White);
                spriteBatch.Draw(playGameIcon, playGameIconBox, Color.White);
                spriteBatch.Draw(scoresIcon, scoresIconBox, Color.White);
                spriteBatch.Draw(quitIcon, quitIconBox, Color.White);
            }
            
            if(gameState == winLose)
            {
                spriteBatch.DrawString(winLoseFont, "NAME: ", winLoseLoc, Color.Red);               
                spriteBatch.DrawString(winLoseFont, name, enterNameLoc, Color.Red);
            }

            if(gameState == highScores)
            {
                nameSort = Array.IndexOf(scoresArray, score);
                namesArray[nameSort] = name;

                for (int i = 0; i < scoresArray.Length; i++)
                {
                    spriteBatch.DrawString(font, Convert.ToString(scoresArray[i]), scoreLocArray[i], Color.Red);
                    
                }
                for (int j = 0; j < namesArray.Length; j++)
                {
                    spriteBatch.DrawString(font, namesArray[j], namesLocArray[j], Color.Red);
                }
                
                spriteBatch.DrawString(winLoseFont, "HIGHSCORES", highScoresLoc, Color.Red);
                spriteBatch.Draw(menuIcon, menuIconBox, Color.White);
            }
            if (gameState == playGame)
            {
                spriteBatch.Draw(smallLog, logsArray[0], Color.White);
                spriteBatch.Draw(smallLog, logsArray[1], Color.White);
                spriteBatch.Draw(smallLog, logsArray[2], Color.White);


                spriteBatch.Draw(mediumLog, logsArray[3], Color.White);
                spriteBatch.Draw(mediumLog, logsArray[4], Color.White);
                spriteBatch.Draw(mediumLog, logsArray[5], Color.White);
                spriteBatch.Draw(mediumLog, logsArray[6], Color.White);

                spriteBatch.Draw(bigLog, logsArray[7], Color.White);
                spriteBatch.Draw(bigLog, logsArray[8], Color.White);


                spriteBatch.Draw(turtle, turtlesArray[0], Color.White);
                spriteBatch.Draw(turtle, turtlesArray[1], Color.White);
                spriteBatch.Draw(turtle, turtlesArray[2], Color.White);
                spriteBatch.Draw(turtle, turtlesArray[3], Color.White);
                spriteBatch.Draw(turtle, turtlesArray[4], Color.White);
                spriteBatch.Draw(turtle, turtlesArray[5], Color.White);

                spriteBatch.Draw(turtle, turtlesArray[6], Color.White);
                spriteBatch.Draw(turtle, turtlesArray[7], Color.White);
                spriteBatch.Draw(turtle, turtlesArray[8], Color.White);
                spriteBatch.Draw(turtle, turtlesArray[9], Color.White);

                spriteBatch.Draw(turtleSpriteSheet, bobbingTurtlesArray[0], bobbingTurtleSourceBox, Color.White);
                spriteBatch.Draw(turtleSpriteSheet, bobbingTurtlesArray[1], bobbingTurtleSourceBox, Color.White);
                spriteBatch.Draw(turtleSpriteSheet, bobbingTurtlesArray[2], bobbingTurtleSourceBox, Color.White);

                spriteBatch.Draw(turtleSpriteSheet, bobbingTurtlesArray[3], bobbingTurtleSourceBox, Color.White);
                spriteBatch.Draw(turtleSpriteSheet, bobbingTurtlesArray[4], bobbingTurtleSourceBox, Color.White);



                if (isJumpingUp == true)
                {
                    spriteBatch.Draw(frogJump, idleSourceBox, Color.White * upVisible);
                }
                else if (isJumpingUp == false)
                {
                    spriteBatch.Draw(frogIdle, idleSourceBox, Color.White * upVisible);
                }


                if (isJumpingDown == true)
                {
                    spriteBatch.Draw(frogJumpDown, idleSourceBox, Color.White * downVisible);
                }
                else if (isJumpingDown == false)
                {
                    spriteBatch.Draw(frogIdleDown, idleSourceBox, Color.White * downVisible);
                }


                if (isJumpingRight == true)
                {
                    spriteBatch.Draw(frogJumpRight, idleSideSourceBox, Color.White * rightVisible);
                }
                else if (isJumpingUp == false)
                {
                    spriteBatch.Draw(frogIdleRight, idleSideSourceBox, Color.White * rightVisible);
                }


                if (isJumpingLeft == true)
                {
                    spriteBatch.Draw(frogJumpLeft, idleSideSourceBox, Color.White * leftVisible);
                }
                else if (isJumpingLeft == false)
                {
                    spriteBatch.Draw(frogIdleLeft, idleSideSourceBox, Color.White * leftVisible);
                }

                spriteBatch.Draw(carOne, carsArray[0], Color.White);
                spriteBatch.Draw(carOne, carsArray[1], Color.White);
                spriteBatch.Draw(carOne, carsArray[2], Color.White);

                spriteBatch.Draw(carTwo, carsArray[3], Color.White);
                spriteBatch.Draw(carTwo, carsArray[4], Color.White);
                spriteBatch.Draw(carTwo, carsArray[5], Color.White);

                spriteBatch.Draw(carThree, carsArray[6], Color.White);
                spriteBatch.Draw(carThree, carsArray[7], Color.White);
                spriteBatch.Draw(carThree, carsArray[8], Color.White);

                spriteBatch.Draw(carFour, carsArray[9], Color.White);
                spriteBatch.Draw(carFour, carsArray[10], Color.White);
                spriteBatch.Draw(carFour, carsArray[11], Color.White);

                spriteBatch.Draw(carFive, carsArray[12], Color.White);
                spriteBatch.Draw(carFive, carsArray[13], Color.White);



                if (death == 1)
                {
                    deathAnimationTimer += gameTime.ElapsedGameTime.Milliseconds;
                    movement = 0;
                    upVisible = 0;
                    downVisible = 0;
                    leftVisible = 0;
                    rightVisible = 0;
                    spriteBatch.Draw(deathSpriteSheet, idleSourceBox, deathSourceBox, Color.White);
                    if (deathAnimationTimer <= deathAnimationInterval)
                    {
                        deathRow = deathFrameNum / deathColumns;
                        deathColumn = deathFrameNum % deathColumns;

                        deathSourceBox.X = deathColumn * deathFrameWidth;
                        deathSourceBox.Y = deathRow * deathFrameHeight;

                        deathRepeatCount++;
                        if (deathRepeatCount == deathRepeatLimit)
                        {
                            deathFrameNum = (deathFrameNum + 1) % deathNumFrames;
                            deathRepeatCount = 0;
                        }

                    }

                    if (deathAnimationTimer >= deathAnimationInterval)
                    {
                        FrogRespawn();
                    }

                }


                spriteBatch.Draw(frogIdleDown, padsArray[0], Color.White * padsVisibleArray[0]);
                spriteBatch.Draw(frogIdleDown, padsArray[1], Color.White * padsVisibleArray[1]);
                spriteBatch.Draw(frogIdleDown, padsArray[2], Color.White * padsVisibleArray[2]);
                spriteBatch.Draw(frogIdleDown, padsArray[3], Color.White * padsVisibleArray[3]);
                spriteBatch.Draw(frogIdleDown, padsArray[4], Color.White * padsVisibleArray[4]);

                spriteBatch.Draw(frogIdle, lifeOne, Color.White * livesArray[0]);
                spriteBatch.Draw(frogIdle, lifeTwo, Color.White * livesArray[1]);
                spriteBatch.Draw(frogIdle, lifeThree, Color.White * livesArray[2]);

                spriteBatch.DrawString(font, "SCORE: " + Convert.ToString(score), scorePos, Color.Red);

                spriteBatch.Draw(timeBar, timeBox, Color.White);
                
            }
            spriteBatch.End();
            base.Draw(gameTime);
        }
        /// <summary>
        /// Moves cars across screen
        /// </summary>
        /// <param name="rect"></param>
        /// array of rectangles
        /// <param name="speeds"></param>
        /// array of car speeds
        /// <returns></returns>
        /// new array of rectangle
        private Rectangle[] MoveCars(Rectangle[] rect, int[] speeds)
        {
            for (int i = 0; i < rect.Length; i++)
            {
                rect[i].X += speeds[speedInterval];

                if ((i + 1) % 3 == 0 || i == 11)
                {
                    speedInterval += 1;
                }
            }
            speedInterval = 0;
            return rect;
        }
        /// <summary>
        /// Respawns cars once the go off screen
        /// </summary>
        /// <param name="rectRespawn"> Array of cars</param>
        /// <param name="respawnCarsX">Array of car X vairables to spawn at</param>
        /// <returns>New array of car rectangles</returns>
        private Rectangle[] RespawnCars(Rectangle[] rectRespawn, int[] respawnCarsX)
        {
            for (int j = 0; j < rectRespawn.Length; j++)
            {
                if (j < 3 || j == 6 || j == 7 || j == 8 || j == 12 || j == 13)
                {
                    if (rectRespawn[j].Right < 0)
                    {
                        rectRespawn[j].X = respawnCarsX[0];
                    }

                }
                else
                {
                    if (rectRespawn[j].Left > screenWidth)
                    {
                        rectRespawn[j].X = respawnCarsX[1];
                    }
                }
            }

            return rectRespawn;
        }
        /// <summary>
        /// Checks for collisoon
        /// </summary>
        /// <param name="frogUpDown">frog box</param>
        /// <param name="cars">obstacle box</param>
        /// <returns>bool if collision or not</returns>
        private bool CollisionCheck(Rectangle frogUpDown, Rectangle[] cars)
        {
            bool collision = false;

            for (int k = 0; k < cars.Length; k++)
            {
                if (frogUpDown.Bottom < cars[k].Top ||
                   frogUpDown.Top > cars[k].Bottom ||
                   frogUpDown.Right < cars[k].Left ||
                   frogUpDown.Left > cars[k].Right)
                {

                    collision = false;
                }
                else
                {
                    collision = true;
                    break;
                }

            }
            return collision;
        }

        //Repsawns frog, takes life
        private void FrogRespawn()
        {
            if(lives == 0)
            {
                lives = 1;
            }
            lives--;
            livesArray[lives] = 0;
            deathFrameNum = 0;
            if (lives > 0)
            {
                idleSourceBox.X = 242;
                idleSourceBox.Y = 470;
                idleSideSourceBox.X = 240;
                idleSideSourceBox.Y = 470;
                upVisible = 1;
                death = 0;
                deathAnimationTimer = 0;
                movement = 1;
                mainTimer = 0;
            }
            if(lives <= 0)
            {
                loseSoundInstance.Play();
                scoresArray = ScoreCompile(score, scoresArray);
                gameState = winLose;
            }
        }

        private Rectangle[] MoveLogs(Rectangle[] rect, int[] speeds)
        {
            for (int i = 0; i < rect.Length; i++)
            {
                rect[i].X += speeds[logSpeedInterval];

                if (i == 2 || i == 6)
                {
                    logSpeedInterval += 1;
                }
            }
            logSpeedInterval = 0;
            return rect;
        }

        private Rectangle[] RespawnLogs(Rectangle[] rectRespawn)
        {
            for (int j = 0; j < rectRespawn.Length; j++)
            {
                if(rectRespawn[j].Left > screenWidth)
                {
                    rectRespawn[j].X = 0 - rectRespawn[j].Width;

                }
            }

            return rectRespawn;
        }

        private int LogMove(Rectangle frogUpDown, Rectangle[] logs)
        {
            int speed = 0;

            for (int k = 0; k < logs.Length; k++)
            {
                if (k < 3)
                {
                    speed = 0;
                }
                else if(k == 3 || k == 4 || k == 5 || k == 6)
                {
                    speed = 1;
                }
                else if (k == 7 || k == 8)
                {
                    speed = 2;
                }
                if (frogUpDown.Bottom < logs[k].Top ||
                   frogUpDown.Top > logs[k].Bottom ||
                   frogUpDown.Right < logs[k].Left ||
                   frogUpDown.Left > logs[k].Right)
                {

                }
                else
                {
                    break;
                }
                
            }
            return logSpeeds[speed];
        }

        private Rectangle[] MoveTurtles(Rectangle[] rect, int speed)
        {
            for (int i = 0; i < rect.Length; i++)
            {
                rect[i].X -= speed;
            }
            return rect;
        }


        private Rectangle[] RespawnTurtles(Rectangle[] rectRespawn)
        {
            for (int j = 0; j < rectRespawn.Length; j++)
            {
                if (rectRespawn[j].Right < backgroundBox.Left)
                {
                    rectRespawn[j].X = screenWidth + rectRespawn[j].Width;

                }
            }

            return rectRespawn;
        }
        /// <summary>
        /// Determines if frog is on a turtle in water
        /// </summary>
        /// <param name="frog">frog box</param>
        /// <returns>bool if frog is in water and on turtle or not</returns>
        private bool WaterTurtleDeath(Rectangle frog)
        {

            if((frog.Bottom < 260 && padCollision == false) && (CollisionCheck(frog, turtlesArray) == false))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// if frog if on bobbing turtle
        /// </summary>
        /// <param name="frog">frog box</param>
        /// <returns>if frog has drowned or not</returns>
        private bool WaterBobbingTurtleDeath(Rectangle frog)
        {

            if ((frog.Bottom < 260 && padCollision == false) && (CollisionCheck(frog, bobbingTurtlesArray) == true) && turtleFrameNum > 4)
            {
                return true;
            }

            if (((frog.Bottom < 260 && padCollision == false) && (CollisionCheck(frog, bobbingTurtlesArray) == false)))               
            {
                return true;
            }
            else
            {
                return false;
            }

        }


        private bool WaterLogDeath(Rectangle frog)
        {

            if ((frog.Bottom < 260 && padCollision == false) && (CollisionCheck(frog, logsArray) == false))
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        private bool OutOfBounds(Rectangle frog)
        {
            if(frog.Left <= backgroundBox.Left || frog.Right >= backgroundBox.Right)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// if frog landed on pad
        /// </summary>
        /// <param name="frog"frog box</param>
        /// <param name="pads">array of pad rectangles</param>
        /// <param name="visibles">array of pad visibiltiies</param>
        /// <param name="index">pad index</param>
        /// <returns></returns>
        private int PadLanding(Rectangle frog, Rectangle[] pads, int[] visibles, int index)
        {
            
            int padIndex = LandingCollisionCheck(frog, pads);
            for (int i = 0; i < pads.Length; i++)
            {
                if(visibles[padIndex] == 0)
                {
                    index = padIndex;
                    break;
                    
                }
                else if(visibles[padIndex] == 1)
                {
                    index = -2;
                }
            }
            return index;
        }
        /// <summary>
        /// Checks which pad frog landed on
        /// </summary>
        /// <param name="frogUpDown">frog box</param>
        /// <param name="pads">array of pads</param>
        /// <returns>index of pad landed on</returns>
        private int LandingCollisionCheck(Rectangle frogUpDown, Rectangle[] pads)
        {
            int index = -1;

            for (int k = 0; k < pads.Length; k++)
            {
                if (frogUpDown.Bottom < pads[k].Top ||
                   frogUpDown.Top > pads[k].Bottom ||
                   frogUpDown.Right < pads[k].Left ||
                   frogUpDown.Left > pads[k].Right)
                {

                    
                }
                else
                {
                    index = k;
                    break;
                }

            }
            return index;
        }
        /// <summary>
        /// checks for win
        /// </summary>
        /// <param name="visibles">array of pad visibilities</param>
        /// <returns>if game has been won or not</returns>
        private bool CheckIfWin(int[] visibles)
        {
            bool win = false;

            if(visibles[0] == 1 && visibles[1] == 1 && visibles[2] == 1 && visibles[3] == 1 && visibles[4] == 1)
            {
                win = true;
            }
            else
            {
                win = false;
            }
            return win;
        }

        private void TurtleAnimation()
        {
            turtleRow = turtleFrameNum / turtleColumns;
            turtleColumn = turtleFrameNum % turtleColumns;

            bobbingTurtleSourceBox.X = turtleColumn * turtleFrameWidth;
            bobbingTurtleSourceBox.Y = turtleRow * turtleFrameHeight;

            turtleRepeatCount++;
            if (turtleRepeatCount == turtleRepeatLimit)
            {
                turtleFrameNum = (turtleFrameNum + 1) % turtleNumFrames;
                turtleRepeatCount = 0;
            }

        }
        //User entering name
        private void EnterName()
        {
            
            if (gameState == winLose)
            {
                if (name.Length < 3)
                {


                    if (prevKb.IsKeyUp(Keys.A) && kb.IsKeyDown(Keys.A))
                    {

                        name += "A";

                    }
                    if (prevKb.IsKeyUp(Keys.B) && kb.IsKeyDown(Keys.B))
                    {
                        name += "B";
                    }
                    if (prevKb.IsKeyUp(Keys.C) && kb.IsKeyDown(Keys.C))
                    {
                        name += "C";
                    }
                    if (prevKb.IsKeyUp(Keys.D) && kb.IsKeyDown(Keys.D))
                    {
                        name += "D";
                    }
                    if (prevKb.IsKeyUp(Keys.E) && kb.IsKeyDown(Keys.E))
                    {
                        name += "E";
                    }
                    if (prevKb.IsKeyUp(Keys.F) && kb.IsKeyDown(Keys.F))
                    {
                        name += "F";
                    }
                    if (prevKb.IsKeyUp(Keys.G) && kb.IsKeyDown(Keys.G))
                    {
                        name += "G";
                    }
                    if (prevKb.IsKeyUp(Keys.H) && kb.IsKeyDown(Keys.H))
                    {
                        name += "H";
                    }
                    if (prevKb.IsKeyUp(Keys.I) && kb.IsKeyDown(Keys.I))
                    {
                        name += "I";
                    }
                    if (prevKb.IsKeyUp(Keys.J) && kb.IsKeyDown(Keys.J))
                    {
                        name += "J";
                    }
                    if (prevKb.IsKeyUp(Keys.K) && kb.IsKeyDown(Keys.K))
                    {
                        name += "K";
                    }
                    if (prevKb.IsKeyUp(Keys.L) && kb.IsKeyDown(Keys.L))
                    {
                        name += "L";
                    }
                    if (prevKb.IsKeyUp(Keys.M) && kb.IsKeyDown(Keys.M))
                    {
                        name += "M";
                    }
                    if (prevKb.IsKeyUp(Keys.N) && kb.IsKeyDown(Keys.N))
                    {
                        name += "N";
                    }
                    if (prevKb.IsKeyUp(Keys.O) && kb.IsKeyDown(Keys.O))
                    {
                        name += "O";
                    }
                    if (prevKb.IsKeyUp(Keys.P) && kb.IsKeyDown(Keys.P))
                    {
                        name += "P";
                    }
                    if (prevKb.IsKeyUp(Keys.Q) && kb.IsKeyDown(Keys.Q))
                    {
                        name += "Q";
                    }
                    if (prevKb.IsKeyUp(Keys.R) && kb.IsKeyDown(Keys.R))
                    {
                        name += "R";
                    }
                    if (prevKb.IsKeyUp(Keys.S) && kb.IsKeyDown(Keys.S))
                    {
                        name += "S";
                    }
                    if (prevKb.IsKeyUp(Keys.T) && kb.IsKeyDown(Keys.T))
                    {
                        name += "T";
                    }
                    if (prevKb.IsKeyUp(Keys.U) && kb.IsKeyDown(Keys.U))
                    {
                        name += "U";
                    }
                    if (prevKb.IsKeyUp(Keys.V) && kb.IsKeyDown(Keys.V))
                    {
                        name += "V";
                    }
                    if (prevKb.IsKeyUp(Keys.W) && kb.IsKeyDown(Keys.W))
                    {
                        name += "W";
                    }
                    if (prevKb.IsKeyUp(Keys.X) && kb.IsKeyDown(Keys.X))
                    {
                        name += "X";
                    }
                    if (prevKb.IsKeyUp(Keys.Y) && kb.IsKeyDown(Keys.Y))
                    {
                        name += "Y";
                    }
                    if (prevKb.IsKeyUp(Keys.Z) && kb.IsKeyDown(Keys.Z))
                    {
                        name += "Z";
                    }
                    
                }
                if (prevKb.IsKeyUp(Keys.Back) && kb.IsKeyDown(Keys.Back) && name.Length >= 1)
                {
                    name = name.Remove(name.Length - 1, 1);
                }
                if (prevKb.IsKeyUp(Keys.Enter) && kb.IsKeyDown(Keys.Enter) && name.Length == 3)
                {
                    gameState = highScores;
                }
            }
        }

        private int[] ScoreCompile(int score, int[] array)
        {
            

            if (score > array[9])
                {
                    
                    array[9] = score;
                    
                }
            
            Array.Sort<int>(array);            
            Array.Reverse(array);
            
            return array;
        }
    }
}