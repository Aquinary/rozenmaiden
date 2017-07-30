#ifndef PLAYER_HPP
#define PLAYER_HPP

namespace Alice
{
	class Player : public Entity
	{
		public:
			Player() : Entity() {};
			Player(std::string filePath) :  Entity(filePath) {};
			void UserEvent(void);
	};
}

#endif //!PLAYER_HPP