# MissionUtils
A BETA helper plugin that patches ``MissionUI`` for it to be easier for modders to add their own mission ("FOCUS >") messages. It currently offers a queue system for displaying messages to players, and aims to not interfere with the default ``MissionUI`` system.

To use it, simply call ``MissionUI.instance.MissionText()`` with the message you want to display as you normally would. The plugin will handle the rest, including queuing them and displaying them in the correct order, with a configurable delay between messages.

![An example of a custom mission sent out to all players](https://raw.githubusercontent.com/PencilFoxStudios/REPOMissionUtils/refs/heads/main/example.png)

## Features
- **Queue System**: The plugin automatically queues messages and displays them in the order they were received. This means you can send multiple messages without worrying about them overlapping or being displayed at the same time.
- **Configurable Delay**: You can configure the delay between messages to suit your needs. This allows you to control how long each message is displayed before the next one is shown.
- **Configure Dead Player Behavior**: You can configure how the plugin behaves when a player dies. By default, the plugin will not show missions to dead players (the way it is in vanilla), but you can change this behavior in the config file, or forcefully override it in your mod's code.
- **Broadcasting**: There's a built-in networking event that allows you to send messages to all players in the game, and it will automatically handle the queuing and displaying of those messages. This means you can easily send messages to all players without having to worry about the underlying mechanics of ``MissionUI`` just by prepending your mission messsage with ``%broadcast%``. The plugin will automatically detect this and send the message to all players in the game. Without it, the message will be sent to the local player only. For example: 
    ```csharp
    // This will send the message "This is a test message to all players!" to all players in the game for 4 seconds
    MissionUI.instance.MissionText("%broadcast%This is a test message to all players!", 
        Color.white,
        Color.white,
        4f
    );
    // This will send the message to the local player only for 4 seconds
    MissionUI.instance.MissionText("This is a test message to the local player!", 
        Color.white,
        Color.white,
        4f
    );
    ```
___
If you have any suggestions, feel free to reach out to me on [Discord](https://discord.gg/yip)!
<br>
<sub>© Pencil Fox Studios SP</sub>