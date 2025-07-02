using System;
using MockRobotMCP;

namespace LabAutomationMCP;

public partial class MockRobot
{
    private Tuple<int, int> _position = new Tuple<int, int>(0, 0);

    public MockRobot()
    {
        Logger.Log("MockRobot initialized");
    }

    public void Move(int x, int y)
    {
        var oldPosition = _position;
        _position = new Tuple<int, int>(_position.Item1 + x, _position.Item2 + y);
        
        var message = $"Robot moved from ({oldPosition.Item1}, {oldPosition.Item2}) to ({_position.Item1}, {_position.Item2})";
        Logger.Log(message);
    }

    public Tuple<int, int> GetPosition()
    {
        var message = $"Robot position: ({_position.Item1}, {_position.Item2})";
        Logger.Log(message);
        return _position;
    }

    public void ResetPosition()
    {
        var oldPosition = _position;
        _position = new Tuple<int, int>(0, 0);
        
        var message = $"Robot position reset from ({oldPosition.Item1}, {oldPosition.Item2}) to (0, 0)";
        Logger.Log(message);
    }

    public void ExecuteCommand(string command)
    {
        var message = $"Executing command: '{command}'";
        Logger.Log(message);
    }
}
