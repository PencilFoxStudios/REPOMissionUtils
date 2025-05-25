# MissionUtils
A helper plugin that patches ``MissionUI`` for it to be easier for modders to add their own mission ("FOCUS >") messages. It currently offers a queue system for displaying messages to players, and aims to not interfere with the default ``MissionUI`` system.

To use it, simply call ``MissionUI.instance.MissionText()`` with the message you want to display as you normally would. The plugin will handle the rest, including queuing them, displaying them in the correct order, with a configurable delay between messages.

## Features
There's a built-in networking event that allows you to send messages to all players in the game, and it will automatically handle the queuing and displaying of those messages. This means you can easily send messages to all players without having to worry about the underlying mechanics of ``MissionUI`` just by prepending your mission messsage with ``%broadcast%``. The plugin will automatically detect this and send the message to all players in the game. Without it, the message will be sent to the local player only.

For example:
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