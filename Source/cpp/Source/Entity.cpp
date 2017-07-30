#include "Header/Application.hpp"
#include "Header/Game.hpp"
#include "Header/Entity.hpp"

using namespace Alice;

/* Конструктор */

Entity::Entity()
{
	
}

Entity::Entity(std::string filePath)
{
	this->SetTexture(filePath.c_str());
}

/* !Конструктор */

/* Сеттеры */
void Entity::SetTexture(std::string filePath)
{
	this->_texture.loadFromFile(filePath);
	this->_sprite.setTexture(this->_texture);
}

void Entity::SetName(std::string name)
{
	this->_name = name;
}

void Entity::SetPosition(sf::Vector2f position)
{
	this->_position = position;
	this->_sprite.setPosition(this->_position.x, this->_position.y);
}

void Entity::SetDirection(sf::Vector2f direction)
{
	this->_direction = direction;
}

void Entity::SetSize(sf::Vector2i size)
{
	this->_size = size;
}

void Entity::SetHitboxSize(sf::Vector2i hitboxSize)
{
	this->_hitboxSize = hitboxSize;
}

void Entity::SetSpeed(float speed)
{
	this->_speed = speed;
}

void Entity::SetTimeSteep(float timeStep)
{
	this->_timeStep = timeStep;
}

/* !Сеттеры */

/* Геттеры */

sf::Sprite Entity::GetTexture(void)
{
	sf::IntRect tempRect
		(
			(int)this->_frame.x * this->_size.x, this->_frame.y * this->_size.y,
			this->_size.x, this->_size.y
		);

	this->_sprite.setTextureRect(sf::IntRect(tempRect));

	return this->_sprite;
}

std::string Entity::GetName(void)
{
	return this->_name;
}

sf::Vector2f Entity::GetPosition(void)
{
	return this->_position;
}

sf::Vector2f Entity::GetDirection(void)
{
	return this->_direction;
}

sf::Vector2i Entity::GetSize(void)
{
	return this->_size;
}

sf::Vector2i Entity::GetHitboxSize(void)
{
	return this->_hitboxSize;
}

float Entity::GetSpeed(void)
{
	return this->_speed;
}

float Entity::GetTimeSteep(void)
{
	return this->_timeStep;
}

/* !Геттеры */

/* Логика */

void Entity::Update(void)
{
	this->UserEvent();
	this->AnimationUpdate();
	this->PositionUpdate();
}

void Entity::Draw(void)
{
	Application::Window.draw(Entity::GetTexture());
}

void Entity::PositionUpdate()
{
	sf::Vector2f tempDirection;
	sf::Vector2f tempPosition;

	tempDirection = this->GetDirection();
	tempPosition = this->GetPosition();

	this->SetPosition
		(
			{
				tempPosition.x + tempDirection.x,
				tempPosition.y + tempDirection.y
			}
		);

	this->SetDirection({0,0});
}

void Entity::AnimationUpdate()
{
	sf::Vector2f tempDirection;
	tempDirection = this->GetDirection();

	if (tempDirection.y < 0) { this->_frame.y = 3; }
	if (tempDirection.x < 0) { this->_frame.y = 1; }
	if (tempDirection.y > 0) { this->_frame.y = 0; }
	if (tempDirection.x > 0) { this->_frame.y = 2; }

	if (tempDirection.x != 0 || tempDirection.y != 0)
	{
		this->_frame.x += this->GetTimeSteep();
		if (this->_frame.x >= 4) { this->_frame.x = 0; }
	}
	else
	{
		this->_frame.x = 1;
	}

}

void Entity::UserEvent(void) {}

/* !Логика */
