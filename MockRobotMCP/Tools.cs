using ModelContextProtocol.Server;
using System.ComponentModel;

namespace Mock;

[McpServerToolType]
public class MockRobotTools
{

    [McpServerTool, Description("Moves the robot by the specified amount in each direction.")]
    public void MoveRobot(MockRobot controller, Tuple<int, int> position)
    {
        controller.Move(position.Item1, position.Item2);
    }

    [McpServerTool, Description("Resets the robot's position to the origin (0, 0).")]
    public void ResetPosition(MockRobot controller)
    {
        controller.ResetPosition();
    }

    [McpServerTool, Description("Executes a command on the robot.")]
    public void ExecuteCommand(MockRobot controller, string command)
    {
        controller.ExecuteCommand(command);
    }

    [McpServerTool, Description("Gets the current position of the robot.")]
    public Tuple<int, int> GetPosition(MockRobot controller)
    {
        return controller.GetPosition();
    }
}
