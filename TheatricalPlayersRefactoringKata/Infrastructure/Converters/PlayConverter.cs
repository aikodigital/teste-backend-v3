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
            var jsonObject = JsonDocument.ParseValue(ref reader).RootElement;

            if (!jsonObject.TryGetProperty("playId", out var playIdProperty))
            {
                throw new JsonException("A propriedade 'playId' está ausente no JSON.");
            }
            var playId = Guid.Parse(playIdProperty.GetString());

            if (!jsonObject.TryGetProperty("name", out var nameProperty))
            {
                throw new JsonException("A propriedade 'name' está ausente no JSON.");
            }
            var name = nameProperty.GetString();

            if (!jsonObject.TryGetProperty("type", out var typeProperty))
            {
                throw new JsonException("A propriedade 'type' está ausente no JSON.");
            }
            var typeString = typeProperty.GetString();
            var genre = Enum.TryParse<Genre>(typeString, out var parsedGenre)
                ? parsedGenre
                : throw new JsonException($"Valor inválido para o gênero: {typeString}");

            if (!jsonObject.TryGetProperty("price", out var priceProperty))
            {
                throw new JsonException("A propriedade 'price' está ausente no JSON.");
            }
            var price = priceProperty.GetDecimal();

            if (!jsonObject.TryGetProperty("audience", out var audienceProperty))
            {
                throw new JsonException("A propriedade 'audience' está ausente no JSON.");
            }
            var audience = audienceProperty.GetInt32();

            return new Play(playId, name, genre, price, audience);
        }

        public override void Write(Utf8JsonWriter writer, Play value, JsonSerializerOptions options)
        {
            writer.WriteStartObject();
            writer.WriteString("playId", value.PlayId.ToString());
            writer.WriteString("name", value.Name);
            writer.WriteString("type", value.Genre.ToString());
            writer.WriteNumber("price", value.Price);
            writer.WriteNumber("audience", value.Audience);
            writer.WriteEndObject();
        }
    }
}