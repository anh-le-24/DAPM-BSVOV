using Microsoft.AspNetCore.SignalR;
using System.Collections.Concurrent;
using System.Threading.Tasks;
using System;

public class SignalingHub : Hub
{
    // In-memory storage of connection IDs for each user
    private static readonly ConcurrentDictionary<string, string> _connectionIds = new();

    // Gửi Offer tới người dùng
    public async Task SendOffer(string toUserId, string offer)
    {
        var connectionId = GetConnectionIdByUserId(toUserId);
        if (connectionId != null)
        {
            await Clients.Client(connectionId).SendAsync("ReceiveOffer", offer);
        }
        else
        {
            await Clients.Caller.SendAsync("Error", "User not found");
        }
    }

    // Gửi Answer tới người dùng
    public async Task SendAnswer(string toUserId, string answer)
    {
        var connectionId = GetConnectionIdByUserId(toUserId);
        if (connectionId != null)
        {
            await Clients.Client(connectionId).SendAsync("ReceiveAnswer", answer);
        }
        else
        {
            await Clients.Caller.SendAsync("Error", "User not found");
        }
    }

    // Gửi ICE Candidate tới người dùng
    public async Task SendIceCandidate(string toUserId, string iceCandidate)
    {
        var connectionId = GetConnectionIdByUserId(toUserId);
        if (connectionId != null)
        {
            await Clients.Client(connectionId).SendAsync("ReceiveIceCandidate", iceCandidate);
        }
        else
        {
            await Clients.Caller.SendAsync("Error", "User not found");
        }
    }

    // Lưu trữ connectionId của người dùng khi họ kết nối
    public override async Task OnConnectedAsync()
    {
        // Assuming user identifier is stored in session or token
        var userId = Context.UserIdentifier ?? Context.ConnectionId;  // Fallback to connection ID if user identifier is not available

        if (!string.IsNullOrEmpty(userId))
        {
            _connectionIds[userId] = Context.ConnectionId; // Store the connection ID in the dictionary
        }

        // You can log the user connection for debugging
        Console.WriteLine($"User {userId} connected with ConnectionId {Context.ConnectionId}");

        await base.OnConnectedAsync();
    }

    // Xóa connectionId khi người dùng ngắt kết nối
    public override async Task OnDisconnectedAsync(Exception exception)
    {
        var userId = Context.UserIdentifier ?? Context.ConnectionId; // Use connection ID if user identifier is not available

        _connectionIds.TryRemove(userId, out _); // Remove the connection ID from the dictionary

        // You can log the disconnection for debugging
        Console.WriteLine($"User {userId} disconnected.");

        await base.OnDisconnectedAsync(exception);
    }

    // Lấy connectionId dựa trên userId
    private string GetConnectionIdByUserId(string userId)
    {
        _connectionIds.TryGetValue(userId, out var connectionId);
        return connectionId; // Return the connectionId or null if user not found
    }
}
