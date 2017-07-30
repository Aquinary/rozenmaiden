#include "../Header/Application.hpp"
#include "../Header/Game.hpp"
#include "../Header/Entity.hpp"
#include "../Header/Player.hpp"
#include "../Header/Terrain.hpp"
#include "../Header/AppScreen/MainMenu.hpp"

using namespace Alice; 

Game::CurrentState Game::State;

sf::Texture MainMenu::TextureTileMap;
sf::Sprite MainMenu::SpritesTileMap;

class Player *MainMenu::player = new Player;  
class Terrain *MainMenu::terrain = new Terrain;


void MainMenu::Init() 
{
	Game::State = Game::CurrentState::MainMenu; 
	
	MainMenu::player->SetTexture("Bin/Data/Image/Entity/Suiginto.png");
	MainMenu::player->SetPosition({120,0});
	MainMenu::player->SetSize({32,32}); 
	MainMenu::player->SetSpeed(1);
	MainMenu::player->SetTimeSteep(0.07f);

	MainMenu::terrain->SetTexture("Bin/Data/Image/TileMap.png");
	MainMenu::terrain->SetSize({32,32});

}
 
void MainMenu::Quit()
{
	delete MainMenu::player;
	Game::State = Game::CurrentState::None; 
}

void MainMenu::Update()
{
	//sf::Thread thr(&Player::UserEvent, player);
	//thr.launch();
	MainMenu::player->Update();
}

void MainMenu::Draw()
{
	MainMenu::player->Draw(); 
	Application::Window.draw(MainMenu::terrain->GetTexture(27));
}


 