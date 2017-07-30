#ifndef MAINMENU_HPP
#define MAINMENU_HPP

namespace Alice
{
	class MainMenu
	{
		public:
			static sf::Texture TextureTileMap;
			static sf::Sprite SpritesTileMap;

			static class Player *player;  

			static class Terrain *terrain;

			static void Init();
			static void Quit();

			static void Update();
			
			static void Draw();
	};
}

#endif //!MAINMENU_HPP