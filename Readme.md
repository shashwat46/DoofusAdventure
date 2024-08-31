# Doofus Adventure Game

## Table of Contents
## Shashwat Singh 21BCI0357

-   [Introduction](#introduction)
-   [Features](#features)
-   [Getting Started](#getting-started)
    -   [Prerequisites](#prerequisites)
    -   [Installation](#installation)
    -   [Running the Game](#running-the-game)
-   [Gameplay](#gameplay)
    -   [Controls](#controls)
    -   [Scoring](#scoring)
-   [Technical Details](#technical-details)
    -   [Camera System](#camera-system)
    -   [Pulpit Spawning](#pulpit-spawning)
    -   [Doofus Movement](#doofus-movement)
    -   [JSON Integration](#json-integration)
-   [Contributing](#contributing)
-   [License](#license)
-   [Acknowledgments](#acknowledgments)

## Introduction

This Game was created as a part of the HitWicket Recruitment Drive 2024. Creator  - Shashwat Singh 21BCI0357
Welcome to **Doofus Adventure Game**, an engaging and challenging game where you guide Doofus, a curious cube, across disappearing platforms called Pulpits. Your goal is to help Doofus walk on as many Pulpits as possible without falling. This game is designed to test your reflexes and strategic thinking as you navigate the ever-changing environment.

## Features

-   **Dynamic Pulpit Spawning**: Only two Pulpits exist at any given time, and they disappear after a random interval.
-   **Challenging Gameplay**: Pulpits appear adjacent to the last one, but not in the same position, making it tricky to stay on course.
-   **Real-Time Score Tracking**: Keep track of how many Pulpits Doofus successfully walks on.
-   **JSON-Driven Configuration**: Game parameters are configurable via a JSON file, allowing easy customization.

## Getting Started

### Prerequisites

To run this project, you'll need:

-   **Unity 2022.3+**: Download Unity
-   **C# Knowledge**: Familiarity with scripting in Unity using C#.

### Installation

1.  Clone this repository to your local machine:
    
    bash
    
    Copy code
    
    `git clone https://github.com/shashwat46/HW_Test_2024.git` 
    
2.  Open the project in Unity:
    
    -   Open Unity Hub.
    -   Click on "Open Project".
    -   Navigate to the cloned repository folder and select it.

### Running the Game

1.  Ensure all required assets are imported and scripts are compiled without errors.
2.  Click on the **Play** button in Unity’s Editor to start the game.

## Gameplay

### Controls

-   **Movement**: Use the arrow keys or `W`, `A`, `S`, `D` keys to move Doofus left, right, forward, or backward.
-   **Objective**: Walk on as many Pulpits as possible without falling off.

### Scoring

-   **Score**: Your score increases by one for each Pulpit Doofus successfully walks on.
-   **Game Over**: The game ends if Doofus falls off the last Pulpit.

## Technical Details

### Camera System

The camera follows Doofus from a fixed offset, ensuring that the player always has a clear view of the action. The camera adjusts its position as Doofus moves across the platforms.

### Pulpit Spawning

Pulpits are dynamically spawned at runtime. Only two Pulpits exist at a time, and they disappear after a random interval specified in the `DoofusDiary.json` file. New Pulpits spawn adjacent to the last one, but never in the same position.

### Doofus Movement

Doofus moves with a smooth and responsive control system based on Unity’s Rigidbody physics. Movement is controlled via input from the arrow keys or `W`, `A`, `S`, `D` keys.

### JSON Integration

The game reads configuration values such as Doofus’s speed and Pulpit timings from a JSON file (`DoofusDiary.json`). This allows for easy adjustments and customization of game parameters without modifying the code.

## Contributing

We welcome contributions to enhance the Doofus Adventure Game. To contribute:

1.  Fork this repository.
2.  Create a feature branch (`git checkout -b feature/YourFeature`).
3.  Commit your changes (`git commit -am 'Add new feature'`).
4.  Push to the branch (`git push origin feature/YourFeature`).
5.  Open a Pull Request.

## License

This project is licensed under the MIT License. See the LICENSE file for details.

## Acknowledgments

-   **Unity Technologies**: For providing the game engine used to create this project.
-   **YouTube Channel**: [Link to video](https://youtu.be/1MdT5c7FuRk) for gameplay inspiration.
-   **Contributors**: Thank you to everyone who has contributed to this project