TARGET = Bin/Alice
PREFIX = /usr/local/bin
SRCS = Source/Main.cpp Source/Application.cpp Source/Game.cpp Source/Entity.cpp Source/Player.cpp Source/Terrain.cpp Source/AppScreen/MainMenu.cpp
OBJS = $(SRCS:.c=.o)
CFLAGS = -lsfml-graphics -lsfml-window -lsfml-system
CC = g++

.PHONY: all clean install uninstall

all: $(TARGET)
$(TARGET): $(OBJS)
	$(CC) -o $(TARGET) $(OBJS) $(CFLAGS)

.c.o:
	$(CC) $(CFLAGS)  -c $< -o $@

clean:
	rm -rf $(TARGET) $(OBJS)
install:
	install $(TARGET) $(PREFIX)
uninstall:
	rm -rf $(PREFIX)/$(TARGET)