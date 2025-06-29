using System;

namespace Mock;

public class MockRobot
{
    private Tuple<int, int> _position = new Tuple<int, int>(0, 0);

    public MockRobot()
    {
        // Initialize the robot's position
        Console.WriteLine("MockRobot initialized at position (0, 0)");
    }

    public void Move(int x, int y)
    {
        // Update the robot's position
        _position = new Tuple<int, int>(_position.Item1 + x, _position.Item2 + y);
        Console.WriteLine($"MockRobot moved to position ({_position.Item1}, {_position.Item2})");
    }

    public Tuple<int, int> GetPosition()
    {
        // Return the current position of the robot
        Console.WriteLine($"MockRobot current position: ({_position.Item1}, {_position.Item2})");
        return _position;
    }

    public void ResetPosition()
    {
        // Reset the robot's position to the origin
        _position = new Tuple<int, int>(0, 0);
        Console.WriteLine("MockRobot position reset to (0, 0)");
    }

    public void ExecuteCommand(string command)
    {
        // Simulate executing a command
        Console.WriteLine($"MockRobot executing command: {command}");
        // Here you could add logic to parse and execute different commands
    }

}
