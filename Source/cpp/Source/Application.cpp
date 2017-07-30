#include "Header/Application.hpp"

using namespace Alice;

sf::RenderWindow Application::Window;
sf::Event Application::Event;

bool Application::IsExecute;

void Application::Init() 
{
	Application::Window.create
		(
			sf::VideoMode(Application::WIDTH, Application::HEIGHT),
			"Rozen Maiden"
		);

	Application::Window.setActive();
	Application::IsExecute = true;
	Application::Window.setFramerateLimit(60);
	Application::Window.setVerticalSyncEnabled(true);
}

void Application::Quit()
{
	Application::IsExecute = false;
}

void Application::Update()
{
	while (Application::Window.pollEvent(Application::Event))
	{
		if (Application::Event.type == sf::Event::Closed)
		{
			std::exit(0);
		}		
	}

	
}

void Application::Clear()
{
	Application::Window.clear(sf::Color::Black);
}

void Application::Display()
{
	Application::Window.display();
}
