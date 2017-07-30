#ifndef APPLICATION_HPP
#define APPLICATION_HPP

#include <iostream>
#include <thread>
#include <SFML/Graphics.hpp>
#include <SFML/System.hpp>

namespace Alice
{
	class Application
	{
		public:
			static const int WIDTH = 640;
			static const int HEIGHT = 480;

			static sf::RenderWindow Window;
			static sf::Event Event;

			static bool IsExecute;

			static void Init(void);
			static void Quit(void);

			static void Update(void);

			static void Clear(void);
			static void Display(void);
	};
}

#endif //!APPLICATION_HPP