# MockRobotMCP

**Author:** Simon Coelho  
**Date:** June 28, 2025

MockRobotMCP is a .NET project that simulates a robot and exposes its controls via the Model Context Protocol (MCP) server. It provides tools to move the robot, reset its position, execute commands, and query its current position.

## Features
- Move the robot by specified x/y amounts
- Reset the robot to the origin (0, 0)
- Execute arbitrary commands on the robot
- Query the robot's current position

## Requirements
- .NET 8.0 SDK or later

## Getting Started

1. **Clone the repository:**
   ```sh
   git clone <repo-url>
   cd MockRobotMCP
   ```
2. **Build the project:**
   ```sh
   dotnet build
   ```
3. **Run the server:**
   ```sh
   dotnet run --project MockRobotMCP/MockRobotMCP.csproj
   ```

## Usage
The server exposes MCP tools for interacting with the robot. You can move the robot, reset its position, execute commands, and get its current position using the provided tool methods.

## Project Structure
- `MockRobot.cs`: Core logic for the mock robot
- `Tools.cs`: MCP tool definitions for robot control
- `Program.cs`: Application entry point and server setup

## VS Code MCP Configuration

To use this project with the Model Context Protocol in VS Code, you must update the `.vscode/mcp.json` file. This file configures how VS Code launches the MCP server. Make sure the `command` and `args` fields correctly point to your project, for example:

```json
{
    "inputs": [],
    "servers": {
        "MyFirstMCP": {
            "type": "stdio",
            "command": "dotnet",
            "args": [
                "run",
                "--project",
                "/Users/simoncoelho/Repos/MockRobotMCP/MockRobotMCP/MockRobotMCP.csproj"
            ]
        }
    }
}
```

Adjust the path if your project is located elsewhere. This ensures VS Code can start and communicate with your MCP server.
