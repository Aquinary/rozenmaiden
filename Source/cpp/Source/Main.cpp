#include "Header/Application.hpp"
#include "Header/Game.hpp"

using namespace Alice;

int main(void)
{
    Game::Init();  

    while (Application::IsExecute)
    {
		Game::Update();
        Game::Draw();
    }

    Game::Quit();
	
	return 0;
}
