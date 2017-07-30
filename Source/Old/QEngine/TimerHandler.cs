using System;

namespace Alice
{
    class TimerHandler
    {
        // Переменная под тики
        private long m_tickStart = 0L;

        public TimerHandler()
        {
            // Сброс таймера
            Reset();
        }

        public void Reset()
        {
            // Сбрасываем таймер
            m_tickStart = DateTime.Now.Ticks;
        }

        public float GetMilliseconds()
        {
            // Получаем количество миллисекунд
            return ((float)(new TimeSpan(DateTime.Now.Ticks - m_tickStart).TotalMilliseconds));
        }
    }
}
