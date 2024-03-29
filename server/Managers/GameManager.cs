using Microsoft.AspNetCore.SignalR;
using trex.Models.Dto.Game;
using Trex.Models;

namespace Trex.Managers;
public class GameManager
{
    public GameManager(IHubCallerClients clients, Lobby lobby)
    {
        _clients = clients.Group(lobby.Id.ToString());
        Map = new Map(lobby.Players);
    }
    public double CurrentScore;
    public double Speed = 5;
    private readonly IClientProxy _clients;
    public Map Map;
    public bool IsRunning => Map.Players.Any(p => p.IsAlive);

    public async void Start()
    {
        await _clients.SendAsync("gameStarted");
        while (IsRunning)
        {
            await Task.Delay(20);
            Task _ = Task.Run(Tick);
        }

         await _clients.SendAsync("gameEnded");   
    }


    public async Task Tick()
    {
        MoveObstacles();
        AddObstacle();
        AddScore();
        
        await _clients.SendAsync("tick", new Tick
        {
            PlayerPositions = Map.Players.ToDictionary(p => p.Id, p => new TickPlayerPosition { X = p.Position.X, Y = p.Position.Y, Score = p.Score ?? CurrentScore }),
            ObstaclePositions = Map.Obstacles.Select(o => new TickObstaclePosition { Type = o.Type.Name, X = o.Position.X, Y = o.Position.Y }).ToArray(),
            Speed = Speed,
        });
    }

    public void CircleCollision(Player player)
    {
        foreach (var obstacle in Map.Obstacles)
        {
            if (Math.Pow((obstacle.Position.X- player.Position.X),2)+ Math.Pow((obstacle.Position.Y- player.Position.Y),2) < Math.Pow((obstacle.Type.CollisionRadius+ player.Trex.collisionRadius),2))
            {
                player.Score = CurrentScore;
                break;
            }
        }
    }



    public void MoveObstacles()
    {
        const int maxSpeed = 13;

        foreach (Obstacle obstacle in Map.Obstacles)
        {
            obstacle.Position.X -= Speed;
            if (Speed < maxSpeed) Speed += 0.001;
        }
    }
    public void AddObstacle()
    {
        if (Map.Obstacles.Last().Position.X < 600)
        {
            double x = Map.Obstacles.Last().Position.X + Random.Shared.Next(150, 600);
            Obstacle.ObstacleType obstacleType = Obstacle.ObstacleType.GetRandom();
            double y = obstacleType.Height / 2;
            Map.Obstacles.Add(new Obstacle(x, y, Obstacle.ObstacleType.GetRandom()));
        }
    }
    public void AddScore()
    {
        CurrentScore += Speed;
    }
}