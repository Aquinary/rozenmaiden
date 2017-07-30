#ifndef ENTITY_HPP
#define ENTITY_HPP

namespace Alice
{
	class Entity
	{
		private:
			/* Приватные поля */
			sf::Texture _texture;
			sf::Sprite _sprite;
			std::string _name;
			sf::Vector2f _position;
			sf::Vector2f _direction;
			sf::Vector2i _size;
			sf::Vector2i _hitboxSize;
			float _speed;
			float _timeStep;
			sf::Vector2f _frame;
			
		public:
			/* Конструктор-деструктор */
			Entity(); 
			Entity(std::string filePath); 

			/* Сеттеры */
			void SetTexture(std::string filePath);
			void SetName(std::string name);
			void SetPosition(sf::Vector2f position);
			void SetDirection(sf::Vector2f direction);
			void SetSize(sf::Vector2i size);
			void SetHitboxSize(sf::Vector2i hitboxSize);
			void SetSpeed(float speed);
			void SetTimeSteep(float timeStep);

			/* Геттеры */
			sf::Sprite GetTexture(void);
			std::string GetName(void);
			sf::Vector2f GetPosition(void);
			sf::Vector2f GetDirection(void);
			sf::Vector2i GetSize(void);
			sf::Vector2i GetHitboxSize(void);
			float GetSpeed(void);
			float GetTimeSteep(void);

			/* Логика */
			void Update(void);
			void Draw(void);
			virtual void UserEvent(void);
		protected:
			void PositionUpdate(void);
			void AnimationUpdate(void);
	};
}

#endif //!ENTITY_HPP