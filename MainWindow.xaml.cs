 using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Tetris
{
   
    public partial class TetrisWindow : Window
    {
        private const int GameSpeed = 700;  

        
        List<System.Media.SoundPlayer> soundList = new List<System.Media.SoundPlayer>();
        DispatcherTimer gameTimer;
        Random shapeRandom;
        private Button StartStopBtn;
        private int rowCount = 0;
        private int columnCount = 0;
        private int leftPos = 0;
        private int downPos = 0;
        private int currentTetrominoWidth;
        private int currentTetrominoHeight;
        private int currentShapeNumber;
        private int nextShapeNumber;
        private int tetrisGridColumn;
        private int tetrisGridRow;
        private int rotation = 0;
        private bool gameActive = false;
        private bool nextShapeDrawed = false;
        private bool nextShapeDrawn = false;
        private int[,] currentTetromino = null;
        private bool isRotated = false;
        private bool bottomCollided = false;
        private bool leftCollided = false;
        private bool rightCollided = false;
        private bool isGameOver = false;
        private int gameSpeed;
        private int levelScale = 60;
        private double gameSpeedCounter = 0;
        private int gameLevel = 1;
        private int gameScore = 0;
        private static Color OTetrominoColor = Colors.GreenYellow;
        private static Color ITetrominoColor = Colors.Red;
        private static Color TTetrominoColor = Colors.Gold;
        private static Color STetrominoColor = Colors.Violet;
        private static Color ZTetrominoColor = Colors.DeepSkyBlue;
        private static Color JTetrominoColor = Colors.Cyan;
        private static Color LTetrominoColor = Colors.LightSeaGreen;
        List<int> currentTetrominoRow = null;
        List<int> currentTetrominoColumn = null;

       
        Color[] shapeColor = { OTetrominoColor, ITetrominoColor, TTetrominoColor,
                               STetrominoColor, ZTetrominoColor, JTetrominoColor, LTetrominoColor };

     
        string[] arrayTetrominos = { "", "OTetromino", "ITetromino_0",
                                        "TTetromino_0", "STetromino_0",
                                        "ZTetromino_0", "JTetromino_0",
                                        "LTetromino_0"
                                   };

        #region Array of tetrominos shape 

        
        public int[,] OTetromino = new int[2, 2] { { 1, 1 },  // * *
                                                    { 1, 1 }}; // * *

        
        public int[,] ITetromino_0 = new int[2, 4] { { 1, 1, 1, 1 }, { 0, 0, 0, 0 } };// * * * *

        public int[,] ITetromino_90 = new int[4, 2] { { 1, 0 },    // *  
                                                      { 1, 0 },    // *
                                                      { 1, 0 },    // *
                                                      { 1, 0 }};   // *
       
        public int[,] TTetromino_0 = new int[2, 3] { { 0, 1, 0 },    //    * 
                                                    { 1, 1, 1 }};   //  * * *

        public int[,] TTetromino_90 = new int[3, 2] { { 1, 0 },      //  * 
                                                     { 1, 1 },      //  * *
                                                     { 1, 0 }};     //  *  

        public int[,] TTetromino_180 = new int[2, 3] { { 1, 1, 1 },   // * * *
                                                      { 0, 1, 0 }};  //   * 

        public int[,] TTetromino_270 = new int[3, 2] { { 0, 1 },      //   * 
                                                      { 1, 1 },      // * *
                                                      { 0, 1 }};     //   *  
        
        public int[,] STetromino_0 = new int[2, 3] { { 0, 1, 1 },    //   * *
                                                    { 1, 1, 0 }};   // * *

        public int[,] STetromino_90 = new int[3, 2] { { 1, 0 },      // *
                                                     { 1, 1 },      // * *
                                                     { 0, 1 }};     //   *
        
        public int[,] ZTetromino_0 = new int[2, 3] { { 1, 1, 0 },    // * *
                                                    { 0, 1, 1 }};   //   * *

        public int[,] ZTetromino_90 = new int[3, 2] { { 0, 1 },      //   *
                                                     { 1, 1 },      // * *
                                                     { 1, 0 }};     // *
       
        public int[,] JTetromino_0 = new int[2, 3] { { 1, 0, 0 },    // * 
                                                    { 1, 1, 1 }};   // * * *

        public int[,] JTetromino_90 = new int[3, 2] { { 1, 1 },      // * * 
                                                     { 1, 0 },      // *
                                                     { 1, 0 }};     // * 

        public int[,] JTetromino_180 = new int[2, 3] { { 1, 1, 1 },   // * * * 
                                                      { 0, 0, 1 }};  //     *

        public int[,] JTetromino_270 = new int[3, 2] { { 0, 1 },      //   * 
                                                      { 0, 1 },      //   *
                                                      { 1, 1 }};     // * *

       
        public int[,] LTetromino_0 = new int[2, 3] { { 0, 0, 1 },    //     * 
                                                    { 1, 1, 1 }};   // * * *

        public int[,] LTetromino_90 = new int[3, 2] { { 1, 0 },      // *  
                                                     { 1, 0 },      // *
                                                     { 1, 1 }};     // * *

        public int[,] LTetromino_180 = new int[2, 3] { { 1, 1, 1 },   // * * * 
                                                      { 1, 0, 0 }};  // *

        public int[,] LTetromino_270 = new int[3, 2] { { 1, 1 },      // * * 
                                                       { 0, 1 },      //   *
                                                       { 0, 1 }};     //   *

        public object TetrominoTask { get; private set; }
        #endregion

        public TetrisWindow()
        {
            InitializeComponent();
            gameSpeed = GameSpeed;
           
            KeyDown += KeyPressed;
            
            gameTimer = new DispatcherTimer();
            gameTimer.Interval = new TimeSpan(0, 0, 0, 0, gameSpeed);
            gameTimer.Tick += GameTimer_Tick;
            tetrisGridColumn = tetrisGrid.ColumnDefinitions.Count;
            tetrisGridRow = tetrisGrid.RowDefinitions.Count;
            shapeRandom = new Random();
            currentShapeNumber = shapeRandom.Next(1, 8);
            nextShapeNumber = shapeRandom.Next(1, 8);
            nextTxt.Visibility = levelTxt.Visibility = GameOverTxt.Visibility = Visibility.Collapsed;
            
            soundList.Add(new System.Media.SoundPlayer(Properties.Resources.collided));
            soundList.Add(new System.Media.SoundPlayer(Properties.Resources.deleteLine));
        }

       
        private void KeyPressed(object sender, KeyEventArgs e)
        {
            if (!gameTimer.IsEnabled) { return; }
            switch (e.Key)
            {
                case Key.Up:
                    rotation += 90;
                    rotation %= 360;
                    RotateShape(rotation);
                    break;
                case Key.Down:
                    downPos++;
                    break;
                case Key.Right:
                   
                    CheckCollision();
                    if (!rightCollided) { leftPos++; }
                    rightCollided = false;
                    break;
                case Key.Left:
                   
                    CheckCollision();
                    if (!leftCollided) { leftPos--; }
                    leftCollided = false;
                    break;
            }
            MoveShape();
        }

      
        private void RotateShape(int rotation_Angle)
        {
          
            if (RotationCollided(rotation))
            {
                rotation -= 90;
                return;
            }

            if (arrayTetrominos[currentShapeNumber].IndexOf("I") == 0)
            {
                if (rotation > 90)
                {
                    rotation = 0;
                    rotation_Angle = 0;
                }
                currentTetromino = GetVariableByString("ITetromino_" + rotation_Angle);
            }
            else if (arrayTetrominos[currentShapeNumber].IndexOf("T") == 0)
            {
                currentTetromino = GetVariableByString("TTetromino_" + rotation_Angle);
            }
            else if (arrayTetrominos[currentShapeNumber].IndexOf("S") == 0)
            {
                if (rotation_Angle > 90) { rotation_Angle = rotation = 0; }
                currentTetromino = GetVariableByString("STetromino_" + rotation_Angle);
            }
            else if (arrayTetrominos[currentShapeNumber].IndexOf("Z") == 0)
            {
                if (rotation_Angle > 90) { rotation_Angle = rotation = 0; }
                currentTetromino = GetVariableByString("ZTetromino_" + rotation_Angle);
            }
            else if (arrayTetrominos[currentShapeNumber].IndexOf("J") == 0)
            {
                currentTetromino = GetVariableByString("JTetromino_" + rotation_Angle);
            }
            else if (arrayTetrominos[currentShapeNumber].IndexOf("L") == 0)
            {
                currentTetromino = GetVariableByString("LTetromino_" + rotation_Angle);
            }
            else if (arrayTetrominos[currentShapeNumber].IndexOf("O") == 0) 
            {
                return;
            }

            isRotated = true;
            AddShape(currentShapeNumber, leftPos, downPos);
        }


     
        private void GameTimer_Tick(object sender, EventArgs e)
        {
            downPos++;
            MoveShape();
            if (gameSpeedCounter >= levelScale)
            {
                if (gameSpeed >= 50)
                {
                    gameSpeed -= 50;
                    gameLevel++;
                    levelTxt.Text = "Level: " + gameLevel.ToString();
                }
                else { gameSpeed = 50; }
                gameTimer.Stop();
                gameTimer.Interval = new TimeSpan(0, 0, 0, 0, gameSpeed);
                gameTimer.Start();
                gameSpeedCounter = 0;
            }
            gameSpeedCounter += (gameSpeed / 1000f);
            
        }

        private void RemoveShape()
        {
            List<Rectangle> rectanglesToRemove = new List<Rectangle>();
            foreach (UIElement element in tetrisGrid.Children)
            {
                if (element is Rectangle rectangle && rectangle.Name.StartsWith("moving_"))
                {
                    rectanglesToRemove.Add(rectangle);
                }
            }

            foreach (Rectangle rectangle in rectanglesToRemove)
            {
                tetrisGrid.Children.Remove(rectangle);
            }
        }
        
      
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (isGameOver)
            {
                tetrisGrid.Children.Clear();
                nextShapeCanvas.Children.Clear();
                GameOverTxt.Visibility = Visibility.Collapsed;
                isGameOver = false;
            }
            if (!gameTimer.IsEnabled)
            {
                if (!gameActive) { scoreTxt.Text = "0"; leftPos = 3; AddShape(currentShapeNumber, leftPos,0,true); }
                nextTxt.Visibility = levelTxt.Visibility = Visibility.Visible;
                levelTxt.Text = "Level: " + gameLevel.ToString();
                gameTimer.Start();
                startStopBtn.Content = "Stop Game";
                gameActive = true;
            }
            else
            {
                gameTimer.Stop();
                StartStopBtn.Content = "Start Game";
            }
        }

       
        private void AddShape(int shapeNumber, int _left = 0, int _down = 0, bool isNew = false)
        {
            
            RemoveShape();
            currentTetrominoRow = new List<int>();
            currentTetrominoColumn = new List<int>();
            Rectangle square = null;
            if (isNew)
            {
                rotation = 0;
                currentTetromino = GetVariableByString(arrayTetrominos[shapeNumber].ToString());
            }
            int firstDim = currentTetromino.GetLength(0);
            int secondDim = currentTetromino.GetLength(1);
            currentTetrominoWidth = secondDim;
            currentTetrominoHeight = firstDim;
          
            if (currentTetromino == ITetromino_90)
            {
                currentTetrominoWidth = 1;
            }
            else if (currentTetromino == ITetromino_0) { currentTetrominoHeight = 1; }
          
            for (int row = 0; row < firstDim; row++)
            {
                for (int column = 0; column < secondDim; column++)
                {
                    int bit = currentTetromino[row, column];
                    if (bit == 1)
                    {
                        square = getBasicSquare(shapeColor[shapeNumber-1]);
                        tetrisGrid.Children.Add(square);
                        square.Name = "moving_" + Grid.GetRow(square) + "_" + Grid.GetColumn(square);
                        if (_down >= tetrisGrid.RowDefinitions.Count - currentTetrominoHeight)
                        {
                            _down = tetrisGrid.RowDefinitions.Count - currentTetrominoHeight;
                        }
                        Grid.SetRow(square, rowCount + _down);
                        Grid.SetColumn(square, columnCount + _left);
                        currentTetrominoRow.Add(rowCount + _down);
                        currentTetrominoColumn.Add(columnCount + _left);

                    }
                    columnCount++;
                }
                columnCount = 0;
                rowCount++;
            }
            columnCount = 0;
            rowCount = 0;
            if (!nextShapeDrawed)
            {
                DrawNextShape(nextShapeNumber);
            }
        }
        
        private void MoveShape()
        {
            leftCollided = false;
            rightCollided = false;

            
            CheckCollision();
            if (leftPos > (tetrisGridColumn - currentTetrominoWidth))
            {
                leftPos = (tetrisGridColumn - currentTetrominoWidth);
            }
            else if (leftPos < 0) { leftPos = 0; }

            if (bottomCollided)
            {
                ShapeStopped();
                return;
            }
            AddShape(currentShapeNumber, leftPos, downPos);
        }

       
        private bool RotationCollided(int rotation_Angle)
        {
            if (CheckCollided(0, currentTetrominoWidth - 1)) { return true; } 
            else if (CheckCollided(0, -(currentTetrominoWidth - 1))) { return true; } 
            else if (CheckCollided(0, -1)) { return true; } 
            else if (CheckCollided(-1, currentTetrominoWidth - 1)) { return true; } 
            else if (CheckCollided(1, currentTetrominoWidth - 1)) { return true; } 
            return false;
        }

       
        private void CheckCollision()
        {
            bottomCollided = CheckCollided(0, 1);
            leftCollided = CheckCollided(-1, 0);
            rightCollided = CheckCollided(1, 0);
        }

        
        private bool CheckCollided(int leftRightOffset, int bottomOffset)
        {
            Rectangle movingSquare;
            int squareRow = 0;
            int squareColumn = 0;
            for (int i = 0; i <= 3; i++)
            {
                squareRow = currentTetrominoRow[i];
                squareColumn = currentTetrominoColumn[i];
                try
                {
                    movingSquare = (Rectangle)tetrisGrid.Children
                    .Cast<UIElement>()
                    .FirstOrDefault(e => Grid.GetRow(e) == squareRow + bottomOffset && Grid.GetColumn(e) == squareColumn + leftRightOffset);
                    if (movingSquare != null)
                    {
                        if (movingSquare.Name.IndexOf("arrived") == 0)
                        {
                            return true;
                        }
                    }
                }
                catch { }
            }
            if (downPos > (tetrisGridRow - currentTetrominoHeight)) { return true; }
            else
            {
                return false;   
            }
        }

      
        private void ShapeStopped()
        {
            gameTimer.Stop();
            PlaySound(0);
            if (downPos <= 2)
            {
                GameOver();
                return;
            }

            int index = 0;
            foreach(var element in tetrisGrid.Children)
            {
                if (element is Rectangle)
                {
                    Rectangle square = (Rectangle)element;
                    if (square.Name.IndexOf("moving_") == 0)
                    {
                        square.Name = square.Name.Replace("moving_", "arrived_");
                    }
                }
            }

            index = 0;
            int lastRemovedLine = -1;
            CheckForCompletedRows();
            currentShapeNumber = nextShapeNumber;
            nextShapeNumber = shapeRandom.Next(1, 8);
            nextShapeDrawed = false;
            leftPos = 3;
            downPos = 0;
            AddShape(currentShapeNumber, leftPos,0, true);
            gameTimer.Start();
        }

      
        private void CheckForCompletedRows()
        {
            int rowCounter = 0;
            for (int rowIndex = tetrisGridRow - 1; rowIndex >= 0; rowIndex--)
            {
                var squaresInRow = tetrisGrid.Children
                    .Cast<UIElement>()
                    .Where(e => Grid.GetRow(e) == rowIndex && e is Rectangle);

                if (squaresInRow.Count() == tetrisGridColumn)
                {
                    List<UIElement> toRemove = new List<UIElement>();
                    
                    foreach (var square in squaresInRow)
                    {
                        toRemove.Add(square);
                    }

                    foreach(var square in toRemove)
                    {
                        tetrisGrid.Children.Remove(square);
                    }

                   
                    foreach (var square in tetrisGrid.Children
                                 .Cast<UIElement>()
                                 .Where(e => Grid.GetRow(e) <= rowIndex))
                    {
                        Grid.SetRow(square, Grid.GetRow(square) + 1);
                    }

                    rowCounter++;
                    rowIndex++;
                }
            }

            ScoreCount(rowCounter);
        }

        
        private void ScoreCount(int rowCounter)
        {
            if (rowCounter == 0) { return; }
            int score = rowCounter * 100 * gameLevel;
            gameScore += score;

            scoreTxt.Text = gameScore.ToString();
        }
        
        private void reset()
        {
            downPos = 0;
            leftPos = 3;
            isRotated = false;
            rotation = 0;
            currentShapeNumber = nextShapeNumber;
            if (!isGameOver) { AddShape(currentShapeNumber, leftPos); }
            nextShapeDrawed = false;
            shapeRandom = new Random();
            nextShapeNumber = shapeRandom.Next(1, 8);
            bottomCollided = false;
            leftCollided = false;
            rightCollided = false;
            currentTetromino = null;
            currentShapeNumber = shapeRandom.Next(1, 8);
            nextShapeNumber = shapeRandom.Next(1, 8);
        }
        
        private void GameOver()
        {
            isGameOver = true;
            reset();
            startStopBtn.Content = "Start Game";
            GameOverTxt.Visibility = Visibility.Visible;
            rowCount = 0;
            columnCount = 0;
            leftPos = 0;
            gameSpeedCounter = 0;
            gameSpeed = GameSpeed;
            gameLevel = 1;
            gameActive = false;
            gameScore = 0;
            nextShapeDrawed = false;
            currentTetromino = null;
            currentShapeNumber = shapeRandom.Next(1, 8);
            nextShapeNumber = shapeRandom.Next(1, 8);
            gameTimer.Interval = new TimeSpan(0, 0, 0, 0, gameSpeed);
        }
        
        private void PlaySound(int index)
        {
            soundList[index].Stop();
            soundList[index].Play();
        }
        
        private void DrawNextShape(int shapeNumber)
        {
            nextShapeCanvas.Children.Clear();
            Rectangle square = null;
            int firstDim = 0;
            int secondDim = 0;
            int shapeOffset = 0;
            int rowOffset = 0;
            int columnOffset = 0;
            if (shapeNumber == 1)
            {
                firstDim = 2;
                secondDim = 2;
                shapeOffset = 1;
            }
            else if (shapeNumber == 2)
            {
                firstDim = 1;
                secondDim = 4;
                shapeOffset = 0;
                rowOffset = 1;
            }
            else if (shapeNumber >= 3 && shapeNumber <= 7)
            {
                firstDim = 2;
                secondDim = 3;
                shapeOffset = 1;
                rowOffset = 0;
            }
            for (int row = 0; row < firstDim; row++)
            {
                for (int column = 0; column < secondDim; column++)
                {
                    int bit = GetVariableByString(arrayTetrominos[shapeNumber].ToString())[row + rowOffset, column + columnOffset];
                    if (bit == 1)
                    {
                        square = getBasicSquare(shapeColor[shapeNumber - 1]);
                        nextShapeCanvas.Children.Add(square);
                        Grid.SetRow(square, row + shapeOffset);
                        Grid.SetColumn(square, column + shapeOffset);
                    }
                }
            }
            nextShapeDrawed = true;
        }
        
        private int[,] GetVariableByString(string variableName)
        {
            return (int[,])this.GetType().GetField(variableName).GetValue(this);
        }
        
        private Rectangle getBasicSquare(Color color)
        {
            Rectangle rectangle = new Rectangle();
            rectangle.Height = rectangle.Width = 20;
            SolidColorBrush brush = new SolidColorBrush(color);
            rectangle.Stroke = Brushes.Black;
            rectangle.Fill = brush;
            return rectangle;
        }
    }
}