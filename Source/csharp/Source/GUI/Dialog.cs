using System;
using System.Threading;
using System.Windows.Forms;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RozenMaiden.GUI
{
    public class Dialog
    {
        #region Fields

        private string _path; // Путь к текстуре
        private Texture2D _texture; // Контейнер для текстуры
        private Vector2 _position; // Позиция элемента
        private Vector2 _fontPosition; // Позиция текста
        private Vector2 _size; // Размеры элемента
        private string[] _text; // Текст элемента
        private SpriteFont _font; // Контейнер шрифта
        private int _letterSpeed; // Скорость набора текста

        private bool _isVisible; // Видимость элемента
        private char[] _drawingTempText; // Временная переменная
        private string _drawingText; // Переменная, выводящая текст
        private bool _isDrawingText; // Флаг, определяющий, вывелся ли текст или нет

        #endregion

        #region Properties

        public string Path // путь к текстуре
        {
            get { return _path; }
            set { _path = value; }
        }

        public Texture2D Texture // контейнер для текстуры
        {
            get { return _texture; }
            set { _texture = value; }
        }

        public Vector2 Position // позиция кнопки
        {
            get { return _position; }
            set { _position = value; }
        }

        public Vector2 FontPosition // позиция текста внутри кнопки
        {
            get { return _fontPosition; }
            set { _fontPosition = value; }
        }

        public Vector2 Size // размер кнопки
        {
            get { return _size; }
            set { _size = value; }
        }

        public string[] Text // текст кнопки
        {
            get { return _text; }
            set { _text = value; }
        }

        public SpriteFont Font // контейнер под шрифта
        {
            get { return _font; }
            set { _font = value; }
        }

        public bool IsVisible
        {
            get { return _isVisible; }
            set { _isVisible = value; }
        }

        public int LetterSpeed
        {
            get { return _letterSpeed; }
            set { _letterSpeed = value; }
        }

        #endregion

        #region Construct

        public Dialog(string filePath)
        {
            this.Path = filePath;
            this.Position = new Vector2(0,0);
            this.Size = new Vector2(0,0);
            this.LetterSpeed = 50;
            this.Text = new string[10];
            this._drawingText = "";

            try
            {
                this.Font = Program.Game.Content.Load<SpriteFont>("Font\\Dialog");
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

        public void Update()
        {
            if (this.IsVisible)
            {
                if (!_isDrawingText)
                {
                    Thread TLetterAdd = new Thread(LetterAdd); // создаем новый поток
                    string tempText = "";

                    for (int i = 0; i < Text.Length; i++)
                    {
                        tempText += Text[i] + "\n";
                    }

                    _drawingTempText = tempText.ToCharArray(); // конвертируем строку в массив символов
                    FontPosition = new Vector2(Position.X + 19, Position.Y + 10);
                    TLetterAdd.Start(); // запускаем поток
                    _isDrawingText = true;
                }
            }
            else
            {
                _isDrawingText = false;
                _drawingText = "";
                _drawingTempText = "".ToCharArray();
            }
        }

        public void Draw()
        {
            if (this.IsVisible)
            {
                Program.Game.SpriteBatch.Begin();
                Program.Game.SpriteBatch.DrawString(this.Font, _drawingText, this.FontPosition, Color.White);
                Program.Game.SpriteBatch.Draw(Texture, Position, Color.White);
                Program.Game.SpriteBatch.End();
            }
        }

        public void LetterAdd() // функция добовляет по одному симоволу каждый 50 млс.
        {
            for (int i = 0; i < _drawingTempText.Length; i++) // перебираем все символы в массиве
            {
                Thread.Sleep(LetterSpeed);
                if (_drawingTempText.Length > 0)
                {
                    _drawingText += _drawingTempText[i].ToString(); // прибавляем за раз по одному символу к результату
                }
            }
        }

        #endregion
    }
}
