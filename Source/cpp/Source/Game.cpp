#include "Header/Application.hpp"
#include "Header/Game.hpp"
#include "Header/AppScreen/MainMenu.hpp"

using namespace Alice;

 
void Game::Init()
{
    Application::Init(); 
    MainMenu::Init();
}

void Game::Quit()
{
    Application::Quit();
    MainMenu::Quit();
}

void Game::Update()
{
    Application::Update();
    switch (Game::State)
	{
		case Game::CurrentState::MainMenu:
            
			MainMenu::Update();
		break;
        default:
            Application::Quit();
        break;
	}
}

void Game::Draw()
{
    Application::Clear();
    switch (Game::State)
	{
		case Game::CurrentState::MainMenu:  
            MainMenu::Draw();
		break;
		default:
            Application::Quit();
        break;
	}
    Application::Display();
}


