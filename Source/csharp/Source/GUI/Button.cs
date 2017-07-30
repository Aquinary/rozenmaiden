using System;
using System.Windows.Forms;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace RozenMaiden.GUI
{
    public class Button
    {
        #region Fields

        private string _path;
        private Texture2D _texture;
        private Vector2 _position;
        private Vector2 _textPosition;
        private Vector2 _size;
        private string _text;
        private ButtonState _state;
        private SpriteFont _font;
        private Color _textColor;

        private bool _isMouseIn;
        private bool _isMouseOut;
        private bool _isCapture;

        private MouseState _mousePrevState = Mouse.GetState();

        #endregion

        #region Properties

        /// <summary>
        /// Путь к текстуре
        /// </summary>      
        public string Path // путь к текстуре
        {
            get { return _path; }
            set { _path = value; }
        }

        /// <summary>
        /// Контейнер для текстуры
        /// </summary>
        public Texture2D Texture
        {
            get { return _texture; }
            set { _texture = value; }
        }

        /// <summary>
        /// Позиция элемента
        /// </summary>
        public Vector2 Position
        {
            get { return _position; }
            set { _position = value; }
        }

        /// <summary>
        /// Позиция текста внутри элемента
        /// </summary>
        public Vector2 TextPosition // позиция текста внутри кнопки
        {
            get { return _textPosition; }
            set { _textPosition = value; }
        }

        /// <summary>
        /// Размер элемента
        /// </summary>
        public Vector2 Size
        {
            get { return _size; }
            set { _size = value; }
        }

        /// <summary>
        /// Текст на элементе
        /// </summary>
        public string Text 
        {
            get { return _text; }
            set { _text = value; }
        }

        /// <summary>
        /// Состояние элемента
        /// </summary>
        public ButtonState State
        {
            get { return _state; }
            set { _state = value; }
        }

        /// <summary>
        /// Контейнер для шрифта
        /// </summary>
        public SpriteFont Font 
        {
            get { return _font; }
            set { _font = value; }
        }

        /// <summary>
        /// Цвет текста
        /// </summary>
        public Color TextColor
        {
            get { return _textColor; }
            set { _textColor = value; }
        }
        #endregion

        #region Events

        public event EventHandler MouseUpHandler;
        public event EventHandler MouseDownHandler;
        public event EventHandler MouseOutHandler;
        public event EventHandler MouseInHandler;

        #endregion

        #region EventMethods

        /// <summary>
        /// Зажатие ПКМ над элементом
        /// </summary>
        private void OnMouseDown()
        {
            EventHandler tempHandler = MouseDownHandler;
            if (tempHandler != null)
            {
                tempHandler(this, EventArgs.Empty); // вызываем событие
            }
            _state = ButtonState.Click;
        }

        /// <summary>
        /// Разжатие ПКМ над элементом
        /// </summary>
        private void OnMouseUp()
        {
            EventHandler tempHandler = MouseUpHandler;
            if (tempHandler != null)
            {
                tempHandler(this, EventArgs.Empty);
            }
            _state = ButtonState.Hover;
        }

        /// <summary>
        /// Вход курсора в зону элемента
        /// </summary>
        private void OnMouseIn()
        {
            if (!_isMouseIn) // если событие не зарегистрировано
            {
                _isCapture = true;
                EventHandler tempHandler = MouseInHandler;
                if (tempHandler != null)
                {
                    tempHandler(this, EventArgs.Empty);
                }
                _isMouseIn = true; // регистрируем его
                _isMouseOut = false;
                _state = ButtonState.Hover;
            }
        }

        /// <summary>
        /// Выход курсора из зоны элемента
        /// </summary>
        private void OnMouseOut()
        {
            if (!_isMouseOut)
            {
                _isCapture = false;
                EventHandler tempHandler = MouseOutHandler;
                if (tempHandler != null)
                {
                    tempHandler(this, EventArgs.Empty);
                }
                _isMouseIn = false;
                _isMouseOut = true;
                _state = ButtonState.Normal;
            }
        }
        #endregion

        #region Enums

        /// <summary>
        /// Определение состояния элемента
        /// </summary>
        public enum ButtonState 
        {
            Normal = 0,
            Hover = 1,
            Click = 2
        }

        #endregion

        #region Construct

        public Button(string filePath)
        {
            this.Path = filePath;
            this.Text = "";
            this._isMouseOut = true;
            try
            {
                this.Font = Program.Game.Content.Load<SpriteFont>("Font\\Button");
                this.Texture = Program.Game.Content.Load<Texture2D>(this.Path);
            }
            catch (Exception ex)
            {
                string tempError = ex.Message + "\n" + this.Path; // формирование строчки с текстом ошибки
                MessageBox.Show(tempError, "Error!");
            }
        }

        #endregion

        #region PublicMethods

        /// <summary>
        /// Обновление логики
        /// </summary>
        public void Update()
        {
            // Получение информации о позиции мыши
            MouseState mouseState = Mouse.GetState();
            Point mousePosition = new Point(mouseState.X, mouseState.Y);

            // Получение информации о положении элемента
            Rectangle buttonRectangle = new Rectangle
            (
                (int) this.Position.X, (int) this.Position.Y,
                (int) this.Size.X, (int) this.Size.Y
            );

            // Проверка пересечения курсора мыши и элемента
            if (buttonRectangle.Contains(mousePosition))
            {
                OnMouseIn();
                if (_isCapture == true) // Фиксирование фокуса на элементе
                {
                    if (mouseState.LeftButton == Microsoft.Xna.Framework.Input.ButtonState.Pressed
                        && _mousePrevState.LeftButton != Microsoft.Xna.Framework.Input.ButtonState.Pressed)
                    {
                        OnMouseDown();
                    }
                    if (_mousePrevState.LeftButton == Microsoft.Xna.Framework.Input.ButtonState.Pressed
                        && mouseState.LeftButton == Microsoft.Xna.Framework.Input.ButtonState.Released)
                    {
                        OnMouseUp();
                    }
                }
            }
            else
            {
                OnMouseOut();
            }
            _mousePrevState = mouseState;
        }

        /// <summary>
        /// Отрисовка элемента
        /// </summary>
        public void Draw()
        {
            Rectangle position = new Rectangle(0, (int) this.State * (int) this.Size.Y, (int) this.Size.X,
                (int) this.Size.Y);

            Vector2 textPositions = new Vector2(this.TextPosition.X + this.Position.X,
                this.TextPosition.Y + this.Position.Y);
         
            Program.Game.SpriteBatch.Begin();
            Program.Game.SpriteBatch.Draw(this.Texture, this.Position, position, Color.White);
            Program.Game.SpriteBatch.DrawString(this.Font, this.Text, textPositions, Color.White);
            Program.Game.SpriteBatch.End();
        }

        #endregion
    }
}





