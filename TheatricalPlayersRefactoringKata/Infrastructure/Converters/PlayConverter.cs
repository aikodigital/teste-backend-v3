using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using TheatricalPlayersRefactoringKata.Core.Entities;

namespace TheatricalPlayersRefactoringKata.Infrastructure.Converters
{
    public class PlayConverter : JsonConverter<Play>
    {
        public override Play Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            using (JsonDocument doc = JsonDocument.ParseValue(ref reader))
            {
                var root = doc.RootElement;

                var playId = root.TryGetProperty("playId", out var playIdElement) ? playIdElement.GetGuid() : (Guid?)null;
                var name = root.TryGetProperty("name", out var nameElement) ? nameElement.GetString() : throw new JsonException("Missing 'name' property.");
                var type = root.TryGetProperty("type", out var typeElement) ? ConvertToGenre(typeElement.GetString()) : throw new JsonException("Missing or invalid 'type' property.");
                var price = root.TryGetProperty("price", out var priceElement) ? priceElement.GetDecimal() : throw new JsonException("Missing 'price' property.");

                var play = new Play(name, type, price, playId);

                return play;
            }
        }

        public override void Write(Utf8JsonWriter writer, Play value, JsonSerializerOptions options)
        {
            writer.WriteStartObject();
            writer.WriteString("playId", value.PlayId.ToString());
            writer.WriteString("name", value.Name);
            writer.WriteString("type", value.Type.ToString());
            writer.WriteNumber("price", value.Price);
            writer.WriteEndObject();
        }

        private Genre ConvertToGenre(string genreString)
        {
            if (Enum.TryParse<Genre>(genreString, out var genre))
            {
                return genre;
            }
            throw new JsonException("Invalid genre value.");
        }
    }
}