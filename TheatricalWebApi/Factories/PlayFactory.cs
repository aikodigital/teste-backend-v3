using System;
using System.Reflection;
using TheatricalPlayersRefactoringKata.Entities;
using TheatricalWebApi.Models;

namespace TheatricalWebApi.Factories;
public static class PlayFactory
{
    public static Play CreatePlay(PlayDTO playDTO)
    {
        if (playDTO == null || string.IsNullOrEmpty(playDTO.Type))
            throw new ArgumentException("PlayDTO or Tipo cannot be null or empty");

        var assembly = Assembly.Load("TheatricalPlayersRefactoringKata"); // Nome do assembly
        var typeName = $"TheatricalPlayersRefactoringKata.Entities.{playDTO.Type}Play";
        var playType = assembly.GetType(typeName);

        if (playType == null)
            throw new InvalidOperationException($"Play type {typeName} is not recognized");

        var playInstance = Activator.CreateInstance(playType, playDTO.Name, playDTO.Lines) as Play;

        if (playInstance == null)
            throw new InvalidOperationException($"Unable to create an instance of {typeName}");

        return playInstance;
    }
}
