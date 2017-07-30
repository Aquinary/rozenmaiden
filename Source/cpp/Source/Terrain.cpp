#include "Header/Application.hpp"
#include "Header/Terrain.hpp" 

using namespace Alice;

/* Конструктор */

Terrain::Terrain() {};

Terrain::Terrain(std::string filePath)
{
	this->SetTexture(filePath.c_str());
}

/* !Конструктор */

/* Сеттеры */

void Terrain::SetTexture(std::string filePath)
{
	this->_texture.loadFromFile(filePath);
	this->_sprite.setTexture(this->_texture);
}

void Terrain::SetSize(sf::Vector2i size) 
{
	this->_size = size;
}

/* !Сеттеры */

/* Геттеры */

sf::Sprite Terrain::GetTexture(int terrainIndex) 
{
	sf::Vector2i tempSize;

	tempSize = this->GetSize();

	int nx = (1024 / tempSize.x);
	int ny = (1024 / tempSize.y);

	sf::IntRect tempRect
		(
			(terrainIndex % nx) * tempSize.x,
			((terrainIndex/ny)) * tempSize.y, tempSize.x, tempSize.y
		); 
	
	this->_sprite.setTextureRect(sf::IntRect(tempRect));

	return this->_sprite;
}

sf::Vector2i Terrain::GetSize(void)
{
	return this->_size;
}

/* !Геттеры */
