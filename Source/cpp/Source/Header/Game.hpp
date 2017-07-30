#ifndef GAME_HPP
#define GAME_HPP

namespace Alice
{
	class Game
	{
		public:
			enum CurrentState
			{
				None,
				MainMenu,
				GameScreen
			} static State;

			static void Init(void);
			static void Quit(void);

			static void Update(void);
		
			static void Draw(void);
	};
}

#endif