namespace SafeCity.Gateway.Authentication.Models;

public record User(string Username, string Password, string Role, string[] Scopes);
