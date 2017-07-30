#include "Header/Application.hpp"
#include "Header/Game.hpp"
#include "Header/Entity.hpp"
#include "Header/Player.hpp"

using namespace Alice;

void Player::UserEvent()
{
	
	sf::Vector2f tempDirection = Player::GetDirection();
	if (sf::Keyboard::isKeyPressed(sf::Keyboard::Left))
	{
		tempDirection.x -= Player::GetSpeed();
	} else

	if (sf::Keyboard::isKeyPressed(sf::Keyboard::Right))
	{
		tempDirection.x += Player::GetSpeed();
	} else

	if (sf::Keyboard::isKeyPressed(sf::Keyboard::Up))
	{
		tempDirection.y -= Player::GetSpeed();
	} else

	if (sf::Keyboard::isKeyPressed(sf::Keyboard::Down))
	{
		tempDirection.y += Player::GetSpeed();
	}
	Player::SetDirection({tempDirection.x,tempDirection.y});
}
