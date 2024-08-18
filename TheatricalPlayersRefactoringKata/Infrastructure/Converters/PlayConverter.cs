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

                var playId = root.TryGetProperty("playId", out var playIdElement) ? playIdElement.GetGuid() : Guid.NewGuid();
                var name = root.GetProperty("name").GetString();
                var typeString = root.GetProperty("type").GetString();
                var price = root.GetProperty("price").GetDecimal();
                var audience = root.TryGetProperty("audience", out var audienceElement) ? audienceElement.GetInt32() : 0;

                if (!Enum.TryParse<Genre>(typeString, true, out var genre))
                {
                    throw new JsonException($"Genre '{typeString}' is invalid.");
                }

                return new Play(playId, name, genre, price, audience);
            }
        }

        public override void Write(Utf8JsonWriter writer, Play value, JsonSerializerOptions options)
        {
            writer.WriteStartObject();
            writer.WriteString("playId", value.PlayId.ToString());
            writer.WriteString("name", value.Name);
            writer.WriteString("type", value.Type.ToString());
            writer.WriteNumber("price", value.Price);
            writer.WriteNumber("audience", value.Audience);
            writer.WriteEndObject();
        }
    }
}