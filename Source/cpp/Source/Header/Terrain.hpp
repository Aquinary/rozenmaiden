#ifndef TERRAIN_HPP
#define TERRAIN_HPP

namespace Alice
{
	class Terrain 
	{
		private:
			sf::Texture _texture;
			sf::Sprite _sprite;
			sf::Vector2i _size;
			
		public:
			/* Конструктор-деструктор */
			Terrain();
			Terrain(std::string filePath);
			~Terrain();

			/* Сеттеры */
			void SetTexture(std::string filePath);
			void SetSize(sf::Vector2i size);

			/* Геттеры */
			sf::Sprite GetTexture(int terrainIndex);
			sf::Vector2i GetSize(void);
	};
}

#endif //!TERRAIN_HPP